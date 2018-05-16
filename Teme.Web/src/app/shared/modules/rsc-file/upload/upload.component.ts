import {Component, ElementRef, Input, OnInit, ViewChild, ViewEncapsulation} from '@angular/core';
import {FileItem, FileUploader} from 'ng2-file-upload';
import {animate, state, style, transition, trigger} from '@angular/animations';
import {environment} from '../../../../../environments/environment';
import {EntityTypes, FilePermissions, FileTypeCodes} from '../file-enums';

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

  @Input() entityType: EntityTypes;
  @Input() entityId: string;
  @Input() private fileType: string;
  @Input() multiple: boolean;
  @Input() required: boolean;
  @Input() permissions: FilePermissions[];

  private _files: any[] = [];

  uploader: FileUploader;

  constructor() {
    this.uploader = new FileUploader({
      url: environment.urls.files + '/files'
    });
    this.uploader.onBuildItemForm = (fileItem, form) => {
      let meta = {
        entityType: this.entityType,
        entityId: this.entityId,
        fileType: this.fileType
      };
      form.append('meta', JSON.stringify(meta));
    };
    this.uploader.onAfterAddingFile = fileItem => {
      this._files.push({id: null, fileItem: fileItem});
      fileItem.onComplete = response => {
        this._files.find(x => x.fileItem == fileItem).id = JSON.parse(response).fileId;
      };
      fileItem.upload();
    };
  }

  public fileTypeValue: string;

  ngOnInit() {
    this.fileTypeValue = FileTypeCodes[this.fileType].value;
  }

  @ViewChild('uploaderEl') uploaderEl: ElementRef;

  selectFile(e) {
    let event = new MouseEvent('click', {bubbles: false});
    this.uploaderEl.nativeElement.dispatchEvent(event);
  }

  show: boolean = true;

  public canAdd(): boolean {
    return this.permissions.includes(FilePermissions.all)
      || this.permissions.includes(FilePermissions.add);
  }

  public downloadFile(item: FileItem) {
    let id = this._files.find(x => x.fileItem == item).id;
    window.location.href = environment.urls.files + '/files/' + id;
  }

}
