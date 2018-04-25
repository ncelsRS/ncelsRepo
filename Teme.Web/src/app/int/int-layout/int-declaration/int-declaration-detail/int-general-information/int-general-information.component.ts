import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {RegisterType} from "../../../../../ext/ext-declaration/ext-declaration/RegisterType";

@Component({
  selector: 'app-int-general-information',
  templateUrl: './int-general-information.component.html',
  styleUrls: ['./int-general-information.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class IntGeneralInformationComponent implements OnInit {
  selectedLevel: string;
  public data = [];
  public date: { year: number, month: number };
  levels: Array<RegisterType> = [
    {code: 'Registration', name: 'Регистрация', key: 1},
    {code: 'Reregistration', name: 'Перерегистрация', key: 2},
    {code: 'Modification', name: 'Внесение изменений', key: 3},
  ];
  public changeTypeSettings = {
    selectMode: 'single',
    hideHeader: false,
    hideSubHeader: false,
    actions: {
      columnTitle: 'Действия',
      add: true,
      edit: true,
      delete: true,
      custom: [],
      position: 'right' // left|right
    },
    add: {
      addButtonContent: '<h4 class="mb-1"><i class="fa fa-plus ml-3 text-success"></i></h4>',
      createButtonContent: '<i class="fa fa-check mr-3 text-success"></i>',
      cancelButtonContent: '<i class="fa fa-times text-danger"></i>'
    },
    edit: {
      editButtonContent: '<i class="fa fa-pencil mr-3 text-primary"></i>',
      saveButtonContent: '<i class="fa fa-check mr-3 text-success"></i>',
      cancelButtonContent: '<i class="fa fa-times text-danger"></i>'
    },
    delete: {
      deleteButtonContent: '<i class="fa fa-trash-o text-danger"></i>',
      confirmDelete: true
    },
    noDataMessage: 'Нет данных',
    columns: {
      id: {
        title: '№\n' +
        '\n' +
        'п\\п',
        editable: false,
        width: '60px',
        type: 'html',
        valuePrepareFunction: (value) => {
          return '<div class="text-center">' + value + '</div>';
        }
      },
      changes: {
        title: 'Изменение',
        type: 'string',
        filter: true
      },
      type: {
        title: 'Тип',
        type: 'string'
      },
      reduction: {
        title: 'Редакция до внесения изменений',
        type: 'string'
      },
      incomeChanges: {
        title: 'Вносимые изменения',
        type: 'string'
      }
    },
    pager: {
      display: true,
      perPage: 10
    }
  };
  public termSettings = {
    selectMode: 'single',
    hideHeader: false,
    hideSubHeader: false,
    actions: {
      columnTitle: 'Действия',
      add: true,
      edit: true,
      delete: true,
      custom: [],
      position: 'right' // left|right
    },
    add: {
      addButtonContent: '<h4 class="mb-1"><i class="fa fa-plus ml-3 text-success"></i></h4>',
      createButtonContent: '<i class="fa fa-check mr-3 text-success"></i>',
      cancelButtonContent: '<i class="fa fa-times text-danger"></i>'
    },
    edit: {
      editButtonContent: '<i class="fa fa-pencil mr-3 text-primary"></i>',
      saveButtonContent: '<i class="fa fa-check mr-3 text-success"></i>',
      cancelButtonContent: '<i class="fa fa-times text-danger"></i>'
    },
    delete: {
      deleteButtonContent: '<i class="fa fa-trash-o text-danger"></i>',
      confirmDelete: true
    },
    noDataMessage: 'Нет данных',
    columns: {
      view: {
        title: 'Вид',
        type: 'string',
        filter: true
      },
      term: {
        title: 'Срок хранения\\Гарантийный срок эксплуатации',
        type: 'string'
      },
      measure: {
        title: 'Ед.изм',
        type: 'string'
      },
      eternal: {
        title: 'Бессрочно',
        type: 'string'
      }
    },
    pager: {
      display: true,
      perPage: 10
    }
  };
  public registerSettings = {
    selectMode: 'single',
    hideHeader: false,
    hideSubHeader: false,
    actions: {
      columnTitle: 'Действия',
      add: true,
      edit: true,
      delete: true,
      custom: [],
      position: 'right' // left|right
    },
    add: {
      addButtonContent: '<h4 class="mb-1"><i class="fa fa-plus ml-3 text-success"></i></h4>',
      createButtonContent: '<i class="fa fa-check mr-3 text-success"></i>',
      cancelButtonContent: '<i class="fa fa-times text-danger"></i>'
    },
    edit: {
      editButtonContent: '<i class="fa fa-pencil mr-3 text-primary"></i>',
      saveButtonContent: '<i class="fa fa-check mr-3 text-success"></i>',
      cancelButtonContent: '<i class="fa fa-times text-danger"></i>'
    },
    delete: {
      deleteButtonContent: '<i class="fa fa-trash-o text-danger"></i>',
      confirmDelete: true
    },
    noDataMessage: 'Нет данных',
    columns: {
      country: {
        title: 'Страна',
        type: 'string',
        filter: true
      },
      registrationNumber: {
        title: '№ регистрационного удостоверения (указывается при наличии)',
        type: 'string'
      },
      issueDate: {
        title: 'Дата выдачи',
        type: 'string'
      },
      term: {
        title: 'Срок действия',
        type: 'string'
      },
      eternal: {
        title: 'Бессрочно',
        type: 'string'
      }
    },
    pager: {
      display: true,
      perPage: 10
    }
  };

  changeLevel(lev: RegisterType) {
    this.selectedLevel = lev.name;
  }

  constructor() { }

  ngOnInit() {
  }

  public onRowSelect(event) {
  }

  public onUserRowSelect(event) {
  }

  public onRowHover(event) {
  }

}
