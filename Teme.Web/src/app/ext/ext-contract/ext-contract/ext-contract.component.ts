import {Component, OnInit, Output, EventEmitter, ViewChild, Input, ViewEncapsulation} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {FormsModule} from '@angular/forms';
import {RegisterType} from './RegisterType'
import {ExtManufacturerComponent} from './ext-manufacturer/ext-manufacturer.component';
import {ExtDeclarantComponent} from './ext-declarant/ext-declarant.component';
import {ExtPayerComponent} from './ext-payer/ext-payer.component';
import {ExtCostComponent} from './ext-cost/ext-cost.component';
import {RefService} from './ext-ref-sevice';
import {TemplateValidation} from '../../../shared/TemplateValidation';



@Component({
  selector: 'app-ext-contract',
  templateUrl: './ext-contract.component.html',
  styleUrls: ['./ext-contract.component.css'],
  providers: [RefService],

})
export class ExtContractComponent  extends TemplateValidation {
  private contractView: string
  public ramoch;

  selectedLevel: string;
  public showAllErr = false;
  @Output() selectedLevelChange = new EventEmitter<string>();
  @Output() idContractEvent = new EventEmitter<string>();
  @Input() childType: string;
  public createContractId;
  public idContract;

  type: string;
  public id: string;
  public items = [];
  public holderTypeItems = [];
  public contractFormVar;
  public holderTypeVar;

  constructor(private route: ActivatedRoute, private refService: RefService) {
    super();

    this.type = 'manufacturer';
    this.getContactForm();
    this.getHolderType();

    this.createContract('1','eaesrg')


  }

  setDeclarationTab(name: string) {
    this.type = name;
  }


  changeLevel(lev: string) {

    console.log(lev);
    this.selectedLevel = lev;
    this.selectedLevelChange.emit(lev);

  }

  ngOnInit(){
    this.route.params
      .subscribe(params => {
       // this.ramoch = param;
        console.log(params);
      });
    console.log(this.id);

  }

  public contract: any = {
    manufactur: {
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

    },

    declarant: {
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
    },

    payer: {
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
    },

    cost: {
      id:null,
      applicationType: null,
      serviceType: null,
      NameIMNRu: null,
      NameIMNKz: null,},
  };


  @ViewChild(ExtManufacturerComponent)
  private ExtManufactur: ExtManufacturerComponent;

  @ViewChild(ExtDeclarantComponent)
  private ExtDeclarant: ExtDeclarantComponent;

  @ViewChild(ExtPayerComponent)
  private ExtPayer: ExtPayerComponent;

  @ViewChild(ExtCostComponent)
  private ExtCost: ExtCostComponent;

  sendToNcels(valid) {
    this.showAllErr = true;
  }

  diagnostic() {
    return JSON.stringify(this.contract);
  }

  getContactForm() {
    this.refService.getContractForm()
      .toPromise()
      .then(response => {
        console.log(response);
        this.contractFormVar = response;
        this.getData(this.contractFormVar);
      })
      .catch(err => {
          console.error(err);
        }
      )

  }

  getData(items) {
    this.items = items;
    for (let item of this.items) {
      if (item.value == 'Registration') {
        item.nameRu = 'Регистрация';
        item.nameKz = 'Регистрация кз';
      }
      else if (item.value == 'Reregistration') {
        item.nameRu = 'Перерегистрация';
        item.nameKz = 'Перерегистрация кз';
      }
      else if (item.value == 'Modification') {
        item.nameRu = 'Внесение изменения';
        item.nameKz = 'Внесение изменения кз';
      }
    }

  }

  getHolderType() {
    this.refService.getHolderType()
      .toPromise()
      .then(response => {
        console.log(response);
        this.holderTypeVar = response;
        this.getHolderTypeData(this.holderTypeVar);
      })
      .catch(err => {
          console.error(err);
        }
      )

  }

  getHolderTypeData(items) {
    this.holderTypeItems = items;
    for (let item of this.holderTypeItems) {
      if (item.value == 'Manufacturer') {
        item.nameRu = 'Производитель';
        item.nameKz = 'Өндіруші';
      }
      else if (item.value == 'Declarant') {
        item.nameRu = 'Перерегистрация';
        item.nameKz = 'Жолдаушы';

      }

    }

  }

  createContract(contractType,contractScpe) {
    this.refService.createContract(contractType,contractScpe)
      .toPromise()
      .then(response => {
        console.log(response);
        this.createContractId = response;
        this.idContract = this.createContractId.id;
        console.log(this.idContract.id)
        this.idContractEvent.emit(this.idContract);
      })
      .catch(err => {
          console.error(err);
        }
      )

  }

  onChildChanged()
  {
    console.log('Change is work')
    this.contract.declarant.id = this.contract.manufactur.id;
    this.contract.declarant.detailId = this.contract.manufactur.detailId;
    this.contract.declarant.isDecRes = this.contract.manufactur.isRes;
    this.contract.declarant.IdNumber = this.contract.manufactur.IdNumber;
    this.contract.declarant.declarantOrgForm = this.contract.manufactur.manufacturOrgForm;
    this.contract.declarant.declarantNameRu = this.contract.manufactur.manufacturNameRu;
    this.contract.declarant.declarantNameKz = this.contract.manufactur.manufacturNameKz;
    this.contract.declarant.declarantNameEn = this.contract.manufactur.manufacturNameEn;
    this.contract.declarant.declarantCountry = this.contract.manufactur.manufacturCountry;
    this.contract.declarant.declarantAddressLegalRu = this.contract.manufactur.manufacturAddressLegalRu;
    this.contract.declarant.declarantAddressFact = this.contract.manufactur.manufacturAddressFact;
    this.contract.declarant.declarantPhone = this.contract.manufactur.manufacturPhone;
    this.contract.declarant.declarantPhone2 = this.contract.manufactur.manufacturPhone;
    this.contract.declarant.declarantEmail = this.contract.manufactur.manufacturEmail;
    this.contract.declarant.declarantBossPosition = this.contract.manufactur.manufacturBossPosition;
    this.contract.declarant.declarantBossPositionKz = this.contract.manufactur.manufacturBossPositionKz;
    this.contract.declarant.declarantBossLastName = this.contract.manufactur.manufacturBossLastName;
    this.contract.declarant.declarantBossFirstName = this.contract.manufactur.manufacturBossFirstName;
    this.contract.declarant.declarantBossMiddleName = this.contract.manufactur.manufacturBossMiddleName;
    this.contract.declarant.declarantBank = this.contract.manufactur.manufacturBankName;
    this.contract.declarant.declarantBankIik = this.contract.manufactur.manufacturBankIik;
    this.contract.declarant.declarantBankCurr = this.contract.manufactur.manufacturCurr;
    this.contract.declarant.declarantBankSwift = this.contract.manufactur.manufacturBankSwift;

  }

  onChildDeclarantChanged()
  {
    console.log('Change is work')
    this.contract.payer.id = this.contract.declarant.id;
    this.contract.payer.detailId = this.contract.declarant.detailId;
    this.contract.payer.IdNumber = this.contract.declarant.IdNumber;
    this.contract.payer.isPayerRes = this.contract.declarant.isDecRes;
    this.contract.payer.payerOrgForm = this.contract.declarant.declarantOrgForm;
    this.contract.payer.payerNameRu = this.contract.declarant.declarantNameRu;
    this.contract.payer.payerCountry = this.contract.declarant.declarantCountry;
    this.contract.payer.payerAddressLegalRu = this.contract.declarant.declarantAddressLegalRu;
    this.contract.payer.payerBankName = this.contract.declarant.declarantBank;
    this.contract.payer.payerBankIik = this.contract.declarant.declarantBankIik;
    this.contract.payer.payerBankCurr = this.contract.declarant.declarantBankIik;
    this.contract.declarantBankSwift = this.contract.declarant.declarantBankSwift;

  }

  onChildManufacturChanged()
  {
    console.log('Change is work')
    this.contract.payer.id = this.contract.manufactur.id;
    this.contract.payer.detailId = this.contract.manufactur.detailId;
    this.contract.payer.IdNumber = this.contract.declarant.IdNumber;
    this.contract.payer.isPayerRes = this.contract.declarant.isDecRes;
    this.contract.payer.payerOrgForm = this.contract.manufactur.manufacturOrgForm;
    this.contract.payer.payerNameRu = this.contract.manufactur.manufacturNameRu;
    this.contract.payer.payerCountry = this.contract.manufactur.manufacturCountry;
    this.contract.payer.payerAddressLegalRu = this.contract.manufactur.manufacturAddressLegalRu;
    this.contract.payer.payerBankName = this.contract.manufactur.manufacturBankName;
    this.contract.payer.payerBankIik = this.contract.manufactur.manufacturBankIik;
    this.contract.payer.payerBankCurr = this.contract.manufactur.manufacturCurr;
    this.contract.declarantBankSwift = this.contract.manufactur.manufacturBankSwift;

  }
}
