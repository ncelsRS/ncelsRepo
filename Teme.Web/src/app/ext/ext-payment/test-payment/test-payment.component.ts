import {Component, forwardRef, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {TemplateValidation} from 'app/shared/TemplateValidation';
import {IconIntModal} from '../../../shared/icon/icon-int-modal';


@Component({
  selector: 'app-test-payment',
  templateUrl: './test-payment.component.html',
  styleUrls: ['./test-payment.component.scss'],
  providers: [
    IconIntModal
  ]
})
export class TestPaymentComponent extends TemplateValidation{
  @Input() showErrors = false;

  public icons = [];

  constructor(public iconModal:  IconIntModal) {

    super()

  }
  ngOnInit() {
  }


}
