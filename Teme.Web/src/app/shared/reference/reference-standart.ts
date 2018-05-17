//Страны
import {Component, forwardRef, Input, OnInit} from '@angular/core';
import {ReferenceService} from './reference.service';
import {TemplateValidation} from '../TemplateValidation';
import {NG_VALUE_ACCESSOR, NG_VALIDATORS} from '@angular/forms';

@Component({
  selector: 'app-reference-country',
  template: `
    <select class="form-control" #templateForm="ngModel" name="templateForm"
            [ngClass]="{'has-error':showItemErrors === true && templateForm.invalid}"
            [(ngModel)]="model" required>
      <option value="" disabled selected>-- Выберите значение --</option>
      <option *ngFor="let item of items" [value]="item.id">{{item.nameRu}}</option>
    </select>
    <!--<button type="button" (click)="getTest()" class="btn btn-warning btn-sm">Test</button>-->
  `,
  styles: [],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => DegreeRiskClass),
    multi: true
  }, {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => DegreeRiskClass),
    multi: true
  },
    ReferenceService
  ]
})
export class DegreeRiskClass extends TemplateValidation implements OnInit  {
  @Input() showItemErrors = false;
  public items: any;

  constructor(private referenceService: ReferenceService) {
    super();
  }
  ngOnInit(){
    this.referenceService.getCountry().subscribe((data)=> {this.items=data});

  }

  getTest(){
    console.log(this.items);
  }
}
