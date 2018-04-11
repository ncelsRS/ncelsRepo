var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { TimelineTimeline2Component } from './timeline-timeline-2.component';
import { LayoutModule } from '../../../../../layouts/layout.module';
import { DefaultComponent } from '../../../default.component';
var routes = [
    {
        "path": "",
        "component": DefaultComponent,
        "children": [
            {
                "path": "",
                "component": TimelineTimeline2Component
            }
        ]
    }
];
var TimelineTimeline2Module = (function () {
    function TimelineTimeline2Module() {
    }
    TimelineTimeline2Module = __decorate([
        NgModule({
            imports: [
                CommonModule, RouterModule.forChild(routes), LayoutModule
            ], exports: [
                RouterModule
            ], declarations: [
                TimelineTimeline2Component
            ]
        })
    ], TimelineTimeline2Module);
    return TimelineTimeline2Module;
}());
export { TimelineTimeline2Module };
//# sourceMappingURL=timeline-timeline-2.module.js.map