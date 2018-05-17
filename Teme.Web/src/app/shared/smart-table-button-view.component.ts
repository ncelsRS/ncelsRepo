import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ViewCell} from 'ng2-smart-table';

@Component({
  selector: 'smart-table-button-view',
  template: `
    <a class="ng2-smart-action ng2-smart-action-edit-edit" (click)="onClick()" href="#" >Изменить</a>
    <!--<button class="btn btn-warning w-100p mb-1" type="button" (click)="onClick()">Изменить</button>-->
    <!--<button (click)="onClick()">{{ renderValue }}</button>-->
    <!--<i class="fa fa-pencil mr-3 text-primary" (click)="onClick()"></i>-->
  `,
})
export class SmartTableButtonViewComponent implements ViewCell, OnInit {
  renderValue: string;

  @Input() value: string | number;
  @Input() rowData: any;

  @Output() edit: EventEmitter<any> = new EventEmitter();

  ngOnInit() {
    //this.renderValue = this.value.toString().toUpperCase();
  }

  onClick() {
    this.edit.emit(this.rowData);
  }
}
