import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {Injectable} from '@angular/core';

@Injectable()
export class ExtPaymentService{

  urlPayment: string = environment.urls.payment;


  constructor(private http: HttpClient){ }

  createPayment(contractId : number){
    console.log(this.urlPayment);
    const body = {contractId: contractId};
    return this.http.post(this.urlPayment + 'Create', body);
    //http://localhost:60825/Payment/Create
  }

  changeModel(obj){
    console.log(obj);
    return  this.http.put(this.urlPayment + "ChangeModel", obj);

  }

}



