import { Component, OnInit } from '@angular/core';
import {ExtDeclarationsActionsComponent} from '../../ext-declaration/ext-declarations/ext-declarations-actions/ext-declarations-actions.component';

@Component({
    selector: 'app-ext-contracts',
    templateUrl: './ext-contracts.component.html',
    styleUrls: ['./ext-contracts.component.css']
})
export class ExtContractsComponent implements OnInit {
  public contractsSettings = {
    selectMode: 'single',
    hideHeader: false,
    hideSubHeader: false,
    noDataMessage: 'Нет данных',
    actions: false,
    prop: {name: 'view', filter: false},
    columns: {
      view: {
        title: '№ Договора',
        type: 'string'
      },
      declarationType: {
        title: 'Дата',
        type: 'string'
      },
      name: {
        title: 'Статус',
        type: 'string'
      },
      number: {
        title: 'Производитель',
        type: 'string'
      },
      currentStatus: {
        title: 'Наименования ИМН/МТ',
        type: 'string'
      },
      sendDate: {
        title: 'Тип',
        type: 'string'
      },
      Action: {
        title: 'Действия',
        type: 'custom',
        renderComponent: ExtDeclarationsActionsComponent,
        onComponentInitFunction(instance) {
          instance.save.subscribe(row => {
            alert(`${row.name} saved!`)
          });
        }
      },
    },
    pager: {
      display: true,
      perPage: 10
    }
  };
    constructor() { }

    ngOnInit() {
    }

}
