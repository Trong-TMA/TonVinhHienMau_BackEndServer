import { Component, OnInit } from '@angular/core';
import { NzUploadChangeParam } from 'ng-zorro-antd/upload';


@Component({
  selector: 'app-de-xuat-ca-nhan',
  templateUrl: './de-xuat-ca-nhan.component.html',
  styleUrls: ['./de-xuat-ca-nhan.component.scss']
})

export class DeXuatCaNhanComponent implements OnInit {

  constructor() {}

  ngOnInit(): void {
  }

  handleChange(info: NzUploadChangeParam): void {}

}
