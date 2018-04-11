var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ErrorsError6Component } from './errors-error-6.component';
import { LayoutModule } from '../../../../../../layouts/layout.module';
var routes = [
    {
        "path": "",
        "component": ErrorsError6Component
    }
];
var ErrorsError6Module = (function () {
    function ErrorsError6Module() {
    }
    ErrorsError6Module = __decorate([
        NgModule({
            imports: [
                CommonModule, RouterModule.forChild(routes), LayoutModule
            ], exports: [
                RouterModule
            ], declarations: [
                ErrorsError6Component
            ]
        })
    ], ErrorsError6Module);
    return ErrorsError6Module;
}());
export { ErrorsError6Module };
//# sourceMappingURL=errors-error-6.module.js.map