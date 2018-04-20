import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {routing} from './int.routing';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {IntHomeComponent} from './int-home/int-home.component';
import {IntLayoutComponent } from './int-layout/int-layout.component';
import {IntMenuComponent} from "./int-menu/int-menu.component";

@NgModule({
  imports: [
    CommonModule,
    routing,
    NgbModule.forRoot()
  ],
  declarations: [
    IntHomeComponent,
    IntLayoutComponent,
    IntMenuComponent
  ]
})
export class IntModule {
}
