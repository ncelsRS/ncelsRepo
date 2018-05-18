//Страны
import {Component, EventEmitter, forwardRef, Input, OnInit, Output} from '@angular/core';
import {ReferenceService} from './reference.service';
import {TemplateValidation} from '../TemplateValidation';
import {NG_VALUE_ACCESSOR, NG_VALIDATORS} from '@angular/forms';

@Component({
  selector: 'app-reference-standart',
  template: `
    <select class="form-control" #templateForm="ngModel" attr.name="{{ref.column}}"
            [ngClass]="{'has-error':showItemErrors === true && templateForm.invalid}"
            [(ngModel)]="model" required (change)="changeModel($event.target)" >
      <option value="" disabled selected>-- Выберите значение --</option>
      <option *ngFor="let item of items" [value]="item.id">{{item.nameRu}}</option>
    </select>
    <!--<button type="button" (click)="getTest()" class="btn btn-warning btn-sm">Test</button>-->
  `,
  styles: [],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => ReferenceStandart),
    multi: true
  }, {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => ReferenceStandart),
    multi: true
  },
    ReferenceService
  ]
})
export class ReferenceStandart extends TemplateValidation implements OnInit  {
  @Input() showItemErrors = false;
  @Input() ref : any;
  @Output() changeModelParent = new EventEmitter<any>();

  changeModel(evnt:any) {
    this.changeModelParent.emit(evnt);
  }
  public items: any;

  constructor(private referenceService: ReferenceService) {
    super();
  }

  ngOnInit(){
    this.referenceService.getReferenceStandart(this.ref.name).subscribe((data)=> {this.items=data});
  }

  getTest(){
    console.log(this.items);
  }
}
