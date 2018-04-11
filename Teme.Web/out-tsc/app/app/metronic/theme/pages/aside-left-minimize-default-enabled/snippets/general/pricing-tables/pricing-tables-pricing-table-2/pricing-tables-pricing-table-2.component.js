var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, ViewEncapsulation } from '@angular/core';
import { Helpers } from '../../../../../../../helpers';
var PricingTablesPricingTable2Component = (function () {
    function PricingTablesPricingTable2Component() {
    }
    PricingTablesPricingTable2Component.prototype.ngOnInit = function () {
    };
    PricingTablesPricingTable2Component.prototype.ngAfterViewInit = function () {
        Helpers.bodyClass('m-page--fluid m--skin- m-content--skin-light2 m-header--fixed m-header--fixed-mobile m-aside-left--enabled m-aside-left--skin-dark m-aside-left--offcanvas m-aside-left--minimize m-brand--minimize m-footer--push m-aside--offcanvas-default');
    };
    PricingTablesPricingTable2Component = __decorate([
        Component({
            selector: "app-pricing-tables-pricing-table-2",
            templateUrl: "./pricing-tables-pricing-table-2.component.html",
            encapsulation: ViewEncapsulation.None,
        }),
        __metadata("design:paramtypes", [])
    ], PricingTablesPricingTable2Component);
    return PricingTablesPricingTable2Component;
}());
export { PricingTablesPricingTable2Component };
//# sourceMappingURL=pricing-tables-pricing-table-2.component.js.map