import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ExtLayoutComponent} from './ext-layout/ext-layout.component';
import {routing} from './ext.routing';
import { TestComponent } from './ext-layout/test/test.component';

@NgModule({
  imports: [
    CommonModule,
    routing
  ],
  declarations: [ExtLayoutComponent, TestComponent],
  exports: [
    ExtLayoutComponent
  ]
})
export class ExtModule {
}
