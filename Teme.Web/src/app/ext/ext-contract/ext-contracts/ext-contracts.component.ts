import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import {RefService} from '../ext-contract/ext-ref-sevice';
import {ExtDeclarationsActionsComponent} from '../../ext-declaration/ext-declarations/ext-declarations-actions/ext-declarations-actions.component';
import {ExtManufacturActionComponent} from '../ext-contract/ext-manufactur-action/ext-manufactur-action.component';

@Component({
    selector: 'app-ext-contracts',
    templateUrl: './ext-contracts.component.html',
    styleUrls: ['./ext-contracts.component.css'],
    providers: [RefService]
})
export class ExtContractsComponent implements OnInit {

  public createContractId;
  public idContract;
  @Output() idContractEvent = new EventEmitter<string>();

  public contractsSettings = {
    selectMode: 'single',
    hideHeader: false,
    hideSubHeader: false,
    noDataMessage: 'Нет данных',
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
    prop: {name: 'view', filter: false},
    columns: {
      view: {
        title: '№ Договора',
        type: 'string'
      },
      declarationType: {
        title: 'Дата',
        type: 'string'
      },
      name: {
        title: 'Статус',
        type: 'string'
      },
      number: {
        title: 'Производитель',
        type: 'string'
      },
      currentStatus: {
        title: 'Наименования ИМН/МТ',
        type: 'string'
      },
      sendDate: {
        title: 'Тип',
        type: 'string'
      },
      Action: {
        title: 'Действия',
        type: 'custom',
        renderComponent: ExtManufacturActionComponent,
        onComponentInitFunction(instance) {
          instance.save.subscribe(row => {
            alert(`${row.name} saved!`)
          });
        }
      },
    },
    pager: {
      display: true,
      perPage: 10
    }
  };

  constructor(private refService: RefService) {
  }

  ngOnInit() {
  }

  createContract(contractType, contractScope) {
    this.refService.createContract(contractType, contractScope)
       .toPromise()
        .then(response => {
          console.log(response);
          this.createContractId = response;
          // this.idContract = this.creataContractId.id;
          // console.log(this.idContract)
          this.idContractEvent.emit(this.createContractId);
        })
        .catch(err => {
            console.error(err);
          }
        )

    }
}

