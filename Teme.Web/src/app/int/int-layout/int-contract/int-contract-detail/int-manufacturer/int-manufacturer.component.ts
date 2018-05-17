import {Component, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {RefIntContractService} from '../../int-contract-service';

@Component({
  selector: 'app-int-manufacturer',
  templateUrl: './int-manufacturer.component.html',
  styleUrls: ['./int-manufacturer.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class IntManufacturerComponent implements OnInit {
  public isRes : string;
  public orgFormVar;
  public countryVarRes;
  public bankVar;
  public currencyVar;
  public listVarRes;
  @Input() idContractChild:string;
  @Input() idManufacurIn:string;


  public model:any = {
    id: null,
    detailId:null,
    manufacturNameKz: null,
    isRes: null,
    manufacturDetailId:null,
    manufacturNameRu: null,
    manufacturAddressLegalRu: null,
    manufacturAddressFact: null,
    manufacturPhone: null,
    manufacturPhone2: null,
    manufacturEmail: null,
    manufacturNameEn: null,
    manufacturCountry: null,
    manufacturOrgForm: null,
    idNumber: null,
    manufacturBossLastName: null,
    manufacturBossFirstName: null,
    manufacturBossMiddleName:null,
    manufacturBossPosition: null,
    manufacturBossPositionKz: null,
    manufacturBankName: null,
    manufacturBankIik: null,
    manufacturCurr: null,
    manufacturBankSwift: null,
    manufacturNoResCountry: null,
    manufacturNoResNameSearch: null,
  }

  @Input() prnRegisterType: string;

  constructor(private refIntService: RefIntContractService) {
    this.getBanks();
    this.getCountry();
    this.getCurrency();
    this.getOrgForm();

  }

  ngOnInit() {
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
    console.log(dataArray);
    this.model.id = dataArray[0].id;
    this.model.idNumber =  dataArray[0].idNumber;
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

  onViewChangeContract(idManufacurIn)
  {
    this.GetDeclarantById(idManufacurIn);
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
