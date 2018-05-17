import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {Injectable} from '@angular/core';

@Injectable()
export class ExtPaymentService{

  urlPayment: string = environment.urls.payment;


  constructor(private http: HttpClient){ }

  createPayment(contractId : number){
    const body = {contractId: contractId};
    return this.http.post(this.urlPayment + 'Create', body);
  }

  changeModel(obj){

    return  this.http.put(this.urlPayment + "ChangeModel", obj);
  }

  getPayment(paymentId:string){
    return  this.http.get(this.urlPayment + "GetPaymentById", { params: {paymentId} });
  }

  getListPayments(contractId:string){
    return  this.http.get(this.urlPayment + "GetListPayments", { params: {contractId} });
  }
}



