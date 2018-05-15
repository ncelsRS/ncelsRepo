import {Component, ElementRef, Input, OnInit, ViewChild, ViewEncapsulation} from '@angular/core';
import {FileUploader} from 'ng2-file-upload';
import {animate, state, style, transition, trigger} from '@angular/animations';

@Component({
  selector: 'rsc-file-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: [
    trigger('hiddenAnimation', [
      state('true', style({
        height: '0px'
      })),
      state('false', style({
        height: '*'
      })),
      transition('true <=> false', animate('200ms ease'))
    ])
  ]
})
export class UploadComponent implements OnInit {

  @Input() label: string;
  @Input() multiple: boolean;



  uploader: FileUploader;

  constructor() {
    this.uploader = new FileUploader({
      url: '',
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
  }

  ngOnInit() {
  }

  @ViewChild('uploaderEl') uploaderEl: ElementRef;

  selectFile(e) {
    let event = new MouseEvent('click', {bubbles: false});
    this.uploaderEl.nativeElement.dispatchEvent(event);
  }

  show: boolean = true;

}
