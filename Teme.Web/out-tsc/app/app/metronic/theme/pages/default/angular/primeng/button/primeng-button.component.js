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
var PrimeNgButtonComponent = (function () {
    function PrimeNgButtonComponent() {
        var _this = this;
        this.clicks = 0;
        this.msgs = [];
        this.items = [
            {
                label: 'Update', icon: 'fa-refresh', command: function () {
                    _this.update();
                }
            },
            {
                label: 'Delete', icon: 'fa-close', command: function () {
                    _this.delete();
                }
            },
            { label: 'Angular.io', icon: 'fa-link', url: 'http://angular.io' },
            { label: 'Theming', icon: 'fa-paint-brush', routerLink: ['/theme'] }
        ];
    }
    PrimeNgButtonComponent.prototype.ngOnInit = function () {
    };
    PrimeNgButtonComponent.prototype.onclickCount = function () {
        this.clicks++;
    };
    PrimeNgButtonComponent.prototype.save = function () {
        this.msgs = [];
        this.msgs.push({ severity: 'info', summary: 'Success', detail: 'Data Saved' });
    };
    PrimeNgButtonComponent.prototype.update = function () {
        this.msgs = [];
        this.msgs.push({ severity: 'info', summary: 'Success', detail: 'Data Updated' });
    };
    PrimeNgButtonComponent.prototype.delete = function () {
        this.msgs = [];
        this.msgs.push({ severity: 'info', summary: 'Success', detail: 'Data Deleted' });
    };
    PrimeNgButtonComponent = __decorate([
        Component({
            selector: "app-primeng-button",
            templateUrl: "./primeng-button.component.html",
            encapsulation: ViewEncapsulation.None,
        }),
        __metadata("design:paramtypes", [])
    ], PrimeNgButtonComponent);
    return PrimeNgButtonComponent;
}());
export { PrimeNgButtonComponent };
//# sourceMappingURL=primeng-button.component.js.map