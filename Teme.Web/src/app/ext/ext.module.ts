import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ExtLayoutComponent} from './ext-layout/ext-layout.component';
import {ExtLayoutBodyComponent} from './ext-layout/ext-layout-body/ext-layout-body.component';
import {ExtLayoutFooterComponent} from './ext-layout/ext-layout-footer/ext-layout-footer.component';
import {routing} from './ext.routing';
import { TestComponent } from './ext-layout/test/test.component';

@NgModule({
  imports: [
    CommonModule,
    routing
  ],
  declarations: [ExtLayoutComponent, TestComponent, ExtLayoutBodyComponent, ExtLayoutFooterComponent],
  exports: [
    ExtLayoutComponent
  ]
})
export class ExtModule {
}
