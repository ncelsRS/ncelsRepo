var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
var DEFAULT_PERFECT_SCROLLBAR_CONFIG = {
    suppressScrollX: true
};
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastrModule } from 'ngx-toastr';
import { MultiselectDropdownModule } from 'angular-2-dropdown-multiselect';
import { PipesModule } from '../theme/pipes/pipes.module';
import { routing } from './pages.routing';
import { PagesComponent } from './pages.component';
import { HeaderComponent } from '../theme/components/header/header.component';
import { FooterComponent } from '../theme/components/footer/footer.component';
import { SidebarComponent } from '../theme/components/sidebar/sidebar.component';
import { VerticalMenuComponent } from '../theme/components/menu/vertical-menu/vertical-menu.component';
import { HorizontalMenuComponent } from '../theme/components/menu/horizontal-menu/horizontal-menu.component';
import { BreadcrumbComponent } from '../theme/components/breadcrumb/breadcrumb.component';
import { BackTopComponent } from '../theme/components/back-top/back-top.component';
import { FullScreenComponent } from '../theme/components/fullscreen/fullscreen.component';
import { ApplicationsComponent } from '../theme/components/applications/applications.component';
import { MessagesComponent } from '../theme/components/messages/messages.component';
import { UserMenuComponent } from '../theme/components/user-menu/user-menu.component';
import { FlagsMenuComponent } from '../theme/components/flags-menu/flags-menu.component';
import { SideChatComponent } from '../theme/components/side-chat/side-chat.component';
import { FavoritesComponent } from '../theme/components/favorites/favorites.component';
import { BlankComponent } from './blank/blank.component';
import { SearchComponent } from './search/search.component';
var PagesModule = (function () {
    function PagesModule() {
    }
    PagesModule = __decorate([
        NgModule({
            imports: [
                CommonModule,
                FormsModule,
                PerfectScrollbarModule,
                ToastrModule.forRoot(),
                NgbModule.forRoot(),
                MultiselectDropdownModule,
                PipesModule,
                routing
            ],
            declarations: [
                PagesComponent,
                HeaderComponent,
                FooterComponent,
                SidebarComponent,
                VerticalMenuComponent,
                HorizontalMenuComponent,
                BreadcrumbComponent,
                BackTopComponent,
                FullScreenComponent,
                ApplicationsComponent,
                MessagesComponent,
                UserMenuComponent,
                FlagsMenuComponent,
                SideChatComponent,
                FavoritesComponent,
                BlankComponent,
                SearchComponent
            ],
            providers: [
                {
                    provide: PERFECT_SCROLLBAR_CONFIG,
                    useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
                }
            ]
        })
    ], PagesModule);
    return PagesModule;
}());
export { PagesModule };
//# sourceMappingURL=pages.module.js.map