import {Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Observable} from "rxjs/Observable";
import 'rxjs/add/operator/map';

@Injectable()
export class RefService {

  res:any=[];

  private headers = new Headers({
    'Access-Control-Allow-Origin': '*',
    'Access-Control-Allow-Methods': 'GET, POST, OPTIONS, PUT, PATCH, DELETE',
    'Access-Control-Allow-Headers': 'Content-Type, Authorization',
    'Access-Control-Allow-Credentials': true
  });

  //let headers = new Headers();
  //headers.append('Content-Type', 'application/json');

  constructor(private http: HttpClient){ }
  //let params = new HttpParams().set("paramName","q").set("paramName2", "q");

  private url = "http://localhost:5121/api/Reference/";
  //constructor(private http: HttpClient){ }

  saveManufacturOrgForm(nameKz:string,nameRu:string){
    this.url=this.url+"SaveOrganizationForm";
    this.http.get(this.url, {
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
      .toPromise()
      .then(response => {
        console.log(response);
      })
      .catch(console.log);
  }


    getManufacturOrgForm() {
    this.url = "http://localhost:5121/api/Reference/OrganizationForm";
    this.res = this.http.get(this.url);
    return this.res;

  }





}
