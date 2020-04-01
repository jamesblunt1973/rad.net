import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { ListComponent } from './list/list.component';
import { NewComponent } from './new/new.component';

const routes: Routes = [
  { path: '', component: ListComponent, pathMatch: 'full' },
  { path: 'new', component: NewComponent }
];

@NgModule({
  declarations: [ListComponent, NewComponent],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ]
})
export class PatientsModule { }
