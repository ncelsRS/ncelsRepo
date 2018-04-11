var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { ThemeComponent } from './theme/theme.component';
import { LayoutModule } from './theme/layouts/layout.module';
import { MetronicRoutingModule } from './metronic-routing.module';
import { MetronicComponent } from './metronic.component';
import { ScriptLoaderService } from "./_services/script-loader.service";
import { ThemeRoutingModule } from "./theme/theme-routing.module";
import { AuthModule } from "./auth/auth.module";
import { CommonModule } from "@angular/common";
var MetronicModule = (function () {
    function MetronicModule() {
    }
    MetronicModule = __decorate([
        NgModule({
            declarations: [
                ThemeComponent,
                MetronicComponent,
            ],
            imports: [
                LayoutModule,
                CommonModule,
                MetronicRoutingModule,
                ThemeRoutingModule,
                AuthModule,
            ],
            providers: [ScriptLoaderService],
            bootstrap: [MetronicComponent]
        })
    ], MetronicModule);
    return MetronicModule;
}());
export { MetronicModule };
//# sourceMappingURL=metronic.module.js.map