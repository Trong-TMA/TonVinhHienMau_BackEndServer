import { DotTonVinhRoutingModule } from './../pages/dot-ton-vinh/dot-ton-vinh-routing.module';
import { DotTonVinhModule } from './../pages/dot-ton-vinh/dot-ton-vinh.module';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { NzBreadCrumbModule } from 'ng-zorro-antd/breadcrumb';
import { NzLayoutModule } from 'ng-zorro-antd/layout';

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard.component';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzInputModule } from 'ng-zorro-antd/input';
import {ButtonModule} from 'primeng/button';

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
    NzIconModule,
    NzTableModule,
    NzDividerModule,
    NzInputModule,
    ButtonModule

  ],
  exports:[
    DashboardComponent,
  ]
})
export class DashboardModule {

}
