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
  isManuf :string;
  _isManuf:string
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
  //   =[{
  //   id: null,
  //   children:null,
  //   code:null,
  //   nameKz:null,
  //   nameRu:null,
  //   calculatorServiceTypeModificationModels:[]
  // }];
  public calculatorModifVar=[];
  public calculatorModifdata=[];
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
  @Input() public idContractChild:string;
  public data;


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
    // if (this.prnRegisterType != null) {
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

    //}

  }

  getCalculatorServiceType(id: string) {
    // if (this.prnRegisterType != null) {
    this.refService.getCalculatorServiceType(id)
      .toPromise()
      .then(
        data => {
          this.calculatorServiceVar = data;
          console.log("step1");
          console.log(data);
          console.log("step2")
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

  getShowPriceList(isImport:string, serviceTypeId: string, serviceTypeModifId?: string)
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
   if(this.isManuf=='frg') {this._isManuf='1'}else{this._isManuf='0'};
    this.getShowPriceList(this._isManuf,this.serviceLevel, this.addServiceLevel);
    console.log(this.priceListVar);


  }


  loadData()
  {
    this.priceListData.push(this.priceListVar);
    console.log(this.priceListData);
    for(let t of this.priceListData)
    {

      console.log(t.price);
      console.log(this.serviceCount);

      let addServiceSum:number=0;

      this.data=new LocalDataSource();
      this.data.add({Cnt:1, workName:this.serviceLevelText,priceWONSD:t.price, countWork:'1',allPrice:t.price+t.price*t.valueAddedTax/100});
      if(t.priceListModificationModels!=null)
      {
        addServiceSum =this.serviceCount*(t.priceListModificationModels.price+t.priceListModificationModels.price*t.priceListModificationModels.valueAddedTax/100);
        this.data.add({Cnt:2, workName:this.addServiceLevelText,priceWONSD:t.priceListModificationModels.price, countWork:this.serviceCount,allPrice:addServiceSum});
      }
      this.data.add({Cnt:'', workName:'',priceWONSD:'Всего', countWork:this.serviceCount+1 ,allPrice:(t.price+t.price*t.valueAddedTax/100)+addServiceSum});

      console.log(this.data);
      this.data.reset();


    }
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
    this.data.reset();
  }

  changeServiceCount(evnt)
  {
    this.serviceCount = Number(evnt.target.value);
  }
}




