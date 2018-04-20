import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {routing} from './int.routing';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {IntHomeComponent} from './int-home/int-home.component';
import {IntLayoutComponent } from './int-layout/int-layout.component';
import {IntMenuComponent} from "./int-menu/int-menu.component";
import { IntContentComponent } from './int-layout/int-content/int-content.component';

@NgModule({
  imports: [
    CommonModule,
    routing,
    NgbModule.forRoot()
  ],
  declarations: [
    IntHomeComponent,
    IntLayoutComponent,
    IntMenuComponent,
    IntContentComponent
  ]
})
export class IntModule {
}
