import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { ReportComponent } from './report/report.component';
import { MedicineDetailsComponent } from './medicine-details/medicine-details.component';

const routes: Routes = [
  { path: '', component: ReportComponent, pathMatch: 'full' }
];



@NgModule({
  declarations: [ReportComponent, MedicineDetailsComponent],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],
  entryComponents: [MedicineDetailsComponent]
})
export class MedicinesAccModule { }
