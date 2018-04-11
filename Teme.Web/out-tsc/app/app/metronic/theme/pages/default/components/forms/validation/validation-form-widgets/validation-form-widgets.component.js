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
var ValidationFormWidgetsComponent = (function () {
    function ValidationFormWidgetsComponent(_script) {
        this._script = _script;
    }
    ValidationFormWidgetsComponent.prototype.ngOnInit = function () {
    };
    ValidationFormWidgetsComponent.prototype.ngAfterViewInit = function () {
        this._script.loadScripts('app-validation-form-widgets', ['assets/demo/default/custom/components/forms/validation/form-widgets.js']);
    };
    ValidationFormWidgetsComponent = __decorate([
        Component({
            selector: "app-validation-form-widgets",
            templateUrl: "./validation-form-widgets.component.html",
            encapsulation: ViewEncapsulation.None,
        }),
        __metadata("design:paramtypes", [ScriptLoaderService])
    ], ValidationFormWidgetsComponent);
    return ValidationFormWidgetsComponent;
}());
export { ValidationFormWidgetsComponent };
//# sourceMappingURL=validation-form-widgets.component.js.map