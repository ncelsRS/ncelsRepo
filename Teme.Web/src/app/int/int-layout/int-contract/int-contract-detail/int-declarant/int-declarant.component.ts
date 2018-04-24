import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {DeclarantDocType} from "../../../../../ext/ext-contract/ext-contract/ext-declarant/DeclarantDocType";

@Component({
  selector: 'app-int-declarant',
  templateUrl: './int-declarant.component.html',
  styleUrls: ['./int-declarant.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class IntDeclarantComponent implements OnInit {
  selectedDeclarantDocType: string;
  levels: Array<DeclarantDocType> = [
    {code: 'Procuration', name: 'Доверенность'},
    {code: 'organizationChart', name: 'Устав'},
  ]
  constructor() { }

  ngOnInit() {
  }

  changeDocLevel(lev: DeclarantDocType) {
    this.selectedDeclarantDocType = lev.name;
  }


}
