import {CUSTOM_ELEMENTS_SCHEMA, NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ExtLayoutBodyComponent} from './ext-layout-body.component';
import {ExtLayoutBodyLeftComponent} from './ext-layout-body-left/ext-layout-body-left.component';
import {ExtLayoutBodyRightComponent} from './ext-layout-body-right/ext-layout-body-right.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [ ExtLayoutBodyComponent, ExtLayoutBodyLeftComponent, ExtLayoutBodyRightComponent],
  entryComponents: [ ExtLayoutBodyComponent, ExtLayoutBodyLeftComponent, ExtLayoutBodyRightComponent],
  exports: [
    ExtLayoutBodyComponent, ExtLayoutBodyLeftComponent, ExtLayoutBodyRightComponent
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class ExtLayoutBodyModule {
}
