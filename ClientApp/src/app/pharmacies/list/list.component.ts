import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';

import { Subscription } from 'rxjs';

import { ConfirmDialogComponent } from '../../shared/components/confirm-dialog/confirm-dialog.component';
import { AlertDialogComponent } from '../../shared/components/alert-dialog/alert-dialog.component';

import { IPharmacy } from '../../shared/models/pharmacy.model';
import { IconsService } from '../../core/icons.service';
import { DataService } from '../../core/data.service';
import { AutoUnsubscribe } from '../../shared/auto-unsubscribe';

@AutoUnsubscribe
@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {

  pharmacies: Array<IPharmacy> = [];
  originalPharmacies: Array<IPharmacy> = [];
  subscriptions: Subscription[] = [];
  counts: number[] = [10];
  countPerPage: number = this.counts[0];
  currentPage: number = 1;
  startIndex: number = 0;
  endIndex: number = this.countPerPage;
  filterModel = {
    str: null,
    num: null,
    address: null
  };

  constructor(public icons: IconsService, private dataService: DataService, private dialog: MatDialog) { }

  ngOnInit() {
    let sub = this.dataService.getPharmacies().subscribe(data => {
      this.pharmacies = data;
      this.originalPharmacies = data;
    });
    this.subscriptions.push(sub);
  }

  changePage(e: number) {
    this.currentPage = e;
    this.calculateBoundaries();
  }

  changeCount(e: number) {
    this.countPerPage = e;
    this.currentPage = 1;
    this.calculateBoundaries();
  }

  calculateBoundaries() {
    this.startIndex = (this.currentPage - 1) * this.countPerPage;
    this.endIndex = this.currentPage * this.countPerPage;
  }

  applyFilter() {
    this.pharmacies = this.originalPharmacies.filter((a: IPharmacy) => {
      return (this.filterModel.str === null || (
        a.name.search(new RegExp(this.filterModel.str, 'i')) !== -1 ||
        a.code.search(new RegExp(this.filterModel.str, 'i')) !== -1
      )) &&
        (this.filterModel.num === null ||
          a.cell.startsWith(this.filterModel.num) ||
          a.tell.startsWith(this.filterModel.num)
        ) &&
        (this.filterModel.address === null ||
          a.address.search(new RegExp(this.filterModel.address, 'i')) !== -1
        )
    });
    this.changePage(1);
  }

  deleteItem(item: IPharmacy) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '250px',
      data: {
        title: '',
        message: 'آیتم مورد نظر حذف شود؟',
        okText: 'بله',
        cancelText: 'خیر'
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        var sub = this.dataService.deletePharmacy(item.id).subscribe(res => {
          var index = this.originalPharmacies.findIndex(a => {
            return a.id == item.id;
          });
          this.originalPharmacies.splice(index, 1);
          this.pharmacies = this.originalPharmacies.slice(0);
        }, error => {
          this.dialog.open(AlertDialogComponent, {
            width: '300px',
            maxWidth: '600px',
            data: {
              icon: 'errorIcon',
              message: JSON.stringify(error.errors),
              title: 'خطا به هنگام حذف داروخانه',
              iconColor: '#c00'
            }
          });
        });
        this.subscriptions.push(sub);
      }
    });
  }

  updateItem(item: IPharmacy) {
    var sub = this.dataService.updatePharmacy(item).subscribe(res => {
      if (res) { // something happened!
        this.dialog.open(AlertDialogComponent, {
          width: '300px',
          maxWidth: '600px',
          data: {
            icon: 'errorIcon',
            message: res,
            title: 'خطا',
            iconColor: '#c00'
          }
        });
      }
      item.edit = false;
    }, error => {
      this.dialog.open(AlertDialogComponent, {
        width: '300px',
        maxWidth: '600px',
        data: {
          icon: 'errorIcon',
          message: error.statusText,
          title: 'خطای ارتباط با سرور',
          iconColor: '#c00'
        }
      });
    });
    this.subscriptions.push(sub);
  }

  sendPassword(item: IPharmacy) {
    var sub = this.dataService.sendPassword(item).subscribe(res => {
      this.dialog.open(AlertDialogComponent, {
        width: '400px',
        maxWidth: '600px',
        data: {
          icon: 'smsIcon',
          message: 'متاسفانه به هنگام ارسال رمز خطایی اتفاق افتاده است. رمز تولید شده ' + res.password + ' می‌باشد.',
          title: 'خطا در ارسال پیامک',
          iconColor: '#c00'
        }
      });
    }, error => {
        this.dialog.open(AlertDialogComponent, {
          width: '300px',
          maxWidth: '600px',
          data: {
            icon: 'errorIcon',
            message: error.statusText,
            title: 'خطای ارتباط با سرور',
            iconColor: '#c00'
          }
        });
    });
    this.subscriptions.push(sub);
  }
}
