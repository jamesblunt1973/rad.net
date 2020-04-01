import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatSnackBar } from '@angular/material';
import { Subscription } from 'rxjs';
import { DataService } from '../../core/data.service';
import { IconsService } from '../../core/icons.service';
import { AlertDialogComponent } from '../../shared/components/alert-dialog/alert-dialog.component';
import { IPharmacy } from '../../shared/models/pharmacy.model';

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
      name: null,
      code: [null, Validators.required],
      tell: [null, [Validators.minLength(4), Validators.maxLength(11), Validators.pattern('[0-9]*')]],
      cell: [null, [Validators.minLength(11), Validators.maxLength(11), Validators.pattern('[0-9]*')]],
      address: null
    });
  }

  newPharmacy() {
    if (this.newForm.valid) {

      let formValue = this.newForm.value;

      const pharmacy: IPharmacy = {
        address: formValue.address || '',
        cell: formValue.cell || '',
        code: formValue.code,
        name: formValue.name || '',
        tell: formValue.tell || '',
        password: ''
      };

      this.dataService.newPharmacy(pharmacy).subscribe(res => {
        this.newForm.reset();
        this.snackBar.open('داروخانه جدید با موفقیت ثبت شد', 'بستن', {
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
            title: 'خطا به هنگام ثبت داروخانه جدید',
            iconColor: '#c00'
          }
        });
      });
    }
  }

}
