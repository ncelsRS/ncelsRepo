import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import 'rxjs/add/operator/map';
import 'rxjs/Rx';
import 'rxjs/add/operator/map';

@Injectable()
export class RefService  {

  url="http://localhost:5121/api/reference/";

  constructor(private http: HttpClient){ }

  saveOrgForm(nameKz:string,nameRu:string){

    return  this.http.get(this.url+"SaveOrganizationForm", {
      params: {
        nameRu: nameKz,
        nameKz: nameRu
      },
      headers:   { "Access-Control-Allow-Origin": "*",
        "Access-Control-Allow-Methods": "GET, POST, OPTIONS, PUT, PATCH, DELETE",
        "Content-Type": "application/json; charset=utf-8",
        "Accept": "application/json",
        "Access-Control-Allow-Credentials": "true",
      },
      observe: 'response'
    })

  }

  getOrgForm(){
    return  this.http.get(this.url+"OrganizationForm");
  }

  saveBank(nameKz:string,nameRu:string){

    return  this.http.get(this.url+"SaveBank", {
      params: {
        nameRu: nameKz,
        nameKz: nameRu
      },
      headers:   { "Access-Control-Allow-Origin": "*",
        "Access-Control-Allow-Methods": "GET, POST, OPTIONS, PUT, PATCH, DELETE",
        "Content-Type": "application/json; charset=utf-8",
        "Accept": "application/json",
        "Access-Control-Allow-Credentials": "true",
      },
      observe: 'response'
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
}
