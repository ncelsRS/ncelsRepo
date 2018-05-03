import {Component, EventEmitter, Input, OnInit, Output, ViewEncapsulation} from '@angular/core';
import {ViewCell} from 'ng2-smart-table';

@Component({
  selector: 'app-ext-manufactur-action',
  templateUrl: './ext-manufactur-action.component.html',
  styleUrls: ['./ext-manufactur-action.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ExtManufacturActionComponent implements OnInit ,  ViewCell{
  renderValue: string;

  @Input() value: string | number;
  @Input() rowData: any;

  @Output() save: EventEmitter<any> = new EventEmitter();

  ngOnInit() {
    this.renderValue = this.value.toString().toUpperCase();
  }

  onClick() {
    this.save.emit(this.rowData);
  }

  constructor() { }


}
