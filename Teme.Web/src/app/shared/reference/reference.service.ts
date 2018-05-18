import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';

@Injectable()
export class ReferenceService{
  referenceUrl:string = environment.urls.admin + '/api/reference/';

  constructor(private http: HttpClient){ }

  getContractForm(){
    return this.http.get( this.referenceUrl + 'ContractForm')
  }

  getCountry(){
    return this.http.get( this.referenceUrl + 'Country')
  }

  getCalculatorServiceType(applicationTypeId:string){
    //const params = {applicationTypeId: applicationTypeId};
    return this.http.get( this.referenceUrl + 'CalculatorServiceType',{ params: {applicationTypeId: applicationTypeId}} )
  }

  getReferenceStandart(referanceName:string){
    return this.http.get( this.referenceUrl + referanceName);
  }
}
