import {Component, forwardRef, Input, OnInit, ViewChild} from '@angular/core';
import {
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR
} from "@angular/forms";
import {TemplateValidation} from '../../../../shared/TemplateValidation';
import {RefService} from '../ext-ref-sevice';

@Component({
  selector: 'app-ext-payer',
  templateUrl: './ext-payer.component.html',
  styleUrls: ['./ext-payer.component.css'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => ExtPayerComponent),
    multi: true
  }, {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => ExtPayerComponent),
    multi: true
  },
    RefService]
})
export class ExtPayerComponent extends TemplateValidation {
  isAddOrgForm = false;
  isAddBankName = false;

  public orgFormVar;
  resOrgFormId:any;

  public bankVar;
  bankId:any;

  public countryVarNoRes;
  public countryVarRes;

  public  currencyVar;

  constructor(private refService: RefService) { super();
    this.getOrgForm();
    this.getBanks();
    this.getCountryNoRes();
    this.getCountry();
    this.getCurrency();}

  ngOnInit() {
  }
  @Input() showErrors = false;

  addOrgForm()
  {
    this.isAddOrgForm = true;
  }

  addBankName()
  {
    this.isAddBankName = true;
  }

  saveOrgForm(nameKz:string,nameRu:string){
    this.refService.saveOrgForm(nameKz,nameRu)
      .toPromise()
      .then(response => {
        console.log(response);
        this.resOrgFormId =response;
        return this.resOrgFormId;
      })
      .then (response => {
          this.orgFormVar.push({id: response as number, code: null, nameRu: nameKz, nameKz: nameRu});
          this.isAddOrgForm = false;
        }
      )
      .catch (err=>
        {
          console.error(err);
        }
      )
    ;
    console.log("step 1");


  }

  declineOrgForm()
  {
    this.isAddOrgForm = false;
  }


  getOrgForm() {
    this.refService.getOrgForm()
      .subscribe(
        data=> {
          this.orgFormVar = data;},
        (err) =>
          console.error(err),
        () =>
          console.log('done loading orgForm')
      );

  }

  saveBank(nameKz:string,nameRu:string){
    var res =  this.refService.saveBank(nameKz,nameRu)
      .toPromise()
      .then(response => {
        console.log(response);
        this.bankId =response;
        return this.bankId;
      })
      .then (response => {
          this.bankVar.push({id: response as number, code: null, nameRu: nameKz, nameKz: nameRu});
          console.log(res);
          this.isAddBankName = false;
        }
      )
      .catch (err=>
        {
          console.error(err);
        }
      )
    ;
    console.log("step 1");


  }

  declineBank()
  {
    this.isAddBankName = false;
  }


  getBanks() {
    this.refService.getBank()
      .subscribe(
        data=> {
          this.bankVar = data;  this.isAddBankName = false;    },
        (err) =>
          console.error(err),
        () =>
          console.log('done loading Bank')
      );

  }


  getCountryNoRes() {
    this.refService.getCountry()
      .subscribe(
        data=> {
          this.countryVarNoRes = data;      },
        (err) =>
          console.error(err),
        () =>
          console.log('done loading Country')
      );

  }

  getCountry() {
    this.refService.getCountry()
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
    this.refService.getCurrency()
      .subscribe(
        data=> {
          this.currencyVar = data;      },
        (err) =>
          console.error(err),
        () =>
          console.log('done loading Currency')
      );

  }

}
