import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {RegisterType} from "../../../../../ext/ext-declaration/ext-declaration/RegisterType";

@Component({
  selector: 'app-int-card',
  templateUrl: './int-card.component.html',
  styleUrls: ['./int-card.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class IntCardComponent implements OnInit {
  RegisterTypes: RegisterType[];
  type: string;
  constructor() {
    this.type = 'general';
  }

  ngOnInit() {
  }

  setDeclarationTab(name: string) {
    this.type = name;
  }

}
