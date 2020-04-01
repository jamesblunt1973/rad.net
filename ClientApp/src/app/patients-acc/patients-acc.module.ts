import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { PatientDetailsComponent } from './patient-details/patient-details.component';
import { ReportComponent } from './report/report.component';

const routes: Routes = [
  { path: '', component: ReportComponent, pathMatch: 'full' }
];


@NgModule({
  declarations: [ReportComponent, PatientDetailsComponent],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],
  entryComponents: [PatientDetailsComponent]
})
export class PatientsAccModule { }
