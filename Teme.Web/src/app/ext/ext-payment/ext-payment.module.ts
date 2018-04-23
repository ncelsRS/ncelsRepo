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


import { SideChatComponent } from './components/side-chat/side-chat.component';
import { BreadcrumbComponent } from './components/breadcrumb/breadcrumb.component';
import { VerticalMenuComponent } from './components/menu/vertical-menu/vertical-menu.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { PipesModule } from 'app/theme/pipes/pipes.module';


//import { HeaderComponent } from './test/components/header/header.component';
//import { FooterComponent } from './test/components/footer/footer.component';
//import { HorizontalMenuComponent } from './test/components/menu/horizontal-menu/horizontal-menu.component';
//import { BackTopComponent } from './test/components/back-top/back-top.component';
//import { FullScreenComponent } from './test/components/fullscreen/fullscreen.component';
//import { ApplicationsComponent } from './test/components/applications/applications.component';
//import { MessagesComponent } from './test/components/messages/messages.component';
//import { UserMenuComponent } from './test/components/user-menu/user-menu.component';
//import { FlagsMenuComponent } from './test/components/flags-menu/flags-menu.component';
//import { FavoritesComponent } from './test/components/favorites/favorites.component';

// import { BlankComponent } from './test/blank/blank.component';
// import { SearchComponent } from './test/search/search.component';

import { Ng2SmartTableModule } from 'ng2-smart-table';

import { CostInfoComponent } from './cost-info/cost-info.component';
import { DataComponent } from './data/data.component';
import { EquipmentComponent } from './equipment/equipment.component';
import { ManufacturerComponent } from './manufacturer/manufacturer.component';
import { SubjectComponent } from './subject/subject.component';
import { AttachmentsComponent } from './attachments/attachments.component';
import { SigningComponent } from './signing/signing.component';
import { TestPaymentComponent } from './test-payment/test-payment.component';

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
    ReactiveFormsModule
  ],
  declarations: [
    ExtPaymentComponent,
    VerticalMenuComponent,
    SideChatComponent,
    BreadcrumbComponent,
    SidebarComponent,

    //HeaderComponent,
    // FooterComponent,
    // HorizontalMenuComponent,
    // BackTopComponent,
    // FullScreenComponent,
    // ApplicationsComponent,
    // MessagesComponent,
    // UserMenuComponent,
    // FlagsMenuComponent,
    // FavoritesComponent,
    // BlankComponent,
    // SearchComponent,

    CostInfoComponent,
    DataComponent,
    EquipmentComponent,
    ManufacturerComponent,
    SubjectComponent,
    AttachmentsComponent,
    SigningComponent,
    TestPaymentComponent
  ],
  //exports: [ExtPaymentComponent],
  providers:[
    {
      provide: PERFECT_SCROLLBAR_CONFIG,
      useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
    }
  ]
})
export class ExtPaymentModule { }
