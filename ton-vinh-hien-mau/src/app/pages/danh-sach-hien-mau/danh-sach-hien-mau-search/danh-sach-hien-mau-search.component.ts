import { QuanLyNguoiHienMauService } from 'src/app/shared/services/quan-ly-nguoi-hien-mau.service';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { differenceInCalendarDays, setHours } from 'date-fns';
import { DisabledTimeFn, DisabledTimePartial } from 'ng-zorro-antd/date-picker';
@Component({
  selector: 'app-danh-sach-hien-mau-search',
  templateUrl: './danh-sach-hien-mau-search.component.html',
  styleUrls: ['./danh-sach-hien-mau-search.component.scss'],
  providers: [FormBuilder]
})
export class DanhSachHienMauSearchComponent implements OnInit {

  today = new Date();
  timeDefaultValue = setHours(new Date(), 0);

  isSpinning: boolean;
  validateForm!: FormGroup;
  filter:string;
  gioitinh:Boolean = new Boolean();
  namsinh: Number= new Number();
  @Output() loadDataEmit: EventEmitter<any>;



  constructor(
    private quanlyNguoiHienMauService: QuanLyNguoiHienMauService,
    private fb: FormBuilder) {
      this.isSpinning = false;
      this.loadDataEmit = new EventEmitter();
      this.filter = ""
  }

  ngOnInit(): void {
  }

  search(){
    this.quanlyNguoiHienMauService.search(this.filter,this.gioitinh,this.namsinh).subscribe((res)=>{
      this.loadDataEmit.emit(res)
    })
  }

  disabledDate = (current: Date): boolean =>
    // Can not select days before today and today
    differenceInCalendarDays(current, this.today) > 0;
}
