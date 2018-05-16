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
import { orders, products, customers, refunds } from '../dashboard.data';
var InfoCardsComponent = (function () {
    function InfoCardsComponent(appSettings) {
        this.appSettings = appSettings;
        this.colorScheme = {
            domain: ['#FFFFFF']
        };
        this.autoScale = true;
        this.settings = this.appSettings.settings;
        this.initPreviousSettings();
    }
    InfoCardsComponent.prototype.ngOnInit = function () {
        this.orders = orders;
        this.products = products;
        this.customers = customers;
        this.refunds = refunds;
        this.orders = this.addRandomValue('orders');
        this.customers = this.addRandomValue('customers');
    };
    InfoCardsComponent.prototype.onSelect = function (event) {
        console.log(event);
    };
    InfoCardsComponent.prototype.addRandomValue = function (param) {
        switch (param) {
            case 'orders':
                for (var i = 1; i < 30; i++) {
                    this.orders[0].series.push({ "name": 1980 + i, "value": Math.ceil(Math.random() * 1000000) });
                }
                return this.orders;
            case 'customers':
                for (var i = 1; i < 15; i++) {
                    this.customers[0].series.push({ "name": 2000 + i, "value": Math.ceil(Math.random() * 1000000) });
                }
                return this.customers;
            default:
                return this.orders;
        }
    };
    InfoCardsComponent.prototype.ngOnDestroy = function () {
        this.orders[0].series.length = 0;
        this.customers[0].series.length = 0;
    };
    InfoCardsComponent.prototype.ngDoCheck = function () {
        var _this = this;
        if (this.checkAppSettingsChanges()) {
            setTimeout(function () { return _this.orders = orders.slice(); });
            setTimeout(function () { return _this.products = products.slice(); });
            setTimeout(function () { return _this.customers = customers.slice(); });
            setTimeout(function () { return _this.refunds = refunds.slice(); });
            this.initPreviousSettings();
        }
    };
    InfoCardsComponent.prototype.checkAppSettingsChanges = function () {
        if (this.previousShowMenuOption != this.settings.theme.showMenu ||
            this.previousMenuOption != this.settings.theme.menu ||
            this.previousMenuTypeOption != this.settings.theme.menuType) {
            return true;
        }
        return false;
    };
    InfoCardsComponent.prototype.initPreviousSettings = function () {
        this.previousShowMenuOption = this.settings.theme.showMenu;
        this.previousMenuOption = this.settings.theme.menu;
        this.previousMenuTypeOption = this.settings.theme.menuType;
    };
    InfoCardsComponent = __decorate([
        Component({
            selector: 'app-info-cards',
            templateUrl: './info-cards.component.html',
            encapsulation: ViewEncapsulation.None
        }),
        __metadata("design:paramtypes", [AppSettings])
    ], InfoCardsComponent);
    return InfoCardsComponent;
}());
export { InfoCardsComponent };
//# sourceMappingURL=info-cards.component.js.map