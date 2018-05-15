import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UploadComponent } from './upload/upload.component';
import {FileUploadModule} from 'ng2-file-upload';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

@NgModule({
  imports: [
    CommonModule,
    FileUploadModule
  ],
  declarations: [UploadComponent],
  exports: [UploadComponent]
})
export class RscFileModule { }
