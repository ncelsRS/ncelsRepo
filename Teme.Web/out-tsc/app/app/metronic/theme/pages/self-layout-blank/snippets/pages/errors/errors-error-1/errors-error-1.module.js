var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ErrorsError1Component } from './errors-error-1.component';
import { LayoutModule } from '../../../../../../layouts/layout.module';
var routes = [
    {
        "path": "",
        "component": ErrorsError1Component
    }
];
var ErrorsError1Module = (function () {
    function ErrorsError1Module() {
    }
    ErrorsError1Module = __decorate([
        NgModule({
            imports: [
                CommonModule, RouterModule.forChild(routes), LayoutModule
            ], exports: [
                RouterModule
            ], declarations: [
                ErrorsError1Component
            ]
        })
    ], ErrorsError1Module);
    return ErrorsError1Module;
}());
export { ErrorsError1Module };
//# sourceMappingURL=errors-error-1.module.js.map