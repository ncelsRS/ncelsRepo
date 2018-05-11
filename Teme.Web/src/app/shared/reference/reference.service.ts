import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';

@Injectable()
export class ReferenceService{
  contractFormUrl:string = environment.urls.reference

  constructor(private http: HttpClient){ }


  getContractForm(){
    return this.http.get(this.contractFormUrl + 'ContractForm')
  }
}
