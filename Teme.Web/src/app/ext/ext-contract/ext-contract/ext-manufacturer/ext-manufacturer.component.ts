import { Component, OnInit, Input} from '@angular/core';
import {RegisterType} from '../RegisterType';

@Component({
  selector: 'app-ext-manufacturer',
  templateUrl: './ext-manufacturer.component.html',
  styleUrls: ['./ext-manufacturer.component.css']
})
export class ExtManufacturerComponent implements OnInit {

  @Input() prnRegisterType: string;



  ngOnInit() {
  }

}
