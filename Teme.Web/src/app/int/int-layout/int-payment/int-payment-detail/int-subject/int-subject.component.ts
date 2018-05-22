import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {IconExtModal} from 'app/shared/icon/icon-ext-modal';
import {TemplateValidation} from '../../../../../shared/TemplateValidation';

@Component({
  selector: 'app-int-subject',
  templateUrl: './int-subject.component.html',
  styleUrls: ['./int-subject.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers:[IconExtModal]
})
export class IntSubjectComponent extends TemplateValidation implements OnInit {

  constructor(public iconModal:  IconExtModal) {
    super();
  }

  ngOnInit() {
  }

}
