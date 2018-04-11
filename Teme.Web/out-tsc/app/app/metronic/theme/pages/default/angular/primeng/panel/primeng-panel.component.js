var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { Component, ViewEncapsulation } from '@angular/core';
var PrimeNgPanelComponent = (function () {
    function PrimeNgPanelComponent() {
        this.index = 0;
    }
    PrimeNgPanelComponent.prototype.ngOnInit = function () {
    };
    PrimeNgPanelComponent.prototype.onTabClose = function (event) {
        this.msgs = [];
        this.msgs.push({ severity: 'info', summary: 'Tab Closed', detail: 'Index: ' + event.index });
    };
    PrimeNgPanelComponent.prototype.onTabOpen = function (event) {
        this.msgs = [];
        this.msgs.push({ severity: 'info', summary: 'Tab Expanded', detail: 'Index: ' + event.index });
    };
    PrimeNgPanelComponent.prototype.openNext = function () {
        this.index = (this.index === 3) ? 0 : this.index + 1;
    };
    PrimeNgPanelComponent.prototype.openPrev = function () {
        this.index = (this.index === 0) ? 3 : this.index - 1;
    };
    PrimeNgPanelComponent = __decorate([
        Component({
            selector: "app-primeng-panel",
            templateUrl: "./primeng-panel.component.html",
            encapsulation: ViewEncapsulation.None,
        })
    ], PrimeNgPanelComponent);
    return PrimeNgPanelComponent;
}());
export { PrimeNgPanelComponent };
//# sourceMappingURL=primeng-panel.component.js.map