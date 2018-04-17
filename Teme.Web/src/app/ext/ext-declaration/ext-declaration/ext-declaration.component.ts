import {Component, OnInit, ViewChild} from '@angular/core';
import {RegisterType} from './RegisterType';
import {FormsModule} from '@angular/forms';
import {ExtGeneralInformationComponent} from './ext-general-information/ext-general-information.component';

@Component({
  selector: 'app-ext-declaration',
  templateUrl: './ext-declaration.component.html',
  styleUrls: ['./ext-declaration.component.css']
})

export class ExtDeclarationComponent implements OnInit {
  RegisterTypes: RegisterType[];
  type: string;
  public showErrors = false;
  public contract: any = {manufacturer: {val: null, val2: null, val3: null}};
  @ViewChild(ExtGeneralInformationComponent)
  private  ExtGeneral: ExtGeneralInformationComponent;

  constructor() {
    this.type = 'general';
  }

  setDeclarationTab(name: string) {
    this.type = name;
  }

  onSubmit (form: FormsModule) {
    // element.all(by.tagName('app-hero-parent'))
  }

  public onTriggerClick() {
    this.ExtGeneral.triggerClick();
  }

  ngOnInit() {}

  sendToNcels(valid) {
    alert(valid.toString());
  }

  diagnostic() {
    return JSON.stringify(this.contract);
  }


}
