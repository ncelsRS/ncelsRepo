import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ExtContractRoutingModule} from './ext-contract-routing.module';
import { ExtContractsComponent } from './ext-contracts/ext-contracts.component';
import { ExtContractComponent } from './ext-contract/ext-contract.component';
import { ExtManufacturerComponent } from './ext-contract/ext-manufacturer/ext-manufacturer.component';
import { ExtAttachmentComponent } from './ext-contract/ext-attachment/ext-attachment.component';
import { ExtCostComponent} from './ext-contract/ext-cost/ext-cost.component';
import { ExtPayerComponent} from './ext-contract/ext-payer/ext-payer.component';
import { ExtDeclarantComponent} from './ext-contract/ext-declarant/ext-declarant.component';

@NgModule({
  imports: [
    CommonModule,
    ExtContractRoutingModule
  ],
  declarations: [
    ExtContractsComponent,
    ExtContractComponent,
    ExtManufacturerComponent,
    ExtPayerComponent,
    ExtCostComponent,
    ExtAttachmentComponent,
    ExtDeclarantComponent
  ]
})
export class ExtContractModule { }
