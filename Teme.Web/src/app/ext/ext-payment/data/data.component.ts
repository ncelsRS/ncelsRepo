import {Component, ElementRef, EventEmitter, forwardRef, Input, OnInit, Output, ViewEncapsulation} from '@angular/core';
import {IconExtModal} from 'app/shared/icon/icon-ext-modal';
import {NG_VALIDATORS, NG_VALUE_ACCESSOR} from '@angular/forms';;
import {TemplateValidation} from 'app/shared/TemplateValidation';

@Component({
  selector: 'app-data',
  templateUrl: './data.component.html',
  styleUrls: ['./data.component.scss'],

  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => DataComponent),
    multi: true
  }, {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => DataComponent),
    multi: true
  },
    IconExtModal
  ]
  //encapsulation: ViewEncapsulation.None,
})
export class DataComponent extends TemplateValidation {

  @Input() showErrors = false;
  @Output() changeModelParent = new EventEmitter<any>();
  degreeRiskClass="degreeRiskClass";
  changeModel(evnt:any) {
    this.changeModelParent.emit(evnt);
  }

  constructor( public iconModal:  IconExtModal) {
    super();
  }

  ngOnInit() {
  }


}
