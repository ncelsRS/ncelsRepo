var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { UserLogin2Component } from './user-login-2.component';
import { LayoutModule } from '../../../../../../layouts/layout.module';
var routes = [
    {
        "path": "",
        "component": UserLogin2Component
    }
];
var UserLogin2Module = (function () {
    function UserLogin2Module() {
    }
    UserLogin2Module = __decorate([
        NgModule({
            imports: [
                CommonModule, RouterModule.forChild(routes), LayoutModule
            ], exports: [
                RouterModule
            ], declarations: [
                UserLogin2Component
            ]
        })
    ], UserLogin2Module);
    return UserLogin2Module;
}());
export { UserLogin2Module };
//# sourceMappingURL=user-login-2.module.js.map