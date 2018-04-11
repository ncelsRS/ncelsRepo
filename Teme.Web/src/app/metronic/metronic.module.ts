import { NgModule } from '@angular/core';
import { ThemeComponent } from './theme/theme.component';
import { LayoutModule } from './theme/layouts/layout.module';

import { MetronicRoutingModule } from './metronic-routing.module';
import { MetronicComponent } from './metronic.component';
import { ScriptLoaderService } from "./_services/script-loader.service";
import { ThemeRoutingModule } from "./theme/theme-routing.module";
import { AuthModule } from "./auth/auth.module";
import { CommonModule } from "@angular/common";

@NgModule({
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
export class MetronicModule {
}