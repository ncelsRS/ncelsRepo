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
var ErrorsError1Component = (function () {
    function ErrorsError1Component() {
    }
    ErrorsError1Component.prototype.ngOnInit = function () {
    };
    ErrorsError1Component.prototype.ngAfterViewInit = function () {
        Helpers.bodyClass('m--skin- m-header--fixed m-header--fixed-mobile m-aside-left--enabled m-aside-left--skin-dark m-aside-left--offcanvas m-footer--push m-aside--offcanvas-default');
    };
    ErrorsError1Component = __decorate([
        Component({
            selector: ".m-grid.m-grid--hor.m-grid--root.m-page",
            templateUrl: "./errors-error-1.component.html",
            encapsulation: ViewEncapsulation.None,
        }),
        __metadata("design:paramtypes", [])
    ], ErrorsError1Component);
    return ErrorsError1Component;
}());
export { ErrorsError1Component };
//# sourceMappingURL=errors-error-1.component.js.map