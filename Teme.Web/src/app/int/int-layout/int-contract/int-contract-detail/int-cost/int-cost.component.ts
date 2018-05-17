import {Component, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {RefIntContractService} from '../../int-contract-service';
import {LocalDataSource} from 'ng2-smart-table';

@Component({
  selector: 'app-int-cost',
  templateUrl: './int-cost.component.html',
  styleUrls: ['./int-cost.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class IntCostComponent implements OnInit {
  @Input() idContractChild:string;
  @Input() idCostIn:string;
  public listVarRes;
  public data;


  model:any= {
    id:null,
    applicationType: null,
    serviceType: null,
    NameIMNRu: null,
    NameIMNKz: null,
    serciveCount:null,
    isImport:null,
  };

  public costsSettings = {
    selectMode: 'single',
    hideHeader: false,
    hideSubHeader: false,
    noDataMessage: 'Нет данных',
    actions: false,
    prop: {name: 'view', filter: false},
    columns: {
      Cnt: {
        title: '№',
        type: 'string',
        filter: false
      },
      workName: {
        title: 'Наименование работ по Прейскуранту',
        type: 'string',
        filter: false
      },
      priceWONSD: {
        title: 'Цена в тенге, без НДС',
        type: 'string',
        filter: false
      },
      countWork: {
        title: 'Количество',
        type: 'string',
        filter: false
      },
      allPrice: {
        title: 'Всего',
        type: 'string',
        filter: false
      },

    },
    pager: {
      display: true,
      perPage: 10
    }
  };

  constructor(private refIntService: RefIntContractService) { }

  ngOnInit() {
  }

  onViewChangeContract()
  {
    this.refIntService.GetContractById(this.idContractChild)
      .toPromise()
      .then(response => {
        console.log(response);
        this.listVarRes = response;
        this.viewData();

      })
      .catch(err => {
          console.error(err);
        }
      )
  }

  viewData() {
    let countCost: number = 0;
    let allSumm: number = 0;
    this.data = new LocalDataSource();
    this.model.NameIMNRu = this.listVarRes.medicalDeviceNameRu;
    this.model.NameIMNKz = this.listVarRes.medicalDeviceNameKz;
    this.model.isImport = (this.listVarRes.costWorkDto[0].isImport)?"1":"0";
    if (this.listVarRes.costWorkDto[0].nameRu != 'undefined') {
      this.data.add({
        Cnt: 1,
        workName: this.listVarRes.costWorkDto[0].nameRu,
        priceWONSD: this.listVarRes.costWorkDto[0].priceWithValueAddedTax,
        countWork: this.listVarRes.costWorkDto[0].count,
        allPrice: this.listVarRes.costWorkDto[0].totalPriceWithValueAddedTax
      });
      countCost = this.listVarRes.costWorkDto[0].count;
      allSumm = this.listVarRes.costWorkDto[0].totalPriceWithValueAddedTax;
    }
    if (this.listVarRes.costWorkDto[1].nameRu != 'undefined') {
      this.data.add({
        Cnt: 2,
        workName: this.listVarRes.costWorkDto[1].nameRu,
        priceWONSD: this.listVarRes.costWorkDto[1].priceWithValueAddedTax,
        countWork: this.listVarRes.costWorkDto[1].count,
        allPrice: this.listVarRes.costWorkDto[1].totalPriceWithValueAddedTax
      });
      countCost = countCost + this.listVarRes.costWorkDto[1].count;
      allSumm = allSumm+this.listVarRes.costWorkDto[0].totalPriceWithValueAddedTax;
      this.model.serciveCount = this.listVarRes.costWorkDto[1].count;
    }
    this.data.add({Cnt: '', workName: '', priceWONSD: 'Всего', countWork: countCost, allPrice: allSumm});
    this.data.reset();
    //this.isViewCost = true;
  }

}
