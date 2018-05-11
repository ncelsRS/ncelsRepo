import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {RouterModule, Routes} from '@angular/router';
import {Ng2SmartTableModule} from 'ng2-smart-table';
import {ModuleWithProviders} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {FileUploadModule} from 'ng2-file-upload';
import {IntCardComponent} from './int-card/int-card.component';
import {IntHistoryComponent} from './int-history/int-history.component';
import { IntAttachmentsComponent } from './int-attachments/int-attachments.component';
import { IntManufacturerComponent } from './int-manufacturer/int-manufacturer.component';
import { IntCostInfoComponent } from './int-cost-info/int-cost-info.component';
import { IntDataComponent } from './int-data/int-data.component';
import { IntEquipmentComponent } from './int-equipment/int-equipment.component';
import { IntSubjectComponent } from './int-subject/int-subject.component';
import {ContractForm} from 'app/shared/reference/contractForm';


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
    NgbModule.forRoot()
  ],
  declarations: [
    IntCardComponent,
    IntHistoryComponent,
    IntAttachmentsComponent,
    IntManufacturerComponent,
    IntCostInfoComponent,
    IntDataComponent,
    IntEquipmentComponent,
    IntSubjectComponent,
    ContractForm
  ],
  exports: [RouterModule],
  entryComponents: [],
})

export class IntPaymentDetailModule {
}

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
