var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExtDeclarationRoutingModule } from './ext-declaration-routing.module';
import { ExtDeclarationsComponent } from './ext-declarations/ext-declarations.component';
import { ExtDeclarationComponent } from './ext-declaration/ext-declaration.component';
import { ExtGeneralInformationComponent } from './ext-declaration/ext-general-information/ext-general-information.component';
import { ExtImnSetComponent } from './ext-declaration/ext-imn-set/ext-imn-set.component';
import { FormsModule } from '@angular/forms';
import { ExtProducerComponent } from './ext-declaration/ext-producer/ext-producer.component';
import { ExtAgreementComponent } from './ext-declaration/ext-agreement/ext-agreement.component';
var ExtDeclarationModule = (function () {
    function ExtDeclarationModule() {
    }
    ExtDeclarationModule = __decorate([
        NgModule({
            imports: [
                CommonModule,
                ExtDeclarationRoutingModule,
                FormsModule
            ],
            declarations: [
                ExtDeclarationsComponent,
                ExtDeclarationComponent,
                ExtGeneralInformationComponent,
                ExtImnSetComponent,
                ExtProducerComponent,
                ExtAgreementComponent
            ]
        })
    ], ExtDeclarationModule);
    return ExtDeclarationModule;
}());
export { ExtDeclarationModule };
//# sourceMappingURL=ext-declaration.module.js.map