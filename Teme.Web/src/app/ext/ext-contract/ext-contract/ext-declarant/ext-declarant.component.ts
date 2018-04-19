import {Component, forwardRef, Input, OnInit, ViewChild} from '@angular/core';
import {DeclarantDocType} from './DeclarantDocType';
import {
  AbstractControl,
  ControlValueAccessor,
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
  ValidationErrors,
  Validator
} from "@angular/forms";


@Component({
    selector: 'app-ext-declarant',
    templateUrl: './ext-declarant.component.html',
    styleUrls: ['./ext-declarant.component.css'],
    providers: [{
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ExtDeclarantComponent),
      multi: true
    }, {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => ExtDeclarantComponent),
      multi: true
    }]
})
export class ExtDeclarantComponent implements ControlValueAccessor, Validator {
  selectedDeclarantDocType: string;
  levels: Array<DeclarantDocType> = [
    {code: 'Procuration', name: 'Доверенность'},
    {code: 'organizationChart', name: 'Устав'},
  ]


  changeDocLevel(lev: DeclarantDocType) {
    this.selectedDeclarantDocType = lev.name;
  }

  constructor() {
    this.selectedDeclarantDocType = 'Procuration';
  }


    ngOnInit() {
  }
  private _model: any = {};
  @ViewChild('DeclarantForm') form;
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
