import {ChangeDetectorRef, Component, OnInit, ViewEncapsulation, Output, EventEmitter, forwardRef, Input} from '@angular/core';
import {debounceTime, distinctUntilChanged, switchMap, tap} from 'rxjs/operators';
import {Subject} from 'rxjs/Subject';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {TemplateValidation} from '../TemplateValidation';
import {NG_VALUE_ACCESSOR, NG_VALIDATORS} from '@angular/forms';

export enum ReferenceType {
  NMI = 'NomenclatureCodeMedProduct',
  StorageConditions = 'StorageCondition',
  Declarant = 'Declarant'
}

@Component({
  selector: 'app-sh-reference',
  inputs: ['referenceName', 'triggerNMI'],
  templateUrl: './sh-reference.component.html',
  styleUrls: ['./sh-reference.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => ShReferenceComponent),
    multi: true
  }, {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => ShReferenceComponent),
    multi: true
  }
  ]
})

export class ShReferenceComponent extends TemplateValidation implements OnInit {
  @Input() showErrors: boolean;
  referenceName: ReferenceType;
  typeahead = new Subject<string>();
  loading = false;
  arrays;
  term;
  urlParent = 'http://localhost:5121/api/Reference/';
  urlChild = '';
  selectList: any[] = [];
  @Output() updateOnChange = new EventEmitter<any>();

  constructor(private http: HttpClient, private cd: ChangeDetectorRef) {
    super();
  }

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

    this.loadReference();
  }

  private loadReference() {
    this.typeahead.pipe(
      tap(() => this.loading = true),
      distinctUntilChanged(),
      debounceTime(200),
      switchMap(term => this.getList(term)),
    ).subscribe(x => {
      this.selectList = x;
      this.loading = false;
      this.cd.markForCheck();
    }, () => {
      this.selectList = [];
    });
  }

  getList(term: string = null): Observable<any> {
    this.term = term;
    this.http.get(this.urlParent + this.urlChild + '/', this.getOptions(term, '0'))
      .subscribe(list => {
        this.arrays = list;
      });

    return Observable.of(this.arrays);
  }

  fetchScroll($event) {

    this.http.get(this.urlParent + this.urlChild + '/', this.getOptions(this.term, this.selectList.length.toString()))
      .toPromise().then(res => { // Success
        this.selectList = this.selectList.concat(res);
      }
    ).catch((err) => console.error(err));
  }

  getOptions(searchWord: string, listLength: string) {
    const options = {
      params: new HttpParams().set('name', searchWord)
        .set('culture', 'ru').set('page', '10').set('counter', listLength)
    };

    return options;
  }

  onChange($event) {
    this.updateOnChange.emit($event);
  }

}
