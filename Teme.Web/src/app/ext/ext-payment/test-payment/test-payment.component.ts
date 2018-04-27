import {Component, forwardRef, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {FormGroup, FormControl, Validators, FormBuilder, NG_VALUE_ACCESSOR, NG_VALIDATORS} from '@angular/forms'
import {TemplateValidation} from 'app/shared/TemplateValidation';


@Component({
  selector: 'app-test-payment',
  templateUrl: './test-payment.component.html',
  styleUrls: ['./test-payment.component.scss'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => TestPaymentComponent),
    multi: true
  }, {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => TestPaymentComponent),
    multi: true
  }
  ]
})
export class TestPaymentComponent extends TemplateValidation{
  @Input() showErrors = false;
  //public form:FormGroup;
  //selectedCountries:string = "";
  public icons = [];
  //myGroup;
  constructor(//public fb:FormBuilder
  ) {

    super()

    this.getData((icons) => {
      this.icons = icons;
    });
  }
  ngOnInit() {
  }

  public getData(icons) {
    const req = new XMLHttpRequest();
    req.open('GET', 'http://localhost:5121/api/reference/ClassifierMedicalArea');
    req.onload = () => {
      icons(JSON.parse(req.response));
    };
    req.send();

  }


}
