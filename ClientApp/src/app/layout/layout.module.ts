import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SharedModule } from '../shared/shared.module';

import { AuthGuard } from '../core/auth.guard';
import { ContainerComponent } from './container/container.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { SidebarComponent } from './sidebar/sidebar.component';

const routes: Routes = [
  {
    path: '', component: ContainerComponent,
    children: [
      {
        path: 'home',
        loadChildren: () => import('../home/home.module').then(m => m.HomeModule),
        canLoad: [AuthGuard]
      },
      {
        path: 'medicines',
        loadChildren: () => import('../medicines/medicines.module').then(m => m.MedicinesModule),
        canLoad: [AuthGuard]
      },
      {
        path: 'medicines-acc',
        loadChildren: () => import('../medicines-acc/medicines-acc.module').then(m => m.MedicinesAccModule),
        canLoad: [AuthGuard]
      },
      {
        path: 'patients',
        loadChildren: () => import('../patients/patients.module').then(m => m.PatientsModule),
        canLoad: [AuthGuard]
      },
      {
        path: 'patients-acc',
        loadChildren: () => import('../patients-acc/patients-acc.module').then(m => m.PatientsAccModule),
        canLoad: [AuthGuard]
      },
      {
        path: 'pharmacies',
        loadChildren: () => import('../pharmacies/pharmacies.module').then(m => m.PharmaciesModule),
        canLoad: [AuthGuard]
      },
      {
        path: 'physicians',
        loadChildren: () => import('../physicians/physicians.module').then(m => m.PhysiciansModule),
        canLoad: [AuthGuard]
      },
      {
        path: 'requests',
        loadChildren: () => import('../requests/requests.module').then(m => m.RequestsModule),
        canLoad: [AuthGuard]
      },
      {
        path: 'verifications',
        loadChildren: () => import('../verifications/verifications.module').then(m => m.VerificationsModule),
        canLoad: [AuthGuard]
      },
      {
        path: 'distributions',
        loadChildren: () => import('../distributions/distributions.module').then(m => m.DistributionsModule),
        canLoad: [AuthGuard]
      },
      {
        path: 'settings',
        loadChildren: () => import('../settings/settings.module').then(m => m.SettingsModule),
        canLoad: [AuthGuard]
      },
      {
        path: '',
        pathMatch: 'full',
        redirectTo: '/home'
      }
    ]
  }
];

@NgModule({
  declarations: [
    ContainerComponent,
    HeaderComponent,
    FooterComponent,
    SidebarComponent
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ]
})
export class LayoutModule { }
