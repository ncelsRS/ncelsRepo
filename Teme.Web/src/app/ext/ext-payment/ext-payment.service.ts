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
    return this.http.post(this.urlPayment + '/Payment/Create', body);
    //http://localhost:60825/Payment/Create
  }




}

  // getData(){
  //   return this.http.get('user.json')
  // }

  // postData(user: User){
  //   const body = {name: user.name, age: user.age};
  //   return this.http.post('http://localhost:60820/api/values', body);
  // }






  // private data: Phone[] = [
  //   { name:"Apple iPhone 7", price: 56000},
  //   { name: "HP Elite x3", price: 56000},
  //   { name: "Alcatel Idol S4", price: 25000}
  // ];
  // getData(): Phone[] {
  //
  //   return this.data;
  // }
  // addData(name: string, price: number){
  //
  //   this.data.push(new Phone(name, price));
  // }

