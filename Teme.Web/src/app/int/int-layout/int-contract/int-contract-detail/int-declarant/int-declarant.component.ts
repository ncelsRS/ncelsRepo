import {Component, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {DeclarantDocType} from "../../../../../ext/ext-contract/ext-contract/ext-declarant/DeclarantDocType";
import {RefIntContractService} from '../../int-contract-service';

@Component({
  selector: 'app-int-declarant',
  templateUrl: './int-declarant.component.html',
  styleUrls: ['./int-declarant.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class IntDeclarantComponent implements OnInit {

  public orgFormVar;
  public countryVarRes;
  public bankVar;
  public currencyVar;
  public listVarRes;
  @Input() idContractChild:string;
  @Input() idDeclarantIn:string;

  levels: Array<DeclarantDocType> = [
    {value: '0', name: 'Доверенность'},
    {value: '1', name: 'Устав'},
  ];

  public model:any = {
    id: null,
    detailId:null,
    manufacturIIN:null,
    DeclarantIsManufacture: null,
    isDecRes:null,
    IdNumber: null,
    declarantNoResCounty: null,
    declarantRuName: null,
    declarantOrgForm: null,
    declarantNameKz: null,
    declarantNameRu: null,
    declarantNameEn: null,
    declarantCountry: null,
    declarantAddressLegalRu: null,
    declarantAddressFact: null,
    declarantPhone: null,
    declarantPhone2: null,
    declarantEmail: null,
    declarantBossLastName: null,
    declarantBossFirstName: null,
    declarantBossMiddleName:null,
    declarantBossPosition: null,
    declarantBossPositionKz: null,
    declarantBank: null,
    declarantBankIik: null,
    declarantBankCurr: null,
    declarantBankSwift: null,
    DeclarantDocType:null,
    DeclarantDocWithoutNumber:null,
    DeclarantDocNumber:null,
    DeclarantDocStartDate:null,
    DeclarantDocEndDate:null,
    DeclarantPerpetualDoc:null,
  }

  constructor(private refIntService: RefIntContractService) {
    this.getBanks();
    this.getCountry();
    this.getCurrency();
    this.getOrgForm();

  }

  ngOnInit() {
  }

  onViewChangeContract(idDeclarant)
  {
    this.GetDeclarantById(idDeclarant);
  }

  GetDeclarantById(val)
  {
    this.refIntService.GetDeclarantById(val)
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
    console.log(dataArray[0].declarantDetailDto.declarantDocType);

    this.model.id = dataArray[0].id;
    this.model.IdNumber = dataArray[0].idNumber;
    this.model.DeclarantIsManufacture  = dataArray[0].declarantIsManufacture;
    this.model.declarantNameRu = dataArray[0].nameRu;
    this.model.declarantNameKz = dataArray[0].nameKz;
    this.model.declarantNameEn = dataArray[0].nameEn ;
    this.model.declarantOrgForm = dataArray[0].organizationFormId;
    this.model.declarantCountry = dataArray[0].countryId;
    this.model.isDecRes = (dataArray[0].isResident)?'res':'unres';
    this.model.declarantDetailId = dataArray[0].declarantDetailDto.id;
    this.model.declarantAddressLegalRu = dataArray[0].declarantDetailDto.legalAddress;
    this.model.declarantAddressFact = dataArray[0].declarantDetailDto.factAddress;
    this.model.declarantBossLastName= dataArray[0].declarantDetailDto.bossLastName;
    this.model.declarantBossFirstName = dataArray[0].declarantDetailDto.bossFirstName;
    this.model.declarantBossMiddleName = dataArray[0].declarantDetailDto.bossMiddleName;
    this.model.declarantBossPosition = dataArray[0].declarantDetailDto.bossPositionRu;
    this.model.declarantBossPositionKz = dataArray[0].declarantDetailDto.bossPositionKz;
    this.model.declarantBank = dataArray[0].declarantDetailDto.bankId;
    this.model.declarantBankIik = dataArray[0].declarantDetailDto.bankIik;
    this.model.declarantBankSwift = dataArray[0].declarantDetailDto.bankSwift;
    this.model.declarantBankCurr = dataArray[0].declarantDetailDto.currencyId;
    this.model.declarantPhone = dataArray[0].declarantDetailDto.phone;
    this.model.declarantPhone2 = dataArray[0].declarantDetailDto.phone2;
    this.model.declarantEmail = dataArray[0].declarantDetailDto.email;
    this.model.DeclarantDocType = dataArray[0].declarantDetailDto.declarantDocType;
    this.model.DeclarantDocWithoutNumber = dataArray[0].declarantDetailDto.declarantDocWithoutNumber;
    this.model.DeclarantDocNumber = dataArray[0].declarantDetailDto.declarantDocNumber;
    this.model.DeclarantDocStartDate = dataArray[0].declarantDetailDto.declarantDocStartDate;
    this.model.DeclarantDocEndDate = dataArray[0].declarantDetailDto.declarantDocEndDate;
    this.model.DeclarantPerpetualDoc = dataArray[0].declarantDetailDto.declarantPerpetualDoc;

  }

  getBanks() {
    this.refIntService.getBank()
      .subscribe(
        data=> {
          this.bankVar = data;      },
        (err) =>
          console.error(err),
        () =>
          console.log('done loading Bank')
      );

  }


  getCountry() {
    this.refIntService.getCountry()
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
    this.refIntService.getCurrency()
      .subscribe(
        data=> {
          this.currencyVar = data;      },
        (err) =>
          console.error(err),
        () =>
          console.log('done loading Currency')
      );

  }

  getOrgForm() {
    this.refIntService.getOrgForm()
      .subscribe(
        data=> {
          this.orgFormVar = data;},
        (err) =>
          console.error(err),
        () =>
          console.log('done loading orgForm')

      );

  }






}
