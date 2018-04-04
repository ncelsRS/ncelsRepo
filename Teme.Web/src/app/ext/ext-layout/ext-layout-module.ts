import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ExtLayoutBodyComponent} from './ext-layout-body/ext-layout-body.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [ExtLayoutBodyComponent ],
  exports: [    ExtLayoutBodyComponent
  ]
})
export class ExtLayoutModule {
}
