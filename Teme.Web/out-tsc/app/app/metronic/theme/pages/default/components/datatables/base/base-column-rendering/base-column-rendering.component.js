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
var BaseColumnRenderingComponent = (function () {
    function BaseColumnRenderingComponent(_script) {
        this._script = _script;
    }
    BaseColumnRenderingComponent.prototype.ngOnInit = function () {
    };
    BaseColumnRenderingComponent.prototype.ngAfterViewInit = function () {
        this._script.loadScripts('app-base-column-rendering', ['assets/demo/default/custom/components/datatables/base/column-rendering.js']);
    };
    BaseColumnRenderingComponent = __decorate([
        Component({
            selector: "app-base-column-rendering",
            templateUrl: "./base-column-rendering.component.html",
            encapsulation: ViewEncapsulation.None,
        }),
        __metadata("design:paramtypes", [ScriptLoaderService])
    ], BaseColumnRenderingComponent);
    return BaseColumnRenderingComponent;
}());
export { BaseColumnRenderingComponent };
//# sourceMappingURL=base-column-rendering.component.js.map