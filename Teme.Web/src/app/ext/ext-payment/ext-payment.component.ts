import {Component, OnInit, ViewEncapsulation, HostListener, ViewChild} from '@angular/core';
import {FormsModule} from '@angular/forms';


@Component({
  selector: 'app-pages',
  templateUrl: './ext-payment.component.html',
  styleUrls: ['./ext-payment.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ExtPaymentComponent implements OnInit {
  public showAllErr = false;
  type: string;

  public declaration: any = {
    CostInfo: {
      contractNumber: null,
    },
    producer: {
      producer: null,
    },
    journal: {
      additional: null,
    }
  };

  constructor() {
    this.type = 'cost-info';
  }

  ngOnInit() {
  }
  setDeclarationTab(name: string) {
    this.type = name;
  }

  //@ViewChild('paymentForm') form;
  sendPaymentRequest(validate) {
    console.log("sendPaymentRequest");
    this.showAllErr = true;
  }

  //отключение заставки
  ngAfterViewInit(){
    document.getElementById('preloader').classList.add('hide');
  }

  onSubmit(form: FormsModule) {
    // element.all(by.tagName('app-hero-parent'))
  }




}
