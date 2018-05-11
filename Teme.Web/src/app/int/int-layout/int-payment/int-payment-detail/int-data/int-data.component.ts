import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {IconExtModal} from 'app/shared/icon/icon-ext-modal';

@Component({
  selector: 'app-int-data',
  templateUrl: './int-data.component.html',
  styleUrls: ['./int-data.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [IconExtModal
  ]
})
export class IntDataComponent implements OnInit {

  constructor(public iconModal:  IconExtModal) { }

  ngOnInit() {
  }

}
