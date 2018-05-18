import {Component, EventEmitter, OnInit, Output, ViewChild, ViewEncapsulation} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {RefIntContractService} from '../../int-contract-service';
import {IntManufacturerComponent} from '../int-manufacturer/int-manufacturer.component';
import {IntDeclarantComponent} from '../int-declarant/int-declarant.component';
import {IntPayerComponent} from '../int-payer/int-payer.component';
import {IntCostComponent} from '../int-cost/int-cost.component';
import {Action} from 'rxjs/scheduler/Action';

@Component({
  selector: 'app-int-card',
  templateUrl: './int-card.component.html',
  styleUrls: ['./int-card.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [RefIntContractService]
})
export class IntCardComponent implements OnInit {
  type: string;
  @Output() idContractEvent = new EventEmitter<string>();
  @Output() idManufacturEvent = new EventEmitter<string>();
  @Output() idDeclarantEvent = new EventEmitter<string>();
  @Output() idPayerEvent = new EventEmitter<string>();

  @ViewChild(IntManufacturerComponent) manufacturChild:IntManufacturerComponent;
  @ViewChild(IntDeclarantComponent) declarantChild:IntDeclarantComponent;
  @ViewChild(IntPayerComponent) payerChild:IntPayerComponent;
  @ViewChild(IntCostComponent) costChild:IntCostComponent;

  public idContract;
  public idManufactur;
  public idDeclarant;
  public idPayer;
  _workflowid:string;
  _prompt:string;

  public MeetRequirements:boolean=true;
  public NotMeetRequirements:boolean=true;

  constructor(private route: ActivatedRoute,private refIntService: RefIntContractService) { this.type = 'manufacturer'}

  ngOnInit() {
    this.route.parent.params
      .subscribe(params => {
        this.idContract = params.id;
        this.getContractById();
        this.idContractEvent.emit(this.idContract);

      })

  }

  setDeclarationTab(name: string) {
    this.type = name;
  }

  getContractById()
  {
    let responseData;
    this.refIntService.GetContractById(this.idContract)
      .toPromise()
      .then(response => {
        responseData = response;
        console.log(responseData);
        console.log("looggss");
        this.idManufactur = responseData.manufacturId;
        this.idDeclarant = responseData.declarantId;
        this.idPayer = responseData.payerId;
        this._workflowid  = responseData.workflowId;
        this.idManufacturEvent.emit(this.idManufactur);
        this.idDeclarantEvent.emit(this.idDeclarant);
        this.idPayerEvent.emit(this.idPayer);
        this.manufacturChild.onViewChangeContract(this.idManufactur);
        this.declarantChild.onViewChangeContract(this.idDeclarant);
        this.payerChild.onViewChangeContract(this.idPayer);
        this.costChild.onViewChangeContract();
        this.ViewActions();
      })
      .catch(err => {
          console.error(err);
        }
      )

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
      NameIMNKz: null,
      isImport:null,
    },

  };
  CozExecutorAgreedRequest()
  {
    let responseData;
    this.refIntService.CozExecutorAgreedRequest(this._prompt,'MeetRequirements', this._workflowid)
      .toPromise()
      .then(response => {
        responseData = response;
        console.log(responseData);
        this.ViewActions();
      })
      .catch(err => {
          console.error(err);
        }
      )
  };

  CozExecutorNotAgreedRequest()
  {
    let responseData;
    this.refIntService.CozExecutorNotAgreedRequest(this._prompt,'NotMeetRequirements', this._workflowid)
      .toPromise()
      .then(response => {
        responseData = response;
        console.log(responseData);
        this.ViewActions();

      })
      .catch(err => {
          console.error(err);
        }
      )

  };




  ViewActions()
  {
    let responseData;
    this.refIntService.GetViewActions(this._workflowid)
      .toPromise()
      .then(response => {
        responseData = response;
        console.log("brrrrrrrrrrr");
        console.log(responseData);
        this._prompt = responseData[0].prompt;
        let keys = Object.keys(responseData[0].options);
        console.log("key",keys);
        keys.forEach(key => {
          let val = responseData[0].options[key];
          console.log('val='+val);
          switch(val) {
            case 'MeetRequirements': {
              this.MeetRequirements = false;
              break;
            }
            case 'NotMeetRequirements': {
              this.NotMeetRequirements = false;
              break;
            }
            default: {
              null;
              break;
            }
          }


        });

      })
      .catch(err => {
          console.error(err);
        }
      )
  }


}
