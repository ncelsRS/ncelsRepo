import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import {RefService} from '../ext-contract/ext-ref-sevice';
import {Router} from "@angular/router";

import {ExtManufacturActionComponent} from '../ext-contract/ext-manufactur-action/ext-manufactur-action.component';

@Component({
    selector: 'app-ext-contracts',
    templateUrl: './ext-contracts.component.html',
    styleUrls: ['./ext-contracts.component.css'],
    providers: [RefService]
})
export class ExtContractsComponent implements OnInit {

  public createContractId;
  public contractList=[];

  public contractsSettings = {
    selectMode: 'single',
    hideHeader: false,
    hideSubHeader: false,
    noDataMessage: 'Нет данных',
    actions: false,
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
      id: {
        title: '№ Договора',
        type: 'string'
      },
      number: {
        title: '№ Договора',
        type: 'string'
      },
      dateCreate: {
        title: 'Дата',
        type: 'string'
      },
      contractForm: {
        title: 'Статус',
        type: 'string'
      },
      nameRu: {
        title: 'Производитель',
        type: 'string'
      },
      medicalDeviceNameRu: {
        title: 'Наименования ИМН/МТ',
        type: 'string'
      },
      contractScope: {
        title: 'Тип',
        type: 'string'
      },
      Action: {
        title: 'Действия',
        type: 'custom',
        renderComponent: ExtManufacturActionComponent,
        onComponentInitFunction(instance) {
          instance.view.subscribe(row => {
           // this.router.navigate(['ext/contracts/6','view',row.id,1]);

          });
          instance.edit.subscribe(row => {
            //this.router.navigate(['ext/contracts/6','edit',row.id,1]);

          });
          instance.dlte.subscribe(row => {

          });
        }
      },
    },
    pager: {
      display: true,
      perPage: 10
    }
  };

  constructor(private refService: RefService,
              private router: Router) {
    this.GetListContracts('eaesrg');
  }

  ngOnInit() {
  }

  createContract(contractType, contractScope) {
    this.refService.createContract(contractType, contractScope)
       .toPromise()
        .then(response => {
          console.log(response);
          this.createContractId = response;
          console.log(this.createContractId);
          this.router.navigate(['ext/contracts/6','create',this.createContractId.id,this.createContractId.workflowId, contractType]);
        })
        .catch(err => {
            console.error(err);
          }
        )

    }

  onCreate(contractType) {

    this.createContract(contractType,'eaesrg');

  }

  GetListContracts(contractScpe)
  {
    this.refService.GetListContracts(contractScpe)
      .toPromise()
      .then(response => {
        console.log(response);
        this.contractList.push(response);
        console.log(this.contractList);
      })
      .catch(err => {
          console.error(err);
        }
      )

  }




}

