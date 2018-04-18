import {Component, EventEmitter, Input, OnInit, Output, ViewEncapsulation} from '@angular/core';
import { ViewCell } from 'ng2-smart-table';

@Component({
  selector: 'app-ext-declarations-actions',
  templateUrl: './ext-declarations-actions.component.html',
  styleUrls: ['./ext-declarations-actions.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ExtDeclarationsActionsComponent implements ViewCell, OnInit {
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
