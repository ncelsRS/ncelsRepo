var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { WidgetsSummernoteComponent } from './widgets-summernote.component';
import { LayoutModule } from '../../../../../../layouts/layout.module';
import { DefaultComponent } from '../../../../default.component';
var routes = [
    {
        "path": "",
        "component": DefaultComponent,
        "children": [
            {
                "path": "",
                "component": WidgetsSummernoteComponent
            }
        ]
    }
];
var WidgetsSummernoteModule = (function () {
    function WidgetsSummernoteModule() {
    }
    WidgetsSummernoteModule = __decorate([
        NgModule({
            imports: [
                CommonModule, RouterModule.forChild(routes), LayoutModule
            ], exports: [
                RouterModule
            ], declarations: [
                WidgetsSummernoteComponent
            ]
        })
    ], WidgetsSummernoteModule);
    return WidgetsSummernoteModule;
}());
export { WidgetsSummernoteModule };
//# sourceMappingURL=widgets-summernote.module.js.map