import {Injectable} from '@angular/core';
import {IdentityRes} from './dtos/IdentityRes';
import {HttpHeaders, HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {Observable} from 'rxjs/Observable';
import {Router} from '@angular/router';

import 'rxjs/add/observable/of';
import {map, share} from 'rxjs/operators';

@Injectable()

export class IdentityProviderSvc {

  constructor(private http: HttpClient) {
    console.log('+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++');
  }

  private static _auth: IdentityRes = null;
  private _identityUrl = environment.urls.identity;

  private isJwtExp(jwt: string): boolean {
    let jwtArr = jwt.split('.');
    let jwtEnc = atob(jwtArr[1]);
    let data = JSON.parse(jwtEnc);
    return +data.exp < Date.now() / 1000;
  }

  public setAuth(auth: IdentityRes): void {
    if (auth) {
      IdentityProviderSvc._auth = auth;
      localStorage.setItem('auth', JSON.stringify(auth));
    }
  }

  private getAuth(): IdentityRes {
    if (!IdentityProviderSvc._auth) {
      let authStr = localStorage.getItem('auth');
      if (authStr)
        IdentityProviderSvc._auth = JSON.parse(authStr);
    }
    return IdentityProviderSvc._auth;
  }

  private refreshAuth(jwt: string): Observable<boolean> {
    let headers = new HttpHeaders().set('Authorization', 'Bearer ' + jwt);
    return this.http.get<IdentityRes>(this._identityUrl + '/account/refresh', {headers})
      .pipe(
        map(res => {
          this.setAuth(res);
          return true;
        })
      );
  }

  public setAuthFromOneTime(oneTime: string): Observable<boolean> {
    if (!oneTime || this.isJwtExp(oneTime))
      this.redirectToLogin();
    return this.refreshAuth(oneTime);
  }

  public checkAuthWithRefresh(): Observable<boolean> {
    let auth = this.getAuth();
    if (!auth)
      this.redirectToLogin();
    if (auth.accessToken && !this.isJwtExp(auth.accessToken))
      return Observable.of(true);
    if (auth.refreshToken && !this.isJwtExp(auth.refreshToken))
      return this.refreshAuth(auth.refreshToken);
  }

  private static _headerRequest: Observable<string> = null;

  public getAuthHeader(): Observable<string> {
    if (!IdentityProviderSvc._headerRequest)
      IdentityProviderSvc._headerRequest = this.checkAuthWithRefresh()
        .pipe(map(res => {
          IdentityProviderSvc._headerRequest = null;
          if (!res) return null;
          let auth = this.getAuth();
          return 'Bearer ' + auth.accessToken;
        })).pipe(share());
    return IdentityProviderSvc._headerRequest;
  }

  public redirectToLogin() {
    let url = environment.urls.identity;
    url += '/account/login?returnUrl=';
    url += window.location.href;
    window.location.href = url;
  }

  public getCurrentUser(): any {
    let auth = this.getAuth();
    if (!auth) return;
    return auth.user;
  }

}
