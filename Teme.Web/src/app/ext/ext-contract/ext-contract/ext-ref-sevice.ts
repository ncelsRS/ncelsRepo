import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import 'rxjs/add/operator/map';
import 'rxjs/Rx';
import 'rxjs/add/operator/map';
import {Headers} from '@angular/http';
import {environment} from '../../../../environments/environment';
import {Observable} from 'rxjs/Observable';

@Injectable()
export class RefService  {

  url= environment.urls.admin + "/api/Reference/";
  urlData=environment.urls.extContract + "/Contract/";


  constructor(private http: HttpClient){
    this.getContractForm();
  }

  saveOrgForm(nameKz:string,nameRu:string):Observable<any>{

    return  this.http.get(this.url+"SaveOrganizationForm", {
      params: {
        nameRu: nameKz,
        nameKz: nameRu
      },
      // headers:   { "Access-Control-Allow-Origin": "*",
      //   "Access-Control-Allow-Methods": "GET, POST, OPTIONS, PUT, PATCH, DELETE",
      //   "Content-Type": "application/json; charset=utf-8",
      //   "Accept": "application/json",
      //   "Access-Control-Allow-Credentials": "true",
      // },
      // observe: 'response'
    });

  }

  getOrgForm(){
    return  this.http.get(this.url+"OrganizationForm");
  }

  saveBank(nameKz:string,nameRu:string){

    return  this.http.get(this.url+"SaveBank", {
      params: {
        nameRu: nameRu,
        nameKz: nameKz
      },
      // headers:   { "Access-Control-Allow-Origin": "*",
      //   "Access-Control-Allow-Methods": "GET, POST, OPTIONS, PUT, PATCH, DELETE",
      //   "Content-Type": "application/json; charset=utf-8",
      //   "Accept": "application/json",
      //   "Access-Control-Allow-Credentials": "true",
      // },
      //bserve: 'response'
    })

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
       //,
      // headers:   { "Access-Control-Allow-Origin": "*",
      //   "Access-Control-Allow-Methods": "GET, POST, OPTIONS, PUT, PATCH, DELETE",
      //   "Content-Type": "application/json; charset=utf-8",
      //   "Accept": "application/json",
      //   "Access-Control-Allow-Credentials": "true",
      // }
    })

  }

  getCalculatorServiceType(applicationTypeId:string){

    return  this.http.get(this.url+"CalculatorServiceType",{
      params: {
        applicationTypeId: applicationTypeId,

      }
      //,
      // headers:   { "Access-Control-Allow-Origin": "*",
      //   "Access-Control-Allow-Methods": "GET, POST, OPTIONS, PUT, PATCH, DELETE",
      //   "Content-Type": "application/json; charset=utf-8",
      //   "Accept": "application/json",
      //   "Access-Control-Allow-Credentials": "true",
      // }
    })

  }

  getShowPriceList(isImport:string, serviceTypeId: string, serviceTypeModifId?: string){

    return  this.http.get(this.url+"ShowPriceList",{
      params: {
        isImport: isImport,
        serviceTypeId:serviceTypeId,
        serviceTypeModifId: serviceTypeModifId
      }
      //,
      // headers:   { "Access-Control-Allow-Origin": "*",
      //   "Access-Control-Allow-Methods": "GET, POST, OPTIONS, PUT, PATCH, DELETE",
      //   "Content-Type": "application/json; charset=utf-8",
      //   "Accept": "application/json",
      //   "Access-Control-Allow-Credentials": "true",
      // }
    })

  }

  createContract(cntType, cntScope){
    console.log(cntType+"   "+cntScope)
    //let headers = new Headers({ 'Content-Type': 'application/json' });
    return  this.http.post(this.urlData+"Create", {
        params: {
          contractType: cntType,
          contractScope: cntScope
        },
        headers: {
          // "Access-Control-Allow-Origin": "*",
          // "Access-Control-Allow-Methods": "GET, POST, OPTIONS, PUT, PATCH, DELETE",
          "Content-Type": "application/json; charset=utf-8",

        }
      }
    // ,{
    //     headers: {
    //       "Access-Control-Allow-Origin": "*",
    //         "Access-Control-Allow-Methods": "GET, POST, OPTIONS, PUT, PATCH, DELETE",
    //         "Content-Type": "application/json; charset=utf-8",
    //         "Accept": "application/json",
    //         "Access-Control-Allow-Credentials": "true",
    //     }
    //   }
    );
  }

  changeModel(obj){
    console.log(obj);
     return  this.http.put(this.urlData+"ChangeModel",obj
       //{ //,
    //   headers:   { "Access-Control-Allow-Origin": "",
    //     "Access-Control-Allow-Methods": "GET, POST, OPTIONS, PUT, PATCH, DELETE",
    //     "Content-Type": "application/json; charset=utf-8",
    //     "Accept": "application/json",
    //     "Access-Control-Allow-Credentials": "true",
    //   }}
    );
    //console.log(obj);
  }

  AddDeclarant(contractId,code){
    return  this.http.get(this.urlData+"AddDeclarant",{
      params: {
        contractId: contractId,
        serviceTypeId:code
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
      }//,
      // headers:   { "Access-Control-Allow-Origin": "*",
      //   "Access-Control-Allow-Methods": "GET, POST, OPTIONS, PUT, PATCH, DELETE",
      //   "Content-Type": "application/json; charset=utf-8",
      //   "Accept": "application/json",
      //   "Access-Control-Allow-Credentials": "true",
      // }


    });
  }
}
