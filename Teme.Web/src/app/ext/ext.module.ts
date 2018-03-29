import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ExtLayoutComponent} from './ext-layout/ext-layout.component';
import {routing} from './ext.routing';
import { TestComponent } from './ext-layout/test/test.component';
import { ExtUpHeaderComponent } from './ext-layout/ext-up-header/ext-up-header.component';

@NgModule({
  imports: [
    CommonModule,
    routing
  ],
  declarations: [ExtLayoutComponent, TestComponent, ExtUpHeaderComponent],
  exports: [
    ExtLayoutComponent, ExtUpHeaderComponent
  ],
  bootstrap: [ExtLayoutComponent, ExtUpHeaderComponent]
})
export class ExtModule {
}
