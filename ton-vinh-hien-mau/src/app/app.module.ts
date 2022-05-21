import { DeXuatCaNhanModule } from './pages/de-xuat-ca-nhan/de-xuat-ca-nhan.module';
import { DotTonVinhRoutingModule } from './pages/dot-ton-vinh/dot-ton-vinh-routing.module';
import { DotTonVinhModule } from './pages/dot-ton-vinh/dot-ton-vinh.module';
import { DashboardRoutingModule } from './dashboard/dashboard-routing.module';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { NzBreadCrumbModule } from 'ng-zorro-antd/breadcrumb';
import {ButtonModule} from 'primeng/button';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    NzLayoutModule,
    NzMenuModule,
    DotTonVinhRoutingModule,
    DashboardRoutingModule,
    NzBreadCrumbModule,
    ButtonModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
