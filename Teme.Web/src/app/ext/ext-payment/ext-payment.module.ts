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
import {SmartTableButtonViewComponent} from '../../shared/smart-table-button-view.component';
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
import {Country} from '../../shared/reference/country';
import { TextMaskModule } from 'angular2-text-mask';
import {CalculatorServiceType} from '../../shared/reference/calculator-service-type';
import {ReferenceStandart} from '../../shared/reference/reference-standart';
import {IconIntButton} from '../../shared/icon/icon-int-button';
import {IconModule} from '../../shared/icon/icon.module';
import {SmartTableReferenceComponent} from '../../shared/smart-table-reference.component';
import { MeasureDropDownComponent } from './equipment/measure-drop-down/measure-drop-down.component';
import {NgSelectModule} from '@ng-select/ng-select';
import {DropDownLocalComponent} from '../../shared/drop-down-local/drop-down-local.component';
import {DropDownRenderComponent} from '../../shared/drop-down-local/drop-down-render';
import { PackagingTypeDropDownComponent } from './equipment/packaging-type-drop-down/packaging-type-drop-down.component';
import { EquipmentTypeDropDownComponent } from './equipment/equipment-type-drop-down/equipment-type-drop-down.component';
import { CountryDropDownComponent } from './equipment/country-drop-down/country-drop-down.component';
import {RscSmartTableModule} from '../../shared/rsc-smart-table.module';
import {RscReferenceModule} from '../../shared/reference/rsc-reference.module';
import { EquipmentImnTableComponent } from './equipment/equipment-imn-table/equipment-imn-table.component';
import { PackagingTableComponent } from './equipment/packaging-table/packaging-table.component';

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
    TextMaskModule,
    IconModule,
    TextMaskModule,
    NgSelectModule,
    RscSmartTableModule,
    RscReferenceModule
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
    Country,
    CalculatorServiceType,
    SmartTableReferenceComponent,
    ReferenceStandart,
    MeasureDropDownComponent,
    DropDownLocalComponent,
    DropDownRenderComponent,
    PackagingTypeDropDownComponent,
    EquipmentTypeDropDownComponent,
    CountryDropDownComponent,
    EquipmentImnTableComponent,
    PackagingTableComponent
  ],
  //exports: [ExtPaymentComponent],
  providers:[
    {
      provide: PERFECT_SCROLLBAR_CONFIG,
      useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
    }
  ],
  entryComponents: [
    MeasureDropDownComponent,
    SmartTableReferenceComponent,
    DropDownLocalComponent,
    DropDownRenderComponent,
    PackagingTypeDropDownComponent,
    EquipmentTypeDropDownComponent,
    CountryDropDownComponent,
    SmartTableButtonViewComponent
    ]
})
export class ExtPaymentModule { }
