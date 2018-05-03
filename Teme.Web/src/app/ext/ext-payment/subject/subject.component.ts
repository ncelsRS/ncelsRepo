import {Component, forwardRef, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {IconModal} from '../../../shared/IconModal';
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
    IconModal
  ]
})
export class SubjectComponent extends TemplateValidation {
  @Input() showErrors = false;

  constructor(public iconModal:  IconModal) {
    super();
  }

  ngOnInit() {
  }

}
