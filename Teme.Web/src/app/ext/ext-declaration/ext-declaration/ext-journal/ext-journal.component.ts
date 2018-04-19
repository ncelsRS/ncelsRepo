import {
  Component, forwardRef, Input, ViewChild,
  ComponentFactoryResolver,
  ViewContainerRef
} from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
  ValidationErrors,
  Validator
} from '@angular/forms';
import { ExtJournalListComponent } from './ext-journal-list/ext-journal-list.component'

@Component({
  selector: 'app-ext-journal',
  templateUrl: './ext-journal.component.html',
  styleUrls: ['./ext-journal.component.css'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => ExtJournalComponent),
    multi: true
  }, {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => ExtJournalComponent),
    multi: true
  }]
})
export class ExtJournalComponent implements ControlValueAccessor, Validator {
  @Input() showErrors = false;
  @ViewChild('journalForm') form;
  @ViewChild('parent', { read: ViewContainerRef }) container: ViewContainerRef;
  private _model: any = {};
  public data = [];
  showAgree: boolean;

  constructor(private _cfr: ComponentFactoryResolver) {
    this.showAgree = false;
  }

  public getData(data) {
    const req = new XMLHttpRequest();
    req.open('GET', 'assets/data/users.json');
    req.onload = () => {
      data(JSON.parse(req.response));
    };
    req.send();
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

  showAgreement(show: boolean) {
    this.showAgree = show;
  }

  ngOnInit() {
  }

  addComponent(){
    var comp = this._cfr.resolveComponentFactory(ExtJournalListComponent);
    var expComponent = this.container.createComponent(comp);
    expComponent.instance._ref = expComponent;
  }

}
