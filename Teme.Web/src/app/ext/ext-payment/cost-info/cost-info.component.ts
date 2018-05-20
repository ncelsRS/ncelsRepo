import {Component, ElementRef, EventEmitter, forwardRef, Input, OnInit, Output, ViewEncapsulation} from '@angular/core';
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

  isNotRegistred = false;
  @Input() showErrors = false;
  @Input() costInfoModal: any;
  costInfoCardBeginDate;

  applicationTypeId: string = "1";

  @Output() changeModelParent = new EventEmitter<any>();
  changeModel(evnt:any) {
    this.changeModelParent.emit(evnt);
  }
  changeModelDate(evnt:any,name:string) {
    console.log("evnt",evnt);
    //let date:Date = new Date( evnt.replace( /(\d{2})-(\d{2})-(\d{4})/, "$2/$1/$3 06:00:00") )
    let date = new Date(evnt.year, evnt.month-1 , evnt.day,6);
    if (date.toString() == 'Invalid Date') {
      const [day, month, year] = evnt.split(".")
      console.log('year', year);
      if(day != undefined && month != undefined && year != undefined) {
        console.log('year!=undefined', year);
        if (day.length == 2 && month.length == 2 && year.length == 4)
          date = new Date(year, month - 1, day, )
      }

    }
    if (date.toString() != 'Invalid Date'){
      let evnt1 = {name:name, value:date.toISOString()};
      let fields = {[evnt1.name]: evnt1.value};
      console.log("fields",fields);

      console.log('invalidBekbol');
      this.changeModelParent.emit(evnt1);

    }

    //let fields = {[evnt.name]: evnt.value};
    //console.log('fields',fields);
  }
  constructor(public iconModal:  IconExtModal//, private paymentService: ExtPaymentService
  ) {
    super();
  }


  ngOnInit() {
    this.model = this.costInfoModal;
  }
  getTest(){
    console.log('costInfoModal',this.costInfoModal);
    // var changeModelHead = ({'id': 3, 'classname': 'Teme.Shared.Data.Context.Payment', 'fields': {CardNumber: "123"}});
    // this.paymentService.changeModel(changeModelHead)
    //   .subscribe(
    //     (data) => console.log(data),
    //     error => console.log(error)
    //   );
  }



}


