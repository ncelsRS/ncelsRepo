var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { TimelineTimeline1Component } from './timeline-timeline-1.component';
import { LayoutModule } from '../../../../../layouts/layout.module';
import { DefaultComponent } from '../../../default.component';
var routes = [
    {
        "path": "",
        "component": DefaultComponent,
        "children": [
            {
                "path": "",
                "component": TimelineTimeline1Component
            }
        ]
    }
];
var TimelineTimeline1Module = (function () {
    function TimelineTimeline1Module() {
    }
    TimelineTimeline1Module = __decorate([
        NgModule({
            imports: [
                CommonModule, RouterModule.forChild(routes), LayoutModule
            ], exports: [
                RouterModule
            ], declarations: [
                TimelineTimeline1Component
            ]
        })
    ], TimelineTimeline1Module);
    return TimelineTimeline1Module;
}());
export { TimelineTimeline1Module };
//# sourceMappingURL=timeline-timeline-1.module.js.map