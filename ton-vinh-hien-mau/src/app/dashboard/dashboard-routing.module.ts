import { DotTonVinhComponent } from './../pages/dot-ton-vinh/dot-ton-vinh.component';
import { DashboardComponent } from './dashboard.component';
import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

const routes: Routes = [
  { path: '', component: DashboardComponent,
  children:[
    {
      path: 'dottonvinh',
      component: DotTonVinhComponent,
    }
  ] },
];

@NgModule({
  imports:[RouterModule.forChild(routes)],
  exports:[RouterModule]
})
export class DashboardRoutingModule { }
