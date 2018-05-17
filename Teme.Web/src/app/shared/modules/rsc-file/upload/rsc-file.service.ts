import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {RscFile} from '../rsc-file';
import {environment} from '../../../../../environments/environment';

@Injectable()
export class RscFileService {

  constructor(private http: HttpClient) {
  }

  getFiles(entityType: string, entityId: number, fileType: string): Observable<RscFile[]> {
    return this.http.get<RscFile[]>(environment.urls.files + '/files', {
      params: new HttpParams()
        .set('entityType', entityType)
        .set('entityId', entityId ? entityId.toString() : null)
        .set('fileType', fileType)
    });
  }

}
