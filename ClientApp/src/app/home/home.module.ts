import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { NgxChartsModule } from '@swimlane/ngx-charts';

import { AuthGuard } from '../core/auth.guard';
import { SharedModule } from '../shared/shared.module';

import { DashboardComponent } from './dashboard/dashboard.component';

const routes: Routes = [
  {
    path: '', component: DashboardComponent, canActivate: [AuthGuard], pathMatch: 'full'
  }
];


@NgModule({
  declarations: [DashboardComponent],
  imports: [
    SharedModule,
    RouterModule.forChild(routes),
    NgxChartsModule
  ]
})
export class HomeModule { }
