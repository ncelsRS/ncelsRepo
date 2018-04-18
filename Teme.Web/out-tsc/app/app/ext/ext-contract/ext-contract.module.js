var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExtContractRoutingModule } from './ext-contract-routing.module';
import { ExtContractsComponent } from './ext-contracts/ext-contracts.component';
import { ExtContractComponent } from './ext-contract/ext-contract.component';
import { ExtManufacturerComponent } from './ext-contract/ext-manufacturer/ext-manufacturer.component';
import { ExtAttachmentComponent } from './ext-contract/ext-attachment/ext-attachment.component';
import { ExtCostComponent } from './ext-contract/ext-cost/ext-cost.component';
import { ExtPayerComponent } from './ext-contract/ext-payer/ext-payer.component';
import { ExtDeclarantComponent } from './ext-contract/ext-declarant/ext-declarant.component';
var ExtContractModule = (function () {
    function ExtContractModule() {
    }
    ExtContractModule = __decorate([
        NgModule({
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
    ], ExtContractModule);
    return ExtContractModule;
}());
export { ExtContractModule };
//# sourceMappingURL=ext-contract.module.js.map