import {ChangeDetectorRef, Component, forwardRef, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {TemplateValidation} from '../TemplateValidation';
import {ShReferenceComponent} from '../sh-reference/sh-reference.component';
import {NG_VALUE_ACCESSOR, NG_VALIDATORS} from '@angular/forms';
import {ReferenceType} from '../sh-reference/sh-reference.component'

@Component({
  selector: 'app-local-reference',
  templateUrl: './local-reference.component.html',
  styleUrls: ['./local-reference.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => ShReferenceComponent),
    multi: true
  }, {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => ShReferenceComponent),
    multi: true
  }]
})
export class LocalReferenceComponent extends TemplateValidation implements OnInit {
  @Input() showErrors: boolean;
  referenceName: ReferenceType;
  photos = [];
  photosBuffer = [];
  bufferSize = 50;
  loading = false;
  urlParent = 'http://localhost:5121/api/Reference/';
  urlChild = '';

  constructor(private http: HttpClient,  private cd: ChangeDetectorRef) { super(); }

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
    }

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

  onChange($event) {
  //  this.updateOnChange.emit($event);
  }

}
