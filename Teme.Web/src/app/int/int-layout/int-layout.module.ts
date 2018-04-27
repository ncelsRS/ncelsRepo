import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {IntContractComponent} from "./int-contract/int-contract.component";
import {IntDeclarationComponent} from "./int-declaration/int-declaration.component";
import {RouterModule, Routes} from "@angular/router";
import {SidebarComponent} from "../components/sidebar/sidebar.component";
import {VerticalMenuComponent} from "../../shared/menu/vertical-menu/vertical-menu.component";
import {Ng2SmartTableModule} from "ng2-smart-table";
import { IntContractBtnComponent } from './int-contract/int-contract-btn/int-contract-btn.component';
import { IntContractDetailComponent } from './int-contract/int-contract-detail/int-contract-detail.component';
import {ModuleWithProviders} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { IntDeclarationBtnComponent } from './int-declaration/int-declaration-btn/int-declaration-btn.component';
import { IntDeclarationDetailComponent } from './int-declaration/int-declaration-detail/int-declaration-detail.component';
import { IntPaymentComponent } from './int-payment/int-payment.component';
import { IntPaymentBtnComponent } from './int-payment/int-payment-btn/int-payment-btn.component';
import { IntPaymentDetailComponent } from './int-payment/int-payment-detail/int-payment-detail.component';

const routes: Routes = [
  {
    path: 'contracts',
    component:IntContractComponent,
  },
  {
    path: 'contracts/:id',
    component:IntContractDetailComponent,
    loadChildren:'./int-contract/int-contract-detail/int-contract-detail.module#IntContractDetailModule'
  },
  {
    path: 'declarations',
    component:IntDeclarationComponent
  },
  {
    path: 'declarations/:id',
    component:IntDeclarationDetailComponent,
    loadChildren:'./int-declaration/int-declaration-detail/int-declaration-detail.module#IntDeclarationDetailModule'
  },
  {
    path: 'payments',
    component:IntPaymentComponent
  },
  {
    path: 'payments/:id',
    component:IntPaymentDetailComponent,
    loadChildren:'./int-payment/int-payment-detail/int-payment-detail.module#IntPaymentDetailModule'
  },

];


@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    Ng2SmartTableModule,
    FormsModule,
    NgbModule.forRoot()
  ],
  declarations: [
    IntContractComponent,
    IntDeclarationComponent,
    VerticalMenuComponent,
    SidebarComponent,
    IntContractBtnComponent,
    IntContractDetailComponent,
    IntDeclarationBtnComponent,
    IntDeclarationDetailComponent,
    IntPaymentComponent,
    IntPaymentBtnComponent,
    IntPaymentDetailComponent,
  ],
  exports: [RouterModule],
  entryComponents: [
    IntContractBtnComponent,
    IntDeclarationBtnComponent,
    IntPaymentBtnComponent,
  ],
})

export class IntLayoutModule {
}

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
