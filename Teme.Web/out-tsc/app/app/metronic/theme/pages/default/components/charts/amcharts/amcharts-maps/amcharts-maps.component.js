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
var AmchartsMapsComponent = (function () {
    function AmchartsMapsComponent(_script) {
        this._script = _script;
    }
    AmchartsMapsComponent.prototype.ngOnInit = function () {
    };
    AmchartsMapsComponent.prototype.ngAfterViewInit = function () {
        this._script.loadScripts('app-amcharts-maps', ['//www.amcharts.com/lib/3/plugins/export/export.min.js',
            'assets/demo/default/custom/components/charts/amcharts/maps.js']);
        Helpers.loadStyles('app-amcharts-maps', [
            '//www.amcharts.com/lib/3/plugins/export/export.css'
        ]);
    };
    AmchartsMapsComponent = __decorate([
        Component({
            selector: "app-amcharts-maps",
            templateUrl: "./amcharts-maps.component.html",
            encapsulation: ViewEncapsulation.None,
        }),
        __metadata("design:paramtypes", [ScriptLoaderService])
    ], AmchartsMapsComponent);
    return AmchartsMapsComponent;
}());
export { AmchartsMapsComponent };
//# sourceMappingURL=amcharts-maps.component.js.map