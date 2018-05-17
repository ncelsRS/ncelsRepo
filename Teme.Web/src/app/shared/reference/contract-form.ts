//Тип регистрации
import {Component, forwardRef, Input, OnInit, Output,EventEmitter} from '@angular/core';
import {NG_VALUE_ACCESSOR, NG_VALIDATORS} from '@angular/forms'
import {TemplateValidation} from 'app/shared/TemplateValidation';
import {reference,referenceEnum} from './reference';
import {ReferenceService} from './reference.service';

@Component({
  selector: 'app-reference-contract-form',
  template: `
    <select class="form-control" #templateForm="ngModel" name="contractForm"
            [ngClass]="{'has-error':showItemErrors === true && templateForm.invalid}"
            [(ngModel)]="model" required (change)="changeModel($event.target)">
      <option value="" disabled selected>-- Выберите значение --</option>
      <option *ngFor="let item of items" [value]="item.id">{{item.nameRu}}</option>
    </select>
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
  },
    ReferenceService
  ]
})
export class ContractForm extends TemplateValidation implements OnInit  {
  @Input() showItemErrors = false;
  @Output() changeModelParent = new EventEmitter<any>();

  changeModel(evnt:any) {
    this.changeModelParent.emit(evnt);
  }


  public items: any = [];

  constructor(private referenceService: ReferenceService) {
    super();
  }
  ngOnInit(){

    this.referenceService.getContractForm().subscribe((data:[referenceEnum])=> {
      data.forEach(d => {
        var ref = this.refs.filter(x => x.code == d.value )[0];
        this.items.push(new reference(d.key, ref.code, ref.nameRu, ref.nameKz) );
      });
    });

  }

  readonly refs = [
    new reference(null, 'Registration', 'Регистрация', 'Регистрация кз'),
    new reference(null, 'Reregistration', 'Перерегистрация', 'Перерегистрация кз'),
    new reference(null, 'Modification', 'Внесение изменения', 'Внесение изменения кз'),
  ];

  getTest(){
    console.log(this.items,this.model);
  }
}
