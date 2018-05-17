import {Component, forwardRef, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {IconExtModal} from '../../../shared/icon/icon-ext-modal';
import {DataComponent} from '../data/data.component';
import {NG_VALIDATORS, NG_VALUE_ACCESSOR} from '@angular/forms';
import {TemplateValidation} from '../../../shared/TemplateValidation';


@Component({
  selector: 'app-subject',
  templateUrl: './subject.component.html',
  styleUrls: ['./subject.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => SubjectComponent),
    multi: true
  }, {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => SubjectComponent),
    multi: true
  },
    IconExtModal
  ]
})
export class SubjectComponent extends TemplateValidation {
  @Input() showErrors = false;

  public phoneMask = ['+','7',' ','(', /[1-9]/, /\d/, /\d/, ')', ' ', /\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/, /\d/]
  public iinMask = [/[1-9](12)/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/]


  constructor(public iconModal:  IconExtModal) {
    super();
  }

  ngOnInit() {
  }

}
