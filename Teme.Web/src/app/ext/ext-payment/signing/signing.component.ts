import { Component, EventEmitter, OnInit, ViewEncapsulation, Output } from '@angular/core';

@Component({
  selector: 'app-signing',
  templateUrl: './signing.component.html',
  styleUrls: ['./signing.component.scss'],
  encapsulation: ViewEncapsulation.None,
  //exportAs: 'ExtPaymentComponent'
})
export class SigningComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  @Output() sendPaymentRequestSign = new EventEmitter<boolean>();
  sendPaymentSign(increased:any) {
    this.sendPaymentRequestSign.emit(increased);
  }

  // sendPaymentRequestChild() {
  //   console.log("123");
  //   this.sendPaymentRequest.emit();
  // }
}
