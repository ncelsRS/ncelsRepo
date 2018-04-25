import {Component, ElementRef, forwardRef, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {NG_VALIDATORS, NG_VALUE_ACCESSOR} from '@angular/forms';
import {IconModal} from 'app/shared/IconModal';
import {TemplateValidation} from 'app/shared/TemplateValidation';


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
    IconModal
  ]
  //encapsulation: ViewEncapsulation.None
})
export class CostInfoComponent extends TemplateValidation {

  @Input() showErrors = false;

  constructor(public iconModal:  IconModal) {
    super();
  }

  ngOnInit() {
  }

}


