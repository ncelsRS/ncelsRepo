import {Component, ElementRef, forwardRef, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {TemplateValidation} from 'app/shared/TemplateValidation';
import {NG_VALIDATORS, NG_VALUE_ACCESSOR} from '@angular/forms';
import {IconModal} from 'app/shared/IconModal';


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
  }]
  //encapsulation: ViewEncapsulation.None
})
export class CostInfoComponent extends IconModal  {

  @Input() showErrors = false;

  constructor(private elementRef: ElementRef) {
    super(elementRef);
  }

  ngOnInit() {
  }

}


