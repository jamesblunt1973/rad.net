import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatSnackBar } from '@angular/material';
import { Subscription } from 'rxjs';
import { DataService } from '../../core/data.service';
import { IconsService } from '../../core/icons.service';
import { AlertDialogComponent } from '../../shared/components/alert-dialog/alert-dialog.component';
import { IPhysician } from '../../shared/models/physician.model';
import { ISpeciality } from '../../shared/models/speciality.model';

@Component({
  selector: 'app-new',
  templateUrl: './new.component.html',
  styleUrls: ['./new.component.scss']
})
export class NewComponent implements OnInit {

  specialities: Array<ISpeciality> = [];
  subscriptions: Subscription[] = [];
  newForm: FormGroup;

  constructor(public icons: IconsService, private dataService: DataService, private fb: FormBuilder, private dialog: MatDialog, private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.newForm = this.fb.group({
      name: null,
      code: [null, Validators.required],
      specialityId: null
    });

    let sub = this.dataService.getSpecialities().subscribe(data => {
      this.specialities = data;
    });
    this.subscriptions.push(sub);

  }

  newPhysician() {
    if (this.newForm.valid) {

      let formValue = this.newForm.value;

      const physician: IPhysician = {
        code: formValue.code,
        name: formValue.name || '',
        specialityId: formValue.specialityId
      };

      this.dataService.newPhysician(physician).subscribe(res => {
        this.newForm.reset();
        this.snackBar.open('پزشک جدید با موفقیت ثبت شد', 'بستن', {
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
            title: 'خطا به هنگام ثبت پزشک جدید',
            iconColor: '#c00'
          }
        });
      });
    }
  }

}
