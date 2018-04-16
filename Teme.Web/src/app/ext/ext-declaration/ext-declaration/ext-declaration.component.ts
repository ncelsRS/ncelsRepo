import {Component, OnInit} from '@angular/core';
import {RegisterType} from './RegisterType';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-ext-declaration',
  templateUrl: './ext-declaration.component.html',
  styleUrls: ['./ext-declaration.component.css']
})

export class ExtDeclarationComponent implements OnInit {
  RegisterTypes: RegisterType[];
  type: string;

  constructor() {
    this.type = 'general';
  }

  setDeclarationTab(name: string) {
    this.type = name;
  }

  onSubmit (form: FormsModule) {
    // element.all(by.tagName('app-hero-parent'))
  }

  ngOnInit() {
  }


}
