import {Component, ElementRef, Input, OnInit, ViewChild, ViewEncapsulation} from '@angular/core';
import {FileItem, FileUploader} from 'ng2-file-upload';
import {animate, state, style, transition, trigger} from '@angular/animations';
import {environment} from '../../../../../environments/environment';
import {EntityTypes, FilePermissions, FileTypeCodes} from '../file-enums';
import {RscFile} from '../rsc-file';
import {RscFileService} from './rsc-file.service';

@Component({
  selector: 'rsc-file-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: [
    trigger('hiddenAnimation', [
      state('true', style({
        height: '0',
        padding: '0'
      })),
      state('false', style({
        height: '*',
        padding: '*'
      })),
      transition('true <=> false', animate('200ms ease'))
    ])
  ]
})
export class UploadComponent implements OnInit {

  @Input() entityType: EntityTypes;
  @Input() entityId: number;
  @Input() private fileType: string;
  @Input() multiple: boolean;
  @Input() required: boolean;
  @Input() permissions: FilePermissions[];

  public files: RscFile[] = [];

  uploader: FileUploader;

  constructor(private svc: RscFileService) {
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
      let file = new RscFile();
      file.filename = fileItem.file.name;
      file.contentType = fileItem.file.type;
      file.fileItem = fileItem;
      this.files.push(file);
      fileItem.onComplete = response => {
        file.id = JSON.parse(response).fileId;
      };
      fileItem.upload();
    };
  }

  public fileTypeValue: string;

  ngOnInit() {
    this.fileTypeValue = FileTypeCodes[this.fileType].value;
    this.svc.getFiles(this.entityType, this.entityId, this.fileType)
      .subscribe(files => {
        this.files = files;
      });
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

  public downloadFile(item: RscFile) {
    window.location.href = environment.urls.files + '/files/' + item.id;
  }

}
