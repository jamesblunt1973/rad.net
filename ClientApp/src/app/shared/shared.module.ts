import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { IconModule } from '@visurel/iconify-angular';
import { MaterialComponentsModule } from './material.module';

import { CounterComponent } from './components/counter/counter.component';
import { DigitComponent } from './components/counter/digit/digit.component';
import { PagerComponent } from './components/pager/pager.component';
import { ConfirmDialogComponent } from './components/confirm-dialog/confirm-dialog.component';
import { AlertDialogComponent } from './components/alert-dialog/alert-dialog.component';
import { MomentJalaaliPipe } from './moment-jalaali.pipe';


@NgModule({
  declarations: [CounterComponent,
    DigitComponent,
    PagerComponent,
    ConfirmDialogComponent,
    AlertDialogComponent,
    MomentJalaaliPipe],
  imports: [
    CommonModule,
    FlexLayoutModule,
    MaterialComponentsModule,
    IconModule,
  ],
  providers: [],
  exports: [
    CommonModule,
    MaterialComponentsModule,
    FlexLayoutModule,
    IconModule,
    FormsModule,
    ReactiveFormsModule,
    CounterComponent,
    PagerComponent,
    ConfirmDialogComponent,
    AlertDialogComponent,
    MomentJalaaliPipe
  ],
  entryComponents: [
    ConfirmDialogComponent,
    AlertDialogComponent
  ]
})
export class SharedModule { }
