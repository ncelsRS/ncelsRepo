import {Component, forwardRef, Input, ViewChild} from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
  ValidationErrors,
  Validator
} from '@angular/forms';
import {RegisterType} from '../RegisterType';
import {TemplateValidation} from "../../../../shared/TemplateValidation";

@Component({
  selector: 'app-ext-general-information',
  templateUrl: './ext-general-information.component.html',
  styleUrls: ['./ext-general-information.component.css'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => ExtGeneralInformationComponent),
    multi: true
  }, {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => ExtGeneralInformationComponent),
    multi: true
  }]
})
export class ExtGeneralInformationComponent extends TemplateValidation  {
  @Input() showErrors = false;
  selectedLevel: string;
  public data = [];
  public date: {year: number, month: number};
  levels: Array<RegisterType> = [
    {code: 'Registration', name: 'Регистрация'},
    {code: 'Reregistration', name: 'Перерегистрация'},
    {code: 'Edit', name: 'Внесение изменений'},

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

  onSubmit() {}

  constructor() {
    super();
    this.selectedLevel = 'Registration';
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

  public onRowSelect(event) {}

  public onUserRowSelect(event) {}

  public onRowHover(event) {}

}
