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
var VerticalMenuComponent = (function () {
    function VerticalMenuComponent(appSettings, menuService, router, elementRef) {
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
                if (window.innerWidth <= 768) {
                    _this.settings.theme.showMenu = false;
                }
            }
        });
    }
    VerticalMenuComponent.prototype.ngOnInit = function () {
        var menu_wrapper = this.elementRef.nativeElement.children[0];
        this.menuService.createMenu(this.menuItems, menu_wrapper, 'vertical');
        if (this.settings.theme.menuType == 'mini')
            jQuery('.menu-item-link').tooltip();
    };
    VerticalMenuComponent.prototype.ngAfterViewInit = function () {
        this.menuService.showActiveSubMenu(this.menuItems);
        var activeLink = this.menuService.getActiveLink(this.menuItems);
        this.menuService.setActiveLink(this.menuItems, activeLink);
    };
    __decorate([
        Input('menuItems'),
        __metadata("design:type", Object)
    ], VerticalMenuComponent.prototype, "menuItems", void 0);
    VerticalMenuComponent = __decorate([
        Component({
            selector: 'app-vertical-menu',
            templateUrl: './vertical-menu.component.html',
            styleUrls: ['./vertical-menu.component.scss'],
            encapsulation: ViewEncapsulation.None,
            providers: [MenuService]
        }),
        __metadata("design:paramtypes", [AppSettings,
            MenuService,
            Router,
            ElementRef])
    ], VerticalMenuComponent);
    return VerticalMenuComponent;
}());
export { VerticalMenuComponent };
//# sourceMappingURL=vertical-menu.component.js.map