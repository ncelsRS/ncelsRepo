import {Component, Input, OnInit, ViewEncapsulation} from '@angular/core';

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

  constructor() { }

  ngOnInit() {
  }



}
