var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BaseRequestOptions, HttpModule } from '@angular/http';
import { MockBackend } from '@angular/http/testing';
import { AuthRoutingModule } from './auth-routing.routing';
import { AuthComponent } from './auth.component';
import { AlertComponent } from './_directives/alert.component';
import { LogoutComponent } from './logout/logout.component';
import { AuthGuard } from './_guards/auth.guard';
import { AlertService } from './_services/alert.service';
import { AuthenticationService } from './_services/authentication.service';
import { UserService } from './_services/user.service';
import { fakeBackendProvider } from './_helpers/index';
var AuthModule = (function () {
    function AuthModule() {
    }
    AuthModule = __decorate([
        NgModule({
            declarations: [
                AuthComponent,
                AlertComponent,
                LogoutComponent,
            ],
            imports: [
                CommonModule,
                FormsModule,
                HttpModule,
                AuthRoutingModule,
            ],
            providers: [
                AuthGuard,
                AlertService,
                AuthenticationService,
                UserService,
                // api backend simulation
                fakeBackendProvider,
                MockBackend,
                BaseRequestOptions,
            ],
            entryComponents: [AlertComponent],
        })
    ], AuthModule);
    return AuthModule;
}());
export { AuthModule };
//# sourceMappingURL=auth.module.js.map