import {Component, EventEmitter, forwardRef, Input, OnInit, Output, ViewEncapsulation} from '@angular/core';
import {IconExtModal} from 'app/shared/icon/icon-ext-modal';
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
    IconExtModal
  ]
})
export class ManufacturerComponent extends TemplateValidation {
  @Input() showErrors = false;
  @Output() changeModelParent = new EventEmitter<any>();
  changeModel(evnt:any) {
    this.changeModelParent.emit(evnt);
  }


  constructor(public iconModal:  IconExtModal) {
    super();
  }

  ngOnInit() {
  }



}
