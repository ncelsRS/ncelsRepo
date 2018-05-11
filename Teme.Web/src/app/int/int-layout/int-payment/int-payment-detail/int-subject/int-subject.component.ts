import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {IconExtModal} from 'app/shared/icon/icon-ext-modal';

@Component({
  selector: 'app-int-subject',
  templateUrl: './int-subject.component.html',
  styleUrls: ['./int-subject.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers:[IconExtModal]
})
export class IntSubjectComponent implements OnInit {

  constructor(public iconModal:  IconExtModal) { }

  ngOnInit() {
  }

}
