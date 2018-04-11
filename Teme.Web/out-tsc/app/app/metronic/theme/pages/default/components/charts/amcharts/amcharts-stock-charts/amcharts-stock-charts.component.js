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
var AmchartsStockChartsComponent = (function () {
    function AmchartsStockChartsComponent(_script) {
        this._script = _script;
    }
    AmchartsStockChartsComponent.prototype.ngOnInit = function () {
    };
    AmchartsStockChartsComponent.prototype.ngAfterViewInit = function () {
        this._script.loadScripts('app-amcharts-stock-charts', ['//www.amcharts.com/lib/3/plugins/export/export.min.js',
            'assets/demo/default/custom/components/charts/amcharts/stock-charts.js']);
        Helpers.loadStyles('app-amcharts-stock-charts', [
            '//www.amcharts.com/lib/3/plugins/export/export.css'
        ]);
    };
    AmchartsStockChartsComponent = __decorate([
        Component({
            selector: "app-amcharts-stock-charts",
            templateUrl: "./amcharts-stock-charts.component.html",
            encapsulation: ViewEncapsulation.None,
        }),
        __metadata("design:paramtypes", [ScriptLoaderService])
    ], AmchartsStockChartsComponent);
    return AmchartsStockChartsComponent;
}());
export { AmchartsStockChartsComponent };
//# sourceMappingURL=amcharts-stock-charts.component.js.map