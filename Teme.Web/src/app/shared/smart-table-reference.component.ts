import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ViewCell} from 'ng2-smart-table';

@Component({
  selector: 'smart-table-country',
  template: `
    {{ renderValue }}
    <!--<a class="ng2-smart-action ng2-smart-action-edit-edit" (click)="onClick()" href="#" >Изменить</a>-->
    <!--<button class="btn btn-warning w-100p mb-1" type="button" (click)="onClick()">Изменить</button>-->
    <!--<button (click)="onClick()">{{ renderValue }}</button>-->
    <!--<i class="fa fa-pencil mr-3 text-primary" (click)="onClick()"></i>-->
  `,
})
export class SmartTableReferenceComponent implements ViewCell, OnInit {
  private devicesMap: any[] = [{ value: '1', title: 'Казахстан' }, { value: '2', title: 'Россия' }, { value: '3', title: 'Белорусия'}];

  renderValue: string;

  @Input() value: string;
  @Input() rowData: any;

  @Output() edit: EventEmitter<any> = new EventEmitter();

  ngOnInit() {
    console.log('this.value', this.value);
    this.renderValue = this.devicesMap.filter(x => x.value == this.value)[0].title;
  }

  onClick() {
    this.edit.emit(this.rowData);
  }



}
