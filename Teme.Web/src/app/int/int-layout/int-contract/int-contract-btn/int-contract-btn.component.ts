import {Component, EventEmitter, Input, OnInit, Output, ViewEncapsulation} from '@angular/core';

@Component({
  selector: 'app-int-contract-btn',
  templateUrl: './int-contract-btn.component.html',
  styleUrls: ['./int-contract-btn.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class IntContractBtnComponent implements OnInit {
  renderValue: string;

  @Input() value: string | number;
  @Input() rowData: any;

  @Output() save: EventEmitter<any> = new EventEmitter();


  constructor() { }

  onClick() {
    window.open("/int/spa/contracts/1")
  }

  ngOnInit() {
    this.renderValue = this.value.toString().toUpperCase();
  }

}
