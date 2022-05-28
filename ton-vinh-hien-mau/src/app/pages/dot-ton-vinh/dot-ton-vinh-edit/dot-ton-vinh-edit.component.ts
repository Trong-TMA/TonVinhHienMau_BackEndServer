import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DottonvinhService } from 'src/app/shared/services/dottonvinh.service';

@Component({
  selector: 'app-dot-ton-vinh-edit',
  templateUrl: './dot-ton-vinh-edit.component.html',
  styleUrls: ['./dot-ton-vinh-edit.component.scss'],
  providers: [FormBuilder]
})
export class DotTonVinhEditComponent implements OnInit {

  validateForm!: FormGroup;
  @Input() dottonvinh :any;
  @Output() LoadDataEvent: EventEmitter<any>;

  constructor(
    private fb: FormBuilder,
    private dottonvinhService: DottonvinhService) {
      this.LoadDataEvent = new EventEmitter();
  }

  submitForm(){
    this.dottonvinh.name = this.validateForm.get('tendottonvinh')?.value
    this.dottonvinh.code = this.validateForm.get('madottonvinh')?.value

    return this.dottonvinhService.editDottonvinh(this.dottonvinh).subscribe((item)=>{
      this.LoadDataEvent.emit();
    })
  }

  ngOnInit(){
    this.validateForm = this.fb.group({
      tendottonvinh: [null, [Validators.required]],
      madottonvinh: [null, [Validators.required]],
    });
  }

}
