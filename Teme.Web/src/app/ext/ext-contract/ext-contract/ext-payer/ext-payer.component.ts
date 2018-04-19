import {Component, forwardRef, Input, OnInit, ViewChild} from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
  ValidationErrors,
  Validator
} from "@angular/forms";

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
export class ExtPayerComponent implements  ControlValueAccessor, Validator {

  constructor() { }

  ngOnInit() {
  }
  private _model: any = {};
  @ViewChild('PayerForm') form;
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

}
