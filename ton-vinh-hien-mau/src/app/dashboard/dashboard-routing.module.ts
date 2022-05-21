import { DeXuatHoGiaDinhComponent } from './../pages/de-xuat-ho-gia-dinh/de-xuat-ho-gia-dinh.component';


import { DashboardComponent } from './dashboard.component';
import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DotTonVinhComponent } from '../pages/dot-ton-vinh/dot-ton-vinh.component';
import { DeXuatCaNhanComponent } from '../pages/de-xuat-ca-nhan/de-xuat-ca-nhan.component';

const routes: Routes = [
  { path: '', component: DashboardComponent,
  children:[
    {
      path: 'Tao-dot-ton-vinh',
      component: DotTonVinhComponent,
    },
    {
      path: 'De-xuat-ca-nhan',
      component: DeXuatCaNhanComponent,
    },
    {
      path: 'De-xuat-ho-gia-dinh',
      component: DeXuatHoGiaDinhComponent,
    }
  ] },
];

@NgModule({
  imports:[RouterModule.forChild(routes)],
  exports:[RouterModule]
})
export class DashboardRoutingModule { }
