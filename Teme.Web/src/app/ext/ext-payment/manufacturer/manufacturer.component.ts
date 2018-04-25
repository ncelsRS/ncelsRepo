import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {IconModal} from 'app/shared/IconModal';

@Component({
  selector: 'app-manufacturer',
  templateUrl: './manufacturer.component.html',
  styleUrls: ['./manufacturer.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [
    IconModal
  ]
})
export class ManufacturerComponent implements OnInit {

  constructor(public iconModal:  IconModal) { }

  ngOnInit() {
  }



}
