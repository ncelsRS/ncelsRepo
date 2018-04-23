import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {routing} from './int.routing';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {IntHomeComponent} from './int-home/int-home.component';
import {IntLayoutComponent } from './int-layout/int-layout.component';
import {IntMenuComponent} from "./int-menu/int-menu.component";
import {FlagsMenuComponent} from './flags-menu/flags-menu.component'
import {MessagesComponent} from './messages/messages.component'
import {UserMenuComponent} from './user-menu/user-menu.component'
import { PipesModule } from '../theme/pipes/pipes.module';
import {HorizontalMenuComponent} from "../shared/menu/horizontal-menu/horizontal-menu.component";

@NgModule({
  imports: [
    CommonModule,
    routing,
    PipesModule,
    NgbModule.forRoot()
  ],
  declarations: [
    IntHomeComponent,
    IntLayoutComponent,
    IntMenuComponent,
    FlagsMenuComponent,
    MessagesComponent,
    UserMenuComponent,
    HorizontalMenuComponent
  ]
})
export class IntModule {
}
