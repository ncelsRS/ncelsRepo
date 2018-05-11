import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {ExtPaymentService} from '../../../ext-payment/ext-payment.service';

@Component({
  selector: 'app-ext-payment-tab',
  templateUrl: './ext-payment-tab.component.html',
  styleUrls: ['./ext-payment-tab.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [ExtPaymentService]
})
export class ExtPaymentTabComponent implements OnInit {
  paymentId: number = null;
  done: boolean = false;
  constructor(private paymentService: ExtPaymentService) { }


    // public menuItems = [
    //     { id: 1, name: 'Регистрация', unicode: '&#xf2bc'},
    //     { id: 2, name: 'Перерегистрация', unicode: '&#xf0c9'},
    //     { id: 3, name: 'Внесение изменения', unicode: '&#xf0a2'}
    // ]

  ngOnInit() {
  }
  closeModal(){

  }
  showPaymentModal(){
    //jQuery('#add-modal').modal('hide');
    jQuery('#frameModal').modal('show');
  }

  test(){
    this.paymentService.createPayment(3)
      .subscribe(
        (data: number) => {this.paymentId=data; this.done=true;},
        error => console.log(error)
      );
    console.log(this.paymentId,this.done);
  }
}
