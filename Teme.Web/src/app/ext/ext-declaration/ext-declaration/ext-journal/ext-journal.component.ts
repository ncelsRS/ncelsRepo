import {Component, OnInit, ViewEncapsulation} from '@angular/core';

@Component({
  selector: 'app-ext-journal',
  templateUrl: './ext-journal.component.html',
  styleUrls: ['./ext-journal.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class ExtJournalComponent implements OnInit {
  public data = [];
  showAgree: boolean;

  public registerSettings = {
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
      additional: {
        title: 'Дополнительно',
        type: 'string',
        filter: true
      },
      materialType: {
        title: 'Тип материала',
        type: 'string'
      },
      productName: {
        title: 'Наименование изделия\\материала\n',
        type: 'string'
      },
      quantity: {
        title: 'Кол-во',
        type: 'string'
      },
      measure: {
        title: 'Ед.изм.' +
        '\n',
        type: 'string'
      },
      series: {
        title: 'Серия' +
        '\n',
        type: 'string'
      },
      issueDate: {
        title: 'Дата изготовления',
        type: 'string'
      },
      term: {
        title: 'Срок годности',
        type: 'string'
      }

    },
    pager: {
      display: true,
      perPage: 10
    }
  };

  constructor() {
    this.showAgree = false;
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

  showAgreement(show: boolean) {
    this.showAgree = show;
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


  ngOnInit() {
  }

}
