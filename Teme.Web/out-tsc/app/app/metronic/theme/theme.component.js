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
import { Helpers } from '../helpers';
import { ScriptLoaderService } from '../_services/script-loader.service';
var ThemeComponent = (function () {
    function ThemeComponent(_script, _router) {
        this._script = _script;
        this._router = _router;
    }
    ThemeComponent.prototype.ngOnInit = function () {
        var _this = this;
        this._script.loadScripts('body', ['assets/vendors/base/vendors.bundle.js', 'assets/demo/default/base/scripts.bundle.js'], true)
            .then(function (result) {
            Helpers.setLoading(false);
            // optional js to be loaded once
            _this._script.loadScripts('head', ['assets/vendors/custom/fullcalendar/fullcalendar.bundle.js']);
        });
        this._router.events.subscribe(function (route) {
            if (route instanceof NavigationStart) {
                mLayout.closeMobileAsideMenuOffcanvas();
                mLayout.closeMobileHorMenuOffcanvas();
                mApp.scrollTop();
                Helpers.setLoading(true);
                // hide visible popover
                $('[data-toggle="m-popover"]').popover('hide');
            }
            if (route instanceof NavigationEnd) {
                // init required js
                mApp.init();
                mUtil.init();
                Helpers.setLoading(false);
                // content m-wrapper animation
                var animation_1 = 'm-animate-fade-in-up';
                $('.m-wrapper').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function (e) {
                    $('.m-wrapper').removeClass(animation_1);
                }).removeClass(animation_1).addClass(animation_1);
            }
        });
    };
    ThemeComponent = __decorate([
        Component({
            selector: ".m-grid.m-grid--hor.m-grid--root.m-page",
            templateUrl: "./theme.component.html",
            encapsulation: ViewEncapsulation.None,
        }),
        __metadata("design:paramtypes", [ScriptLoaderService, Router])
    ], ThemeComponent);
    return ThemeComponent;
}());
export { ThemeComponent };
//# sourceMappingURL=theme.component.js.map