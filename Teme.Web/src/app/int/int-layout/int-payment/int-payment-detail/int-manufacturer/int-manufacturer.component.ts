import {Component, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {IconExtModal} from 'app/shared/icon/icon-ext-modal';

@Component({
  selector: 'app-int-manufacturer',
  templateUrl: './int-manufacturer.component.html',
  styleUrls: ['./int-manufacturer.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers:[IconExtModal]
})
export class IntManufacturerComponent implements OnInit {


  constructor(public iconModal:  IconExtModal) { }

  ngOnInit() {
  }

}
