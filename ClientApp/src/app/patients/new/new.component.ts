import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatSnackBar } from '@angular/material';
import { Subscription } from 'rxjs';
import { DataService } from '../../core/data.service';
import { IconsService } from '../../core/icons.service';
import { AlertDialogComponent } from '../../shared/components/alert-dialog/alert-dialog.component';
import { IPatient } from '../../shared/models/patient.model';

@Component({
  selector: 'app-new',
  templateUrl: './new.component.html',
  styleUrls: ['./new.component.scss']
})
export class NewComponent implements OnInit {

  subscriptions: Subscription[] = [];
  newForm: FormGroup;

  constructor(public icons: IconsService, private dataService: DataService, private fb: FormBuilder, private dialog: MatDialog, private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.newForm = this.fb.group({
      name: [null],
      nationalCode: [null, [Validators.required, Validators.pattern('^[0-9]{10}$')]],
      birthDate: null,
      cell: [null, [Validators.minLength(11), Validators.maxLength(11), Validators.pattern('[0-9]*')]],
      tell: [null, [Validators.minLength(4), Validators.maxLength(11), Validators.pattern('[0-9]*')]],
      gender: null
    });
  }

  newPatient() {
    if (this.newForm.valid) {

      let formValue = this.newForm.value;

      const patient: IPatient = {
        birthDate: formValue.birthDate,
        cell: formValue.cell || '',
        gender: formValue.gender,
        name: formValue.name || '',
        nationalCode: formValue.nationalCode,
        tell: formValue.tell || ''
      };

      this.dataService.newPatient(patient).subscribe(res => {
        this.newForm.reset();
        this.snackBar.open('بیمار جدید با موفقیت ثبت شد', 'بستن', {
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
            title: 'خطا به هنگام ثبت بیمار جدید',
            iconColor: '#c00'
          }
        });
      });
    }
  }

}
