import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { Subscription } from 'rxjs';

import { ConfirmDialogComponent } from '../../shared/components/confirm-dialog/confirm-dialog.component';
import { AlertDialogComponent } from '../../shared/components/alert-dialog/alert-dialog.component';

import { IconsService } from '../../core/icons.service';
import { DataService } from '../../core/data.service';
import { AutoUnsubscribe } from '../../shared/auto-unsubscribe';
import { IPhysician } from '../../shared/models/physician.model';
import { ISpeciality } from '../../shared/models/speciality.model';

@AutoUnsubscribe
@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {

  specialities: Array<ISpeciality> = [];
  originalSpecialities: Array<ISpeciality> = [];
  physicians: Array<IPhysician> = [];
  originalPhysicians: Array<IPhysician> = [];
  subscriptions: Subscription[] = [];
  counts: number[] = [10];
  countPerPage: number = this.counts[0];
  currentPage: number = 1;
  startIndex: number = 0;
  endIndex: number = this.countPerPage;
  filterModel = {
    str: null,
    code: null,
    specialityId: null
  };

  constructor(public icons: IconsService, private dataService: DataService, private dialog: MatDialog) { }

  ngOnInit() {
    let sub = this.dataService.getSpecialities().subscribe(data => {
      this.originalSpecialities = data;
      for (let speciality of this.originalSpecialities) {
        this.specialities[speciality.id] = speciality;
      }
    });
    this.subscriptions.push(sub);

    sub = this.dataService.getPhysicians().subscribe(data => {
      this.physicians = data;
      this.originalPhysicians = data;
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
    this.physicians = this.originalPhysicians.filter((a: IPhysician) => {
      return (this.filterModel.str === null || (
        a.name.search(new RegExp(this.filterModel.str, 'i')) !== -1
      )) &&
        (this.filterModel.code === null || a.code.startsWith(this.filterModel.code)) &&
        (this.filterModel.specialityId === null || a.specialityId == this.filterModel.specialityId)
    });
    this.changePage(1);
  }

  deleteItem(item: IPhysician) {
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
        var sub = this.dataService.deletePhysician(item.id).subscribe(res => {
          var index = this.originalPhysicians.findIndex(a => {
            return a.id == item.id;
          });
          this.originalPhysicians.splice(index, 1);
          this.physicians = this.originalPhysicians.slice(0);
        }, error => {
          this.dialog.open(AlertDialogComponent, {
            width: '300px',
            maxWidth: '600px',
            data: {
              icon: 'errorIcon',
              message: JSON.stringify(error.errors),
              title: 'خطا به هنگام حذف پزشک',
              iconColor: '#c00'
            }
          });
        });
        this.subscriptions.push(sub);
      }
    });
  }

  updateItem(item: IPhysician) {
    var sub = this.dataService.updatePhysician(item).subscribe(res => {
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
