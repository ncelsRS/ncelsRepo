var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Injectable, Renderer2 } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { verticalMenuItems } from './menu';
import { horizontalMenuItems } from './menu';
var MenuService = (function () {
    function MenuService(location, renderer2, router) {
        this.location = location;
        this.renderer2 = renderer2;
        this.router = router;
    }
    MenuService.prototype.getVerticalMenuItems = function () {
        return verticalMenuItems;
    };
    MenuService.prototype.getHorizontalMenuItems = function () {
        return horizontalMenuItems;
    };
    MenuService.prototype.createMenu = function (menu, nativeElement, type) {
        if (type == 'vertical') {
            this.createVerticalMenu(menu, nativeElement);
        }
        if (type == 'horizontal') {
            this.createHorizontalMenu(menu, nativeElement);
        }
    };
    MenuService.prototype.createVerticalMenu = function (menu, nativeElement) {
        var _this = this;
        var menu0 = this.renderer2.createElement('div');
        this.renderer2.setAttribute(menu0, 'id', 'menu0');
        menu.forEach(function (menuItem) {
            if (menuItem.parentId == 0) {
                var subMenu = _this.createVerticalMenuItem(menu, menuItem);
                _this.renderer2.appendChild(menu0, subMenu);
            }
        });
        this.renderer2.appendChild(nativeElement, menu0);
    };
    MenuService.prototype.createHorizontalMenu = function (menu, nativeElement) {
        var _this = this;
        var nav = this.renderer2.createElement('div');
        this.renderer2.setAttribute(nav, 'id', 'navigation');
        var ul = this.renderer2.createElement('ul');
        this.renderer2.addClass(ul, 'menu');
        this.renderer2.appendChild(nav, ul);
        menu.forEach(function (menuItem) {
            if (menuItem.parentId == 0) {
                var subMenu = _this.createHorizontalMenuItem(menu, menuItem);
                _this.renderer2.appendChild(ul, subMenu);
            }
        });
        this.renderer2.appendChild(nativeElement, nav);
    };
    MenuService.prototype.createVerticalMenuItem = function (menu, menuItem) {
        var _this = this;
        var div = this.renderer2.createElement('div');
        this.renderer2.addClass(div, 'card');
        var link = this.renderer2.createElement('a');
        this.renderer2.addClass(link, 'menu-item-link');
        this.renderer2.setAttribute(link, 'data-toggle', 'tooltip');
        this.renderer2.setAttribute(link, 'data-placement', 'right');
        this.renderer2.setAttribute(link, 'data-animation', 'false');
        this.renderer2.setAttribute(link, 'data-container', '.vertical-menu-tooltip-place');
        this.renderer2.setAttribute(link, 'data-original-title', menuItem.title);
        var icon = this.renderer2.createElement('i');
        this.renderer2.addClass(icon, 'fa');
        this.renderer2.addClass(icon, 'fa-' + menuItem.icon);
        this.renderer2.appendChild(link, icon);
        var span = this.renderer2.createElement('span');
        this.renderer2.addClass(span, 'menu-title');
        this.renderer2.appendChild(link, span);
        var menuText = this.renderer2.createText(menuItem.title);
        this.renderer2.appendChild(span, menuText);
        this.renderer2.setAttribute(link, 'id', 'link' + menuItem.id);
        this.renderer2.addClass(link, 'transition');
        this.renderer2.appendChild(div, link);
        if (menuItem.routerLink) {
            this.renderer2.listen(link, "click", function () {
                _this.router.navigate([menuItem.routerLink]);
                _this.setActiveLink(menu, link);
                _this.closeOtherSubMenus(div);
            });
        }
        if (menuItem.href) {
            this.renderer2.setAttribute(link, 'href', menuItem.href);
        }
        if (menuItem.target) {
            this.renderer2.setAttribute(link, 'target', menuItem.target);
        }
        if (menuItem.hasSubMenu) {
            this.renderer2.addClass(link, 'collapsed');
            var caret = this.renderer2.createElement('b');
            this.renderer2.addClass(caret, 'fa');
            this.renderer2.addClass(caret, 'fa-angle-up');
            this.renderer2.appendChild(link, caret);
            this.renderer2.setAttribute(link, 'data-toggle', 'collapse');
            this.renderer2.setAttribute(link, 'href', '#collapse' + menuItem.id);
            var collapse = this.renderer2.createElement('div');
            this.renderer2.setAttribute(collapse, 'id', 'collapse' + menuItem.id);
            this.renderer2.setAttribute(collapse, 'data-parent', '#menu' + menuItem.parentId);
            this.renderer2.addClass(collapse, 'collapse');
            this.renderer2.appendChild(div, collapse);
            this.createSubMenu(menu, menuItem.id, collapse, 'vertical');
        }
        return div;
    };
    MenuService.prototype.createHorizontalMenuItem = function (menu, menuItem) {
        var _this = this;
        var li = this.renderer2.createElement('li');
        this.renderer2.addClass(li, 'menu-item');
        var link = this.renderer2.createElement('a');
        this.renderer2.addClass(link, 'menu-item-link');
        this.renderer2.setAttribute(link, 'data-toggle', 'tooltip');
        this.renderer2.setAttribute(link, 'data-placement', 'top');
        this.renderer2.setAttribute(link, 'data-animation', 'false');
        this.renderer2.setAttribute(link, 'data-container', '.horizontal-menu-tooltip-place');
        this.renderer2.setAttribute(link, 'data-original-title', menuItem.title);
        var icon = this.renderer2.createElement('i');
        this.renderer2.addClass(icon, 'fa');
        this.renderer2.addClass(icon, 'fa-' + menuItem.icon);
        this.renderer2.appendChild(link, icon);
        var span = this.renderer2.createElement('span');
        this.renderer2.addClass(span, 'menu-title');
        this.renderer2.appendChild(link, span);
        var menuText = this.renderer2.createText(menuItem.title);
        this.renderer2.appendChild(span, menuText);
        this.renderer2.appendChild(li, link);
        this.renderer2.setAttribute(link, 'id', 'link' + menuItem.id);
        this.renderer2.addClass(link, 'transition');
        if (menuItem.routerLink) {
            this.renderer2.listen(link, "click", function () {
                _this.router.navigate([menuItem.routerLink]);
                _this.setActiveLink(menu, link);
            });
        }
        if (menuItem.href) {
            this.renderer2.setAttribute(link, 'href', menuItem.href);
        }
        if (menuItem.target) {
            this.renderer2.setAttribute(link, 'target', menuItem.target);
        }
        if (menuItem.hasSubMenu) {
            this.renderer2.addClass(li, 'menu-item-has-children');
            var subMenu = this.renderer2.createElement('ul');
            this.renderer2.addClass(subMenu, 'sub-menu');
            this.renderer2.appendChild(li, subMenu);
            this.createSubMenu(menu, menuItem.id, subMenu, 'horizontal');
        }
        return li;
    };
    MenuService.prototype.createSubMenu = function (menu, menuItemId, parentElement, type) {
        var _this = this;
        var menus = menu.filter(function (item) { return item.parentId === menuItemId; });
        menus.forEach(function (menuItem) {
            var subMenu = null;
            if (type == 'vertical') {
                subMenu = _this.createVerticalMenuItem(menu, menuItem);
            }
            if (type == 'horizontal') {
                subMenu = _this.createHorizontalMenuItem(menu, menuItem);
            }
            _this.renderer2.appendChild(parentElement, subMenu);
        });
    };
    MenuService.prototype.closeOtherSubMenus = function (elem) {
        var children = (this.renderer2.parentNode(elem)).children;
        for (var i = 0; i < children.length; i++) {
            var child = this.renderer2.nextSibling(children[i].children[0]);
            if (child) {
                this.renderer2.addClass(children[i].children[0], 'collapsed');
                this.renderer2.removeClass(child, 'show');
            }
        }
    };
    MenuService.prototype.getActiveLink = function (menu) {
        var url = this.location.path();
        var routerLink = url; // url.substring(1, url.length);
        var activeMenuItem = menu.filter(function (item) { return item.routerLink === routerLink; });
        if (activeMenuItem[0]) {
            var activeLink = document.querySelector("#link" + activeMenuItem[0].id);
            return activeLink;
        }
        return false;
    };
    MenuService.prototype.setActiveLink = function (menu, link) {
        if (link) {
            menu.forEach(function (menuItem) {
                var activeLink = document.querySelector("#link" + menuItem.id);
                if (activeLink) {
                    if (activeLink.classList.contains('active-link')) {
                        activeLink.classList.remove('active-link');
                    }
                }
            });
            this.renderer2.addClass(link, 'active-link');
        }
    };
    MenuService.prototype.showActiveSubMenu = function (menu) {
        var url = this.location.path();
        var routerLink = url; //url.substring(1, url.length);
        var activeMenuItem = menu.filter(function (item) { return item.routerLink === routerLink; });
        if (activeMenuItem[0]) {
            var activeLink = document.querySelector("#link" + activeMenuItem[0].id);
            var parent_1 = this.renderer2.parentNode(activeLink);
            while (this.renderer2.parentNode(parent_1)) {
                parent_1 = this.renderer2.parentNode(parent_1);
                if (parent_1.className == 'collapse') {
                    var parentMenu = menu.filter(function (item) { return item.id === activeMenuItem[0].parentId; });
                    var activeParentLink = document.querySelector("#link" + parentMenu[0].id);
                    this.renderer2.removeClass(activeParentLink, 'collapsed');
                    this.renderer2.addClass(parent_1, 'show');
                }
                if (parent_1.classList.contains('menu-wrapper')) {
                    break;
                }
            }
        }
    };
    MenuService.prototype.addNewMenuItem = function (menu, newMenuItem, type) {
        menu.push(newMenuItem);
        if (newMenuItem.parentId != 0) {
            var parentMenu = menu.filter(function (item) { return item.id === newMenuItem.parentId; });
            if (parentMenu.length) {
                if (!parentMenu[0].hasSubMenu) {
                    parentMenu[0].hasSubMenu = true;
                    // parentMenu[0].routerLink = null;
                }
            }
        }
        var menu_wrapper = null;
        if (type == 'vertical') {
            menu_wrapper = document.getElementById('vertical-menu');
        }
        if (type == 'horizontal') {
            menu_wrapper = document.getElementById('horizontal-menu');
        }
        while (menu_wrapper.firstChild) {
            menu_wrapper.removeChild(menu_wrapper.firstChild);
        }
        this.createMenu(menu, menu_wrapper, type);
    };
    MenuService = __decorate([
        Injectable(),
        __metadata("design:paramtypes", [Location,
            Renderer2,
            Router])
    ], MenuService);
    return MenuService;
}());
export { MenuService };
//# sourceMappingURL=menu.service.js.map