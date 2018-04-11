var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { PricingTablesPricingTable2Component } from './pricing-tables-pricing-table-2.component';
import { LayoutModule } from '../../../../../../layouts/layout.module';
import { AsideLeftMinimizeDefaultEnabledComponent } from '../../../../aside-left-minimize-default-enabled.component';
var routes = [
    {
        "path": "",
        "component": AsideLeftMinimizeDefaultEnabledComponent,
        "children": [
            {
                "path": "",
                "component": PricingTablesPricingTable2Component
            }
        ]
    }
];
var PricingTablesPricingTable2Module = (function () {
    function PricingTablesPricingTable2Module() {
    }
    PricingTablesPricingTable2Module = __decorate([
        NgModule({
            imports: [
                CommonModule, RouterModule.forChild(routes), LayoutModule
            ], exports: [
                RouterModule
            ], declarations: [
                PricingTablesPricingTable2Component
            ]
        })
    ], PricingTablesPricingTable2Module);
    return PricingTablesPricingTable2Module;
}());
export { PricingTablesPricingTable2Module };
//# sourceMappingURL=pricing-tables-pricing-table-2.module.js.map