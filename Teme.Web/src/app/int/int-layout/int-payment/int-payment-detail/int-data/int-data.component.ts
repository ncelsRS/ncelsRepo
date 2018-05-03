import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {IconModal} from 'app/shared/IconModal';

@Component({
  selector: 'app-int-data',
  templateUrl: './int-data.component.html',
  styleUrls: ['./int-data.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [IconModal
  ]
})
export class IntDataComponent implements OnInit {

  constructor(public iconModal:  IconModal) { }

  ngOnInit() {
  }

}
