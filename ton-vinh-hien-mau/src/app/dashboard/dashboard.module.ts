import { NzMenuModule } from 'ng-zorro-antd/menu';
import { NzBreadCrumbModule } from 'ng-zorro-antd/breadcrumb';
import { NzLayoutModule } from 'ng-zorro-antd/layout';

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard.component';
import { NzIconModule } from 'ng-zorro-antd/icon';

@NgModule({
  declarations: [
    DashboardComponent,
  ],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    NzLayoutModule,
    NzBreadCrumbModule,
    NzMenuModule,
    NzIconModule

  ],
  exports:[
    DashboardComponent,
  ]
})
export class DashboardModule {

}
