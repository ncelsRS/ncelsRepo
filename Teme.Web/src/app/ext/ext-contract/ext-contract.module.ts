import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ExtContractRoutingModule} from './ext-contract-routing.module';
import { ExtContractsComponent } from './ext-contracts/ext-contracts.component';
import { ExtContractComponent } from './ext-contract/ext-contract.component';
import { ExtDeclarantComponent } from './ext-contract/ext-declarant/ext-declarant.component';

@NgModule({
  imports: [
    CommonModule,
    ExtContractRoutingModule
  ],
  declarations: [
    ExtContractsComponent,
    ExtContractComponent,
    ExtDeclarantComponent
  ]
})
export class ExtContractModule { }
