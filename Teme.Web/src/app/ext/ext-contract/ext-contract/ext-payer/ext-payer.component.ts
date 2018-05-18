import {Component, EventEmitter, forwardRef, Input, OnInit, Output, ViewChild} from '@angular/core';
import {
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR
} from "@angular/forms";
import {TemplateValidation} from '../../../../shared/TemplateValidation';
import {RefService} from '../ext-ref-sevice';

@Component({
  selector: 'app-ext-payer',
  templateUrl: './ext-payer.component.html',
  styleUrls: ['./ext-payer.component.css'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => ExtPayerComponent),
    multi: true
  }, {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => ExtPayerComponent),
    multi: true
  },
    RefService]
})
export class ExtPayerComponent extends TemplateValidation {
  isAddOrgForm = false;
  isAddBankName = false;

  public orgFormVar;
  resOrgFormId:any;

  public bankVar;
  bankId:any;

  public countryVarNoRes;
  public countryVarRes;
  public viewAddPayer=false;
  public disabledAddDetail=true;
  public disabledPayerDetail=true;
  @Input() idContractChild:string;
  @Input() viewAction:string;
  @Output() onChangedManufYes = new EventEmitter<boolean>();
  @Output() onChangedDeclarantYes = new EventEmitter<boolean>();

  public  currencyVar;
  public  choosePayerVar;
  public items = [];
  public listVarRes;
  public listVarNoRes;
  _isNewSubject:boolean = false;
  changeModelHead;
  changeModelRes:any;

  public iinMask = [/\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/];

  constructor(private refService: RefService) { super();
    this.getOrgForm();
    this.getBanks();
    this.getCountryNoRes();
    this.getCountry();
    this.getCurrency();
    this.getChoosePayer();

  }

  ngOnInit() {
  }
  @Input() showErrors = false;

  addOrgForm()
  {
    this.isAddOrgForm = true;
  }

  addBankName()
  {
    this.isAddBankName = true;
  }

  saveOrgForm(nameKz:string,nameRu:string){
    this.refService.saveOrgForm(nameKz,nameRu)
      .toPromise()
      .then(response => {
        console.log(response);
        this.resOrgFormId =response;
        return this.resOrgFormId;
      })
      .then (response => {
          this.orgFormVar.push({id: response as number, code: null, nameRu: nameKz, nameKz: nameRu});
          this.model.payerOrgForm = response as number;
        this.changedModelRef({'id': this.model.id, 'classname': 'Teme.Shared.Data.Context.Declarant', 'fields': {'OrganizationFormId': this.model.payerOrgForm }});
          this.isAddOrgForm = false;
        }
      )
      .catch (err=>
        {
          console.error(err);
        }
      )
    ;
    console.log("step 1");


  }

  declineOrgForm()
  {
    this.isAddOrgForm = false;
  }


  getOrgForm() {
    this.refService.getOrgForm()
      .subscribe(
        data=> {
          this.orgFormVar = data;},
        (err) =>
          console.error(err),
        () =>
          console.log('done loading orgForm')
      );

  }

  searchPayerNoRes()
  {
    this.refService.SearchDeclarantNonResident(this.model.payerNoResCountry)
      .toPromise()
      .then(response => {
        console.log(response);
        this.listVarNoRes = response ;
      })
      .catch (err=>
        {
          console.error(err);
        }
      )
    ;

  }

  saveBank(nameKz:string,nameRu:string){
    var res =  this.refService.saveBank(nameKz,nameRu)
      .toPromise()
      .then(response => {
        console.log(response);
        this.bankId =response;
        return this.bankId;
      })
      .then (response => {
          this.bankVar.push({id: response as number, code: null, nameRu: nameKz, nameKz: nameRu});
          this.model.payerBankName = response as number;
          this.changedModelRef({'id': this.model.detailId, 'classname': 'Teme.Shared.Data.Context.DeclarantDetail', 'fields': {'BankId': this.model.payerBankName }});
          console.log(res);
          this.isAddBankName = false;
        }
      )
      .catch (err=>
        {
          console.error(err);
        }
      )
    ;
    console.log("step 1");


  }

  declineBank()
  {
    this.isAddBankName = false;
  }


  getBanks() {
    this.refService.getBank()
      .subscribe(
        data=> {
          this.bankVar = data;  this.isAddBankName = false;    },
        (err) =>
          console.error(err),
        () =>
          console.log('done loading Bank')
      );

  }


  getCountryNoRes() {
    this.refService.getCountry()
      .subscribe(
        data=> {
          this.countryVarNoRes = data;      },
        (err) =>
          console.error(err),
        () =>
          console.log('done loading Country')
      );

  }

  getCountry() {
    this.refService.getCountry()
      .subscribe(
        data=> {
          this.countryVarRes = data;      },
        (err) =>
          console.error(err),
        () =>
          console.log('done loading Country')
      );

  }


  getCurrency() {
    this.refService.getCurrency()
      .subscribe(
        data=> {
          this.currencyVar = data;      },
        (err) =>
          console.error(err),
        () =>
          console.log('done loading Currency')
      );

  }


    onChoosePayer(lev)
  {
    console.log(lev);
    if (lev=='1') {
      this.onChangedDeclarantYes.emit(lev);
      this.changeModelHead =
        ({'id': this.idContractChild, 'classname': 'Teme.Shared.Data.Context.Contract',
          'fields':
            {'ChoosePayer': '1',
              'PayerId': this.model.id,
              'PayerDetailsId': this.model.detailId
            }
        });
    }
    else if(lev=='2'){
      this.onChangedManufYes.emit(lev);
      this.changeModelHead =
        ({'id': this.idContractChild, 'classname': 'Teme.Shared.Data.Context.Contract',
          'fields':
            {'ChoosePayer': '2',
              'PayerId': this.model.id,
              'PayerDetailsId': this.model.detailId
            }
        });
    }
    else if(lev=='3')
    {
      this.clearModel();
      this.changeModelHead =
        ({'id': this.idContractChild, 'classname': 'Teme.Shared.Data.Context.Contract',
          'fields':
            {'ChoosePayer': '2',
              'PayerId': this.model.id,
              'PayerDetailsId': this.model.detailId
            }
        });
      this.disabledPayerDetail = false;

    }

  }

  getChoosePayer() {
    this.refService.getChosenPayer()
      .toPromise()
      .then(response => {
        console.log(response);
        this.choosePayerVar = response;
        this.getData(this.choosePayerVar);
      })
      .catch(err => {
          console.error(err);
        }
      )

  }

  getData(items) {
    this.items = items;
    for (let item of this.items) {
      if (item.value == 'Declarant') {
        item.nameRu = 'Заявитель';
        item.nameKz = 'Заявитель кз';
      }
      else if (item.value == 'Manufactur') {
        item.nameRu = 'Прозводитель';
        item.nameKz = 'Прозводитель кз';
      }
      else if (item.value == 'Other') {
        item.nameRu = 'Третье лицо';
        item.nameKz = 'Третье лицо кз';
      }
    }

  }

  searchPayer(val)
  {
    this.refService.SearchDeclarantResident(val)
      .toPromise()
      .then(response => {
        this.listVarRes = response ;
        console.log(response);
        if(response==null)
        {
          this.viewAddPayer = true;
        }
        else
        {
          this.loadData(this.listVarRes);
          this.disabledAddDetail = false;
        }
      })
      .catch (err=>
        {
          console.error(err);
        }
      )
    ;

  }

  loadData(data)
  {
    let dataArray=[];
    dataArray.push(data);

   // this.model.id = dataArray[0].Id;
    this.model.isPayerRes = (dataArray[0].isResident)?'res':'unres';
    this.model.isPayerJuridical = dataArray[0].IdJuridical;

    this.model.IdNumber = dataArray[0].idNumber;
    this.model.payerOrgForm = dataArray[0].organizationFormId;
    this.model.payerNameRu = dataArray[0].nameRu;
    this.model.payerCountry = dataArray[0].countryId;
    this.model.detailId = dataArray[0].declarantDetailDto.id;
    this.model.payerAddressLegalRu = dataArray[0].declarantDetailDto.legalAddress;
    this.model.payerBankName = dataArray[0].declarantDetailDto.bankName;
    this.model.payerBankIik = dataArray[0].declarantDetailDto.bankIik;
    this.model.payerBankCurr = dataArray[0].declarantDetailDto.currencyId;
    this.model.declarantBankSwift = dataArray[0].declarantDetailDto.bankSwift;


  }

  clearModel()
  {
    this.model.id =null;
    this.model.isPayerRes = null;
    this.model.isPayerJuridical = null;

    this.model.IdNumber =null;
    this.model.payerOrgForm = null;
    this.model.payerNameRu =null;
    this.model.payerCountry = null;
    this.model.detailId = null;
    this.model.payerAddressLegalRu = null;
    this.model.payerBankName = null;
    this.model.payerBankIik = null;
    this.model.payerBankCurr = null;
    this.model.declarantBankSwift = null;


  }

  createPayer()
  {
    let responseData;
    //this.idContractChild
    this.refService.AddDeclarant(this.idContractChild, 'Payer')
      .toPromise()
      .then(response => {
        console.log(response);
        responseData = response;
        this.model.id  = responseData.id;
        this.model.detailId = responseData.detailId;
        this.disabledAddDetail = false;
        this._isNewSubject;
        this.onChangedModelAddNewSbj();

      })
      .catch (err=>
        {
          console.error(err);
        }
      )
    ;
  }

  onChangedModel(evnt) {

    if (evnt.name == 'NameKz' || evnt.name == 'NameRu' ||
      evnt.name == 'NameEn' || evnt.name == 'OrganizationFormId' || evnt.name == 'CountryId') {
      this.changeModelHead = ({
          'id': this.model.id,
          'classname': 'Teme.Shared.Data.Context.Declarant', 'fields': {[evnt.name]: evnt.value}
        })
      }
    else {
      this.changeModelHead = ({
        'id': this.model.detailId,
        'classname': 'Teme.Shared.Data.Context.DeclarantDetail', 'fields': {[evnt.name]: evnt.value}
      })
    }


    if(this.viewAction!='view')
    {
      if(evnt.name != 'IsJuridical' && evnt.name != 'IsResident' && evnt.name != 'IdNumber')
      this.changedModelRef(this.changeModelHead);
      if(this._isNewSubject || this.viewAction=='edit')
        if(evnt.name == 'IsJuridical')
          this.changedModelRef({'id': this.model.id, 'classname': 'Teme.Shared.Data.Context.Declarant', 'fields': {'IsJuridical':(this.model.isPayerJuridical == 'fl')?"1":"0"}});
        if(evnt.name == 'IsResident')
          this.changedModelRef({'id': this.model.id, 'classname': 'Teme.Shared.Data.Context.Declarant', 'fields': {'IsResident':(this.model.isPayerRes == 'res')?"1":"0"}});
        if(evnt.name == 'IdNumber')
          this.changedModelRef({'id': this.model.id, 'classname': 'Teme.Shared.Data.Context.Declarant', 'fields': {'IdNumber': this.model.IdNumber}})

    }

  }

  onChangedModelAddNewSbj() {

    if(this.viewAction!='view')
    {
      console.log(this.model.isPayerJuridical);
      this.changedModelRef({'id': this.model.id, 'classname': 'Teme.Shared.Data.Context.Declarant', 'fields': {'IsJuridical':(this.model.isPayerJuridical === 'fl')?"1":"0"}});
      this.changedModelRef({'id': this.model.id, 'classname': 'Teme.Shared.Data.Context.Declarant', 'fields': {'IsResident':(this.model.isPayerRes === 'res')?"1":"0"}});
      this.changedModelRef({'id': this.model.id, 'classname': 'Teme.Shared.Data.Context.Declarant', 'fields': {'IdNumber': this.model.IdNumber}})
    }

  }

  onChangeNameNoRes()
  {
    if(this.model.payerNoResName =="-1")
      this.createPayer();
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

  onViewChangeContract()
  {
    console.log("payer="+this.model.id);
    if (this.viewAction == 'view')
      this.GetDeclarantById(this.model.id);
    if (this.viewAction == 'edit') {
      this.GetDeclarantById(this.model.id);
      this.disabledPayerDetail = false;
      this.disabledAddDetail = false;
    }

  }

  GetDeclarantById(val)
  {
    this.refService.GetDeclarantById(val)
      .toPromise()
      .then(response => {
        this.listVarRes = response ;
        console.log(response);
        this.loadData(response);
      })
      .catch (err=>
        {
          console.error(err);
        }
      )
    ;

  }

 }
