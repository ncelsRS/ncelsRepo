import {Component,   Input,  forwardRef, ViewChild} from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
  ValidationErrors,
  Validator
} from "@angular/forms";

@Component({
  selector: 'app-ext-manufacturer',
  templateUrl: './ext-manufacturer.component.html',
  styleUrls: ['./ext-manufacturer.component.css'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => ExtManufacturerComponent),
    multi: true
  }, {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => ExtManufacturerComponent),
    multi: true
  }]
})
export class ExtManufacturerComponent implements  ControlValueAccessor, Validator{

   isAddOrgForm = false;
   isAddBankName = false;
  private _model: any = {};
  @Input() prnRegisterType: string;
   @ViewChild('ManufacturerForm') form;
   @Input() showErrors = false;

  get model() {
    return this._model;
  }

  set model(v) {
    this._model = v;
    this.change(v);
  }

  private change = _ => {
  };
  private touch = () => {
  };

  registerOnChange(fn: any): void {
    this.change = fn;
  }

  registerOnTouched(fn: any): void {
    this.touch = fn;
  }

  setDisabledState(isDisabled: boolean): void {
  }

  writeValue(obj: any): void {
    this.model = obj;
    this.change(obj);
  }

  registerOnValidatorChange(fn: () => void): void {
  }

  validate(c: AbstractControl): ValidationErrors | null {
    if (this.form.valid) return null;
    return {error: true};
  }

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
