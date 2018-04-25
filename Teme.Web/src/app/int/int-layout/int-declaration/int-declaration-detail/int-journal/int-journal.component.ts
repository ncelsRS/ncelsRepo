import {
  Component,
  ComponentFactoryResolver,
  forwardRef,
  Input,
  OnInit,
  ViewChild,
  ViewContainerRef,
  ViewEncapsulation
} from '@angular/core';
import {AbstractControl, NG_VALIDATORS, NG_VALUE_ACCESSOR, ValidationErrors} from "@angular/forms";
import {IntJournalListComponent} from "./int-journal-list/int-journal-list.component"

@Component({
  selector: 'app-int-journal',
  templateUrl: './int-journal.component.html',
  styleUrls: ['./int-journal.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => IntJournalComponent),
    multi: true
  }, {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => IntJournalComponent),
    multi: true
  }]
})
export class IntJournalComponent implements OnInit {
  @Input() showErrors = false;
  @ViewChild('journalForm') form;
  @ViewChild('parent', {read: ViewContainerRef}) container: ViewContainerRef;
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
    var comp = this._cfr.resolveComponentFactory(IntJournalListComponent);
    var expComponent = this.container.createComponent(comp);
    expComponent.instance._ref = expComponent;
  }
}
