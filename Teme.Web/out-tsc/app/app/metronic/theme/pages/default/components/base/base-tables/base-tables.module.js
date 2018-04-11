var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { BaseTablesComponent } from './base-tables.component';
import { LayoutModule } from '../../../../../layouts/layout.module';
import { DefaultComponent } from '../../../default.component';
var routes = [
    {
        "path": "",
        "component": DefaultComponent,
        "children": [
            {
                "path": "",
                "component": BaseTablesComponent
            }
        ]
    }
];
var BaseTablesModule = (function () {
    function BaseTablesModule() {
    }
    BaseTablesModule = __decorate([
        NgModule({
            imports: [
                CommonModule, RouterModule.forChild(routes), LayoutModule
            ], exports: [
                RouterModule
            ], declarations: [
                BaseTablesComponent
            ]
        })
    ], BaseTablesModule);
    return BaseTablesModule;
}());
export { BaseTablesModule };
//# sourceMappingURL=base-tables.module.js.map