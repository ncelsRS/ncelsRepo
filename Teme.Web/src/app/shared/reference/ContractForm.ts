import {Component, forwardRef, Input, OnInit} from '@angular/core';
import {NG_VALUE_ACCESSOR, NG_VALIDATORS} from '@angular/forms'
import {TemplateValidation} from 'app/shared/TemplateValidation';
import {ConstantContractForm} from './constant/contractContractForm';
import {reference} from './reference';
import {ReferenceService} from './reference.service';

@Component({
  selector: 'app-reference-contract-form-validation',
  template: `
    <select class="form-control" #templateForm="ngModel" name="templateForm"
            [ngClass]="{'has-error':showItemErrors === true && templateForm.invalid}"
            [(ngModel)]="model" required>
      <option value="" disabled selected>-- Выберите значение --</option>
      <option *ngFor="let item of items" [value]="item.id">{{item.nameRu}}</option>
    </select>
    <!--{{model}}-->
    <!--<button type="button" (click)="getTest()" class="btn btn-warning btn-sm">Test</button>-->
  `,
  styles: [],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => ContractForm),
    multi: true
  }, {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => ContractForm),
    multi: true
  },ReferenceService
  ]
})
export class ContractForm extends TemplateValidation implements OnInit  {
  @Input() showItemErrors = false;
  // model: any;

  readonly items = [
    new reference(null, 'Registration', 'Регистрация', 'Регистрация кз'),
    new reference(null, 'Reregistration', 'Перерегистрация', 'Перерегистрация кз'),
    new reference(null, 'Modification', 'Внесение изменения', 'Внесение изменения кз'),
  ];
  public dataArr;

  constructor(private referenceService: ReferenceService) {
    super();
    this.getData((dataArr) => {

      for (let data of this.dataArr){

      }
      //this.items = items;
      for (let item of this.items) {
        item.id = "" + dataArr.filter(data => data.value===item.code )[0].key;
      }
    });
  }
  ngOnInit(){
  }

  // public getConstantContractForm():Array<reference> {
  //   return ConstantContractForm;
  // }
  public getData(dataArr) {
    const req = new XMLHttpRequest();
    req.open('GET', 'http://localhost:5121/api/reference/ContractForm');
    req.onload = () => {
      dataArr(JSON.parse(req.response));
    };
    req.send();


    //ttt =[];
    //this.referenceService.getContractForm().subscribe(data => dataArr=data);
    //dataArr(ttt)
  }

  getTest(){
    console.log(this.model);
  }
}
