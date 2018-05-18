import { NgModule  } from '@angular/core';
import { CommonModule } from '@angular/common';
import {IconExtButton} from './icon-ext-button';
import {IconIntButton} from './icon-int-button';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  declarations: [
    IconExtButton,
    IconIntButton
  ],
  exports: [
    IconExtButton,
    IconIntButton
  ],
})
export class IconModule { }
