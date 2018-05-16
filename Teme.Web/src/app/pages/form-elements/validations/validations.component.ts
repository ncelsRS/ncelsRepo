import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { CustomValidators } from 'ng2-validation';

@Component({
  selector: 'app-validations',
  templateUrl: './validations.component.html',
  encapsulation: ViewEncapsulation.None
})
export class ValidationsComponent implements OnInit {
  public form: FormGroup;
  constructor(public formBuilder: FormBuilder) { }

  ngOnInit() {
      let password = new FormControl('', Validators.required);
      let certainPassword = new FormControl('', CustomValidators.equalTo(password));

      let first = new FormControl('', Validators.required);
      let second = new FormControl('', CustomValidators.notEqualTo(first));

      this.form = this.formBuilder.group({ 
          required: ['', Validators.required],
          minLength: ['', Validators.compose([Validators.required, CustomValidators.min(3)])],
          maxLength:  ['', Validators.compose([Validators.required,  Validators.maxLength(10)])],
          base64:  ['', CustomValidators.base64],
          creditCard:  ['', CustomValidators.creditCard],
          date: ['', CustomValidators.date],
          dateISO: ['', CustomValidators.dateISO],
          maxDate: ['', CustomValidators.maxDate('2017-09-09')],
          minDate: ['', CustomValidators.minDate('2017-09-09')],
          digits: ['', CustomValidators.digits],
          email: ['', CustomValidators.email],
          equal: ['', CustomValidators.equal('5')],            
          notEqual: ['', CustomValidators.notEqual('10')],
          password: password,
          certainPassword: certainPassword,
          first: first,
          second: second,
          greaterThan: ['', CustomValidators.gt(10)],
          greaterThanEqual: ['', CustomValidators.gte(15)],
          lessThan: ['', CustomValidators.lt(10)],
          lessThanEqual: ['', CustomValidators.lte(10)],
          max: ['', CustomValidators.max(10)],
          min: ['', CustomValidators.min(10)],
          number: ['', CustomValidators.number],
          phone: ['', CustomValidators.phone('US')],
          range: ['', CustomValidators.range([10, 20])],
          rangeLength: ['', CustomValidators.rangeLength([5, 9])],
          url: ['', CustomValidators.url]
      });
  }

  public submitForm(value: any){
    console.log(value);
  }

}
