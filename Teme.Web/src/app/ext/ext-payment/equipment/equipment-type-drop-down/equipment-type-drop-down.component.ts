import {Component, EventEmitter, Input, OnInit, Output, ViewEncapsulation} from '@angular/core';
import {DefaultEditor, ViewCell} from 'ng2-smart-table';

@Component({
  selector: 'app-equipment-type-drop-down',
  templateUrl: './equipment-type-drop-down.component.html',
  styleUrls: ['./equipment-type-drop-down.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class EquipmentTypeDropDownComponent extends DefaultEditor  implements OnInit  {

  refName = 'EquipmentType';
  @Output() setSelected: EventEmitter<any> = new EventEmitter();
  public selectedId;

  constructor() {
    super();
  }

  ngOnInit() {
    this.selectedId = this.cell.getValue().id;
    this.setSelected.emit(this.selectedId);
  }

  updateItem($event){
    this.cell.newValue = $event;
  }
}
