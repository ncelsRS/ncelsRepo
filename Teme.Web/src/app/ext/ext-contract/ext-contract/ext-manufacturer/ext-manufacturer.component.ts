import {Component, Input, forwardRef, Injectable} from '@angular/core';


import {
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR
 } from "@angular/forms";
import {TemplateValidation} from '../../../../shared/TemplateValidation';
import {RefService} from '../ext-ref-sevice';

@Component({
  selector: 'app-ext-manufacturer',
  templateUrl: './ext-manufacturer.component.html',
  styleUrls: ['./ext-manufacturer.component.css'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => ExtManufacturerComponent),
    multi: true
  }, {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => ExtManufacturerComponent),
    multi: true
  }, RefService]
})
@Injectable()
export class ExtManufacturerComponent extends TemplateValidation  {

   isAddOrgForm = false;
   isAddBankName = false;

  public orgFormVar;
  resOrgFormId:any;

  public bankVar;
  bankId:any;

  public countryVarNoRes;
  public countryVarRes;

  public  currencyVar;


  @Input() prnRegisterType: string;
   @Input() showErrors = false;

  constructor(private refService: RefService){
    super();
    this.getOrgForm();
    this.getBanks();
    this.getCountryNoRes();
    this.getCountry();
    this.getCurrency();
  }

  OnInit()
  {

  }

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
          this.bankVar = data;      },
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
