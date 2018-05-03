import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/distinctUntilChanged';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/delay';
import {HttpClient, HttpParams, HttpHeaders} from '@angular/common/http';

let arrays;
let termStore;

@Injectable()
export class DataService {

  constructor(private http: HttpClient) {
  }

  getPeople(term: string = null): Observable<any> {

    termStore = term;
    const options = {params: new HttpParams().set('name', term).set('culture', "ru").set('page', "10").set('counter', "0")};

    this.http.get('http://localhost:5121/api/Reference/NomenclatureCodeMedProduct/', options).subscribe(photos => {
      arrays = photos;
    });

    return Observable.of(arrays);
  }

  fetchScrollService(counter: number = 0) {

    const options = {params: new HttpParams().set('name', termStore).set('culture', "ru").set('page', "50").set('counter', counter.toString())};

    this.http.get('http://localhost:5121/api/Reference/NomenclatureCodeMedProduct/', options).subscribe(photos => {
      arrays = photos;
    });

    return arrays;
  }
}
