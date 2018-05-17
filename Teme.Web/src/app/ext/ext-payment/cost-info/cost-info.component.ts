import {Component, ElementRef, forwardRef, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {NG_VALIDATORS, NG_VALUE_ACCESSOR} from '@angular/forms';
import {IconExtModal} from 'app/shared/icon/icon-ext-modal';
import {TemplateValidation} from 'app/shared/TemplateValidation';
import {NgbDateParserFormatter, NgbDatepickerI18n} from '@ng-bootstrap/ng-bootstrap';
import {NgbDatePTParserFormatter} from '../../../shared/datepicker/NgbDatePTParserFormatter';
import {CustomDatepickerI18n, I18n} from '../../../shared/datepicker/CustomDatepickerI18n';
import {ExtPaymentService} from '../ext-payment.service';

@Component({
  selector: 'app-cost-info',
  templateUrl: './cost-info.component.html',
  styleUrls: ['./cost-info.component.scss'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => CostInfoComponent),
    multi: true
  }, {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => CostInfoComponent),
    multi: true
  },
    IconExtModal, ExtPaymentService,
    [I18n, { provide: NgbDatepickerI18n, useClass: CustomDatepickerI18n }],
    [{provide: NgbDateParserFormatter, useClass: NgbDatePTParserFormatter}],
  ]
  //encapsulation: ViewEncapsulation.None
})
export class CostInfoComponent extends TemplateValidation {
  // public dateMask = [ /[1-9]/, /\d/, '.', /\d/, /\d/, '.', /\d/, /\d/, /\d/, /\d/];
  isNotRegistred = false;
  @Input() showErrors = false;
  @Input() costInfoModal: any;

  applicationTypeId: string = "1";


  constructor(public iconModal:  IconExtModal//, private paymentService: ExtPaymentService
  ) {
    super();
  }


  ngOnInit() {
    this.model = this.costInfoModal;
  }
  // getTest(){
  //
  //   var changeModelHead = ({'id': 3, 'classname': 'Teme.Shared.Data.Context.Payment', 'fields': {CardNumber: "123"}});
  //   this.paymentService.changeModel(changeModelHead)
  //     .subscribe(
  //       (data) => console.log(data),
  //       error => console.log(error)
  //     );
  // }



}


