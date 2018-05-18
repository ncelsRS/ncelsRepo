import {Component, EventEmitter, Input, OnInit, Output, ViewEncapsulation} from '@angular/core';

@Component({
  selector: 'app-int-payment-btn',
  templateUrl: './int-payment-btn.component.html',
  styleUrls: ['./int-payment-btn.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class IntPaymentBtnComponent implements OnInit {
  renderValue: string;

  @Input() value: string | number;
  @Input() rowData: any;

  @Output() save: EventEmitter<any> = new EventEmitter();

  constructor() { }

  ngOnInit() {
    this.renderValue = this.value.toString().toUpperCase();
  }

  onClick() {
    window.open("/int/spa/payments/" + this.value)
  }


}
