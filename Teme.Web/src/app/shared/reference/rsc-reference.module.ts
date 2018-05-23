import {NgModule} from '@angular/core';
import {ContractForm} from './contract-form';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    FormsModule
  ],
  declarations: [ContractForm],
  exports: [ContractForm]
})

export class RscReferenceModule {
  constructor() {
  }
}
