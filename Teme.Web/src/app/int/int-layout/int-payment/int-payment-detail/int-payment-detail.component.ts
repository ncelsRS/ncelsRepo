import {Component, OnInit, ViewEncapsulation} from '@angular/core';
import {Menu} from "app/theme/components/menu/menu.model";

@Component({
  selector: 'app-int-payment-detail',
  templateUrl: './int-payment-detail.component.html',
  styleUrls: ['./int-payment-detail.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class IntPaymentDetailComponent implements OnInit {

  public menuItems: Array<any>;

  constructor() {
  }

  ngOnInit() {
    this.menuItems = this.getMenuItems();
  }

  getMenuItems() {
    return [
      new Menu(112312, 'Карточка заявки на платеж', '/int/spa/payments/:id/card', null, 'tachometer', null, false, 0),
      new Menu(212312, 'Вложения', '/int/spa/payments/:id/attachments', null,'keyboard-o', null, false, 0),
      new Menu(334343, 'История согласования', '/int/spa/payments/:id/history', null, 'creative-commons', null, false, 0),
    ]
  }
}
