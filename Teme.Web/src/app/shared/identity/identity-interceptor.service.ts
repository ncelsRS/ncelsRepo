import {Injectable} from '@angular/core';
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {IdentityProviderSvc} from './IdentityProviderSvc';
import {Observable} from 'rxjs/Observable';
import {Router} from '@angular/router';
import {catchError, map, mergeMap} from 'rxjs/operators';

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
      .pipe(mergeMap(authHeader => {
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
      }));
  }
}
