import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {Menu} from 'app/shared/menu/menu.model';
import {IntPaymentBtnComponent} from './int-payment-btn/int-payment-btn.component';

@Component({
  selector: 'app-int-payment',
  templateUrl: './int-payment.component.html',
  styleUrls: ['./int-payment.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class IntPaymentComponent implements OnInit {
  public menuItems: Array<any>;
  public data = [{
    number: '1321',
    paymentAddition: 'sdfg',
    contractType: 'asd',
    typeServices: 'sdac', sendDate: 'sdac',
    GV: 'sdac', DEF: 'sdac', declarant: 'sdac', productType: 'sdac',
    name: 'sdac', startDate: 'sdac', endDate: 'sdac'
  }];
  public table1Settings = {
    selectMode: 'single',  //single|multi
    hideHeader: false,
    hideSubHeader: false,
    actions: false,
    noDataMessage: 'Нет данных',
    columns: {
      number: {
        title: 'Номер',
        type: 'custom',
        renderComponent: IntPaymentBtnComponent,
        onComponentInitFunction(instance) {
          instance.save.subscribe(row => {
            alert(`${row.name} saved!`)
          });
        }
      },
      paymentAddition: {
        title: 'Заявка на платеж',
        type: 'string'
      },
      contractType: {
        title: 'Тип договора (Тип доп соглаш)',
        type: 'string'
      },
      typeServices: {
        title: 'Тип услуги',
        type: 'string'
      },
      sendDate: {
        title: 'Дата отправки',
        type: 'string'
      },
      GV: {
        title: 'Согласование стоимости(ГВ МРД МИ)',
        type: 'string'
      },
      DEF: {
        title: 'Согласование стоимости(ДЭФ)',
        type: 'string'
      },
      declarant: {
        title: 'Заявитель',
        type: 'string'
      },
      productType: {
        title: 'Вид продукции',
        type: 'string'
      },
      name: {
        title: 'Наименование продукции',
        type: 'string'
      },
      startDate: {
        title: 'Дата начала',
        type: 'string'
      },
      endDate: {
        title: 'Дата окончания',
        type: 'string'
      }
    },
    pager: {
      display: true,
      perPage: 10
    }
  };

  constructor() {
  }

  ngOnInit() {
    this.menuItems = this.getMenuItems();
  }

  public onDeleteConfirm(event): void {
    if (window.confirm('Are you sure you want to delete?')) {
      event.confirm.resolve();
    } else {
      event.confirm.reject();
    }
  }

  public onRowSelect(event) {
    // console.log(event);
  }

  public onUserRowSelect(event) {
    //console.log(event);   //this select return only one page rows
  }

  public onRowHover(event) {
    //console.log(event);
  }

  getMenuItems() {
    return [
      new Menu (1, 'Не распределенные', '', null, 'tachometer', null, false, 0),
      new Menu (2, 'В работе', '', null, 'users', null, false, 0),
      new Menu (3, 'На карректировке', null, null, 'laptop', null, true, 0),
      new Menu (4, 'Требует согдасования', '', null, 'keyboard-o', null, false, 0),
      new Menu (5, 'Согласованные', '', null, 'address-card-o', null, false, 0),
      new Menu (6, 'Не согласованные', '', null, 'creative-commons', null, false, 0),
      new Menu (7, 'На формирование счета на оплату', '', null, 'th-list', null, false, 0),
      new Menu (8, 'Активные', '', null, 'font-awesome', null, false, 0),
      new Menu (9, 'Все', '', null, 'object-group', null, false, 0),
    ]
  }

}
