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
var WizardWizard2Component = (function () {
    function WizardWizard2Component(_script) {
        this._script = _script;
    }
    WizardWizard2Component.prototype.ngOnInit = function () {
    };
    WizardWizard2Component.prototype.ngAfterViewInit = function () {
        this._script.loadScripts('app-wizard-wizard-2', ['assets/demo/default/custom/components/forms/wizard/wizard.js']);
    };
    WizardWizard2Component = __decorate([
        Component({
            selector: "app-wizard-wizard-2",
            templateUrl: "./wizard-wizard-2.component.html",
            encapsulation: ViewEncapsulation.None,
        }),
        __metadata("design:paramtypes", [ScriptLoaderService])
    ], WizardWizard2Component);
    return WizardWizard2Component;
}());
export { WizardWizard2Component };
//# sourceMappingURL=wizard-wizard-2.component.js.map