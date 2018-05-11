import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {DeclarantDocType} from "../../../../../ext/ext-contract/ext-contract/ext-declarant/DeclarantDocType";

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

  levels: Array<DeclarantDocType> = [
    {code: '0', name: 'Доверенность'},
    {code: '1', name: 'Устав'},
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

  constructor() { }

  ngOnInit() {
  }




}
