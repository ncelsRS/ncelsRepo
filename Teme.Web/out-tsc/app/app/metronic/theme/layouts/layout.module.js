var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { LayoutComponent } from './layout/layout.component';
import { AsideLeftMinimizeDefaultEnabledComponent } from '../pages/aside-left-minimize-default-enabled/aside-left-minimize-default-enabled.component';
import { HeaderNavComponent } from './header-nav/header-nav.component';
import { DefaultComponent } from '../pages/default/default.component';
import { AsideNavComponent } from './aside-nav/aside-nav.component';
import { FooterComponent } from './footer/footer.component';
import { QuickSidebarComponent } from './quick-sidebar/quick-sidebar.component';
import { ScrollTopComponent } from './scroll-top/scroll-top.component';
import { TooltipsComponent } from './tooltips/tooltips.component';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HrefPreventDefaultDirective } from '../../_directives/href-prevent-default.directive';
import { UnwrapTagDirective } from '../../_directives/unwrap-tag.directive';
var LayoutModule = (function () {
    function LayoutModule() {
    }
    LayoutModule = __decorate([
        NgModule({
            declarations: [
                LayoutComponent,
                AsideLeftMinimizeDefaultEnabledComponent,
                HeaderNavComponent,
                DefaultComponent,
                AsideNavComponent,
                FooterComponent,
                QuickSidebarComponent,
                ScrollTopComponent,
                TooltipsComponent,
                HrefPreventDefaultDirective,
                UnwrapTagDirective,
            ],
            exports: [
                LayoutComponent,
                AsideLeftMinimizeDefaultEnabledComponent,
                HeaderNavComponent,
                DefaultComponent,
                AsideNavComponent,
                FooterComponent,
                QuickSidebarComponent,
                ScrollTopComponent,
                TooltipsComponent,
                HrefPreventDefaultDirective,
            ],
            imports: [
                CommonModule,
                RouterModule,
            ]
        })
    ], LayoutModule);
    return LayoutModule;
}());
export { LayoutModule };
//# sourceMappingURL=layout.module.js.map