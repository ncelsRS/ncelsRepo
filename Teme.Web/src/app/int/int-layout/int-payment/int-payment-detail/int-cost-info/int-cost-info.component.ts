import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {IconExtModal} from 'app/shared/icon/icon-ext-modal';
import {TemplateValidation} from '../../../../../shared/TemplateValidation';

@Component({
  selector: 'app-int-cost-info',
  templateUrl: './int-cost-info.component.html',
  styleUrls: ['./int-cost-info.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [
    IconExtModal
]
})
export class IntCostInfoComponent extends TemplateValidation implements OnInit {

  constructor(public iconModal:  IconExtModal) {
    super();
  }

  ngOnInit() {
  }

  modelCustom = {};
  customDay = '';
  isDisabled = true;
  modelCustom2 = {};

}
