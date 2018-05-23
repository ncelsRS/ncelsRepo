import {Injectable} from '@angular/core';
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {IdentityProviderSvc} from './IdentityProviderSvc';
import {Observable} from 'rxjs/Observable';
import {Router} from '@angular/router';
import {catchError, map, mergeMap} from 'rxjs/operators';
import 'rxjs/add/observable/throw';

@Injectable()
export class IdentityInterceptorService implements HttpInterceptor {

  constructor(
    private router: Router,
    private identity: IdentityProviderSvc
  ) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (req.headers.has('Authorization'))
      return next.handle(req);
    return this.identity.getAuthHeader()
      .pipe(
        mergeMap(authHeader => {
          let _req = req;
          if (authHeader)
            _req = req.clone({
              setHeaders: {
                Authorization: `${authHeader}`
              }
            });
          return next.handle(_req)
            .pipe(catchError(err => {
              if (err.status == 401)
                this.identity.redirectToLogin();
              return Observable.throw(err);
            }));
        }),
        catchError(err => {
          console.log(err);
          if (err.status || err.status === 0)
            return Observable.throw(err);
          return next.handle(req)
            .pipe(catchError(err => {
              if (err.status == 401)
                this.identity.redirectToLogin();
              return Observable.throw(err);
            }));
        })
      );
  }
}
