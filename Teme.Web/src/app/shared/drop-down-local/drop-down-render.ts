import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ViewCell} from 'ng2-smart-table';

@Component({
  selector: 'drop-down-render',
  template: `{{ renderValue }}`,
})
export class DropDownRenderComponent implements ViewCell, OnInit {
  renderValue: string;

  @Input() value;
  @Input() rowData: any;

  @Output() edit: EventEmitter<any> = new EventEmitter();

  ngOnInit() {
    this.renderValue = this.value.nameRu
  }

  onClick() {
    this.edit.emit(this.rowData);
  }

}
