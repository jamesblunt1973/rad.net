import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Observable, Subscription } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { DataService } from '../../core/data.service';
import { IconsService } from '../../core/icons.service';
import { AlertDialogComponent } from '../../shared/components/alert-dialog/alert-dialog.component';
import { ICompany } from '../../shared/models/company.model';
import { IMedicine } from '../../shared/models/medicine.model';
import { UsageTypeNames } from '../../shared/models/usage-type.enum';

@Component({
  selector: 'app-new',
  templateUrl: './new.component.html',
  styleUrls: ['./new.component.scss']
})
export class NewComponent implements OnInit, OnDestroy {

  companies: ICompany[] = [];
  filteredProducers: Observable<ICompany[]>;
  filteredDistributors: Observable<ICompany[]>;

  subscriptions: Subscription[] = [];
  usageTypeNames = UsageTypeNames;
  newForm: FormGroup;

  constructor(public icons: IconsService, private dataService: DataService, private fb: FormBuilder, private dialog: MatDialog, private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.newForm = this.fb.group({
      brandEnName: null,
      brandFaName: null,
      distributor: null,
      dosage: null,
      enName: null,
      faName: null,
      genericCode: [null, [Validators.maxLength(5), Validators.pattern('[0-9]*')]],
      ircCode: null,
      maximumUsageAmount: [null, Validators.required],
      producer: null,
      unit: 'عدد',
      usagePeriod: [null, Validators.required],
      usageType: null
    });

    let sub = this.dataService.getCompanies().subscribe(data => {
      this.companies = data;
    });
    this.subscriptions.push(sub);

    this.filteredProducers = this.newForm.get('producer').valueChanges
      .pipe(
        startWith(''),
        map(value => {
          return this.companies.filter(a => {
            var rx = new RegExp(value, 'i');
            return a.name.match(rx) !== null;
          });
        })
      );

    this.filteredDistributors = this.newForm.get('distributor').valueChanges
      .pipe(
        startWith(''),
        map(value => {
          return this.companies.filter(a => {
            var rx = new RegExp(value, 'i');
            return a.name.match(rx) !== null;
          });
        })
      );
  }

  ngOnDestroy(): void {
    for (let sub of this.subscriptions) {
      sub.unsubscribe();
    }
  }

  newMedicine() {
    if (this.newForm.valid) {

      let formValue = this.newForm.value;

      const medicine: IMedicine = {
        brandEnName: formValue.brandEnName || '',
        brandFaName: formValue.brandFaName || '',
        distributorId: formValue.distributor && formValue.distributor.id ? formValue.distributor.id : null,
        dosage: formValue.dosage || '',
        enName: formValue.enName || '',
        faName: formValue.faName || '',
        genericCode: formValue.genericCode || '',
        ircCode: formValue.ircCode || '',
        maximumUsageAmount: +formValue.maximumUsageAmount,
        producerId: formValue.producer && formValue.producer.id ? formValue.producer.id : null,
        unit: formValue.unit || '',
        usagePeriod: +formValue.usagePeriod,
        usageType: formValue.usageType
      };

      if (medicine.ircCode === '' && medicine.genericCode === '') {
        this.dialog.open(AlertDialogComponent, {
          width: '300px',
          maxWidth: '600px',
          data: {
            icon: 'errorIcon',
            message: 'کد IRC یا جنریک الزامی است',
            title: 'خطا به هنگام ثبت داروی جدید',
            iconColor: '#c00'
          }
        });
        return;
      }

      this.dataService.newMedicine(medicine).subscribe(res => {
        this.newForm.reset();
        this.snackBar.open('داروی جدید با موفقیت ثبت شد', 'بستن', {
          duration: 2000,
        });
      }, error => {
        console.log(error);
        this.dialog.open(AlertDialogComponent, {
          width: '300px',
          maxWidth: '600px',
          data: {
            icon: 'errorIcon',
            message: 'شرح کامل خطا در کنسول ثبت شده است.',
            title: 'خطا به هنگام ثبت داروی جدید',
            iconColor: '#c00'
          }
        });
      });
    }
  }

  getCompanyTitle(company?: ICompany): string | undefined {
    return company != null ? company.name : undefined;
  }

}
