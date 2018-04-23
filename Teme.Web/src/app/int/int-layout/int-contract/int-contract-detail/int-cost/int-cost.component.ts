import { Component, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-int-cost',
  templateUrl: './int-cost.component.html',
  styleUrls: ['./int-cost.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class IntCostComponent implements OnInit {
  public costsSettings = {
    selectMode: 'single',
    hideHeader: false,
    hideSubHeader: false,
    noDataMessage: 'Нет данных',
    actions: false,
    prop: {name: 'view', filter: false},
    columns: {
      view: {
        title: '№',
        type: 'string'
      },
      declarationType: {
        title: 'Наименование работ по Прейскуранту',
        type: 'string'
      },
      name: {
        title: 'Цена в тенге, без НДС',
        type: 'string'
      },
      number: {
        title: 'Количество',
        type: 'string'
      },
      currentStatus: {
        title: 'Всего',
        type: 'string'
      },
      // sendDate: {
      //   title: 'Тип',
      //   type: 'string'
      // },
      // Action: {
      //   title: 'Действия',
      //   type: 'custom',
      //   renderComponent: ExtDeclarationsActionsComponent,
      //   onComponentInitFunction(instance) {
      //     instance.save.subscribe(row => {
      //       alert(`${row.name} saved!`)
      //     });
      //   }
      // },
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
