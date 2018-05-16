var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CalendarModule } from 'angular-calendar';
import { DirectivesModule } from '../../theme/directives/directives.module';
import { AppCalendarComponent } from './app-calendar.component';
export var routes = [
    { path: '', component: AppCalendarComponent, pathMatch: 'full' }
];
var AppCalendarModule = (function () {
    function AppCalendarModule() {
    }
    AppCalendarModule = __decorate([
        NgModule({
            imports: [
                CommonModule,
                FormsModule,
                CalendarModule,
                DirectivesModule,
                RouterModule.forChild(routes)
            ],
            declarations: [
                AppCalendarComponent
            ]
        })
    ], AppCalendarModule);
    return AppCalendarModule;
}());
export { AppCalendarModule };
//# sourceMappingURL=app-calendar.module.js.map