import {Component, EventEmitter, Input, OnInit, Output, ViewEncapsulation} from '@angular/core';
import {DefaultEditor, ViewCell} from 'ng2-smart-table';

@Component({
  selector: 'app-measure-drop-down',
  templateUrl: './measure-drop-down.component.html',
  styleUrls: ['./measure-drop-down.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class MeasureDropDownComponent extends DefaultEditor  implements OnInit {
  refName = 'Measure';
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
