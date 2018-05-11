import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {IconExtModal} from 'app/shared/icon/icon-ext-modal';

@Component({
  selector: 'app-int-cost-info',
  templateUrl: './int-cost-info.component.html',
  styleUrls: ['./int-cost-info.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [
    IconExtModal
]
})
export class IntCostInfoComponent implements OnInit {

  constructor(public iconModal:  IconExtModal) { }

  ngOnInit() {
  }

}
