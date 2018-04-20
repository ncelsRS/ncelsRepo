import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FileUploadModule } from 'ng2-file-upload';
import { FormsModule } from '@angular/forms';
import { ExtContractRoutingModule} from './ext-contract-routing.module';
import { ExtContractsComponent } from './ext-contracts/ext-contracts.component';
import { ExtContractComponent } from './ext-contract/ext-contract.component';
import { ExtManufacturerComponent } from './ext-contract/ext-manufacturer/ext-manufacturer.component';
import { ExtAttachmentComponent } from './ext-contract/ext-attachment/ext-attachment.component';
import { ExtCostComponent} from './ext-contract/ext-cost/ext-cost.component';
import { ExtPayerComponent} from './ext-contract/ext-payer/ext-payer.component';
import { ExtDeclarantComponent} from './ext-contract/ext-declarant/ext-declarant.component';
import { ExtPaymentTabComponent } from './ext-contract/ext-payment-tab/ext-payment-tab.component';
import { ExtCostInfoComponent } from './ext-contract/ext-payment-tab/ext-cost-info/ext-cost-info.component';
import {Ng2SmartTableModule} from 'ng2-smart-table';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  imports: [
    CommonModule,
    ExtContractRoutingModule,
    FileUploadModule,
    FormsModule,
    Ng2SmartTableModule,
    NgbModule.forRoot()
  ],
  declarations: [
    ExtContractsComponent,
    ExtContractComponent,
    ExtManufacturerComponent,
    ExtCostComponent,
    ExtAttachmentComponent,
    ExtDeclarantComponent,
    ExtPaymentTabComponent,
    ExtCostInfoComponent,
    ExtPayerComponent
  ]
})
export class ExtContractModule { }
