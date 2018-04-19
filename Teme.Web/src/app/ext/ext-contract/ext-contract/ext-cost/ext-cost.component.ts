import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
  ValidationErrors,
  Validator
} from "@angular/forms";
import {ExtDeclarationsActionsComponent} from '../../../ext-declaration/ext-declarations/ext-declarations-actions/ext-declarations-actions.component';

@Component({
  selector: 'app-ext-cost',
  templateUrl: './ext-cost.component.html',
  styleUrls: ['./ext-cost.component.css']
})
export class ExtCostComponent implements ControlValueAccessor, Validator {
  isViewCost = false;
  private _model: any = {};
  @ViewChild('Form') form;
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

  constructor() { }

  ngOnInit() {
  }

  public costsSettings = {
    selectMode: 'single',
    hideHeader: false,
    hideSubHeader: false,
    noDataMessage: 'Нет данных',
    actions: false,
    prop: {name: 'view', filter: false},
    columns: {
      view: {
        title: '№',
        type: 'string'
      },
      declarationType: {
        title: 'Наименование работ по Прейскуранту',
        type: 'string'
      },
      name: {
        title: 'Цена в тенге, без НДС',
        type: 'string'
      },
      number: {
        title: 'Количество',
        type: 'string'
      },
      currentStatus: {
        title: 'Всего',
        type: 'string'
      },
      // sendDate: {
      //   title: 'Тип',
      //   type: 'string'
      // },
      // Action: {
      //   title: 'Действия',
      //   type: 'custom',
      //   renderComponent: ExtDeclarationsActionsComponent,
      //   onComponentInitFunction(instance) {
      //     instance.save.subscribe(row => {
      //       alert(`${row.name} saved!`)
      //     });
      //   }
      // },
    },
    pager: {
      display: true,
      perPage: 10
    }
  };

  viewCost()
  {
    this.isViewCost = true;
  }

}
