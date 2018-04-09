import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ExtLayoutComponent} from './ext-layout/ext-layout.component';
import {routing} from './ext.routing';
import {ExtUpHeaderComponent} from './ext-layout/ext-up-header/ext-up-header.component';
import {ExpMenuComponent} from './ext-layout/exp-menu/exp-menu.component';
import {ExtLayoutFooterComponent} from './ext-layout/ext-layout-footer/ext-layout-footer.component';
import {ExtLayoutHomeComponent} from './ext-layout-pages/ext-layout-home/ext-layout-home.component';

@NgModule({
  imports: [
    CommonModule,
    routing
  ],
  declarations: [
    ExtLayoutComponent,
    ExtUpHeaderComponent,
    ExpMenuComponent,
    ExtLayoutFooterComponent,
    ExtLayoutHomeComponent
  ],
  exports: [
    ExtLayoutComponent,
    ExtUpHeaderComponent,
    ExtLayoutFooterComponent
  ]
})
export class ExtModule {
}
