import {Component, ElementRef, forwardRef, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {IconModal} from 'app/shared/IconModal';
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
    IconModal
  ]
  //encapsulation: ViewEncapsulation.None,
})
export class DataComponent extends TemplateValidation {

  @Input() showErrors = false;

  constructor( public iconModal:  IconModal) {
    super();
  }

  ngOnInit() {
  }


}
