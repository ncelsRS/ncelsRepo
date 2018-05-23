import {Component, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {EquipmentTypeDropDownComponent} from '../equipment-type-drop-down/equipment-type-drop-down.component';
import {DropDownRenderComponent} from '../../../../shared/drop-down-local/drop-down-render';
import {CountryDropDownComponent} from '../country-drop-down/country-drop-down.component';
import {EquipmentImnTableService} from './equipment-imn-table-service';

@Component({
  selector: 'app-equipment-imn-table',
  templateUrl: './equipment-imn-table.component.html',
  styleUrls: ['./equipment-imn-table.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [EquipmentImnTableService]
})
export class EquipmentImnTableComponent implements OnInit {
  public equipmentSettings = {
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
      equipmentType: {
        title: 'Тип',
        type: 'custom',
        editor: {
          type: 'custom',
          component: EquipmentTypeDropDownComponent
        },
        renderComponent: DropDownRenderComponent
      },
      name: {
        title: 'Наименование',
        type: 'string'
      },
      code: {
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
        type: 'custom',
        editor: {
          type: 'custom',
          component: CountryDropDownComponent
        },
        renderComponent: DropDownRenderComponent
      }
    },
    pager: {
      display: true,
      perPage: 5
    }
  };
  @Input() public equipmentData;
  @Input() public paymentId;

  constructor(private equipmentImnTableService: EquipmentImnTableService) { }

  ngOnInit() {
  }

  public DeleteEquipmentRecord(event) {
    this.equipmentImnTableService.deleteEquipment(event.data, this.paymentId)
      .toPromise()
      .then(res => {
        event.confirm.resolve();
      })
      .catch(err => {
          console.error(err);
        }
      );
  }

  public updateEquipmentRecord(event) {
    this.equipmentImnTableService.updateEquipment(event.newData, this.paymentId)
      .toPromise()
      .then(res => {
        event.confirm.resolve(event.newData);
      })
      .catch(err => {
          console.error(err);
        }
      );
  }

  public addEquipmentRecord(event) {
    console.log('addEquipmentRecord = ' + event);
    this.equipmentImnTableService.saveEquipment(event.newData, this.paymentId)
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

}
