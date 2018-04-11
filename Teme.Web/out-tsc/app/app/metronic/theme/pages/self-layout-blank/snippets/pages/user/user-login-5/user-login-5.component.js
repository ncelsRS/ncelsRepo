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
import { ScriptLoaderService } from '../../../../../../../_services/script-loader.service';
var UserLogin5Component = (function () {
    function UserLogin5Component(_script) {
        this._script = _script;
    }
    UserLogin5Component.prototype.ngOnInit = function () {
    };
    UserLogin5Component.prototype.ngAfterViewInit = function () {
        this._script.loadScripts('.m-grid.m-grid--hor.m-grid--root.m-page', ['assets/snippets/pages/user/login.js']);
        Helpers.bodyClass('m--skin- m-header--fixed m-header--fixed-mobile m-aside-left--enabled m-aside-left--skin-dark m-aside-left--offcanvas m-footer--push m-aside--offcanvas-default');
    };
    UserLogin5Component = __decorate([
        Component({
            selector: ".m-grid.m-grid--hor.m-grid--root.m-page",
            templateUrl: "./user-login-5.component.html",
            encapsulation: ViewEncapsulation.None,
        }),
        __metadata("design:paramtypes", [ScriptLoaderService])
    ], UserLogin5Component);
    return UserLogin5Component;
}());
export { UserLogin5Component };
//# sourceMappingURL=user-login-5.component.js.map