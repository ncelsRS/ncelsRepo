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
var MapsJqvmapComponent = (function () {
    function MapsJqvmapComponent(_script) {
        this._script = _script;
    }
    MapsJqvmapComponent.prototype.ngOnInit = function () {
    };
    MapsJqvmapComponent.prototype.ngAfterViewInit = function () {
        this._script.loadScripts('app-maps-jqvmap', ['assets/vendors/custom/jqvmap/jqvmap.bundle.js',
            'assets/demo/default/custom/components/maps/jqvmap.js']);
    };
    MapsJqvmapComponent = __decorate([
        Component({
            selector: "app-maps-jqvmap",
            templateUrl: "./maps-jqvmap.component.html",
            encapsulation: ViewEncapsulation.None,
        }),
        __metadata("design:paramtypes", [ScriptLoaderService])
    ], MapsJqvmapComponent);
    return MapsJqvmapComponent;
}());
export { MapsJqvmapComponent };
//# sourceMappingURL=maps-jqvmap.component.js.map