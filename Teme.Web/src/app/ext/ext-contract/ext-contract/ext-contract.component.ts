import {Component, OnInit, Output, EventEmitter, ViewChild} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {FormsModule} from '@angular/forms';
import {RegisterType} from './RegisterType'
import {ExtManufacturerComponent} from './ext-manufacturer/ext-manufacturer.component';
import {ExtDeclarantComponent} from './ext-declarant/ext-declarant.component';
import {ExtPayerComponent} from './ext-payer/ext-payer.component';

@Component({
  selector: 'app-ext-contract',
  templateUrl: './ext-contract.component.html',
  styleUrls: ['./ext-contract.component.css']
})
export class ExtContractComponent implements OnInit {
  selectedLevel: string;
  public showAllErr = false;
  @Output() selectedLevelChange = new EventEmitter<string>();
  levels: Array<RegisterType> = [
    {code: 'Registration', name: 'Регистрация'},
    {code: 'Reregistration', name: 'Перерегистрация'},
    {code: 'Edit', name: 'Внесение изменений'},
  ];


  type: string;
  public id: string;

  constructor(private route: ActivatedRoute) {
    this.type = 'manufacturer';
    this.selectedLevel = 'Registration';
  }

   setDeclarationTab(name: string) {
    this.type = name;
  }

  changeLevel(lev: RegisterType) {
    this.selectedLevel = lev.name;
  }

  onNameChange(lev: string){

    this.selectedLevel = lev;
    this.selectedLevelChange.emit(lev);
  }

  ngOnInit() {
    this.route.params
      .subscribe(params => {
        this.id = params.id;
      });
  }

  public contract: any = {
    manufactur: {manufacturNameKz: null, manufacturNameRu: null,
      manufacturAddressLegalRu: null, manufacturAddressFact: null,
      manufacturPhone: null, manufacturEmail: null, manufacturNameEn: null,
      manufacturCountry:null, manufacturOrgForm: null, bin: null, manufacturBossLastName: null,
      manufacturBossFirstName: null, manufacturBossPosition: null, manufacturBossPositionKz: null, manufacturBankName: null,
      manufacturBankIik: null, manufacturCurr: null, bankBik: null, manufacturNoResCountry:null, manufacturNoResName:null,

    },

    declarant: {
      declarantBin: null, declarantNoResCounty: null, declarantRuName: null,
      declarantOrgForm:null, declarantNameKz: null, declarantNameRu:null,declarantNameEn: null, declarantCountry:null,
      declarantAddressLegalRu:null, declarantAddressFact:null,declarantPhone: null,declarantPhone2:null, declarantEmail:null,declarantBossLastName:null,
      declarantBossFirstName:null,declarantBossPosition:null,declarantBossPositionKz:null,declarantBank:null,declarantBankIik:null,declarantBankCurr:null,declarantBankSwift:null,
      },

    payer: {payerBin: null, payerNoRes:null,payerNoResName:null,payerOrgForm:null,payerCountry:null,payerAddressLegalRu:null,payerBankName:null,
      payerBankIik:null, payerBankSwift:null,payerBankCurr:null,
    },

    cost: {val6: null},
  };


  @ViewChild(ExtManufacturerComponent)
  private ExtManufactur: ExtManufacturerComponent;

  @ViewChild(ExtDeclarantComponent)
  private ExtDeclarant: ExtDeclarantComponent;

  @ViewChild(ExtPayerComponent)
  private ExtPayer: ExtPayerComponent;

  sendToNcels(valid) {
    this.showAllErr = true;
  }

  diagnostic() {
    return JSON.stringify(this.contract);
  }

}
