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
var InfoPanelsComponent = (function () {
    function InfoPanelsComponent(appSettings) {
        this.appSettings = appSettings;
        this.sales = [{ name: 'sales', value: 0.81, extra: { format: 'percent' } }];
        this.salesBgColor = { domain: ['#2F3E9E'] };
        this.likes = [{ name: 'likes', value: 47588 }];
        this.likesBgColor = { domain: ['#D22E2E'] };
        this.downloads = [{ name: 'downloads', value: 189730 }];
        this.downloadsBgColor = { domain: ['#378D3B'] };
        this.profit = [{ name: 'profit', value: 52470, extra: { format: 'currency' } }];
        this.profitBgColor = { domain: ['#0096A6'] };
        this.messages = [{ name: 'messages', value: 75296 }];
        this.messagesBgColor = { domain: ['#606060'] };
        this.members = [{ name: 'members', value: 216279 }];
        this.membersBgColor = { domain: ['#F47B00'] };
        this.settings = this.appSettings.settings;
        this.initPreviousSettings();
    }
    InfoPanelsComponent.prototype.infoLabelFormat = function (c) {
        switch (c.data.name) {
            case 'sales':
                return "<i class=\"fa fa-shopping-cart mr-2\"></i>" + c.label;
            case 'likes':
                return "<i class=\"fa fa-thumbs-o-up mr-2\"></i>" + c.label;
            case 'downloads':
                return "<i class=\"fa fa-download mr-2\"></i>" + c.label;
            case 'profit':
                return "<i class=\"fa fa-money mr-2\"></i>" + c.label;
            case 'messages':
                return "<i class=\"fa fa-comment-o mr-2\"></i>" + c.label;
            case 'members':
                return "<i class=\"fa fa-user mr-2\"></i>" + c.label;
            default:
                return c.label;
        }
    };
    InfoPanelsComponent.prototype.infoValueFormat = function (c) {
        switch (c.data.extra ? c.data.extra.format : '') {
            case 'currency':
                return "$" + Math.round(c.value).toLocaleString();
            case 'percent':
                return Math.round(c.value * 100) + "%";
            default:
                return c.value.toLocaleString();
        }
    };
    InfoPanelsComponent.prototype.onSelect = function (event) {
        console.log(event);
    };
    InfoPanelsComponent.prototype.ngDoCheck = function () {
        var _this = this;
        if (this.checkAppSettingsChanges()) {
            setTimeout(function () { return _this.sales = _this.sales.slice(); });
            setTimeout(function () { return _this.likes = _this.likes.slice(); });
            setTimeout(function () { return _this.downloads = _this.downloads.slice(); });
            setTimeout(function () { return _this.profit = _this.profit.slice(); });
            setTimeout(function () { return _this.messages = _this.messages.slice(); });
            setTimeout(function () { return _this.members = _this.members.slice(); });
            this.initPreviousSettings();
        }
    };
    InfoPanelsComponent.prototype.checkAppSettingsChanges = function () {
        if (this.previousShowMenuOption != this.settings.theme.showMenu ||
            this.previousMenuOption != this.settings.theme.menu ||
            this.previousMenuTypeOption != this.settings.theme.menuType) {
            return true;
        }
        return false;
    };
    InfoPanelsComponent.prototype.initPreviousSettings = function () {
        this.previousShowMenuOption = this.settings.theme.showMenu;
        this.previousMenuOption = this.settings.theme.menu;
        this.previousMenuTypeOption = this.settings.theme.menuType;
    };
    InfoPanelsComponent = __decorate([
        Component({
            selector: 'app-info-panels',
            templateUrl: './info-panels.component.html',
            encapsulation: ViewEncapsulation.None
        }),
        __metadata("design:paramtypes", [AppSettings])
    ], InfoPanelsComponent);
    return InfoPanelsComponent;
}());
export { InfoPanelsComponent };
//# sourceMappingURL=info-panels.component.js.map