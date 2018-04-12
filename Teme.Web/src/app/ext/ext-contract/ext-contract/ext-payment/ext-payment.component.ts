import { Component, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-ext-payment',
  templateUrl: './ext-payment.component.html',
  styleUrls: ['./ext-payment.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ExtPaymentComponent implements OnInit {

  constructor() { }


    public menuItems = [
        { id: 1, name: 'Регистрация', unicode: '&#xf2bc'},
        { id: 2, name: 'Перерегистрация', unicode: '&#xf0c9'},
        { id: 3, name: 'Внесение изменения', unicode: '&#xf0a2'}
    ]

  ngOnInit() {
  }

}
