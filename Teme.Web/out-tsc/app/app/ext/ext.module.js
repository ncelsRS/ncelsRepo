var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExtLayoutComponent } from './ext-layout/ext-layout.component';
import { routing } from './ext.routing';
import { ExtUpHeaderComponent } from './ext-layout/ext-up-header/ext-up-header.component';
import { ExpMenuComponent } from './ext-layout/exp-menu/exp-menu.component';
import { ExtLayoutFooterComponent } from './ext-layout/ext-layout-footer/ext-layout-footer.component';
import { ExtLayoutHomeComponent } from './ext-layout-pages/ext-layout-home/ext-layout-home.component';
var ExtModule = (function () {
    function ExtModule() {
    }
    ExtModule = __decorate([
        NgModule({
            imports: [
                CommonModule,
                routing,
            ],
            declarations: [
                ExtLayoutComponent,
                ExtUpHeaderComponent,
                ExpMenuComponent,
                ExtLayoutFooterComponent,
                ExtLayoutHomeComponent
            ],
            exports: [
                ExtLayoutComponent,
                ExtUpHeaderComponent,
                ExtLayoutFooterComponent
            ]
        })
    ], ExtModule);
    return ExtModule;
}());
export { ExtModule };
//# sourceMappingURL=ext.module.js.map