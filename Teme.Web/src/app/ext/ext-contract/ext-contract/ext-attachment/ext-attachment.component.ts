import {Component, Input} from '@angular/core';
import {FileUploader} from 'ng2-file-upload';
import {FileTypeCodes, FilePermissions, EntityTypes} from '../../../../shared/modules/rsc-file/file-enums';

const URL = 'https://evening-anchorage-3159.herokuapp.com/api/';

@Component({
  selector: 'app-ext-attachment',
  templateUrl: './ext-attachment.component.html',
  styleUrls: ['./ext-attachment.component.css']
})
export class ExtAttachmentComponent {

  @Input() contractId: number;

  public fileEntityTypes = EntityTypes;
  public fileTypes = FileTypeCodes;
  public filePermissions = FilePermissions;

  uploader: FileUploader;
  hasBaseDropZoneOver: boolean;
  hasAnotherDropZoneOver: boolean;
  response: string;

  constructor() {
    this.uploader = new FileUploader({
      url: URL,
      disableMultipart: true, // 'DisableMultipart' must be 'true' for formatDataFunction to be called.
      formatDataFunctionIsAsync: true,
      formatDataFunction: async (item) => {
        return new Promise((resolve, reject) => {
          resolve({
            name: item._file.name,
            length: item._file.size,
            contentType: item._file.type,
            date: new Date()
          });
        });
      }
    });
    this.hasBaseDropZoneOver = false;
    this.hasAnotherDropZoneOver = false;
    this.response = '';
    this.uploader.response.subscribe(res => this.response = res);
  }

  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  public fileOverAnother(e: any): void {
    this.hasAnotherDropZoneOver = e;
  }
}
