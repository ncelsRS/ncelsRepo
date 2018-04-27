import {Component, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {IconModal} from 'app/shared/IconModal';

@Component({
  selector: 'app-int-manufacturer',
  templateUrl: './int-manufacturer.component.html',
  styleUrls: ['./int-manufacturer.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers:[IconModal]
})
export class IntManufacturerComponent implements OnInit {


  constructor(public iconModal:  IconModal) { }

  ngOnInit() {
  }

}
