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
import { ScriptLoaderService } from '../../../../../../../_services/script-loader.service';
var BaseHtmlTableComponent = (function () {
    function BaseHtmlTableComponent(_script) {
        this._script = _script;
    }
    BaseHtmlTableComponent.prototype.ngOnInit = function () {
    };
    BaseHtmlTableComponent.prototype.ngAfterViewInit = function () {
        this._script.loadScripts('app-base-html-table', ['assets/demo/default/custom/components/datatables/base/html-table.js']);
    };
    BaseHtmlTableComponent = __decorate([
        Component({
            selector: "app-base-html-table",
            templateUrl: "./base-html-table.component.html",
            encapsulation: ViewEncapsulation.None,
        }),
        __metadata("design:paramtypes", [ScriptLoaderService])
    ], BaseHtmlTableComponent);
    return BaseHtmlTableComponent;
}());
export { BaseHtmlTableComponent };
//# sourceMappingURL=base-html-table.component.js.map