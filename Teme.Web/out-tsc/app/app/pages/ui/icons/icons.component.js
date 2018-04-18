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
import { IconsService } from './icons.service';
var IconsComponent = (function () {
    function IconsComponent(iconsService) {
        this.iconsService = iconsService;
        this.icons = iconsService.getIcons();
    }
    IconsComponent.prototype.ngOnInit = function () {
    };
    IconsComponent = __decorate([
        Component({
            selector: 'app-icons',
            templateUrl: './icons.component.html',
            styleUrls: ['./icons.component.scss'],
            encapsulation: ViewEncapsulation.None,
            providers: [IconsService]
        }),
        __metadata("design:paramtypes", [IconsService])
    ], IconsComponent);
    return IconsComponent;
}());
export { IconsComponent };
//# sourceMappingURL=icons.component.js.map