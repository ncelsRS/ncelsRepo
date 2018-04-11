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
var WizardWizard4Component = (function () {
    function WizardWizard4Component(_script) {
        this._script = _script;
    }
    WizardWizard4Component.prototype.ngOnInit = function () {
    };
    WizardWizard4Component.prototype.ngAfterViewInit = function () {
        this._script.loadScripts('app-wizard-wizard-4', ['assets/demo/default/custom/components/forms/wizard/wizard.js']);
    };
    WizardWizard4Component = __decorate([
        Component({
            selector: "app-wizard-wizard-4",
            templateUrl: "./wizard-wizard-4.component.html",
            encapsulation: ViewEncapsulation.None,
        }),
        __metadata("design:paramtypes", [ScriptLoaderService])
    ], WizardWizard4Component);
    return WizardWizard4Component;
}());
export { WizardWizard4Component };
//# sourceMappingURL=wizard-wizard-4.component.js.map