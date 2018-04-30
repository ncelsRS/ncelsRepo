import {Component, forwardRef, Input, OnInit, ChangeDetectorRef } from '@angular/core';
import {
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR
} from '@angular/forms';
import {RegisterType} from '../RegisterType';
import {TemplateValidation} from "../../../../shared/TemplateValidation";
import {DeclarationReferenceService} from "../../declaration-reference-service";
import { HttpClient } from '@angular/common/http';
import {DataService} from './data.service'
import { distinctUntilChanged, debounceTime, switchMap, tap } from 'rxjs/operators';
import { Subject } from 'rxjs/Subject';

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
  }, DeclarationReferenceService, DataService ]
})
export class ExtGeneralInformationComponent extends TemplateValidation implements OnInit {
  @Input() showErrors = false;
  selectedLevel: string;
  public data = [];
  public classifierMedicalArea;
  public degreeRiskClass;
  public storageConditions;
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

  public onRowSelect(event) {
  }

  public onUserRowSelect(event) {
  }

  public onRowHover(event) {
  }

  constructor(private referenceService: DeclarationReferenceService, private http: HttpClient, private dataService: DataService, private cd: ChangeDetectorRef) {
    super();
  }

  ngOnInit() {
    let items = this.http.get('http://localhost:5121/api/Reference/NomenclatureCodeMedProduct/');

    this.loadPeople3();

    this.http.get<any[]>('https://jsonplaceholder.typicode.com/photos').subscribe(photos => {
      this.photos = photos;
      this.photosBuffer = this.photos.slice(0, this.bufferSize);
    });

    this.referenceService.getClassifierMedicalArea().then(
      res => { // Success
        this.classifierMedicalArea = res;
        return res;
      }
    ).catch((err) => console.error(err));

    this.referenceService.getDegreeRiskClass().then(
      res => { // Success
        this.degreeRiskClass = res;
        return res;
      }
    ).catch((err) => console.error(err));

    this.referenceService.getStorageCondition().then(
      res => { // Success
        this.storageConditions = res;
        return res;
      }
    ).catch((err) => console.error(err));

  }

  photos = [];
  photosBuffer = [];
  bufferSize = 50;
  loading = false;
  people3Typeahead = new Subject<string>();
  people3Loading = false;
  people3: any[] = [];

  fetchMore() {
    const len = this.photosBuffer.length;
    const more = this.photos.slice(len, this.bufferSize + len);
    this.loading = true;
    // using timeout here to simulate backend API delay
    setTimeout(() => {
      this.loading = false;
      this.photosBuffer = this.photosBuffer.concat(more);
    }, 200)
  }

  fetchScroll($event) {
    let abc = this.dataService.fetchScrollService(this.people3.length);
    this.people3 = this.people3.concat(abc);

    console.log($event);
  }

  onChange($event) {
    console.log($event);
  }

  private loadPeople3() {
    this.people3Typeahead.pipe(
      tap(() => this.people3Loading = true),
      distinctUntilChanged(),
      debounceTime(200),
      switchMap(term => this.dataService.getPeople(term)),
    ).subscribe(x => {
      this.people3 = x;
      this.people3Loading = false;
      this.cd.markForCheck();
    }, () => {
      this.people3 = [];
    });
  }


}
