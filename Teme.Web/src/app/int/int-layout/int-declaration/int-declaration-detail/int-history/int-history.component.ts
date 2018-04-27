import { Component, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-int-history',
  templateUrl: './int-history.component.html',
  styleUrls: ['./int-history.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class IntHistoryComponent implements OnInit {
  public data = [{
    status: '1321',
    executor: 'Испониель',
    structure: 'asd',
    actionDate: '12.11.2018'
  }];
  public historyTableSettings = {
    selectMode: 'single',  //single|multi
    hideHeader: false,
    hideSubHeader: false,
    actions: false,
    noDataMessage: 'Нет данных',
    columns: {
      status: {
        title: 'Статус',
        type: 'string'
      },
      executor: {
        title: 'Исполнитель',
        type: 'string'
      },
      structure: {
        title: 'Структурное подразделение',
        type: 'string'
      },
      actionDate: {
        title: 'Дата действия',
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
  }

}
