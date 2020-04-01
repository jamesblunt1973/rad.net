import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { DataService } from '../../core/data.service';
import { IconsService } from '../../core/icons.service';
import { ISetting } from '../../shared/models/setting';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  constructor(public icons: IconsService, private dataService: DataService, private snackBar: MatSnackBar) { }

  changePassModel = {
    newpass: '',
    oldpass: ''
  };

  smsKey = '';

  ngOnInit() {
  }

  changePass() {
    this.dataService.changePass(this.changePassModel).subscribe(() => {
      this.snackBar.open('رمز عبور با موفقیت تغییر کرد', null, {
        duration: 2000,
        direction: 'rtl',
        horizontalPosition: 'right',
        verticalPosition: 'top',
        panelClass: 'customSnackBar'
      });
      this.changePassModel = {
        newpass: '',
        oldpass: ''
      };
    }, error => {
      console.log(error);
      this.snackBar.open('خطا به هنگام تغییر رمز عبور', null, {
        duration: 2000,
        direction: 'rtl',
        horizontalPosition: 'right',
        verticalPosition: 'top',
        panelClass: 'customErrorSnackBar'
      });
    });
  }

  updateSmsKey() {
    let model: ISetting = {
      key: 'sms-key',
      value: this.smsKey
    };
    this.dataService.updateSettings(model).subscribe(() => {
      this.snackBar.open('کلید ارسال پیامک با موفقت به روز رسانی شد', null, {
        duration: 2000,
        direction: 'rtl',
        horizontalPosition: 'right',
        verticalPosition: 'top',
        panelClass: 'customSnackBar'
      });
      this.smsKey = '';
    }, error => {
      console.log(error);
      this.snackBar.open('خطا به هنگام به روز رسانی', null, {
        duration: 2000,
        direction: 'rtl',
        horizontalPosition: 'right',
        verticalPosition: 'top',
        panelClass: 'customErrorSnackBar'
      });
    });
  }

}
