import {Component, EventEmitter, OnInit, Output, ViewEncapsulation} from '@angular/core';
import {DefaultEditor} from 'ng2-smart-table';

@Component({
  selector: 'app-packaging-type-drop-down',
  templateUrl: './packaging-type-drop-down.component.html',
  styleUrls: ['./packaging-type-drop-down.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class PackagingTypeDropDownComponent extends DefaultEditor  implements OnInit {

  refName = 'PackagingType';
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
