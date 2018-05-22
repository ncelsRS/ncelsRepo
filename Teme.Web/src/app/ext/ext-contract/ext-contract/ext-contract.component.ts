import {Component, OnInit, Output, EventEmitter, ViewChild, Input, ViewEncapsulation} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {ExtManufacturerComponent} from './ext-manufacturer/ext-manufacturer.component';
import {ExtDeclarantComponent} from './ext-declarant/ext-declarant.component';
import {ExtPayerComponent} from './ext-payer/ext-payer.component';
import {ExtCostComponent} from './ext-cost/ext-cost.component';
import {RefService} from './ext-ref-sevice';
import {TemplateValidation} from '../../../shared/TemplateValidation';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-ext-contract',
  templateUrl: './ext-contract.component.html',
  styleUrls: ['./ext-contract.component.css'],
  providers: [RefService],

})
export class ExtContractComponent  extends TemplateValidation {


  public ramoch;
  @ViewChild(ExtManufacturerComponent) manufacturChild:ExtManufacturerComponent;
  @ViewChild(ExtDeclarantComponent) declarantChild:ExtDeclarantComponent;
  @ViewChild(ExtPayerComponent) payerChild:ExtPayerComponent;
  @ViewChild(ExtCostComponent) costChild:ExtCostComponent;

  selectedLevel: string;
  @Input()  showAllErr = false;
  @Input()  sendCozWithOutKey = false;
  @Output() selectedLevelChange = new EventEmitter<string>();
  @Output() idContractEvent = new EventEmitter<string>();
  @Output() viewEvent = new EventEmitter<string>();
  @Input() childType: string;
  @Output() onChangeViewType = new EventEmitter<string>();
  public idContract;
  _idworkflow:string;
  _contractType:string;
  public viewCostStatus:boolean = false;
  public viewPaymentStatus:boolean = false;
  public viewtype: string;

  type: string;
  public id: string;
  public items = [];
  public holderTypeItems = [];
  public contractFormVar;
  public holderTypeVar;
  changeModelHead;

  constructor(private route: ActivatedRoute, private refService: RefService,
              private toastr: ToastrService) {
    super();
    this.getContactForm();
    this.getHolderType();
    this.type = 'manufacturer';
  }

  setDeclarationTab(name: string) {
    this.type = name;
  }


  changeLevel(lev) {
    this.onChangedModel(lev.target);
    this.selectedLevel = lev.target.value;
    this.selectedLevelChange.emit(this.selectedLevel);

  }

  ngOnInit(){
    this.route.params
      .subscribe(params => {
        this.idContract = params.idContract;
        this.idContractEvent.emit(this.idContract);
        this._idworkflow  = params.workflowId;
        this.viewtype = params.viewtype;
        this._contractType = params.contractType;
        console.log(params);
        this.viewAction();

      });
    console.log(this.id);

  }



  public contract: any = {
    contractData:{
      holderType: null,
      contractForm:null
    },
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
      payerNoResCountry: null,
      payerAddressLegalRu: null,
      payerBankName: null,
      payerBankIik: null,
      payerBankBin:null,
      payerBankSwift: null,
      payerBankCurr: null,
    },

    cost: {
      id:null,
      applicationType: null,
      serviceType: null,
      NameIMNRu: null,
      NameIMNKz: null,
      isImport:null,

      serciveCount:null,
    },

  };


  @ViewChild(ExtManufacturerComponent)
  private ExtManufactur: ExtManufacturerComponent;

  @ViewChild(ExtDeclarantComponent)
  private ExtDeclarant: ExtDeclarantComponent;

  @ViewChild(ExtPayerComponent)
  private ExtPayer: ExtPayerComponent;

  @ViewChild(ExtCostComponent)
  private ExtCost: ExtCostComponent;

  sendToNcels() {
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
        item.nameRu = 'Заявитель';
        item.nameKz = 'Жолдаушы';

      }

    }

  }

  viewAction()
  {
    if(this.viewtype == 'view')
    {
      this.getContractById()
      if(this._contractType === "1") {
        this.viewPaymentStatus = true;
        this.viewCostStatus = false;
      }
      if(this._contractType === "2") {
        this.viewPaymentStatus = true;
        this.viewCostStatus = true;
      }

    }
    else if(this.viewtype == 'edit') {
      this.getContractById();
      if(this._contractType === "1") {
        this.viewPaymentStatus = true;
        this.viewCostStatus = false;
      }
      if(this._contractType === "2") {
        this.viewPaymentStatus = true;
        this.viewCostStatus = true;
      }


    }
    else if(this.viewtype === 'create')
    {
        if(this._contractType === "1") {
        this.viewPaymentStatus = true;
        this.viewCostStatus = false;
      }
        if(this._contractType === "2") {
        this.viewPaymentStatus = true;
        this.viewCostStatus = true;
      }
    }
  }

    getContractById()
    {
      let responseData;
    this.refService.GetContractById(this.idContract)
      .toPromise()
      .then(response => {
        console.log(response);
        responseData = response;
        this.contract.contractData.holderType = responseData.holderType;
        this.contract.contractData.contractForm = responseData.contractForm;
        this.contract.manufactur.id = responseData.manufacturId;
        this.contract.manufactur.detailId = responseData.manufacturDetailId;
        this.contract.declarant.id =  responseData.declarantId;
        this.contract.declarant.detailId = responseData.declarantDetailId;
        this.contract.payer.id = responseData.payerId;
        this.contract.payer.detailId = responseData.payerDetailId;
        this._contractType = responseData.contractType;
        this._idworkflow = responseData.workflowId;
        this.viewEvent.emit(this.viewtype);
        this.manufacturChild.onViewChangeContract();
        this.declarantChild.onViewChangeContract();
        this.payerChild.onViewChangeContract();

        //this.costChild.onViewChangeContract();
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
    this.contract.declarant.IdNumber = this.contract.manufactur.idNumber;
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
    this.contract.payer.payerBankCurr = this.contract.declarant.declarantBankCurr;
    this.contract.payer.payerBankSwift = this.contract.declarant.declarantBankSwift;
    this.contract.payer.payerBankBin = this.contract.declarant.IdNumber;

  }

  onChildManufacturChanged()
  {
    console.log('Change is work')
    this.contract.payer.id = this.contract.manufactur.id;
    this.contract.payer.detailId = this.contract.manufactur.detailId;
    this.contract.payer.IdNumber = this.contract.manufactur.idNumber;
    this.contract.payer.isPayerRes = this.contract.manufactur.isRes;
    this.contract.payer.payerOrgForm = this.contract.manufactur.manufacturOrgForm;
    this.contract.payer.payerNameRu = this.contract.manufactur.manufacturNameRu;
    this.contract.payer.payerCountry = this.contract.manufactur.manufacturCountry;
    this.contract.payer.payerAddressLegalRu = this.contract.manufactur.manufacturAddressLegalRu;
    this.contract.payer.payerBankName = this.contract.manufactur.manufacturBankName;
    this.contract.payer.payerBankIik = this.contract.manufactur.manufacturBankIik;
    this.contract.payer.payerBankCurr = this.contract.manufactur.manufacturCurr;
    this.contract.declarantBankSwift = this.contract.manufactur.manufacturBankSwift;
    this.contract.payer.payerBankBin = this.contract.declarant.idNumber;

  };

  onChangedModel(evnt) {


      this.changeModelHead = ({
        'id': this.idContract,
        'classname': 'Teme.Shared.Data.Context.Contract', 'fields': {[evnt.name]: evnt.value}
      })
      this.changedModelRef(this.changeModelHead);

  }

  changedModelRef(obj)
  {
    this.refService.changeModel(obj)
      .toPromise()
      .then(response => {
        console.log(response);
      })
      .catch (err=>
        {
          console.error(err);
        }
      )
    ;

  }

  SendCozWithOutKey()
  {
    console.log(this._contractType);
    this.refService.SendOrRemoveSendWithoutSign(this._idworkflow,this._contractType)
      .toPromise()
      .then(response => {
        console.log(response);
        this.toastr.success('Договор отправлен в ЦОЗ!', 'Уведомление!');
      })
      .catch (err=>
        {
          console.error(err);
        }
      )
    ;
  }
}
