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
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { DragulaModule } from 'ng2-dragula';
import { ResizableModule } from 'angular-resizable-element';
import { DirectivesModule } from '../../theme/directives/directives.module';
import { DragDropComponent } from './drag-drop/drag-drop.component';
import { ToasterComponent } from './toaster/toaster.component';
import { ResizableComponent } from './resizable/resizable.component';
export var routes = [
    { path: '', redirectTo: 'drag-drop', pathMatch: 'full' },
    { path: 'drag-drop', component: DragDropComponent, data: { breadcrumb: 'Drag and Drop' } },
    { path: 'resizable', component: ResizableComponent, data: { breadcrumb: 'Resizable' } },
    { path: 'toaster', component: ToasterComponent, data: { breadcrumb: 'Toaster' } }
];
var ToolsModule = (function () {
    function ToolsModule() {
    }
    ToolsModule = __decorate([
        NgModule({
            imports: [
                CommonModule,
                RouterModule.forChild(routes),
                FormsModule,
                PerfectScrollbarModule,
                DragulaModule,
                ResizableModule,
                DirectivesModule
            ],
            declarations: [
                DragDropComponent,
                ToasterComponent,
                ResizableComponent
            ]
        })
    ], ToolsModule);
    return ToolsModule;
}());
export { ToolsModule };
//# sourceMappingURL=tools.module.js.map