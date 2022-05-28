import { DonviService } from './../../../shared/services/donvi.service';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NzModalService } from 'ng-zorro-antd/modal';

@Component({
  selector: 'app-don-vi-list',
  templateUrl: './don-vi-list.component.html',
  styleUrls: ['./don-vi-list.component.scss']
})
export class DonViListComponent implements OnInit {

  isSpinning: boolean;
  @Input() listDonvi: any;
  @Output() loadDataEmit: EventEmitter<any>;

  isVisible = false;

  constructor(private donviService: DonviService,private modal: NzModalService) {
    this.loadDataEmit =  new EventEmitter();
    this.isSpinning = false;
  }

  ngOnInit(): void {
  }

  loadDottonvinh(){
    this.loadDataEmit.emit();
  }

  deleteDTV(item: any){
    // this.isSpinning = true;
    // this.donviService.deleteDottonvinh(item?.id).subscribe((item)=>{
    //   this.loadDataEmit.emit();
    // })

  }

  showModal(item: any){
    // this.isVisible = true;
    // this.dtv.id = item.id;
    // this.dtv.name = item.tenDotTonVinh;
    // this.dtv.code = item.maDotTonVinh;
  }


  handleCancel(): void {
    this.isVisible = false;
  }

}
