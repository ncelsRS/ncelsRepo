import { Component, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-int-payer',
  templateUrl: './int-payer.component.html',
  styleUrls: ['./int-payer.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class IntPayerComponent implements OnInit {

  public items = [];
  public orgFormVar;
  public countryVarRes;
  public bankVar;
  public currencyVar;

  public model:any = {
    id:null,
    detailId:null,
    ChoosePayer:null,
    IdNumber: null,
    isPayerRes:null,
    isPayerJuridical:null,
    payerNameRu: null,
    payerNoResName: null,
    payerOrgForm: null,
    payerCountry: null,
    payerAddressLegalRu: null,
    payerBankName: null,
    payerBankIik: null,
    payerBankSwift: null,
    payerBankCurr: null,
  };

  constructor() { }

  ngOnInit() {
  }

}
