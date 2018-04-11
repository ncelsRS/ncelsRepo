var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
var ExtContractComponent = (function () {
    function ExtContractComponent(route) {
        this.route = route;
    }
    ExtContractComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.route.params
            .subscribe(function (params) {
            _this.id = params.id;
        });
    };
    ExtContractComponent = __decorate([
        Component({
            selector: 'app-ext-contract',
            templateUrl: './ext-contract.component.html',
            styleUrls: ['./ext-contract.component.css']
        }),
        __metadata("design:paramtypes", [ActivatedRoute])
    ], ExtContractComponent);
    return ExtContractComponent;
}());
export { ExtContractComponent };
//# sourceMappingURL=ext-contract.component.js.map