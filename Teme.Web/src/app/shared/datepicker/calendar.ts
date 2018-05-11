//Страны
import {Component, forwardRef, Input, OnInit} from '@angular/core';
import { NgbDatepickerConfig, NgbDateParserFormatter } from '@ng-bootstrap/ng-bootstrap';
import { NgbDatePTParserFormatter } from './NgbDatePTParserFormatter';
import { NgbDatepickerI18n } from '@ng-bootstrap/ng-bootstrap';
import { CustomDatepickerI18n, I18n } from './CustomDatepickerI18n';

@Component({
  selector: 'app-calendar',
  template: `
    <input class="form-control" placeholder="ddd"
           name="dp" [(ngModel)]="modelCustom" ngbDatepicker [dayTemplate]="customDay"
           [markDisabled]="isDisabled"
           #dc="ngbDatepicker">
    <div class="input-group-append" (click)="dc.toggle()">
      <span class="input-group-text"><i class="fa fa-calendar"></i></span>
    </div>
  `,
  styles: [],
  providers: [
    [I18n, { provide: NgbDatepickerI18n, useClass: CustomDatepickerI18n }],
    [{provide: NgbDateParserFormatter, useClass: NgbDatePTParserFormatter}],
  ],
})
export class Calendar implements OnInit  {

  constructor() {
  }
  ngOnInit(){
  }

}
