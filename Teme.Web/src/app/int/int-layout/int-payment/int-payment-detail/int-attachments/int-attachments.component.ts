import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {FileUploader} from "ng2-file-upload";
import {IconExtModal} from 'app/shared/icon/icon-ext-modal';

const URL = 'https://evening-anchorage-3159.herokuapp.com/api/';

@Component({
  selector: 'app-int-attachments',
  templateUrl: './int-attachments.component.html',
  styleUrls: ['./int-attachments.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [
    IconExtModal
  ]
})
export class IntAttachmentsComponent implements OnInit {
  uploader: FileUploader;
  hasBaseDropZoneOver: boolean;
  hasAnotherDropZoneOver: boolean;
  response: string;
  constructor(public iconModal:  IconExtModal) {
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
  ngOnInit() {
  }

}

