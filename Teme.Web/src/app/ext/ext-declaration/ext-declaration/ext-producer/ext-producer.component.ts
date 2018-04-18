import {Component, forwardRef, Input, ViewChild} from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
  ValidationErrors,
  Validator
} from "@angular/forms";
import {ExtGeneralInformationComponent} from "../ext-general-information/ext-general-information.component";

@Component({
  selector: 'app-ext-producer',
  templateUrl: './ext-producer.component.html',
  styleUrls: ['./ext-producer.component.css'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => ExtProducerComponent),
    multi: true
  }, {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => ExtProducerComponent),
    multi: true
  }]
})
export class ExtProducerComponent implements ControlValueAccessor, Validator {
  private _model: any = {};
  @ViewChild('producerForm') form;
  @Input() showErrors = false;

  constructor() {
  }

  ngOnInit() {
  }


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
