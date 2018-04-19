import {Component, forwardRef, Input, ViewChild} from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
  ValidationErrors,
  Validator
} from '@angular/forms';

@Component({
  selector: 'app-ext-journal',
  templateUrl: './ext-journal.component.html',
  styleUrls: ['./ext-journal.component.css'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => ExtJournalComponent),
    multi: true
  }, {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => ExtJournalComponent),
    multi: true
  }]
})
export class ExtJournalComponent implements ControlValueAccessor, Validator {
  @Input() showErrors = false;
  @ViewChild('journalForm') form;
  private _model: any = {};
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

  get model() {
    return this._model;
  }

  set model(v) {
    this._model = v;
    this.change(v);
  }

  private change = _ => {
  };
  private touch = () => {
  };

  registerOnChange(fn: any): void {
    this.change = fn;
  }

  registerOnTouched(fn: any): void {
    this.touch = fn;
  }

  setDisabledState(isDisabled: boolean): void {
  }

  writeValue(obj: any): void {
    this.model = obj;
    this.change(obj);
  }

  registerOnValidatorChange(fn: () => void): void {
  }

  validate(c: AbstractControl): ValidationErrors | null {
    if (this.form.valid) return null;
    return {error: true};
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
