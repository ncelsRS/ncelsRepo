import {Component, forwardRef, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {IconModal} from 'app/shared/IconModal';
import {DataComponent} from '../data/data.component';
import {NG_VALIDATORS, NG_VALUE_ACCESSOR} from '@angular/forms';
import {TemplateValidation} from '../../../shared/TemplateValidation';

@Component({
  selector: 'app-manufacturer',
  templateUrl: './manufacturer.component.html',
  styleUrls: ['./manufacturer.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => ManufacturerComponent),
    multi: true
  }, {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => ManufacturerComponent),
    multi: true
  },
    IconModal
  ]
})
export class ManufacturerComponent extends TemplateValidation {
  @Input() showErrors = false;

  constructor(public iconModal:  IconModal) {
    super();
  }

  ngOnInit() {
  }



}
