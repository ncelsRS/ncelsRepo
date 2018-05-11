import {Component, Input, forwardRef, Injectable} from '@angular/core';

import {
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR
 } from "@angular/forms";
import {TemplateValidation} from '../../../../shared/TemplateValidation';
import {RefService} from '../ext-ref-sevice';

@Component({
  selector: 'app-ext-manufacturer',
  templateUrl: './ext-manufacturer.component.html',
  styleUrls: ['./ext-manufacturer.component.css'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => ExtManufacturerComponent),
    multi: true
  }, {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => ExtManufacturerComponent),
    multi: true
  }, RefService]
})
@Injectable()
export class ExtManufacturerComponent extends TemplateValidation  {


  isAddOrgForm = false;
  isAddBankName = false;
  changeModelRes:any;

  public orgFormVar;
  resOrgFormId:any;

  public bankVar;
  bankId:any;

  public countryVarNoRes;
  public countryVarRes;
  public  currencyVar;
  public viewAddManufacture=false;
  public disabledAddDetail=true;
  public listVarNoRes;
  public listVarRes;
  changeModelHead;

   @Input() prnRegisterType: string;
   @Input() showErrors = false;
   @Input() idContractChild:string;
   @Input() viewAction:string;

  constructor(private refService: RefService){
    super();
    this.getOrgForm();
    this.getBanks();
    this.getCountryNoRes();
    this.getCountry();
    this.getCurrency();

  }

  OnInit()
  {


  }

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
    }

  declineBank()
  {
    this.isAddBankName = false;
  }


  getBanks() {
    this.refService.getBank()
      .subscribe(
        data=> {
          this.bankVar = data;      },
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
        'id': this.model.id,
        'classname': 'Teme.Shared.Data.Context.DeclarantDetail', 'fields': {[evnt.name]: evnt.value}
      })
    }


    if(!this.listVarRes)
    {
      console.log(evnt.value);
      console.log(this.changeModelHead);
      this.changedModelRef(this.changeModelHead);

    }

  }

  onChangedModelAddNewSbj() {


    if (this.model.isRes == 'res') {
      this.changeModelHead =
        ({'id': this.model.id, 'classname': 'Teme.Shared.Data.Context.Declarant', 'fields': {IsResident: 1}})
    }
    else {
      this.changeModelHead = ({
        'id': this.model.id, 'classname': 'Teme.Shared.Data.Context.Declarant', 'fields': {IsResident: 0}});
    }

    if(!this.listVarRes)
    {
      console.log(this.changeModelHead);
      this.changedModelRef(this.changeModelHead);
      this.changedModelRef({'id': this.model.id, 'classname': 'Teme.Shared.Data.Context.Declarant', 'fields': {'IdNumber': this.model.IdNumber}});
    }

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


  createManufactur()
  {
    let responseData;
    //this.idContractChild
    this.refService.AddDeclarant(this.idContractChild, 'Manufactur')
      .toPromise()
      .then(response => {
        console.log(response);
        responseData = response;
        this.model.id  = responseData.id;
        this.model.detailId = responseData.detailId;
        this.disabledAddDetail = false;
        this.onChangedModelAddNewSbj();

      })
      .catch (err=>
        {
          console.error(err);
        }
      )
    ;
  }


  searchManufacturNoRes()
  {
    this.refService.SearchDeclarantNonResident(this.model.manufacturNoResCountry)
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

  searchManufactur(val) {
    this.refService.SearchDeclarantResident(val)
      .toPromise()
      .then(response => {
        this.listVarRes = response;
        console.log(response);
        if (response == null) {
          this.viewAddManufacture = true;

        }
        else {
          this.loadData(response);
        }
      })
      .catch(err => {
          console.error(err);
        }
      );
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

  loadData(data)
  {
    let dataArray=[];
    dataArray.push(data);
    console.log(dataArray);

    this.model.id = dataArray[0].id;
    this.model.manufacturNameRu = dataArray[0].nameRu;
    this.model.manufacturNameKz = dataArray[0].nameKz;
    this.model.manufacturNameEn = dataArray[0].nameEn ;
    this.model.manufacturOrgForm = dataArray[0].organizationFormId;
    this.model.manufacturCountry = dataArray[0].countryId;
    this.model.isRes = (dataArray[0].isResident)?'res':'unres';
    this.model.manufacturDetailId = dataArray[0].declarantDetailDto.id;
    this.model.manufacturAddressLegalRu = dataArray[0].declarantDetailDto.legalAddress;
    this.model.manufacturAddressFact = dataArray[0].declarantDetailDto.factAddress;
    this.model.manufacturBossLastName= dataArray[0].declarantDetailDto.bossLastName;
    this.model.manufacturBossFirstName = dataArray[0].declarantDetailDto.bossFirstName;
    this.model.manufacturBossMiddleName = dataArray[0].declarantDetailDto.bossMiddleName;
    this.model.manufacturBossPosition = dataArray[0].declarantDetailDto.bossPositionRu;
    this.model.manufacturBossPositionKz = dataArray[0].declarantDetailDto.bossPositionKz;
    this.model.manufacturBankName = dataArray[0].declarantDetailDto.bankName;
    this.model.manufacturBankIik = dataArray[0].declarantDetailDto.bankIik;
    this.model.manufacturBankSwift = dataArray[0].declarantDetailDto.bankSwift;
    this.model.manufacturCurr = dataArray[0].declarantDetailDto.currencyId;
    this.model.manufacturPhone = dataArray[0].declarantDetailDto.phone;
    this.model.manufacturPhone2 = dataArray[0].declarantDetailDto.phone2;
    this.model.manufacturEmail = dataArray[0].declarantDetailDto.email;


  }

  onViewChangeContract()
  {
    console.log("viewAction="+this.viewAction);
    this.GetDeclarantById(this.model.id);

  }

  }



