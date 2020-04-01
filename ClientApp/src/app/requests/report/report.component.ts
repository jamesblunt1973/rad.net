import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material';
import { Observable, Subscription } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { DataService } from '../../core/data.service';
import { IconsService } from '../../core/icons.service';
import { AutoUnsubscribe } from '../../shared/auto-unsubscribe';
import { IPatient } from '../../shared/models/patient.model';
import { IPharmacy } from '../../shared/models/pharmacy.model';
import { IRequest } from '../../shared/models/request.model';

@AutoUnsubscribe
@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss']
})
export class ReportComponent implements OnInit {

  list: Array<IRequest> = [];

  filterForm = new FormGroup({
    pharmacy: new FormControl(),
    patient: new FormControl(),
    dateFrom: new FormControl(),
    dateTo: new FormControl(),
    trackNo: new FormControl(),
    requestType: new FormControl()
  });

  pharmacies: Array<IPharmacy> = [];
  filteredPharmacies: Observable<IPharmacy[]>;

  patients: Array<IPatient> = [];
  filteredPatients: Observable<IPatient[]>;

  subscriptions: Subscription[] = [];
  counts: number[] = [10];
  countPerPage: number = this.counts[0];
  currentPage: number = 1;
  startIndex: number = 0;
  endIndex: number = this.countPerPage;

  constructor(public icons: IconsService, private dataService: DataService, private dialog: MatDialog) { }

  ngOnInit() {
    let sub = this.dataService.getPharmacies().subscribe(data => {
      this.pharmacies = data;
    });
    this.subscriptions.push(sub);

    sub = this.dataService.getPatients().subscribe(data => {
      this.patients = data;
    });
    this.subscriptions.push(sub);

    this.filteredPharmacies = this.filterForm.get('pharmacy').valueChanges
      .pipe(
        startWith(''),
        map(value => {
          return this.pharmacies.filter(a => a.name.includes(value))
        })
      );

    this.filteredPatients = this.filterForm.get('patient').valueChanges
      .pipe(
        startWith(''),
        map(value => {
          return this.patients.filter(a => {
            return a.name.includes(value) ||
              a.nationalCode.startsWith(value)
          });
        })
      );
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

  getPharmacyName(pharmacy?: IPharmacy): string | undefined {
    return pharmacy ? pharmacy.name : undefined;
  }

  getPatientName(patient?: IPatient): string | undefined {
    return patient ? (patient.nationalCode + ' - ' + patient.name) : undefined;
  }

  getReport() {
    let formValue = this.filterForm.value;
    var model = {
      pharmacyId: formValue.pharmacy ? formValue.pharmacy.id : null,
      patientId: formValue.patient ? formValue.patient.id : null,
      trackNo: formValue.trackNo ? formValue.trackNo : null,
      isCanceled: formValue.requestType ? (formValue.requestType == 2) : null,
      dateFrom: formValue.dateFrom,
      dateTo: formValue.dateTo
    };

    this.dataService.getRequests(model).subscribe(data => {
      this.list = data;
    });
  }
}
