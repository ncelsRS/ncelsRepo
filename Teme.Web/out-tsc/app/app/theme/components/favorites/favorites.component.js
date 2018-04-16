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
import { MenuService } from '../menu/menu.service';
var FavoritesComponent = (function () {
    function FavoritesComponent(menuService) {
        var _this = this;
        this.menuService = menuService;
        this.favoriteModel = [];
        this.favoriteSettings = {
            enableSearch: true,
            checkedStyle: 'fontawesome',
            buttonClasses: 'btn btn-secondary btn-block',
            dynamicTitleMaxItems: 0,
            displayAllSelectedText: true
        };
        this.favoriteTexts = {
            checkAll: 'Select all',
            uncheckAll: 'Unselect all',
            checked: 'menu item selected',
            checkedPlural: 'menu items selected',
            searchPlaceholder: 'Find menu item...',
            defaultTitle: 'Select favorite menu items',
            allSelected: 'All selected',
        };
        this.favoriteOptions = [];
        this.favorites = [];
        this.menuItems = this.menuService.getVerticalMenuItems().filter(function (menu) { return menu.routerLink != null || menu.href != null; });
        this.menuItems.forEach(function (item) {
            var menu = {
                id: item.id,
                name: item.title,
                routerLink: item.routerLink,
                href: item.href,
                icon: item.icon,
                target: item.target
            };
            _this.favoriteOptions.push(menu);
        });
        if (sessionStorage["favorites"]) {
            this.favorites = JSON.parse(sessionStorage.getItem("favorites"));
            this.favorites.forEach(function (favorite) {
                _this.favoriteModel.push(favorite.id);
            });
        }
    }
    FavoritesComponent.prototype.onDropdownOpened = function () {
        this.toggle = true;
    };
    FavoritesComponent.prototype.onDropdownClosed = function () {
        this.toggle = false;
    };
    FavoritesComponent.prototype.onChange = function () {
        var _this = this;
        this.favorites.length = 0;
        this.favoriteModel.forEach(function (i) {
            var favorite = _this.favoriteOptions.find(function (mail) { return mail.id === +i; });
            _this.favorites.push(favorite);
        });
        sessionStorage.setItem("favorites", JSON.stringify(this.favorites));
    };
    FavoritesComponent = __decorate([
        Component({
            selector: 'app-favorites',
            templateUrl: './favorites.component.html',
            styleUrls: ['./favorites.component.scss'],
            encapsulation: ViewEncapsulation.None,
            providers: [MenuService]
        }),
        __metadata("design:paramtypes", [MenuService])
    ], FavoritesComponent);
    return FavoritesComponent;
}());
export { FavoritesComponent };
//# sourceMappingURL=favorites.component.js.map