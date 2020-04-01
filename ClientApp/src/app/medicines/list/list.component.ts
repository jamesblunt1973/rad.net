import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { Subscription } from 'rxjs';

import { ConfirmDialogComponent } from '../../shared/components/confirm-dialog/confirm-dialog.component';
import { AlertDialogComponent } from '../../shared/components/alert-dialog/alert-dialog.component';

import { IMedicine } from '../../shared/models/medicine.model';
import { IconsService } from '../../core/icons.service';
import { DataService } from '../../core/data.service';
import { ICompany } from '../../shared/models/company.model';
import { UsageTypeNames } from '../../shared/models/usage-type.enum';
import { AutoUnsubscribe } from '../../shared/auto-unsubscribe';

@AutoUnsubscribe
@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {

  companies: Array<ICompany> = [];
  originalCompanies: Array<ICompany> = [];
  medicines: Array<IMedicine> = [];
  originalMedicines: Array<IMedicine> = [];
  subscriptions: Subscription[] = [];
  counts: number[] = [10];
  countPerPage: number = this.counts[0];
  currentPage: number = 1;
  startIndex: number = 0;
  endIndex: number = this.countPerPage;
  usageTypeNames = UsageTypeNames;
  filterModel = {
    str: null,
    company: null
  };

  constructor(public icons: IconsService, private dataService: DataService, private dialog: MatDialog) { }

  ngOnInit() {
    let sub = this.dataService.getCompanies().subscribe(data => {
      this.originalCompanies = data;
      for (let company of this.originalCompanies) {
        this.companies[company.id] = company;
      }
    });
    this.subscriptions.push(sub);
    sub = this.dataService.getMedicines().subscribe(data => {
      this.medicines = data;
      this.originalMedicines = data;
    });
    this.subscriptions.push(sub);
  }


  checkLTR(ch: number) {
    if ((ch >= 65 && ch <= 90) || (ch >= 97 && ch <= 122))
      return 'latin-text';
    return '';
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
    this.medicines = this.originalMedicines.filter((a: IMedicine) => {
      return (this.filterModel.str === null || (
        a.enName.search(new RegExp(this.filterModel.str, 'i')) !== -1 ||
        a.faName.search(new RegExp(this.filterModel.str, 'i')) !== -1 ||
        a.brandEnName.search(new RegExp(this.filterModel.str, 'i')) !== -1 ||
        a.brandFaName.search(new RegExp(this.filterModel.str, 'i')) !== -1 ||
        a.genericCode.search(new RegExp(this.filterModel.str, 'i')) !== -1 ||
        a.ircCode.search(new RegExp(this.filterModel.str, 'i')) !== -1)) &&
        (this.filterModel.company === null || (
          (a.producerId && a.producerId == this.filterModel.company) ||
          (a.distributorId && a.distributorId == this.filterModel.company)))
    });
    this.changePage(1);
  }

  deleteItem(item: IMedicine) {
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
        var sub = this.dataService.deleteMedicine(item.id).subscribe(res => {
          var index = this.originalMedicines.findIndex(a => {
            return a.id == item.id;
          });
          this.originalMedicines.splice(index, 1);
          this.medicines = this.originalMedicines.slice(0);
        }, error => {
            this.dialog.open(AlertDialogComponent, {
              width: '300px',
              maxWidth: '600px',
              data: {
                icon: 'errorIcon',
                message: JSON.stringify(error.errors),
                title: 'خطا به هنگام حذف دارو',
                iconColor: '#c00'
              }
            });
        });
        this.subscriptions.push(sub);
      }
    });
  }

  updateItem(item: IMedicine) {
    var sub = this.dataService.updateMedicine(item).subscribe(res => {
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
}
