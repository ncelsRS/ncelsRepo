import {Component, EventEmitter, Input, OnInit, Output, ViewEncapsulation} from '@angular/core';
import {debounceTime, distinctUntilChanged, switchMap, tap} from 'rxjs/operators';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {environment} from '../../../environments/environment';
import {Subject} from 'rxjs/Subject';
import {Cell, DefaultEditor, Editor, ViewCell} from 'ng2-smart-table';
import {ReferenceType} from '../enums/ReferenceType';

@Component({
  selector: 'app-drop-down-local',
  templateUrl: './drop-down-local.component.html',
  styleUrls: ['./drop-down-local.component.scss'],
  encapsulation: ViewEncapsulation.None
})

export class DropDownLocalComponent extends DefaultEditor  implements OnInit, ViewCell {
  @Input()
  referenceName: ReferenceType;
  typeahead = new Subject<string>();
  photos = [];
  photosBuffer = [];
  bufferSize = 20;
  loading = false;
  urlParent = environment.urls.admin + '/api/Reference/';
  urlChild = '';
  @Input() value: string;
  @Input() rowData: any;
  @Input() searchFields: string;
  @Input() titleField: string;
  @Output() updateItemCell: EventEmitter<any> = new EventEmitter();
  @Input() selected;

  constructor(private http: HttpClient) {super();}

  ngOnInit() {
    switch (this.referenceName) {
      case ReferenceType.NMI:
        this.urlChild = ReferenceType.NMI;
        break;
      case ReferenceType.StorageConditions:
        this.urlChild = ReferenceType.StorageConditions;
        break;
      case ReferenceType.Declarant:
        this.urlChild = ReferenceType.Declarant;
        break;
      case ReferenceType.Measure:
        this.urlChild = ReferenceType.Measure;
        break;
      case ReferenceType.ClassifierMedicalArea:
        this.urlChild = ReferenceType.ClassifierMedicalArea;
        break;
      case ReferenceType.PackagingType:
        this.urlChild = ReferenceType.PackagingType;
        break;
      case ReferenceType.EquipmentType:
        this.urlChild = ReferenceType.EquipmentType;
        break;
      case ReferenceType.Country:
        this.urlChild = ReferenceType.Country;
        break;
    }
    this.loadReference();
  }

  private loadReference() {
    this.http.get<any[]>(this.urlParent + this.urlChild + '/').subscribe(photos => {
      this.photos = photos;
      this.photosBuffer = this.photos.slice(0, this.bufferSize);
    });
  }

  fetchMore() {
    const len = this.photosBuffer.length;
    const more = this.photos.slice(len, this.bufferSize + len);
    this.loading = true;
    // using timeout here to simulate backend API delay
    setTimeout(() => {
      this.loading = false;
      this.photosBuffer = this.photosBuffer.concat(more);
    }, 200)
  }

  public onChange($event) {
  }

  updateItem($event) {
    this.updateItemCell.emit($event);
  }

  setSelected(id){
    this.selected = id;
  }

}
