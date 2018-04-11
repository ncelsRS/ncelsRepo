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
import { ScriptLoaderService } from '../../../../_services/script-loader.service';
var IndexComponent = (function () {
    function IndexComponent(_script) {
        this._script = _script;
    }
    IndexComponent.prototype.ngOnInit = function () {
    };
    IndexComponent.prototype.ngAfterViewInit = function () {
        this._script.loadScripts('app-index', ['assets/app/js/dashboard.js']);
    };
    IndexComponent = __decorate([
        Component({
            selector: "app-index",
            templateUrl: "./index.component.html",
            encapsulation: ViewEncapsulation.None,
        }),
        __metadata("design:paramtypes", [ScriptLoaderService])
    ], IndexComponent);
    return IndexComponent;
}());
export { IndexComponent };
//# sourceMappingURL=index.component.js.map