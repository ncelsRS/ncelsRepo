import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import 'rxjs/add/operator/map';
import 'rxjs/Rx';
import 'rxjs/add/operator/map';
import {ContentType} from '@angular/http/src/enums';
import {environment} from '../../../../environments/environment';

@Injectable()
export class RefService  {

  url = environment.urls.reference;
  urlData = environment.urls.contract;


  constructor(private http: HttpClient){
    this.getContractForm();
  }

  saveOrgForm(nameKz:string,nameRu:string){

    return  this.http.put(this.url+"SaveOrganizationForm",  {
   },{
    params: {
      nameRu: nameKz,
      nameKz: nameRu
      // observe: 'response'
    },
      headers:   { "Access-Control-Allow-Origin": "*",
          "Access-Control-Allow-Methods": "GET, POST, OPTIONS, PUT, PATCH, DELETE",
          "Content-Type": "application/json; charset=utf-8",
          "Accept": "application/json",
          "Access-Control-Allow-Credentials": "true",
        }
    })

  }

  getOrgForm(){
    return  this.http.get(this.url+"OrganizationForm");
  }

  saveBank(nameKz:string,nameRu:string){
     let data = ({  nameRu: nameRu, nameKz: nameKz});
     console.log(data);
    console.log("rr");
    return  this.http.put(this.url+"SaveBank", data

    )

  }

  getBank(){
    return  this.http.get(this.url+"Bank");
  }

  getCountry(){
    return  this.http.get(this.url+"Country");
  }

  getCurrency(){
    return  this.http.get(this.url+"Currency");
  }

  getContractForm(){
    return  this.http.get(this.url+"ContractForm");
  }

  getChosenPayer(){
    return  this.http.get(this.url+"ChosenPayer");
  }

  getHolderType(){
    return  this.http.get(this.url+"HolderType");
  }


  getCalculatorApplicationType(contractScope:string,contractForm:string){

    return  this.http.get(this.url+"CalculatorApplicationType",{
      params: {
        contractScope: contractScope,
        contractForm: contractForm
       }
    })

  }

  getCalculatorServiceType(applicationTypeId:string){

    return  this.http.get(this.url+"CalculatorServiceType",{
      params: {
        applicationTypeId: applicationTypeId,

      }
    })

  }

  getShowPriceList(isImport:string, serviceTypeId: string, serviceTypeModifId?: string){

    return  this.http.get(this.url+"ShowPriceList",{
      params: {
        isImport: isImport,
        serviceTypeId:serviceTypeId,
        serviceTypeModifId: serviceTypeModifId
      }

    })

  }

  createContract(cntType, cntScope){
    return  this.http.post(this.urlData+"Create", {
          contractType: cntType,
          contractScope: cntScope

      }
    );
  }

  changeModel(obj){
    console.log("ww");
    console.log(obj);
     return  this.http.put(this.urlData+"ChangeModel",obj
    );

  }

  AddDeclarant(contractId,code){
    return  this.http.get(this.urlData+"AddDeclarant",{
      params: {
        contractId: contractId,
        code:code
    }});
  }

  SearchDeclarantResident(iin){
    return  this.http.get(this.urlData+"SearchDeclarantResident",{
      params: {
        iin: iin
      }});
  }

  SearchDeclarantNonResident(countryId){
    return  this.http.get(this.urlData+"SearchDeclarantNonResident",{
      params: {
        countryId: countryId
      }
    });
  }

  GetListContracts(contractScope){
    return  this.http.get(this.urlData+"GetListContracts",{
      params: {
        contractScope: contractScope
      }
    });
  }
}
