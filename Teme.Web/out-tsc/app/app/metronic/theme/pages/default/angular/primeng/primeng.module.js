var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { PrimengComponent } from './primeng.component';
import { PrimeNgInputComponent } from './input/primeng-input.component';
import { PrimeNgButtonComponent } from './button/primeng-button.component';
import { PrimeNgPanelComponent } from './panel/primeng-panel.component';
import { DefaultComponent } from '../../default.component';
import { LayoutModule } from '../../../../layouts/layout.module';
import { AccordionModule, ButtonModule, CheckboxModule, ChipsModule, CodeHighlighterModule, ColorPickerModule, InputMaskModule, FieldsetModule, GrowlModule, InputTextModule, MultiSelectModule, PanelModule, RadioButtonModule, SelectButtonModule, SplitButtonModule, TabViewModule } from 'primeng/primeng';
var routes = [
    {
        path: "",
        component: DefaultComponent,
        children: [
            {
                path: "",
                component: PrimengComponent,
                children: [
                    { path: 'input', component: PrimeNgInputComponent },
                    { path: 'button', component: PrimeNgButtonComponent },
                    { path: 'panel', component: PrimeNgPanelComponent },
                ]
            }
        ]
    },
];
var PrimengModule = (function () {
    function PrimengModule() {
    }
    PrimengModule = __decorate([
        NgModule({
            imports: [
                CommonModule, RouterModule.forChild(routes),
                LayoutModule,
                FormsModule,
                // primeng modules
                ButtonModule,
                CheckboxModule,
                ChipsModule,
                CodeHighlighterModule,
                ColorPickerModule,
                InputMaskModule,
                GrowlModule,
                InputTextModule,
                MultiSelectModule,
                RadioButtonModule,
                SelectButtonModule,
                SplitButtonModule,
                TabViewModule,
                AccordionModule,
                PanelModule,
                FieldsetModule
            ], declarations: [
                PrimengComponent,
                PrimeNgInputComponent,
                PrimeNgButtonComponent,
                PrimeNgPanelComponent
            ]
        })
    ], PrimengModule);
    return PrimengModule;
}());
export { PrimengModule };
//# sourceMappingURL=primeng.module.js.map