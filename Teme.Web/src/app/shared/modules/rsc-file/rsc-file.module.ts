import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UploadComponent } from './upload/upload.component';
import {FileUploadModule} from 'ng2-file-upload';
import {RscFileService} from './upload/rsc-file.service';

@NgModule({
  imports: [
    CommonModule,
    FileUploadModule
  ],
  declarations: [UploadComponent],
  providers: [RscFileService],
  exports: [UploadComponent]
})
export class RscFileModule { }
