import { ImportNguoiHienMauComponent } from './../pages/import-nguoi-hien-mau/import-nguoi-hien-mau.component';
import { DanhSachHienMauCreateComponent } from './../pages/danh-sach-hien-mau/danh-sach-hien-mau-create/danh-sach-hien-mau-create.component';
import { DanhSachHienMauComponent } from './../pages/danh-sach-hien-mau/danh-sach-hien-mau.component';
import { DonViComponent } from './../pages/don-vi/don-vi.component';
import { DeXuatHoGiaDinhComponent } from './../pages/de-xuat-ho-gia-dinh/de-xuat-ho-gia-dinh.component';
import { DashboardComponent } from './dashboard.component';
import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
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
    },
    {
      path: 'Don-vi',
      component: DonViComponent,
    },
    {
      path: 'Danh-sach-hien-mau',
      component: DanhSachHienMauComponent,
    },
    {
      path: 'Tao-moi',
      component: DanhSachHienMauCreateComponent,
    },
    {
      path: 'Import-nguoi-hien-mau',
      component: ImportNguoiHienMauComponent,
    }
  ] },
];

@NgModule({
  imports:[RouterModule.forChild(routes)],
  exports:[RouterModule]
})
export class DashboardRoutingModule { }
