import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

import { ILogin } from '../shared/models/login.model';
import { environment } from '../../environments/environment';

@Injectable()
export class AuthService {
  constructor(private router: Router, private http: HttpClient) { }

  login(data: ILogin) {
    return this.http.post(environment.apiUrl + 'auth/login', data);
  }

  register(data: ILogin) {
    return this.http.post(environment.apiUrl + 'auth/register', data);
  }

  private userStatus = new BehaviorSubject<string>('');

  changeUserStatus(un: string) {
    this.userStatus.next(un);
  }

  getUsername(): Observable<string> {
    return this.userStatus.asObservable();
  }

  checkUser(): boolean | Observable<boolean> {
    var token = localStorage.getItem('token');
    if (!token) {
      this.router.navigate(['auth/login']);
      return false;
    }
    return this.http.get<any>(environment.apiUrl + 'auth/checkUser').pipe(
      map(res => {
        if (res && res.name) {
          this.changeUserStatus(res.name);
          return true;
        }
        else {
          localStorage.removeItem('token');
          this.router.navigate(['auth/login']);
          return false;
        }
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
    this.changeUserStatus('');
    this.router.navigate(['auth/login']);
  }
}
