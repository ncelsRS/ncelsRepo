import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {IconModal} from 'app/shared/IconModal';

@Component({
  selector: 'app-int-cost-info',
  templateUrl: './int-cost-info.component.html',
  styleUrls: ['./int-cost-info.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [
    IconModal
]
})
export class IntCostInfoComponent implements OnInit {

  constructor(public iconModal:  IconModal) { }

  ngOnInit() {
  }

}
