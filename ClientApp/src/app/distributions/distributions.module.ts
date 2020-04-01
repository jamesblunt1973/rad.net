import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { NewComponent } from './new/new.component';
import { ReportComponent } from './report/report.component';
import { DistributionDetailsComponent } from './distribution-details/distribution-details.component';

const routes: Routes = [
  { path: '', component: ReportComponent, pathMatch: 'full' },
  { path: 'new', component: NewComponent }
];

@NgModule({
  declarations: [NewComponent, ReportComponent, DistributionDetailsComponent],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],
  entryComponents: [DistributionDetailsComponent]
})
export class DistributionsModule { }
