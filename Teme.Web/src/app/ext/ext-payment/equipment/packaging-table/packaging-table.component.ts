import {Component, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {PackagingTypeDropDownComponent} from '../packaging-type-drop-down/packaging-type-drop-down.component';
import {MeasureDropDownComponent} from '../measure-drop-down/measure-drop-down.component';
import {DropDownRenderComponent} from '../../../../shared/drop-down-local/drop-down-render';
import {PackagingTableService} from './packaging-table-service';

@Component({
  selector: 'app-packaging-table',
  templateUrl: './packaging-table.component.html',
  styleUrls: ['./packaging-table.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers:[PackagingTableService]
})
export class PackagingTableComponent implements OnInit {
  @Input() public boxData;
  @Input() public paymentId;

  boxSettings = {
    selectMode: 'single',  //single|multi
    hideHeader: false,
    hideSubHeader: false,
    actions: {
      columnTitle: 'Actions',
      add: true,
      edit: true,
      delete: true,
      custom: [],
      position: 'right' // left|right
    },
    add: {
      addButtonContent: '<h4 class="mb-1"><i class="fa fa-plus ml-3 text-success"></i></h4>',
      createButtonContent: '<i class="fa fa-check mr-3 text-success"></i>',
      cancelButtonContent: '<i class="fa fa-times text-danger"></i>',
      confirmCreate: true
    },
    edit: {
      editButtonContent: '<i class="fa fa-pencil mr-3 text-primary"></i>',
      saveButtonContent: '<i class="fa fa-check mr-3 text-success"></i>',
      cancelButtonContent: '<i class="fa fa-times text-danger"></i>',
      confirmSave: true
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
        valuePrepareFunction: (value) => {
          return '<div class="text-center">' + value + '</div>';
        }
      },
      packagingType: {
        title: 'Вид',
        type: 'custom',
        editor: {
          type: 'custom',
          component: PackagingTypeDropDownComponent
        }, renderComponent: DropDownRenderComponent
      },
      name: {
        title: 'Наименование',
        type: 'string'
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
        type: 'custom',
        editor: {
          type: 'custom',
          component: MeasureDropDownComponent
        }, renderComponent: DropDownRenderComponent
      },
      numberUnitsInBox: {
        title: 'Кол-во ед. в упаковке',
        type: 'number'
      },
      shortDescription: {
        title: 'Краткое описание',
        type: 'number'
      }
    },
    pager: {
      display: true,
      perPage: 5
    }
  }

  constructor(private packagingTableService: PackagingTableService) { }

  ngOnInit() {
  }

  public addPackagingRecord(event) {
    this.packagingTableService.savePackaging(event.newData, this.paymentId)
      .toPromise()
      .then(res => {
        event.newData.id = res;
        event.confirm.resolve(event.newData);
      })
      .catch(err => {
          console.error(err);
        }
      );
  }

  public updatePackagingRecord(event): void {
    this.packagingTableService.updatePackaging(event.newData, this.paymentId)
      .toPromise()
      .then(res => {
        event.confirm.resolve(event.newData);
      })
      .catch(err => {
          console.error(err);
        }
      );
  }

  public DeletePackagingRecord(event) {
    this.packagingTableService.deletePackaging(event.data, this.paymentId)
      .toPromise()
      .then(res => {
        event.confirm.resolve();
      })
      .catch(err => {
          console.error(err);
        }
      );
  }

}
