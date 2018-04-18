var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ExtContractsComponent } from './ext-contracts/ext-contracts.component';
import { ExtContractComponent } from './ext-contract/ext-contract.component';
var routes = [
    {
        path: '',
        data: {
            title: 'Договора'
        },
        children: [
            {
                path: '',
                component: ExtContractsComponent,
                data: { title: 'Договора' }
            },
            {
                path: ':id',
                data: { title: 'Договор' },
                component: ExtContractComponent
            }
        ]
    }
];
var ExtContractRoutingModule = (function () {
    function ExtContractRoutingModule() {
    }
    ExtContractRoutingModule = __decorate([
        NgModule({
            imports: [RouterModule.forChild(routes)],
            exports: [RouterModule]
        })
    ], ExtContractRoutingModule);
    return ExtContractRoutingModule;
}());
export { ExtContractRoutingModule };
//# sourceMappingURL=ext-contract-routing.module.js.map