var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { PricingTablesPricingTable1Component } from './pricing-tables-pricing-table-1.component';
import { LayoutModule } from '../../../../../../layouts/layout.module';
import { AsideLeftMinimizeDefaultEnabledComponent } from '../../../../aside-left-minimize-default-enabled.component';
var routes = [
    {
        "path": "",
        "component": AsideLeftMinimizeDefaultEnabledComponent,
        "children": [
            {
                "path": "",
                "component": PricingTablesPricingTable1Component
            }
        ]
    }
];
var PricingTablesPricingTable1Module = (function () {
    function PricingTablesPricingTable1Module() {
    }
    PricingTablesPricingTable1Module = __decorate([
        NgModule({
            imports: [
                CommonModule, RouterModule.forChild(routes), LayoutModule
            ], exports: [
                RouterModule
            ], declarations: [
                PricingTablesPricingTable1Component
            ]
        })
    ], PricingTablesPricingTable1Module);
    return PricingTablesPricingTable1Module;
}());
export { PricingTablesPricingTable1Module };
//# sourceMappingURL=pricing-tables-pricing-table-1.module.js.map