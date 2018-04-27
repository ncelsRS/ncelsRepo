import {Component, EventEmitter, Input, OnInit, Output, ViewEncapsulation} from '@angular/core';

@Component({
  selector: 'app-int-declaration-btn',
  templateUrl: './int-declaration-btn.component.html',
  styleUrls: ['./int-declaration-btn.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class IntDeclarationBtnComponent implements OnInit {
  renderValue: string;

  @Input() value: string | number;
  @Input() rowData: any;

  @Output() save: EventEmitter<any> = new EventEmitter();

  constructor() { }

  ngOnInit() {
    this.renderValue = this.value.toString().toUpperCase();
  }

  onClick() {
    window.open("/int/spa/declarations/1")
  }

}
