var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Directive, ElementRef, Input } from '@angular/core';
var HrefPreventDefaultDirective = (function () {
    function HrefPreventDefaultDirective(el) {
        this.el = el;
    }
    HrefPreventDefaultDirective.prototype.ngAfterViewInit = function () {
    };
    HrefPreventDefaultDirective.prototype.preventDefault = function (event) {
        if (this.href.length === 0 || this.href === '#') {
            event.preventDefault();
        }
    };
    __decorate([
        Input(),
        __metadata("design:type", String)
    ], HrefPreventDefaultDirective.prototype, "href", void 0);
    HrefPreventDefaultDirective = __decorate([
        Directive({
            selector: "[href]",
            host: { '(click)': 'preventDefault($event)' },
        }),
        __metadata("design:paramtypes", [ElementRef])
    ], HrefPreventDefaultDirective);
    return HrefPreventDefaultDirective;
}());
export { HrefPreventDefaultDirective };
//# sourceMappingURL=href-prevent-default.directive.js.map