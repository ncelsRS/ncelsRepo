import {Component, forwardRef, Input} from '@angular/core';
import {NG_VALUE_ACCESSOR, NG_VALIDATORS} from '@angular/forms'
import {TemplateValidation} from 'app/shared/TemplateValidation';

@Component({
  selector: 'app-reference-contract-form-validation',
  template: `
    <select class="form-control" #templateForm="ngModel" name="templateForm"
            [ngClass]="{'has-error':showErrors === true && templateForm.invalid}"
            [(ngModel)]="model.contractForm" required>
      <option value="" disabled selected>-- Выберите значение1 --</option>
      <option *ngFor="let item of items" [value]="item.key">{{item.nameRu}}</option>
    </select>
  `,
  styles: [],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => ContractFormValidation),
    multi: true
  }, {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => ContractFormValidation),
    multi: true
  }
  ]
})
export class ContractFormValidation extends TemplateValidation {
  @Input() showErrors = false;
  //selectedCountries: string = '';
  public items = [];

  constructor() {



    super();

    // console.log("bekbol",this.model);

    this.getData((items) => {
      this.items = items;
      for (let item of this.items) {
        if (item.value == 'Registration') {
          item.nameRu = 'Регистрация';
          item.nameKz = 'Регистрация кз';
        }
        else if (item.value == 'Reregistration') {
          item.nameRu = 'Перерегистрация';
          item.nameKz = 'Перерегистрация кз';
        }
        else if (item.value == 'Modification') {
          item.nameRu = 'Внесение изменения';
          item.nameKz = 'Внесение изменения кз';
        }
      }

    });
  }

  public getData(items) {
    const req = new XMLHttpRequest();
    req.open('GET', 'http://localhost:5121/api/Reference/ContractForm');
    req.onload = () => {
      items(JSON.parse(req.response));
    };
    req.send();
  }

}
