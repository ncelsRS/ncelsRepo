var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { DirectivesModule } from '../../../theme/directives/directives.module';
import { SmartComponent } from './smart/smart.component';
import { NgxComponent } from './ngx/ngx.component';
export var routes = [
    { path: '', redirectTo: 'smart', pathMatch: 'full' },
    { path: 'smart', component: SmartComponent, data: { breadcrumb: 'Smart DataTable' } },
    { path: 'ngx', component: NgxComponent, data: { breadcrumb: 'NGX DataTable' } }
];
var DynamicTablesModule = (function () {
    function DynamicTablesModule() {
    }
    DynamicTablesModule = __decorate([
        NgModule({
            imports: [
                CommonModule,
                RouterModule,
                Ng2SmartTableModule,
                NgxDatatableModule,
                DirectivesModule,
                RouterModule.forChild(routes)
            ],
            declarations: [
                SmartComponent,
                NgxComponent
            ]
        })
    ], DynamicTablesModule);
    return DynamicTablesModule;
}());
export { DynamicTablesModule };
//# sourceMappingURL=dynamic-tables.module.js.map