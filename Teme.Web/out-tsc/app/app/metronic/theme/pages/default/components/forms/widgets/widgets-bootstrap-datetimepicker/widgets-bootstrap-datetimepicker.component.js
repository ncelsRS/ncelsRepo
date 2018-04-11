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
var WidgetsBootstrapDatetimepickerComponent = (function () {
    function WidgetsBootstrapDatetimepickerComponent(_script) {
        this._script = _script;
    }
    WidgetsBootstrapDatetimepickerComponent.prototype.ngOnInit = function () {
    };
    WidgetsBootstrapDatetimepickerComponent.prototype.ngAfterViewInit = function () {
        this._script.loadScripts('app-widgets-bootstrap-datetimepicker', ['assets/demo/default/custom/components/forms/widgets/bootstrap-datetimepicker.js']);
    };
    WidgetsBootstrapDatetimepickerComponent = __decorate([
        Component({
            selector: "app-widgets-bootstrap-datetimepicker",
            templateUrl: "./widgets-bootstrap-datetimepicker.component.html",
            encapsulation: ViewEncapsulation.None,
        }),
        __metadata("design:paramtypes", [ScriptLoaderService])
    ], WidgetsBootstrapDatetimepickerComponent);
    return WidgetsBootstrapDatetimepickerComponent;
}());
export { WidgetsBootstrapDatetimepickerComponent };
//# sourceMappingURL=widgets-bootstrap-datetimepicker.component.js.map