import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { Subscription } from 'rxjs';

import { ConfirmDialogComponent } from '../../shared/components/confirm-dialog/confirm-dialog.component';
import { AlertDialogComponent } from '../../shared/components/alert-dialog/alert-dialog.component';

import { IPatient } from '../../shared/models/patient.model';
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

  patients: Array<IPatient> = [];
  originalPatients: Array<IPatient> = [];
  subscriptions: Subscription[] = [];
  counts: number[] = [10];
  countPerPage: number = this.counts[0];
  currentPage: number = 1;
  startIndex: number = 0;
  endIndex: number = this.countPerPage;
  filterModel = {
    str: null,
    nationalCode: null,
    cell: null
  };

  constructor(public icons: IconsService, private dataService: DataService, private dialog: MatDialog) { }

  ngOnInit() {
    let sub = this.dataService.getPatients().subscribe(data => {
      this.patients = data;
      this.originalPatients = data;
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
    this.patients = this.originalPatients.filter((a: IPatient) => {
      return (this.filterModel.str === null || (
        a.name.search(new RegExp(this.filterModel.str, 'i')) !== -1
      )) &&
        (this.filterModel.nationalCode === null || a.nationalCode.startsWith(this.filterModel.nationalCode)) &&
        (this.filterModel.cell === null || a.cell.startsWith(this.filterModel.cell))
    });
    this.changePage(1);
  }

  deleteItem(item: IPatient) {
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
        var sub = this.dataService.deletePatient(item.id).subscribe(res => {
          var index = this.originalPatients.findIndex(a => {
            return a.id == item.id;
          });
          this.originalPatients.splice(index, 1);
          this.patients = this.originalPatients.slice(0);
        }, error => {
          this.dialog.open(AlertDialogComponent, {
            width: '300px',
            maxWidth: '600px',
            data: {
              icon: 'errorIcon',
              message: JSON.stringify(error.errors),
              title: 'خطا به هنگام حذف بیمار',
              iconColor: '#c00'
            }
          });
        });
        this.subscriptions.push(sub);
      }
    });
  }

  updateItem(item: IPatient) {
    var sub = this.dataService.updatePatient(item).subscribe(res => {
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
      console.log(error);
      this.dialog.open(AlertDialogComponent, {
        width: '300px',
        maxWidth: '600px',
        data: {
          icon: 'errorIcon',
          message: 'شرح کامل خطا در کنسول ثبت شده است.',
          title: 'خطای ارتباط با سرور',
          iconColor: '#c00'
        }
      });
    });
    this.subscriptions.push(sub);
  }
}
