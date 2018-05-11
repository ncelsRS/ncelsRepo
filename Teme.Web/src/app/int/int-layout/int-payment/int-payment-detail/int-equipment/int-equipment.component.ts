import {Component, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {IconExtModal} from 'app/shared/icon/icon-ext-modal';

@Component({
  selector: 'app-int-equipment',
  templateUrl: './int-equipment.component.html',
  styleUrls: ['./int-equipment.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers:[IconExtModal]
})
export class IntEquipmentComponent {

  @Input() showErrors = false;
  public equipmentData = [{
    rowNumber:1,
    type: 'Тип',
    name: 'Наименование',
    idCode: '12344',
    model: 'Модель',
    manufacturer: 'Производитель',
    country: 'Страна',
  }];
  public equipmentSettings = {
    selectMode: 'single',  //single|multi
    hideHeader: false,
    hideSubHeader: false,
    actions: {
      columnTitle: 'Actions',
      add: false,
      edit: false,
      delete: false,
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
    noDataMessage: 'No data found',
    columns: {
      rowNumber: {
        title: '№',
        editable: false,
        width: '60px',
        type: 'html',
        valuePrepareFunction: (value) => { return '<div class="text-center">' + value + '</div>'; }
      },
      type: {
        title: 'Тип',
        type: 'string'
      },
      name: {
        title: 'Наименование',
        type: 'html',
        editor: {
          type: 'list',
          config: {
            list: [{ value: 'Antonette', title: 'Antonette' }, { value: 'Bret', title: 'Bret' }, {
              value: '<b>Samantha</b>',
              title: 'Samantha'
            }]
          }
        }
      },
      idCode: {
        title: 'ID',
        type: 'string'
      },
      model: {
        title: 'Модель',
        type: 'string'
      },
      manufacturer: {
        title: 'Производитель',
        type: 'number'
      },
      country: {
        title: 'Страна',
        type: 'number'
      }
    },
    pager: {
      display: true,
      perPage: 5
    }
  };

  public boxData = [{
    rowNumber:1,
    type: 'Вид',
    name: 'Наименование',
    sizeWidth: 10,
    sizeHeight: 5,
    sizeLength: 35,
    sizeMeasure: 80,
    numberUnitsInBox:300,
    shortDescription: 'Краткое описание'
  }];
  public boxSettings = {
    selectMode: 'single',  //single|multi
    hideHeader: false,
    hideSubHeader: false,
    actions: {
      columnTitle: 'Actions',
      add: false,
      edit: false,
      delete: false,
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
    noDataMessage: 'No data found',
    columns: {
      rowNumber: {
        title: '№',
        editable: false,
        width: '60px',
        type: 'html',
        valuePrepareFunction: (value) => { return '<div class="text-center">' + value + '</div>'; }
      },
      type: {
        title: 'Вид',
        type: 'string'
        //filter: true
      },
      name: {
        title: 'Наименование',
        type: 'html',
        editor: {
          type: 'list',
          config: {
            list: [{ value: 'Antonette', title: 'Antonette' }, { value: 'Bret', title: 'Bret' }, {
              value: '<b>Samantha</b>',
              title: 'Samantha'
            }]
          }
        }
      },
      sizeWidth: {
        title: 'Размер Ширина',
        type: 'string'
      },
      sizeHeight: {
        title: 'Размер Высота',
        type: 'string'
      },
      sizeLength: {
        title: 'Размер Длина',
        type: 'number'
      },
      sizeMeasure: {
        title: 'Еденица измерения',
        type: 'number'
      },
      numberUnitsInBox: {
        title: 'Кол-во ед. в упаковке',
        type: 'number'
      }
      ,
      shortDescription: {
        title: 'Краткое описание',
        type: 'number'
      }
    },
    pager: {
      display: true,
      perPage: 5
    }
  };

  constructor(public iconModal:  IconExtModal) {
    // this.getData((data) => {
    //   this.data = data;
    // });
  }

  // public getData(data) {
  //   const req = new XMLHttpRequest();
  //   req.open('GET', 'assets/data/users.json');
  //   req.onload = () => {
  //     data(JSON.parse(req.response));
  //   };
  //   req.send();
  // }

  public onDeleteConfirm(event): void {
    if (window.confirm('Are you sure you want to delete?')) {
      event.confirm.resolve();
    } else {
      event.confirm.reject();
    }
  }

  public onRowSelect(event){
    // console.log(event);
  }

  public onUserRowSelect(event){
    //console.log(event);   //this select return only one page rows
  }

  public onRowHover(event){
    //console.log(event);
  }


}
