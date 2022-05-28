import { DonviService } from './../../../shared/services/donvi.service';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-don-vi-create',
  templateUrl: './don-vi-create.component.html',
  styleUrls: ['./don-vi-create.component.scss'],
  providers: [FormBuilder]
})
export class DonViCreateComponent implements OnInit {

  validateForm!: FormGroup;
  @Output() LoadDataEvent: EventEmitter<any>;

  constructor(
    private fb: FormBuilder,
    private donviService: DonviService) {
      this.LoadDataEvent = new EventEmitter();
  }

  submitForm(){
    // return this.donviService.createDottonvinh(this.validateForm.get('madottonvinh')?.value).subscribe((item)=>{
    //   this.LoadDataEvent.emit();
    // });
  }

  ngOnInit(){
    this.validateForm = this.fb.group({
      madottonvinh: [null, [Validators.required]]

    });
  }
}
