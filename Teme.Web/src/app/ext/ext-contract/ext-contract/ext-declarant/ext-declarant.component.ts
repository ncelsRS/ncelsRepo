import {Component, forwardRef, Input,  Injectable, EventEmitter, Output} from '@angular/core';
import {DeclarantDocType} from './DeclarantDocType';
import {TemplateValidation} from '../../../../shared/TemplateValidation';
import {
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
} from "@angular/forms";
import {RefService} from '../ext-ref-sevice';




@Component({
    selector: 'app-ext-declarant',
    templateUrl: './ext-declarant.component.html',
    styleUrls: ['./ext-declarant.component.css'],
    providers: [{
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ExtDeclarantComponent),
      multi: true
    }, {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => ExtDeclarantComponent),
      multi: true
    },
      RefService]
})

@Injectable()
export class ExtDeclarantComponent  extends TemplateValidation{
  isAddOrgForm = false;
  isAddBankName = false;
  selectedDeclarantDocType: string;
  levels: Array<DeclarantDocType> = [
    {code: '0', name: 'Доверенность'},
    {code: '1', name: 'Устав'},
  ]

  public orgFormVar;
  resOrgFormId:any;
  public bankVar;
  bankId:any;
  public countryVarNoRes;
  public countryVarRes;
  public currencyVar;
  public viewAddDeclarant=false;
  public disabledAddDetail=true;
  public listVarNoRes;
  public listVarRes;
  changeModelRes:any;
  changeModelHead;
  @Input() idContractChild:string;
  @Input() showErrors = false;
  @Output() onChangedManufYes = new EventEmitter<boolean>();




  changeDocLevel(lev: DeclarantDocType) {
    this.selectedDeclarantDocType = lev.name;
  }

  constructor( private refService: RefService) {
    super();
    this.getOrgForm();
    this.getBanks();
    this.getCountryNoRes();
    this.getCountry();
    this.getCurrency()
  }


    ngOnInit() {
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
    console.log("step 1");


  }

  declineBank()
  {
    this.isAddBankName = false;
  }

  addBankName()
  {
    this.isAddBankName = true;
  }

  addOrgForm()
  {
    this.isAddOrgForm = true;
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

  onManufChange(chk)
  {
    console.log(chk);
    if (chk) {
      this.onChangedManufYes.emit(chk);
      this.changeModelHead =
        ({'id': this.idContractChild, 'classname': 'Teme.Shared.Data.Context.Contract',
          'fields':
            {'DeclarantIsManufacture': '1',
              'DeclarantId': this.model.id,
              'DeclarantDetailsId': this.model.detailId
            }
        });
      console.log(this.changeModelHead);

    }

  }


  onChangedModel(evnt) {
    console.log(evnt);
    console.log(evnt.name+" stepssssss "+evnt.value);

    if (evnt.name == 'NameKz' || evnt.name == 'NameRu' ||
      evnt.name == 'NameEn' || evnt.name == 'OrganizationFormId' || evnt.name == 'CountryId') {
       this.changeModelHead = ({
          'id': this.model.id,
          'classname': 'Teme.Shared.Data.Context.Declarant', 'fields': {[evnt.name]: evnt.value}
        })
      }

    else if (evnt.name == 'DeclarantDocWithoutNumber'||evnt.name == 'DeclarantPerpetualDoc')
    {
      if (evnt.value=='on')
      { this.changeModelHead =
        ({'id': this.model.id, 'classname': 'Teme.Shared.Data.Context.DeclarantDetail', 'fields': {[evnt.name]: 1}})}
        else {this.changeModelHead =
        ({'id': this.model.id, 'classname': 'Teme.Shared.Data.Context.DeclarantDetail', 'fields': {[evnt.name]:0}})};

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

    if (this.model.isDecRes == 'res') {
      this.changeModelHead =
        ({'id': this.model.id, 'classname': 'Teme.Shared.Data.Context.Declarant', 'fields': {'IsResident': 1}})
    }
    else {
      this.changeModelHead = ({
        'id': this.model.id, 'classname': 'Teme.Shared.Data.Context.Declarant', 'fields': {'IsResident': 0}});
    }

    if(!this.listVarRes)
    {
      console.log(this.changeModelHead);
      this.changedModelRef(this.changeModelHead);
      // this.changedModelRef({'id': this.model.id, 'classname': 'Teme.Shared.Data.Context.Declarant', 'fields': {'idNumber': 1}});
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


  createDeclarant()
  {
    let responseData;
    //this.idContractChild
    this.refService.AddDeclarant(this.idContractChild, 'Declarant')
      .toPromise()
      .then(response => {
        console.log(response);
        responseData = response;
        this.model.id  = responseData.id;
        this.model.detailId  = responseData.detailId;
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

  searchDeclarant(val)
  {
    this.refService.SearchDeclarantResident(val)
      .toPromise()
      .then(response => {
        this.listVarRes = response ;
        console.log(response);
        if(response==null)
        {
          this.viewAddDeclarant = true;
        }
        else
        {
          this.loadData(this.listVarRes);
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

    this.model.id = dataArray[0].id;
    this.model.declarantNameRu = dataArray[0].nameRu;
    this.model.declarantNameKz = dataArray[0].nameKz;
    this.model.declarantNameEn = dataArray[0].nameEn ;
    this.model.declarantOrgForm = dataArray[0].organizationFormId;
    this.model.declarantCountry = dataArray[0].countryId;
    this.model.isRes = (dataArray[0].isResident)?'res':'unres';
    this.model.declarantDetailId = dataArray[0].declarantDetailDto.id;
    this.model.declarantAddressLegalRu = dataArray[0].declarantDetailDto.legalAddress;
    this.model.declarantAddressFact = dataArray[0].declarantDetailDto.factAddress;
    this.model.declarantBossLastName= dataArray[0].declarantDetailDto.bossLastName;
    this.model.declarantBossFirstName = dataArray[0].declarantDetailDto.bossFirstName;
    this.model.declarantBossMiddleName = dataArray[0].declarantDetailDto.bossMiddleName;
    this.model.declarantBossPosition = dataArray[0].declarantDetailDto.bossPositionRu;
    this.model.declarantBossPositionKz = dataArray[0].declarantDetailDto.bossPositionKz;
    this.model.declarantBankName = dataArray[0].declarantDetailDto.bankId;
    this.model.declarantBankIik = dataArray[0].declarantDetailDto.bankIik;
    this.model.declarantBankSwift = dataArray[0].declarantDetailDto.bankSwift;
    this.model.declarantCurr = dataArray[0].declarantDetailDto.currencyId;
    this.model.declarantPhone = dataArray[0].declarantDetailDto.phone;
    this.model.manufacturPhone2 = dataArray[0].declarantDetailDto.phone2;
    this.model.declarantEmail = dataArray[0].declarantDetailDto.email;
    this.model.DeclarantDocType = dataArray[0].declarantDetailDto.declarantDocType;
    this.model.DeclarantDocWithoutNumber = dataArray[0].declarantDetailDto.declarantDocWithoutNumber;
    this.model.DeclarantDocNumber = dataArray[0].declarantDetailDto.declarantDocNumber;
    this.model.DeclarantDocStartDate = dataArray[0].declarantDetailDto.declarantDocStartDate;
    this.model.DeclarantDocEndDate = dataArray[0].declarantDetailDto.declarantDocEndDate;
    this.model.DeclarantPerpetualDoc = dataArray[0].declarantDetailDto.declarantPerpetualDoc;

  }

  onChangedModelDate(name,val){
    this.changedModelRef({'id': this.model.id, 'classname': 'Teme.Shared.Data.Context.DeclarantDetail', 'fields': {[name]: new Date(val.year,val.month, val.day)}});
    //this.changedModelRef({'id': this.model.id, 'classname': 'Teme.Shared.Data.Context.DeclarantDetail', 'fields': {'DeclarantDocEndDate': this.model.DeclarantDocEndDate.value}});
  };

}


