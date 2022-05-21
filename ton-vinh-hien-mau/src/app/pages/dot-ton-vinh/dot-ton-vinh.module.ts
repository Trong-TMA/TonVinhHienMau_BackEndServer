import { DotTonVinhComponent } from './dot-ton-vinh.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DotTonVinhRoutingModule } from './dot-ton-vinh-routing.module';
import {InputTextModule} from 'primeng/inputtext';
import { ReactiveFormsModule } from '@angular/forms';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzFormModule } from 'ng-zorro-antd/form';

@NgModule({
  declarations: [
    DotTonVinhComponent,
  ],
  imports: [
    CommonModule,
    DotTonVinhRoutingModule,
    InputTextModule,
    NzFormModule,
    NzInputModule,
    ReactiveFormsModule,
    NzButtonModule,
    NzIconModule,
    NzTableModule,
    NzDividerModule
  ],
  exports:[
    DotTonVinhComponent
  ]
})
export class DotTonVinhModule { }
