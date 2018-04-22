import {Component, ElementRef, OnInit, ViewEncapsulation} from '@angular/core';
import {IconModal} from '../../../shared/IconModal';

@Component({
  selector: 'app-data',
  templateUrl: './data.component.html',
  styleUrls: ['./data.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class DataComponent extends IconModal {

  constructor(private elementRef: ElementRef) {
    super(elementRef);
  }



}
