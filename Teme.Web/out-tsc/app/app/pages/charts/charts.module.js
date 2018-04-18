var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { DirectivesModule } from '../../theme/directives/directives.module';
import { BarComponent } from './bar/bar.component';
import { PieComponent } from './pie/pie.component';
import { LineComponent } from './line/line.component';
import { BubbleComponent } from './bubble/bubble.component';
export var routes = [
    { path: '', redirectTo: 'bar', pathMatch: 'full' },
    { path: 'bar', component: BarComponent, data: { breadcrumb: 'Bar Charts' } },
    { path: 'pie', component: PieComponent, data: { breadcrumb: 'Pie Charts' } },
    { path: 'line', component: LineComponent, data: { breadcrumb: 'Line Charts' } },
    { path: 'bubble', component: BubbleComponent, data: { breadcrumb: 'Bubble Charts' } }
];
var ChartsModule = (function () {
    function ChartsModule() {
    }
    ChartsModule = __decorate([
        NgModule({
            imports: [
                CommonModule,
                NgxChartsModule,
                DirectivesModule,
                RouterModule.forChild(routes)
            ],
            declarations: [
                BarComponent,
                PieComponent,
                LineComponent,
                BubbleComponent
            ]
        })
    ], ChartsModule);
    return ChartsModule;
}());
export { ChartsModule };
//# sourceMappingURL=charts.module.js.map