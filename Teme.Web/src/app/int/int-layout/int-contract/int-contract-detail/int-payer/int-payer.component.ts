import { Component, OnInit, ViewEncapsulation, Input } from '@angular/core';
import {RefIntContractService} from '../../int-contract-service';

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
  public listVarRes;
  @Input() idContractChild:string;
  @Input() idPayerIn:string;

  public model:any = {
    id:null,
    detailId:null,
    ChoosePayer:null,
    idNumber: null,
    isPayerRes:null,
    isPayerJuridical:null,
    payerNameRu: null,
    payerOrgForm: null,
    payerCountry: null,
    payerAddressLegalRu: null,
    payerBankName: null,
    payerBankIik: null,
    payerBankSwift: null,
    payerBankCurr: null,
  };

  constructor(private refIntService: RefIntContractService) {
    this.getBanks();
    this.getCountry();
    this.getCurrency();
    this.getOrgForm();


  }

  ngOnInit() {
  }

  loadData(data)
  {
    let dataArray=[];
    dataArray.push(data);

    this.model.id = dataArray[0].Id;
    this.model.isPayerRes = (dataArray[0].isResident)?'res':'unres';
    this.model.isPayerJuridical = dataArray[0].Id;

    this.model.idNumber = dataArray[0].idNumber;
    this.model.payerOrgForm = dataArray[0].organizationFormId;
    this.model.payerNameRu = dataArray[0].nameRu;
    this.model.payerCountry = dataArray[0].countryId;
    this.model.detailId = dataArray[0].declarantDetailDto.id;
    this.model.payerAddressLegalRu = dataArray[0].declarantDetailDto.legalAddress;
    this.model.payerBankName = dataArray[0].declarantDetailDto.bankName;
    this.model.payerBankIik = dataArray[0].declarantDetailDto.bankIik;
    this.model.payerBankCurr = dataArray[0].declarantDetailDto.currencyId;
    this.model.declarantBankSwift = dataArray[0].declarantDetailDto.bankSwift;


  }

  onViewChangeContract(idPayer)
  {
    this.GetDeclarantById(idPayer);
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
