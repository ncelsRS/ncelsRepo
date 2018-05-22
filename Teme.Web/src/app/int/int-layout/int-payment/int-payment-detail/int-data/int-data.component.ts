import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {IconExtModal} from 'app/shared/icon/icon-ext-modal';
import {TemplateValidation} from '../../../../../shared/TemplateValidation';

@Component({
  selector: 'app-int-data',
  templateUrl: './int-data.component.html',
  styleUrls: ['./int-data.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [IconExtModal
  ]
})
export class IntDataComponent extends TemplateValidation implements OnInit {

  constructor(public iconModal:  IconExtModal) {
    super();
  }

  ngOnInit() {
  }

}
