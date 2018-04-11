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
import { Router, NavigationStart, NavigationEnd } from '@angular/router';
import { Helpers } from "./helpers";
var MetronicComponent = (function () {
    function MetronicComponent(_router) {
        this._router = _router;
        this.title = 'app';
        this.globalBodyClass = 'm-page--loading-non-block m-page--fluid m--skin- m-content--skin-light2 m-header--fixed m-header--fixed-mobile m-aside-left--enabled m-aside-left--skin-dark m-aside-left--offcanvas m-footer--push m-aside--offcanvas-default';
    }
    MetronicComponent.prototype.ngOnInit = function () {
        var _this = this;
        this._router.events.subscribe(function (route) {
            if (route instanceof NavigationStart) {
                Helpers.setLoading(true);
                Helpers.bodyClass(_this.globalBodyClass);
            }
            if (route instanceof NavigationEnd) {
                Helpers.setLoading(false);
            }
        });
    };
    MetronicComponent = __decorate([
        Component({
            selector: 'body',
            templateUrl: './metronic.component.html',
            encapsulation: ViewEncapsulation.None,
        }),
        __metadata("design:paramtypes", [Router])
    ], MetronicComponent);
    return MetronicComponent;
}());
export { MetronicComponent };
//# sourceMappingURL=metronic.component.js.map