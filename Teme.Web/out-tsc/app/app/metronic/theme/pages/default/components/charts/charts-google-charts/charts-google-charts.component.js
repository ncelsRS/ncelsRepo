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
import { ScriptLoaderService } from '../../../../../../_services/script-loader.service';
var ChartsGoogleChartsComponent = (function () {
    function ChartsGoogleChartsComponent(_script) {
        this._script = _script;
    }
    ChartsGoogleChartsComponent.prototype.ngOnInit = function () {
    };
    ChartsGoogleChartsComponent.prototype.ngAfterViewInit = function () {
        this._script.loadScripts('app-charts-google-charts', ['assets/demo/default/custom/components/charts/google-charts.js']);
        google.load('visualization', '1', { packages: ['corechart', 'bar', 'line'], callback: GoogleChartsDemo.runDemos() });
    };
    ChartsGoogleChartsComponent = __decorate([
        Component({
            selector: "app-charts-google-charts",
            templateUrl: "./charts-google-charts.component.html",
            encapsulation: ViewEncapsulation.None,
        }),
        __metadata("design:paramtypes", [ScriptLoaderService])
    ], ChartsGoogleChartsComponent);
    return ChartsGoogleChartsComponent;
}());
export { ChartsGoogleChartsComponent };
//# sourceMappingURL=charts-google-charts.component.js.map