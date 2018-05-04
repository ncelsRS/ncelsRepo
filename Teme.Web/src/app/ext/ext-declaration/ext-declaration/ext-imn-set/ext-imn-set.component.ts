import {Component, OnInit} from '@angular/core';
import {PackageUploadBtnComponent} from "./package-upload-btn/package-upload-btn.component";
import {IntDeclarationBtnComponent} from "../../../../int/int-layout/int-declaration/int-declaration-btn/int-declaration-btn.component";

@Component({
  selector: 'app-ext-imn-set',
  templateUrl: './ext-imn-set.component.html',
  styleUrls: ['./ext-imn-set.component.css']
})
export class ExtImnSetComponent implements OnInit {
  public imnTable: boolean
  public data = [];
  public complectationImnSettings = {
    selectMode: 'single',  //single|multi
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
      type: {
        title: 'Тип',
        type: 'string',
        filter: true
      },
      name: {
        title: 'Наименование',
        type: 'string'
      },
      ID: {
        title: 'ID',
        type: 'number'
      },
      model: {
        title: 'Модель',
        type: 'string'
      },
      user: {
        title: 'Производитель',
        type: 'string'
      },
      country: {
        title: 'Страна',
        type: 'string'
      }
    },
    pager: {
      display: true,
      perPage: 10
    }
  };
  public packagingSettings = {
    selectMode: 'single',  //single|multi
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
      type: {
        title: 'Вид (первичная или вторичная)',
        type: 'html',
        editor: {
          type: 'list',
          config: {
            list: [{value: 'Первичный', title: 'Первичный'}, {value: 'Вторичный', title: 'Вторичный'}, {
              value: '<b>Промежуточный</b>',
              title: 'Промежуточный'
            }]
          }
        }
      },
      name: {
        title: 'Наименование',
        type: 'string'
      },
      ID: {
        title: 'Значение',
        type: 'string'
      },
      model: {
        title: 'Ед.изм.',
        type: 'html',
        editor: {
          type: 'list',
          config: {
            list: [{value: 'грамм', title: 'грамм'}, {value: 'литр', title: 'литр'}, {
              value: '<b>тонна</b>',
              title: 'тонна'
            }]
          }
        }
      },
      user: {
        title: 'Кол-во ед. в упаковке\n',
        type: 'string'
      },
      country: {
        title: 'Краткое описание',
        type: 'string'
      },
      choose: {
        title: 'Файл',
        type: 'custom',
        filter: false,
        renderComponent: PackageUploadBtnComponent
      }
    },
    pager: {
      display: true,
      perPage: 10
    }
  };

  //PackageUploadBtnComponent
  constructor() {
    this.imnTable = true;
  }

  ngOnInit() {
  }

  public showImnTable(show: boolean) {
    this.imnTable = show;
  }

  public getData(data) {
    const req = new XMLHttpRequest();
    req.open('GET', 'assets/data/users.json');
    req.onload = () => {
      data(JSON.parse(req.response));
    };
    req.send();
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
