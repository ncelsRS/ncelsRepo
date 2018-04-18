var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ExtDeclarationsComponent } from './ext-declarations/ext-declarations.component';
import { ExtDeclarationComponent } from './ext-declaration/ext-declaration.component';
var routes = [
    {
        path: '',
        data: {
            title: 'Заявления'
        },
        children: [
            {
                path: '',
                component: ExtDeclarationsComponent,
                data: { title: 'Заявления' }
            },
            {
                path: ':id',
                data: { title: 'Заявление' },
                component: ExtDeclarationComponent
            }
        ]
    }
];
var ExtDeclarationRoutingModule = (function () {
    function ExtDeclarationRoutingModule() {
    }
    ExtDeclarationRoutingModule = __decorate([
        NgModule({
            imports: [RouterModule.forChild(routes)],
            exports: [RouterModule]
        })
    ], ExtDeclarationRoutingModule);
    return ExtDeclarationRoutingModule;
}());
export { ExtDeclarationRoutingModule };
//# sourceMappingURL=ext-declaration-routing.module.js.map