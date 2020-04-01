import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatSnackBar } from '@angular/material';
import { Observable, Subscription } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { DataService } from '../../core/data.service';
import { IconsService } from '../../core/icons.service';
import { AlertDialogComponent } from '../../shared/components/alert-dialog/alert-dialog.component';
import { MedicineValidator } from '../../shared/medicine.validator';
import { ICompany } from '../../shared/models/company.model';
import { IMedicine } from '../../shared/models/medicine.model';
import { IPharmacy } from '../../shared/models/pharmacy.model';
import { UsageTypeNames } from '../../shared/models/usage-type.enum';

@Component({
  selector: 'app-new',
  templateUrl: './new.component.html',
  styleUrls: ['./new.component.scss']
})
export class NewComponent implements OnInit {

  subscriptions: Subscription[] = [];
  newForm: FormGroup;
  unit = 'عدد';
  usageTypeNames = UsageTypeNames;

  companies: ICompany[] = [];

  pharmacies: Array<IPharmacy> = [];
  filteredPharmacies: Observable<IPharmacy[]>;

  medicines: Array<IMedicine> = [];
  filteredMedicines: Observable<IMedicine[]>;

  constructor(public icons: IconsService, private fb: FormBuilder, private dataService: DataService, private dialog: MatDialog, private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.newForm = this.fb.group({
      medicine: [null, [Validators.required, MedicineValidator]],
      companyId: [null, Validators.required],
      pharmacy: [null, Validators.required],
      amount: [null, [Validators.required, Validators.pattern('([0-9]*[.])?[0-9]+')]],
      date: [null, Validators.required]
    });

    let sub = this.dataService.getPharmacies().subscribe(data => {
      this.pharmacies = data;
    });
    this.subscriptions.push(sub);

    sub = this.dataService.getMedicines().subscribe(data => {
      this.medicines = data;
    });
    this.subscriptions.push(sub);

    sub = this.dataService.getCompanies().subscribe(data => {
      this.companies = data;
    });
    this.subscriptions.push(sub);

    this.filteredPharmacies = this.newForm.get('pharmacy').valueChanges
      .pipe(
        startWith(''),
        map(value => {
          return this.pharmacies.filter(a => a.name.includes(value))
        })
      );

    this.filteredMedicines = this.newForm.get('medicine').valueChanges
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

  getPharmacyName(pharmacy?: IPharmacy): string | undefined {
    return pharmacy ? pharmacy.name : undefined;
  }

  getMedicineTitle(medicine?: IMedicine): string | undefined {
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

  medicineSelect(e) {
    if (e.option.value && e.option.value.unit)
      this.unit = e.option.value.unit;
  }

  newDistribution() {
    if (!this.newForm.valid)
      return;
    let formValue = this.newForm.value;
    let model = {
      medicineId: formValue.medicine ? formValue.medicine.id : null,
      companyId: formValue.companyId ? formValue.companyId : null,
      pharmacyId: formValue.pharmacy ? formValue.pharmacy.id : null,
      amount: formValue.amount ? +formValue.amount : null,
      dateTime: formValue.date
    };
    let sub = this.dataService.newDistribution(model).subscribe(a => {
      this.newForm.reset();
      this.snackBar.open('رکورد جدید با موفقیت ثبت شد', 'بستن', {
        duration: 2000,
      });
    }, error => {
      this.dialog.open(AlertDialogComponent, {
        width: '300px',
        maxWidth: '600px',
        data: {
          icon: 'errorIcon',
          message: JSON.stringify(error.errors),
          title: 'خطا به هنگام ثبت اطلاعات',
          iconColor: '#c00'
        }
      });
    });
    this.subscriptions.push(sub);
  }

}
