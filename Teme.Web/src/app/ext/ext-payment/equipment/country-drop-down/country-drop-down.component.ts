import {Component, EventEmitter, Input, OnInit, Output, ViewEncapsulation} from '@angular/core';
import {DefaultEditor, ViewCell} from 'ng2-smart-table';

@Component({
  selector: 'app-country-drop-down',
  templateUrl: './country-drop-down.component.html',
  styleUrls: ['./country-drop-down.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class CountryDropDownComponent extends DefaultEditor implements OnInit {

  refName = 'Country';
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
