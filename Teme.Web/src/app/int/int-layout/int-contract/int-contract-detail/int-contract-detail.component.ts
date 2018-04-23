import {Component, OnInit, ViewEncapsulation} from '@angular/core';
import {Menu} from "../../../../ext/ext-payment/components/menu/menu.model";
import {RegisterType} from "../../../../ext/ext-contract/ext-contract/RegisterType";

@Component({
  selector: 'app-int-contract-detail',
  templateUrl: './int-contract-detail.component.html',
  styleUrls: ['./int-contract-detail.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class IntContractDetailComponent implements OnInit {
  public menuItems: Array<any>;

  constructor() {
  }

  ngOnInit() {
    this.menuItems = this.getMenuItems();
  }

  getMenuItems() {
    return [
      new Menu(112312, 'Карточка договора', '/int/spa/contracts/:id/card', null, 'tachometer', null, false, 0),
      new Menu(212312, 'Вложения', '/int/spa/contracts/:id/attachments', null,'keyboard-o', null, false, 0),
      new Menu(334343, 'История согласования', '/int/spa/contracts/:id/history', null, 'creative-commons', null, false, 0),
    ]
  }


}
