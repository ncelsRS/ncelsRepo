import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastrModule } from 'ngx-toastr';
import { MultiselectDropdownModule } from 'angular-2-dropdown-multiselect';

import { routing } from './ext-payment.routing';
import { ExtPaymentComponent } from './ext-payment.component';
import { PipesModule } from 'app/theme/pipes/pipes.module';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import {FileUploadModule} from 'ng2-file-upload';
import { CostInfoComponent } from './cost-info/cost-info.component';
import { DataComponent } from './data/data.component';
import { EquipmentComponent } from './equipment/equipment.component';
import { ManufacturerComponent } from './manufacturer/manufacturer.component';
import { SubjectComponent } from './subject/subject.component';
import { AttachmentsComponent } from './attachments/attachments.component';
import { SigningComponent } from './signing/signing.component';
import {TestPaymentComponent} from './test-payment/test-payment.component';
import {ContractForm} from '../../shared/reference/contract-form';
import {Country} from '../../shared/reference/country';
import { TextMaskModule } from 'angular2-text-mask';
import {CalculatorServiceType} from '../../shared/reference/calculator-service-type';
import {Calendar} from '../../shared/datepicker/calendar';
import {IconButton} from '../../shared/icon/icon-button';
import {SmartTableButtonViewComponent} from '../../shared/smart-table-button-view.component';
import {ReferenceStandart} from '../../shared/reference/reference-standart';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    PerfectScrollbarModule,
    ToastrModule.forRoot(),
    NgbModule.forRoot(),
    MultiselectDropdownModule,
    PipesModule,
    routing,
    Ng2SmartTableModule,
    FileUploadModule,
    ReactiveFormsModule,
    TextMaskModule
  ],
  declarations: [
    ExtPaymentComponent,
    CostInfoComponent,
    DataComponent,
    EquipmentComponent,
    ManufacturerComponent,
    SubjectComponent,
    AttachmentsComponent,
    SigningComponent,
    TestPaymentComponent,
    ContractForm,
    Country,
    CalculatorServiceType,
    Calendar,
    IconButton,
    SmartTableButtonViewComponent,
    ReferenceStandart
  ],
  //exports: [ExtPaymentComponent],
  providers:[
    {
      provide: PERFECT_SCROLLBAR_CONFIG,
      useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
    }
  ],
  entryComponents: [
    SmartTableButtonViewComponent,
    ]
})
export class ExtPaymentModule { }
