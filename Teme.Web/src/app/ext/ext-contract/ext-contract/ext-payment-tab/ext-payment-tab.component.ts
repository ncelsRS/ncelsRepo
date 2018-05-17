import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {ExtPaymentService} from '../../../ext-payment/ext-payment.service';
import {SmartTableButtonViewComponent} from '../../../../shared/smart-table-button-view.component';
import { Pipe, PipeTransform } from '@angular/core';
import {DomSanitizer,SafeResourceUrl,} from '@angular/platform-browser';
import {PaymentDto} from '../../../ext-payment/payment-dto';

@Component({
  selector: 'app-ext-payment-tab',
  templateUrl: './ext-payment-tab.component.html',
  styleUrls: ['./ext-payment-tab.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [ExtPaymentService]
})

//@Pipe({ name: 'safe' })
export class ExtPaymentTabComponent implements OnInit//, PipeTransform
{
  paymentId: string = '3';
  done: boolean = false;
  urlPayment:SafeResourceUrl;
  constructor(private paymentService: ExtPaymentService, private sanitizer: DomSanitizer) {

  }

  ngOnInit() {
    let contractId="3";
    this.paymentService.getListPayments(contractId).subscribe((data)=> {
      this.paymentData = data;
    });
  }



  createPayment(){
    this.paymentService.createPayment(3)
      .subscribe(
        (data: number) => {
           this.setUrlAndModalShow(data.toString());
         },
        error => console.log(error)
      );
  }

  public setUrlAndModalShow(paymentId:string){
    this.paymentId=paymentId;
    this.urlPayment = this.sanitizer.bypassSecurityTrustResourceUrl("ext/payment?paymentId=" + this.paymentId )
    jQuery('#frameModal').modal('show');
  }
  public paymentData:any;
  // public paymentData = [{
  //   rowNumber:1,
  //   numberPayment: 'Наименование',
  //   contractForm: '12344',
  //   sendDate: 'Модель',
  //   status: 'Производитель',
  //   action: 'Страна',
  // }];
  public paymentSettings = {
    selectMode: 'single',  //single|multi
    hideHeader: false,
    hideSubHeader: false,
    actions: false,
    columns: {
      id: {
        title: '№',
        editable: false,
        width: '60px',
        type: 'html',
        valuePrepareFunction: (value) => { return '<div class="text-center">' + value + '</div>'; }
      },
      numberPayment: {
        title: 'Номер заявки на платеж',
        type: 'string'
      },
      contractForm: {
        title: 'Тип заявки на платеж',
        type: 'string'
      },
      sendDate: {
        title: 'Дата первой отправки',
        type: 'string'
      },
      status: {
        title: 'Статус',
        type: 'number'
      },
      action: {
        title: 'Действия',
        type: 'custom',
        renderComponent: SmartTableButtonViewComponent,
        onComponentInitFunction: (instance: any) => {
          console.log("instance", instance, instance.view);
          instance.edit.subscribe(row => {
            console.log(`${row.id} view is work!`);
            this.setUrlAndModalShow(row.id.toString());

          });
        }
        // onComponentInitFunction(instance) {
        //   console.log("instance", instance, instance.view);
        //   instance.edit.subscribe(row => {
        //     console.log(`${row.id} view is work!`);
        //     this.setUrlAndModalShow(row.id.toString());
        //
        //   });
        // }
      }
    },
    pager: {
      display: true,
      perPage: 5
    }
  };


  public onDeleteConfirm(event): void {
    if (window.confirm('Are you sure you want to delete?')) {
      event.confirm.resolve();
    } else {
      event.confirm.reject();
    }

  }

  getTest(){
    console.log('paymentData',this.paymentData)
  }


}
