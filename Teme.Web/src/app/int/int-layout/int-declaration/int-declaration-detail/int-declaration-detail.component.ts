import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {Menu} from "../../../../shared/menu/menu.model";

@Component({
  selector: 'app-int-declaration-detail',
  templateUrl: './int-declaration-detail.component.html',
  styleUrls: ['./int-declaration-detail.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class IntDeclarationDetailComponent implements OnInit {
  public menuItems: Array<any>;

  constructor() { }

  ngOnInit() {
    this.menuItems = this.getMenuItems();
  }

  getMenuItems() {
    return [
      new Menu(7312, 'Карточка заявки', '/int/spa/declarations/:id/card', null, 'tachometer', null, false, 0),
      new Menu(4312, 'Вложения', '/int/spa/declarations/:id/attachments', null,'keyboard-o', null, false, 0),
      new Menu(3343, 'История согласования', '/int/spa/declarations/:id/history', null, 'creative-commons', null, false, 0),
    ]
  }

}
