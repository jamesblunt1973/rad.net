import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IconsService } from '../../core/icons.service';
import { AuthService } from '../../core/auth.service';
import { ILogin } from '../../shared/models/login.model';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  model: ILogin = {
    password: '',
    username: ''
  };

  loading = false;

  constructor(public icons: IconsService, private authService: AuthService, private router: Router, private snackBar: MatSnackBar) { }

  ngOnInit() {
  }

  login(loginForm) {
    if (loginForm.valid) {
      this.loading = true;
      this.authService.login(this.model).subscribe((res: any) => {
        localStorage.setItem('token', res.token);
        this.authService.changeUserStatus(res.name);
        this.router.navigate(['']);
      }, error => {
        this.loading = false;
        this.snackBar.open(error.error.title + ': نام کاربری یا کلمه عبور اشتباه است', 'بستن', {
          duration: 2000,
        });
      });
    }
  }

}
