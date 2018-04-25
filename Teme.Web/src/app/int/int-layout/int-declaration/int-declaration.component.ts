import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {Menu} from "../../../ext/ext-payment/components/menu/menu.model";
import {IntDeclarationBtnComponent} from "./int-declaration-btn/int-declaration-btn.component";

@Component({
  selector: 'app-int-declaration',
  templateUrl: './int-declaration.component.html',
  styleUrls: ['./int-declaration.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class IntDeclarationComponent implements OnInit {
  public menuItems: Array<any>;
  public data = [{
    number: '1321 #1223', paymentApplication: 'sdfg',
    contractType: 'asd', serviceType: 'sdac', sendDate: 'sdac',
    GV: 'sdac', DEF: 'sdac', declarant: 'sdac', productType: 'sdac',
    name: 'sdac', startDate: 'sdac', endDate: 'sdac'
  }];
  public declarationsTableSettings = {
    selectMode: 'single',  //single|multi
    hideHeader: false,
    hideSubHeader: false,
    actions: false,
    noDataMessage: 'Нет данных',
    columns: {
      number: {
        title: 'Номер',
        type: 'custom',
        renderComponent: IntDeclarationBtnComponent,
        onComponentInitFunction(instance) {
          instance.save.subscribe(row => {
            alert(`${row.name} saved!`)
          });
        }
      },
      paymentApplication: {
        title: 'Заявка на платеж',
        type: 'string'
      },
      contractType: {
        title: 'Тип договора(Тип соглашения)',
        type: 'string'
      },
      serviceType: {
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
        title: 'Наиме-\nнование\nпродукции',
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

  constructor() { }

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

  getMenuItems() {
    return [
      new Menu(712312, 'Не распределенные', null, null, 'tachometer', null, false, 0),
      new Menu(412312, 'В работе', null, null,'keyboard-o', null, false, 0),
      new Menu(634343, 'Требуют согласования', null, null, 'creative-commons', null, false, 0),

      new Menu(7123, 'На корректировке', null, null, 'tachometer', null, false, 0),
      new Menu(4123, 'Согласованные', null, null,'keyboard-o', null, false, 0),
      new Menu(6343, 'Не согласованные', null, null, 'creative-commons', null, false, 0),

      new Menu(7312, 'На формировании счета на оплату', null, null, 'tachometer', null, false, 0),
      new Menu(4112, 'Активные', null, null,'keyboard-o', null, false, 0),
      new Menu(6343, 'Все', null, null, 'creative-commons', null, false, 0),

    ]
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

}
