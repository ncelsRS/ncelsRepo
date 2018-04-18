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
import { AppSettings } from '../../../app.settings';
import { cost } from '../dashboard.data';
function getNewTime(d) {
    var h = (d.getHours() < 10 ? '0' : '') + d.getHours(), m = (d.getMinutes() < 10 ? '0' : '') + d.getMinutes(), s = (d.getSeconds() < 10 ? '0' : '') + d.getSeconds(), time = h + ":" + m + ":" + s;
    return time;
}
var CostComponent = (function () {
    function CostComponent(appSettings) {
        var _this = this;
        this.appSettings = appSettings;
        this.showXAxis = true;
        this.showYAxis = true;
        this.gradient = true;
        this.tooltipDisabled = false;
        this.showLegend = false;
        this.showXAxisLabel = true;
        this.xAxisLabel = 'Time';
        this.showYAxisLabel = true;
        this.yAxisLabel = 'Cost';
        this.colorScheme = {
            domain: ['#0096A6', '#D22E2E']
        };
        this.autoScale = true;
        this.settings = this.appSettings.settings;
        this.initPreviousSettings();
        setInterval(function () {
            _this.cost = _this.addRandomValue().slice();
        }, 3000);
    }
    CostComponent.prototype.ngOnInit = function () {
        this.cost = cost;
    };
    CostComponent.prototype.onSelect = function (event) {
        console.log(event);
    };
    CostComponent.prototype.addRandomValue = function () {
        var value1 = Math.random() * 1000000;
        this.cost[0].series.push({ "name": getNewTime(new Date()), "value": value1 });
        var value2 = Math.random() * 1000000;
        this.cost[1].series.push({ "name": getNewTime(new Date()), "value": value2 });
        if (this.cost[0].series.length > 5)
            this.cost[0].series.splice(0, 1);
        if (this.cost[1].series.length > 5)
            this.cost[1].series.splice(0, 1);
        return this.cost;
    };
    CostComponent.prototype.ngOnDestroy = function () {
        this.cost[0].series.length = 0;
    };
    CostComponent.prototype.ngDoCheck = function () {
        var _this = this;
        if (this.checkAppSettingsChanges()) {
            setTimeout(function () { return _this.cost = cost.slice(); });
            this.initPreviousSettings();
        }
    };
    CostComponent.prototype.checkAppSettingsChanges = function () {
        if (this.previousShowMenuOption != this.settings.theme.showMenu ||
            this.previousMenuOption != this.settings.theme.menu ||
            this.previousMenuTypeOption != this.settings.theme.menuType) {
            return true;
        }
        return false;
    };
    CostComponent.prototype.initPreviousSettings = function () {
        this.previousShowMenuOption = this.settings.theme.showMenu;
        this.previousMenuOption = this.settings.theme.menu;
        this.previousMenuTypeOption = this.settings.theme.menuType;
    };
    CostComponent = __decorate([
        Component({
            selector: 'app-cost',
            templateUrl: './cost.component.html',
            encapsulation: ViewEncapsulation.None
        }),
        __metadata("design:paramtypes", [AppSettings])
    ], CostComponent);
    return CostComponent;
}());
export { CostComponent };
//# sourceMappingURL=cost.component.js.map