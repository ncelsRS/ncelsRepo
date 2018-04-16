var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, ViewEncapsulation, ElementRef, Input } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { MenuService } from '../menu.service';
import { AppSettings } from '../../../../app.settings';
var HorizontalMenuComponent = (function () {
    function HorizontalMenuComponent(appSettings, menuService, router, elementRef) {
        var _this = this;
        this.appSettings = appSettings;
        this.menuService = menuService;
        this.router = router;
        this.elementRef = elementRef;
        this.settings = this.appSettings.settings;
        this.router.events.subscribe(function (event) {
            if (event instanceof NavigationEnd) {
                window.scrollTo(0, 0);
                var activeLink = _this.menuService.getActiveLink(_this.menuItems);
                _this.menuService.setActiveLink(_this.menuItems, activeLink);
                jQuery('.tooltip').tooltip('hide');
            }
        });
    }
    HorizontalMenuComponent.prototype.ngOnInit = function () {
        var menu_wrapper = this.elementRef.nativeElement.children[0];
        this.menuService.createMenu(this.menuItems, menu_wrapper, 'horizontal');
        if (this.settings.theme.menuType == 'mini')
            jQuery('.menu-item-link').tooltip();
    };
    HorizontalMenuComponent.prototype.ngAfterViewInit = function () {
        var activeLink = this.menuService.getActiveLink(this.menuItems);
        this.menuService.setActiveLink(this.menuItems, activeLink);
    };
    __decorate([
        Input('menuItems'),
        __metadata("design:type", Object)
    ], HorizontalMenuComponent.prototype, "menuItems", void 0);
    HorizontalMenuComponent = __decorate([
        Component({
            selector: 'app-horizontal-menu',
            templateUrl: './horizontal-menu.component.html',
            styleUrls: ['./horizontal-menu.component.scss'],
            encapsulation: ViewEncapsulation.None,
            providers: [MenuService]
        }),
        __metadata("design:paramtypes", [AppSettings,
            MenuService,
            Router,
            ElementRef])
    ], HorizontalMenuComponent);
    return HorizontalMenuComponent;
}());
export { HorizontalMenuComponent };
//# sourceMappingURL=horizontal-menu.component.js.map