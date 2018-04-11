var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { LogoutComponent } from "./auth/logout/logout.component";
var routes = [
    { path: 'login', loadChildren: './auth/auth.module#AuthModule' },
    { path: 'logout', component: LogoutComponent }
];
var MetronicRoutingModule = (function () {
    function MetronicRoutingModule() {
    }
    MetronicRoutingModule = __decorate([
        NgModule({
            imports: [RouterModule.forChild(routes)],
            exports: [RouterModule]
        })
    ], MetronicRoutingModule);
    return MetronicRoutingModule;
}());
export { MetronicRoutingModule };
//# sourceMappingURL=metronic-routing.module.js.map