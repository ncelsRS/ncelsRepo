import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {RouterModule, Routes} from "@angular/router";
import {Ng2SmartTableModule} from "ng2-smart-table";
import {ModuleWithProviders} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {IntHistoryComponent} from './int-history/int-history.component';
import {IntAttachmentsComponent} from './int-attachments/int-attachments.component';
import {IntCardComponent} from './int-card/int-card.component';
import {IntManufacturerComponent} from "./int-manufacturer/int-manufacturer.component";
import {IntDeclarantComponent} from "./int-declarant/int-declarant.component"
import {IntPayerComponent} from "./int-payer/int-payer.component";
import {IntCostComponent} from "./int-cost/int-cost.component";
import {FileUploadModule} from "ng2-file-upload";
import {IconModule} from '../../../../shared/icon/icon.module';


const routes: Routes = [
  {
    path: 'card',
    component: IntCardComponent,
  },
  {
    path: 'attachments',
    component: IntAttachmentsComponent,
  },
  {
    path: 'history',
    component: IntHistoryComponent,
  }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    Ng2SmartTableModule,
    FormsModule,
    FileUploadModule,
    NgbModule.forRoot(),
    IconModule
  ],
  declarations: [
    IntAttachmentsComponent,
    IntCardComponent,
    IntManufacturerComponent,
    IntDeclarantComponent,
    IntPayerComponent,
    IntCostComponent,
    IntHistoryComponent
  ],
  exports: [RouterModule],
  entryComponents: [],
})

export class IntContractDetailModule {
}

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
