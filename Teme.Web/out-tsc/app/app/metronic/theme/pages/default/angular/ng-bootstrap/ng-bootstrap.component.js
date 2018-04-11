var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, Input, ViewEncapsulation } from '@angular/core';
import { FormBuilder } from "@angular/forms";
import { ModalDismissReasons, NgbModal } from "@ng-bootstrap/ng-bootstrap";
var now = new Date();
var NgBootstrapComponent = (function () {
    function NgBootstrapComponent(formBuilder, modalService) {
        this.formBuilder = formBuilder;
        this.modalService = modalService;
        this.alerts = [];
        this.isCollapsed = false;
        this.page = 4;
        this.name = 'World';
        this.ratingSelected = 0;
        this.ratingHovered = 0;
        this.ratingReadonly = false;
        this.timepickerTime = { hour: 13, minute: 30 };
        this.timepickerMeridian = true;
        this.checkboxModel = { left: true, middle: false, right: false };
    }
    NgBootstrapComponent.prototype.ngOnInit = function () {
        this.alertInit();
    };
    NgBootstrapComponent.prototype.alertInit = function () {
        this.alerts.push({
            id: 1,
            type: 'success',
            message: 'This is an success alert',
        }, {
            id: 2,
            type: 'info',
            message: 'This is an info alert',
        }, {
            id: 3,
            type: 'warning',
            message: 'This is a warning alert',
        }, {
            id: 4,
            type: 'danger',
            message: 'This is a danger alert',
        });
        this.backup = this.alerts.map(function (alert) { return Object.assign({}, alert); });
    };
    NgBootstrapComponent.prototype.alertClose = function (alert) {
        var index = this.alerts.indexOf(alert);
        this.alerts.splice(index, 1);
    };
    NgBootstrapComponent.prototype.alertReset = function () {
        this.alerts = this.backup.map(function (alert) { return Object.assign({}, alert); });
    };
    NgBootstrapComponent.prototype.datepickerToday = function () {
        this.datepickerModel = { year: now.getFullYear(), month: now.getMonth() + 1, day: now.getDate() };
    };
    NgBootstrapComponent.prototype.modalOpen = function (content) {
        var _this = this;
        this.modalService.open(content).result.then(function (result) {
            _this.modalClose = "Closed with: " + result;
        }, function (reason) {
            _this.modalClose = "Dismissed " + _this.modalDismissReason(reason);
        });
    };
    NgBootstrapComponent.prototype.timepickerToggle = function () {
        this.timepickerMeridian = !this.timepickerMeridian;
    };
    NgBootstrapComponent.prototype.modalDismissReason = function (reason) {
        if (reason === ModalDismissReasons.ESC) {
            return 'by pressing ESC';
        }
        else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
            return 'by clicking on a backdrop';
        }
        else {
            return "with: " + reason;
        }
    };
    __decorate([
        Input(),
        __metadata("design:type", Array)
    ], NgBootstrapComponent.prototype, "alerts", void 0);
    NgBootstrapComponent = __decorate([
        Component({
            selector: ".m-grid__item.m-grid__item--fluid.m-wrapper",
            templateUrl: "./ng-bootstrap.component.html",
            encapsulation: ViewEncapsulation.None,
        }),
        __metadata("design:paramtypes", [FormBuilder,
            NgbModal])
    ], NgBootstrapComponent);
    return NgBootstrapComponent;
}());
export { NgBootstrapComponent };
//# sourceMappingURL=ng-bootstrap.component.js.map