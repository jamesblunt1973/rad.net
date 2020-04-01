import { Component, OnInit } from '@angular/core';
import { IDistributionReportItem } from '../../shared/models/distribution-report-item.model';
import { FormGroup, FormControl } from '@angular/forms';
import { IPharmacy } from '../../shared/models/pharmacy.model';
import { Observable, Subscription } from 'rxjs';
import { IMedicine } from '../../shared/models/medicine.model';
import { UsageTypeNames } from '../../shared/models/usage-type.enum';
import { IconsService } from '../../core/icons.service';
import { DataService } from '../../core/data.service';
import { MatDialog } from '@angular/material';
import { startWith, map } from 'rxjs/operators';
import { DistributionDetailsComponent } from '../distribution-details/distribution-details.component';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss']
})
export class ReportComponent implements OnInit {

  list: IDistributionReportItem[] = [];

  filterForm = new FormGroup({
    pharmacy: new FormControl(),
    medicine: new FormControl(),
    dateFrom: new FormControl(),
    dateTo: new FormControl()
  });

  pharmacies: Array<IPharmacy> = [];
  filteredPharmacies: Observable<IPharmacy[]>;

  medicines: Array<IMedicine> = [];
  filteredMedicines: Observable<IMedicine[]>;

  subscriptions: Subscription[] = [];
  counts: number[] = [10];
  countPerPage: number = this.counts[0];
  currentPage: number = 1;
  startIndex: number = 0;
  endIndex: number = this.countPerPage;

  usageTypeNames = UsageTypeNames;

  constructor(public icons: IconsService, private dataService: DataService, private dialog: MatDialog) { }

  ngOnInit() {
    let sub = this.dataService.getPharmacies().subscribe(data => {
      this.pharmacies = data;
    });
    this.subscriptions.push(sub);

    sub = this.dataService.getMedicines().subscribe(data => {
      this.medicines = data;
    });
    this.subscriptions.push(sub);

    this.filteredPharmacies = this.filterForm.get('pharmacy').valueChanges
      .pipe(
        startWith(''),
        map(value => {
          return this.pharmacies.filter(a => a.name.includes(value))
        })
      );

    this.filteredMedicines = this.filterForm.get('medicine').valueChanges
      .pipe(
        startWith(''),
        map(value => {
          return this.medicines.filter(a => {
            return a.brandEnName.includes(value) ||
              a.brandFaName.includes(value) ||
              a.enName.includes(value) ||
              a.faName.includes(value) ||
              a.genericCode.startsWith(value) ||
              a.ircCode.startsWith(value)
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

  getMedicineTitle(medicine?: IMedicine): string | undefined {
    //return medicine ? (medicine.faName + '-' + UsageTypeNames[medicine.usageType] + '-' + medicine.dosage) : undefined;
    if (!medicine)
      return undefined;
    let str = '';
    if (medicine.genericCode)
      str += ', جنریک: ' + medicine.genericCode;
    if (medicine.ircCode)
      str += ', IRC: ' + medicine.ircCode;
    if (medicine.faName)
      str += ', ' + medicine.faName;
    return str.substr(2);
}

  getReport() {
    let formValue = this.filterForm.value;
    var model = {
      pharmacyId: formValue.pharmacy ? formValue.pharmacy.id : null,
      medicineId: formValue.medicine ? formValue.medicine.id : null,
      dateFrom: formValue.dateFrom,
      dateTo: formValue.dateTo
    };

    let sub = this.dataService.getDistributions(model).subscribe(res => {
      this.list = res;
    });
    this.subscriptions.push(sub);
  }

  getDetails(item: IDistributionReportItem) {
    let formValue = this.filterForm.value;
    var model = {
      medicineId: item.medicineId,
      pharmacyId: item.pharmacyId,
      dateFrom: formValue.dateFrom,
      dateTo: formValue.dateTo
    };
    let sub = this.dataService.getDistributionDetailsReport(model).subscribe(res => {
      this.dialog.open(DistributionDetailsComponent, {
        width: '96%',
        maxWidth: '96%',
        data: res
      });

    });
    this.subscriptions.push(sub);
  }

}
