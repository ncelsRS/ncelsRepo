import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FileUploadModule } from 'ng2-file-upload';
import { FormsModule } from '@angular/forms';
import {ExtContractRoutingModule} from './ext-contract-routing.module';
import { ExtContractsComponent } from './ext-contracts/ext-contracts.component';
import { ExtContractComponent } from './ext-contract/ext-contract.component';
import { ExtManufacturerComponent } from './ext-contract/ext-manufacturer/ext-manufacturer.component';
import { ExtAttachmentComponent } from './ext-contract/ext-attachment/ext-attachment.component';
import { ExtCostComponent} from './ext-contract/ext-cost/ext-cost.component';
import { ExtPayerComponent} from './ext-contract/ext-payer/ext-payer.component';
import { ExtDeclarantComponent} from './ext-contract/ext-declarant/ext-declarant.component';
import { ExtPaymentComponent } from './ext-contract/ext-payment/ext-payment.component';

@NgModule({
  imports: [
    CommonModule,
    ExtContractRoutingModule,
    FileUploadModule,
      FormsModule
  ],
  declarations: [
    ExtContractsComponent,
    ExtContractComponent,
    ExtManufacturerComponent,
    ExtPayerComponent,
    ExtCostComponent,
    ExtAttachmentComponent,
    ExtDeclarantComponent,
    ExtPaymentComponent
  ]
})
export class ExtContractModule { }
