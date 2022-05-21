import { DeXuatCaNhanRoutingModule } from './de-xuat-ca-nhan-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NzStepsModule } from 'ng-zorro-antd/steps';
import { DeXuatCaNhanComponent } from './de-xuat-ca-nhan.component';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import {
  StepBackwardOutline,
  CaretLeftOutline,
  SettingOutline
} from '@ant-design/icons-angular/icons';
import { IconDefinition } from '@ant-design/icons-angular/public_api';
import { NzFormModule} from 'ng-zorro-antd/form';
import { ReactiveFormsModule } from '@angular/forms';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzUploadModule } from 'ng-zorro-antd/upload';
const icons: IconDefinition[] = [
  StepBackwardOutline,
  CaretLeftOutline,
  SettingOutline
];

@NgModule({
  declarations: [
    DeXuatCaNhanComponent
  ],
  imports: [
    CommonModule,
    NzSelectModule,
    NzStepsModule,
    DeXuatCaNhanRoutingModule,
    NzButtonModule,
    NzIconModule.forChild(icons),
    NzFormModule,
    ReactiveFormsModule,
    NzUploadModule,
    NzButtonModule
  ],
  exports:[
    DeXuatCaNhanComponent
  ]
})
export class DeXuatCaNhanModule { }
