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

  constructor() {
    this.type = 'cost-info';
  }

  ngOnInit() {
  }
  setDeclarationTab(name: string) {
    this.type = name;
  }
  sendPaymentRequest(validate) {
    this.showAllErr = true;
  }

  onSubmit(form: FormsModule) {
    // element.all(by.tagName('app-hero-parent'))
  }



  public declaration: any = {
    costInfo: {
      contractForm: "1",
      contractNumber: null,
      conclusionBeginDate: null,
      conclusionEndDate: null,
      registrationNumber: null
    },
    data: {
      nameKz: null,
    },
    equipment: {
      additional: null,
    },
    manufacturer: {
      additional: null,
    },
    subject: {
      additional: null,
    },
    attacments: {
      additional: null,
    },
    signing: {
      additional: null,
    },
    testPayment: {
      testSelect: null,
      contractForm: null
    }
  };
  //отключение заставки
  ngAfterViewInit(){
    document.getElementById('preloader').classList.add('hide');
  }

  public dropdownValues: any = [{
    value: "v1", label: "f1"
  },{
    value: "v2", label: "f2"
  }]

  getTestVar(){
    console.log(this.declaration);
  }
}
