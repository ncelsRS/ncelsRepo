import {Component, EventEmitter, forwardRef, Input, OnInit, Output, ViewChild} from '@angular/core';

import {
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR
} from '@angular/forms';

import {TemplateValidation} from '../../../../shared/TemplateValidation';
import {RefService} from '../ext-ref-sevice';
import {Ng2SmartTableComponent} from 'ng2-smart-table/ng2-smart-table.component';
import {Ng2SmartTableModule} from 'ng2-smart-table';
import {LocalDataSource} from 'ng2-smart-table';
import {forEach} from '@angular/router/src/utils/collection';


@Component({
  selector: 'app-ext-cost',
  templateUrl: './ext-cost.component.html',
  styleUrls: ['./ext-cost.component.css'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => ExtCostComponent),
    multi: true
  }, {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => ExtCostComponent),
    multi: true
  },
    RefService]
})
export class ExtCostComponent extends TemplateValidation {
  isViewCost = false;
  private _prnRegisterType: string;
  @Input() showErrors = false;

  @Input()
  set prnRegisterType(val: string) {
    this._prnRegisterType = val;
    this.getCalculatorApplicationType("eaesrg",this._prnRegisterType);

  }

  get prnRegisterType() {
    return this._prnRegisterType;
  }

  public calculatorApplicationTypeVar;
  public calculatorServiceVar;
  public calculatorServiceData=[];

  public calculatorModifVar=[];
  public calculatorModifdata=[];
  _dataCostWork=[];
  public applTypeLevel;
  public serviceLevel;
  public serciveCount;
  addServiceLevel:string;
  serviceCount:number=1;
  serviceLevelText;
  public addServiceLevelText;
  public priceListVar;
  public priceListData =[];
  public disabledCount = false;
  public disabledViewCost = true;
  public isContractViewCost;
  @Input() public idContractChild:string;
  @Input() viewAction:string;
  public data;
  public listVarRes;
  changeModelHead;
  changeModelRes;



  constructor(private refService: RefService) {
    super();

  }

  ngOnInit() {

  }

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


  getCalculatorApplicationType(scopeCode:string, contractForm:string) {
    this.refService.getCalculatorApplicationType(scopeCode, contractForm)
      .toPromise()
      .then(
        data => {
          this.calculatorApplicationTypeVar = data;
          console.log(data);
        })
      .catch(err => {
          console.error(err);
        }
      );
  }

  getCalculatorServiceType(id: string) {
    this.refService.getCalculatorServiceType(id)
      .toPromise()
      .then(
        data => {
          this.calculatorServiceVar = data;
        })
      .catch(err => {
          console.error(err);
        }
      );

    //}

  }


  changeApplTypeLevel(lev: string) {
    console.log(lev);
    this.applTypeLevel = lev;
    this.getCalculatorServiceType(lev);
  }

  changeServiceLevel(lev) {
    console.log(lev);
    console.log(lev.target.selectedOptions[0].text)
    console.log(this.prnRegisterType);
    this.serviceLevel = lev.target.value;
    this.serviceLevelText = lev.target.selectedOptions[0].text;
    this.calculatorServiceData.push(this.calculatorServiceVar);
      this.calculatorModifVar.push(this.calculatorServiceData[0])  ;

      for (let item of this.calculatorModifVar[0]) {
        if(item.id.toString() == lev.target.value)
        {

          this.calculatorModifdata= item.calculatorServiceTypeModificationModels;
          console.log(this.calculatorModifdata);


        }
      }

  }

  getShowPriceList(isImport, serviceTypeId: string, serviceTypeModifId?: string)
  {
    // if (this.prnRegisterType != null) {
    this.refService.getShowPriceList(isImport, serviceTypeId, serviceTypeModifId)
      .toPromise()
      .then(
        data => {
          this.priceListVar=data
        })
      .then( data => {
          this.loadData();
          console.log(this.priceListData);
          this.isViewCost = true;
        }
      )
      .catch(err => {
          console.error(err);
        }
      );
  }

  viewCost() {

    this.getShowPriceList(this.model.isImport,this.serviceLevel, this.addServiceLevel);
    console.log(this.priceListVar);
    this.disabledViewCost = true;

  }


  loadData()
  {
    this.priceListData=[];
    this.priceListData.push(this.priceListVar);
    console.log(this.priceListData);
    for(let t of this.priceListData)
    {
      let addServiceSum:number=0;

      this.data=new LocalDataSource();
      this._dataCostWork = [];
      console.log("IsImport= "+this.model.IsImport);
      this.data.add({Cnt:1, workName:this.serviceLevelText,priceWONSD:t.price, countWork:'1',allPrice:t.price+t.price*t.valueAddedTax/100});
      this._dataCostWork.push({PriceListId:Number(t.id),ContractId:Number(this.idContractChild), Count: 1, IsImport:(this.model.isImport==='1')?true:false,PriceWithValueAddedTax:t.price,TotalPriceWithValueAddedTax:t.price+t.price*t.valueAddedTax/100})
      if(t.priceListModificationModels!=null)
      {
        addServiceSum =this.serviceCount*(t.priceListModificationModels.price+t.priceListModificationModels.price*t.priceListModificationModels.valueAddedTax/100);
        this.data.add({Cnt:2, workName:this.addServiceLevelText,priceWONSD:t.priceListModificationModels.price, countWork:this.model.serciveCount,allPrice:addServiceSum});
        this._dataCostWork.push({PriceListId:Number(t.priceListModificationModels.id),ContractId:Number(this.idContractChild), Count: this.model.serciveCount, IsImport:(this.model.isImport==='1')?true:false ,PriceWithValueAddedTax:t.price,TotalPriceWithValueAddedTax:addServiceSum})
      }
      this.data.add({Cnt:'', workName:'',priceWONSD:'Всего', countWork:this.serviceCount+1 ,allPrice:(t.price+t.price*t.valueAddedTax/100)+addServiceSum});




      this.data.reset();


    }
    this.saveCalculateCost();
  }

  onAddService(evnt)
  {
    console.log(evnt.value);
     if(evnt.value =='99'){

       this.disabledCount = true;
       this.addServiceLevel=null;
       this.serviceCount = 0;
       this.addServiceLevelText = null;
     }
     else{
       this.addServiceLevel=evnt.value;
       this.addServiceLevelText = evnt.selectedOptions[0].text;
       this.disabledCount = false;
     }
  }

  changeAddImport()
  {
    this.disabledViewCost = false;
  }

  clearCost(){
    this.data=null;
    this._dataCostWork = null;
    this.DeleteCostWork();
    this.disabledViewCost = false;
  }

  changeServiceCount(evnt)
  {
    this.serviceCount = Number(evnt.target.value);
  }

  onChangedModel(evnt) {
    this.changeModelHead = ({
        'id': this.idContractChild,
        'classname': 'Teme.Shared.Data.Context.Contract', 'fields': {[evnt.name]: evnt.value}
      });
    console.log(this.changeModelHead);
    this.changedModelRef(this.changeModelHead);
  }



  changedModelRef(obj)
  {
    this.refService.changeModel(obj)
      .toPromise()
      .then(response => {
        console.log(response);
        this.changeModelRes = response ;
      })
      .catch (err=>
        {
          console.error(err);
        }
      )
    ;

  }

  saveCalculateCost()
  {

    this.refService.SaveCostWorked(this._dataCostWork)
      .toPromise()
      .then(response => {
        console.log(response);
      })
      .catch (err=>
        {

          console.error(err);
        }
      );
  }

  DeleteCostWork()
  {
    this.refService.DeleteCostWork(this.idContractChild)
      .toPromise()
      .then(response => {
        console.log(response);
      })
      .catch (err=>
        {

          console.error(err);
        }
      );

  }


  onViewChangeContract()
  {

    this.refService.GetContractById(this.idContractChild)
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

    if (this.viewAction == 'view') {
      this.isContractViewCost = true;
      this.isViewCost = true;
    }
    if (this.viewAction == 'edit') {
      this.isContractViewCost = false;
      this.isViewCost = false;
    }
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
    this.isViewCost = true;
  }










}




