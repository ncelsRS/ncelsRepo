var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { UserLogin4Component } from './user-login-4.component';
import { LayoutModule } from '../../../../../../layouts/layout.module';
var routes = [
    {
        "path": "",
        "component": UserLogin4Component
    }
];
var UserLogin4Module = (function () {
    function UserLogin4Module() {
    }
    UserLogin4Module = __decorate([
        NgModule({
            imports: [
                CommonModule, RouterModule.forChild(routes), LayoutModule
            ], exports: [
                RouterModule
            ], declarations: [
                UserLogin4Component
            ]
        })
    ], UserLogin4Module);
    return UserLogin4Module;
}());
export { UserLogin4Module };
//# sourceMappingURL=user-login-4.module.js.map