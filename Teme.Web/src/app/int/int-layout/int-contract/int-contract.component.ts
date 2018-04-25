import {Component, OnInit, ViewEncapsulation} from '@angular/core';
import {IntContractBtnComponent} from "./int-contract-btn/int-contract-btn.component";

@Component({
  selector: 'app-int-contract',
  templateUrl: './int-contract.component.html',
  styleUrls: ['./int-contract.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class IntContractComponent implements OnInit {
  public data = [{
    number: '1321 #1223',
    contractAddition: 'sdfg',
    contractType: 'asd',
    type: 'sdac', serviceType: 'sdac', sendDate: 'sdac', legal: 'sdac',
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
        renderComponent: IntContractBtnComponent,
        onComponentInitFunction(instance) {
          instance.save.subscribe(row => {
            alert(`${row.name} saved!`)
          });
        }
      },
      contractAddition: {
        title: 'Договор или доп соглашение',
        type: 'string'
      },
      contractType: {
        title: 'Тип договора(Тип соглашения)',
        type: 'string'
      },
      type: {
        title: 'Тип',
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
      legal: {
        title: 'Согласование юридической части',
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
        title: 'Наиме-\nнование',
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

}
