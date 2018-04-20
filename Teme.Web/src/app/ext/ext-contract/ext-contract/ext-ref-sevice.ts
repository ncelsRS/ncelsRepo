import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
//import {User} from './user';
//import {Observable} from 'rxjs/Observable';

@Injectable()
export class RefService {

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

  private url = "http://10.20.44.57:81/api/Reference/OrganizationForm";
  //constructor(private http: HttpClient){ }

  saveOrgForm(){
    this.http.get(this.url, {
      // params: {
      //   nameRu: 'JSC',
      //   nameKz: 'JSC'
      // },
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






}
