import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ExtLayoutComponent} from './ext-layout/ext-layout.component';
import {routing} from './ext.routing';
import { TestComponent } from './ext-layout/test/test.component';
import { ExtUpHeaderComponent } from './ext-layout/ext-up-header/ext-up-header.component';
import { ExpMenuComponent } from './ext-layout/exp-menu/exp-menu.component';
import { ExtLayoutBodyComponent } from './ext-layout/ext-layout-body/ext-layout-body.component';
import { ExtLayoutFooterComponent} from './ext-layout/ext-layout-footer/ext-layout-footer.component';

@NgModule({
  imports: [
    CommonModule,
    routing
  ],
  declarations: [ExtLayoutComponent, TestComponent, ExtUpHeaderComponent, ExpMenuComponent, ExtLayoutBodyComponent, ExtLayoutFooterComponent
  ],
  exports: [
    ExtLayoutComponent, ExtUpHeaderComponent, ExtLayoutBodyComponent, ExtLayoutFooterComponent
  ],
  bootstrap: [ExtLayoutComponent, ExtUpHeaderComponent, ExtLayoutBodyComponent, ExtLayoutFooterComponent]
})
export class ExtModule {
}
