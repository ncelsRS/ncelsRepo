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
var PrimengComponent = (function () {
    function PrimengComponent() {
    }
    PrimengComponent.prototype.ngOnInit = function () {
    };
    PrimengComponent = __decorate([
        Component({
            selector: ".m-grid__item.m-grid__item--fluid.m-wrapper",
            templateUrl: "./primeng.component.html",
            encapsulation: ViewEncapsulation.None,
            styleUrls: [
                '../../../../../../../../node_modules/primeng/resources/primeng.css',
                '../../../../../../../../node_modules/primeng/resources/themes/bootstrap/theme.css',
            ]
        }),
        __metadata("design:paramtypes", [])
    ], PrimengComponent);
    return PrimengComponent;
}());
export { PrimengComponent };
//# sourceMappingURL=primeng.component.js.map