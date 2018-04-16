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
import { countries } from '../dashboard.data';
var VisitorsComponent = (function () {
    function VisitorsComponent(appSettings) {
        this.appSettings = appSettings;
        this.colorScheme = {
            domain: ['#378D3B', '#D22E2E', '#F47B00', '#AAAAAA']
        };
        this.gradient = true;
        this.tooltipDisabled = false;
        this.settings = this.appSettings.settings;
        this.initPreviousSettings();
    }
    VisitorsComponent.prototype.visitorsLabelFormat = function (c) {
        switch (c.label) {
            case 'Germany':
                return "<span class=\"flag-icon flag-icon-de mr-2\"></span>" + c.label;
            case 'France':
                return "<span class=\"flag-icon flag-icon-fr mr-2\"></span>" + c.label;
            case 'Great Britain':
                return "<span class=\"flag-icon flag-icon-gb mr-2\"></span>" + c.label;
            default:
                return c.label;
        }
    };
    VisitorsComponent.prototype.ngOnInit = function () {
        var _this = this;
        setTimeout(function () { return _this.countries = countries; });
        this.countries = countries;
    };
    VisitorsComponent.prototype.onSelect = function (event) {
        console.log(event);
    };
    VisitorsComponent.prototype.ngDoCheck = function () {
        var _this = this;
        if (this.checkAppSettingsChanges()) {
            setTimeout(function () { return _this.countries = countries.slice(); });
            this.initPreviousSettings();
        }
    };
    VisitorsComponent.prototype.checkAppSettingsChanges = function () {
        if (this.previousShowMenuOption != this.settings.theme.showMenu ||
            this.previousMenuOption != this.settings.theme.menu ||
            this.previousMenuTypeOption != this.settings.theme.menuType) {
            return true;
        }
        return false;
    };
    VisitorsComponent.prototype.initPreviousSettings = function () {
        this.previousShowMenuOption = this.settings.theme.showMenu;
        this.previousMenuOption = this.settings.theme.menu;
        this.previousMenuTypeOption = this.settings.theme.menuType;
    };
    VisitorsComponent = __decorate([
        Component({
            selector: 'app-visitors',
            templateUrl: './visitors.component.html',
            styleUrls: ['./visitors.component.scss'],
            encapsulation: ViewEncapsulation.None
        }),
        __metadata("design:paramtypes", [AppSettings])
    ], VisitorsComponent);
    return VisitorsComponent;
}());
export { VisitorsComponent };
//# sourceMappingURL=visitors.component.js.map