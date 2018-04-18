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
import { FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AppSettings } from '../../app.settings';
import { Menu } from '../../theme/components/menu/menu.model';
import { MenuService } from '../../theme/components/menu/menu.service';
import 'rxjs/add/operator/debounceTime';
var DynamicMenuComponent = (function () {
    function DynamicMenuComponent(fb, toastrService, appSettings, menuService) {
        this.fb = fb;
        this.toastrService = toastrService;
        this.appSettings = appSettings;
        this.menuService = menuService;
        this.targets = ['_blank', '_self'];
        this.icons = [
            { name: 'address-card-o', unicode: '&#xf2bc' },
            { name: 'bars', unicode: '&#xf0c9' },
            { name: 'bell-o', unicode: '&#xf0a2' },
            { name: 'calendar', unicode: '&#xf073' },
            { name: 'circle', unicode: '&#xf111' },
            { name: 'circle-o', unicode: '&#xf10c' },
            { name: 'cog', unicode: '&#xf013' },
            { name: 'comment', unicode: '&#xf075' },
            { name: 'comment-o', unicode: '&#xf0e5' },
            { name: 'credit-card', unicode: '&#xf09d' },
            { name: 'desktop', unicode: '&#xf108' },
            { name: 'exclamation-triangle', unicode: '&#xf071' },
            { name: 'folder', unicode: '&#xf07b' },
            { name: 'folder-o', unicode: '&#xf114' },
            { name: 'heart', unicode: '&#xf004' },
            { name: 'search', unicode: '&#xf002' }
        ];
        this.settings = this.appSettings.settings;
        if (this.settings.theme.menu == 'vertical') {
            this.menuItems = this.menuService.getVerticalMenuItems();
        }
        if (this.settings.theme.menu == 'horizontal') {
            this.menuItems = this.menuService.getHorizontalMenuItems();
        }
    }
    DynamicMenuComponent.prototype.ngOnInit = function () {
        this.form = this.fb.group({
            title: ['', Validators.compose([Validators.required, Validators.minLength(3)])],
            routerLink: null,
            href: null,
            icon: null,
            target: null,
            hasSubMenu: false,
            parentId: 0
        });
    };
    DynamicMenuComponent.prototype.ngAfterViewInit = function () {
        var _this = this;
        this.form.valueChanges.debounceTime(500).subscribe(function (menu) {
            if (menu.routerLink && menu.routerLink != '') {
                _this.form.controls['href'].setValue(null);
                _this.form.controls['href'].disable();
                _this.form.controls['target'].setValue(null);
                _this.form.controls['target'].disable();
            }
            else {
                _this.form.controls['href'].enable();
                _this.form.controls['target'].enable();
            }
            if (menu.href && menu.href != '') {
                _this.form.controls['routerLink'].setValue(null);
                _this.form.controls['routerLink'].disable();
                _this.form.controls['hasSubMenu'].setValue(false);
                _this.form.controls['hasSubMenu'].disable();
            }
            else {
                _this.form.controls['routerLink'].enable();
                _this.form.controls['hasSubMenu'].enable();
            }
        });
    };
    DynamicMenuComponent.prototype.onSubmit = function (menu) {
        if (this.form.valid) {
            var lastId = this.menuItems[this.menuItems.length - 1].id;
            var newMenuItem = new Menu(lastId + 1, menu['title'], menu['routerLink'], menu['href'], menu['icon'], menu['target'], menu['hasSubMenu'], parseInt(menu['parentId']));
            this.menuService.addNewMenuItem(this.menuItems, newMenuItem, this.settings.theme.menu);
            this.toastrService.success('New menu item successfully added !', menu['title']);
            this.form.reset({
                hasSubMenu: false,
                parentId: 0
            });
        }
        if (this.settings.theme.menuType == 'mini') {
            jQuery('.menu-item-link').tooltip('enable');
        }
        else {
            jQuery('.menu-item-link').tooltip('disable');
        }
    };
    DynamicMenuComponent = __decorate([
        Component({
            selector: 'app-dynamic-menu',
            templateUrl: './dynamic-menu.component.html',
            encapsulation: ViewEncapsulation.None,
            providers: [MenuService]
        }),
        __metadata("design:paramtypes", [FormBuilder, typeof (_a = typeof ToastrService !== "undefined" && ToastrService) === "function" && _a || Object, AppSettings,
            MenuService])
    ], DynamicMenuComponent);
    return DynamicMenuComponent;
    var _a;
}());
export { DynamicMenuComponent };
//# sourceMappingURL=dynamic-menu.component.js.map