import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { ReportComponent } from './report/report.component';

const routes: Routes = [
  { path: '', component: ReportComponent, pathMatch: 'full' }
];


@NgModule({
  declarations: [ReportComponent],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ]
})
export class VerificationsModule { }
