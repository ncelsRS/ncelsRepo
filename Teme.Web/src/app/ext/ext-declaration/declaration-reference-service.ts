import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Injectable} from '@angular/core';

const httpOptions = {
  headers: new HttpHeaders({'Content-Type': 'application/json'})
};

@Injectable()
export class DeclarationReferenceService {
  url = 'http://localhost:5121/api/Reference/';

  constructor(private http: HttpClient) {
  }

  public getClassifierMedicalArea(){
    let promise = this.http.get(this.url + 'ClassifierMedicalArea').toPromise();
    return promise;
  }

  public getDegreeRiskClass(){
    let promise = this.http.get(this.url + 'DegreeRiskClass').toPromise();
    return promise;
  }

  public getStorageCondition(){
    let promise = this.http.get(this.url + 'StorageCondition').toPromise();
    return promise;
  }

  public getOrganizationForm(){
    let promise = this.http.get(this.url + 'OrganizationForm').toPromise();
    return promise;
  }

  public getCountry(){
    let promise = this.http.get(this.url + 'Country').toPromise();
    return promise;
  }


}
