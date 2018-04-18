import { Component, OnInit } from '@angular/core';
import {DeclarantDocType} from './DeclarantDocType';

@Component({
    selector: 'app-ext-declarant',
    templateUrl: './ext-declarant.component.html',
    styleUrls: ['./ext-declarant.component.css']
})
export class ExtDeclarantComponent implements OnInit {
  selectedDeclarantDocType: string;
  levels: Array<DeclarantDocType> = [
    {code: 'Procuration', name: 'Доверенность'},
    {code: 'organizationChart', name: 'Устав'},
  ]


  changeDocLevel(lev: DeclarantDocType) {
    this.selectedDeclarantDocType = lev.name;
  }

  constructor() {
    this.selectedDeclarantDocType = 'Procuration';
  }


    ngOnInit() {
    }

}
