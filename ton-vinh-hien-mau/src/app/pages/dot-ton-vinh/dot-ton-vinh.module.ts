import { DotTonVinhComponent } from './dot-ton-vinh.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DotTonVinhRoutingModule } from './dot-ton-vinh-routing.module';
import {ButtonModule} from 'primeng/button';
import {InputTextModule} from 'primeng/inputtext';

@NgModule({
  declarations: [
    DotTonVinhComponent,
  ],
  imports: [
    CommonModule,
    DotTonVinhRoutingModule,
    ButtonModule,
    InputTextModule
  ],
  exports:[
    DotTonVinhComponent
  ]
})
export class DotTonVinhModule { }
