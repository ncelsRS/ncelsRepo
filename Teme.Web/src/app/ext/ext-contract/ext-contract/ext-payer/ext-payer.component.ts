import {Component, forwardRef, Input, OnInit, ViewChild} from '@angular/core';
import {
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR
} from "@angular/forms";
import {TemplateValidation} from '../../../../shared/TemplateValidation';

@Component({
  selector: 'app-ext-payer',
  templateUrl: './ext-payer.component.html',
  styleUrls: ['./ext-payer.component.css'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => ExtPayerComponent),
    multi: true
  }, {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => ExtPayerComponent),
    multi: true
  }]
})
export class ExtPayerComponent extends TemplateValidation {
  isAddOrgForm = false;
  isAddBankName = false;
  constructor() { super()}

  ngOnInit() {
  }
  @Input() showErrors = false;

  addOrgForm()
  {
    this.isAddOrgForm = true;
  }

  saveOrgForm()
  {
    this.isAddOrgForm = false;
  }

  addBankName()
  {
    this.isAddBankName = true;
  }

  saveBankName()
  {
    this.isAddBankName = false;
  }

}
