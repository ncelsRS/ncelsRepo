var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { Injectable } from '@angular/core';
import { Settings } from './app.settings.model';
var AppSettings = (function () {
    function AppSettings() {
        this.settings = new Settings('StartNG', 'Angular Admin Template with Bootstrap 4', {
            menu: 'vertical',
            menuType: 'default',
            showMenu: true,
            navbarIsFixed: true,
            footerIsFixed: false,
            sidebarIsFixed: true,
            showSideChat: false,
            sideChatIsHoverable: true,
            skin: 'light' //light , dark, blue, green, combined, purple, orange, brown, grey, pink
        });
    }
    AppSettings = __decorate([
        Injectable()
    ], AppSettings);
    return AppSettings;
}());
export { AppSettings };
//# sourceMappingURL=app.settings.js.map