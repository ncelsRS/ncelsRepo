import {Injectable} from '@angular/core';
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {IdentityProviderSvc} from './IdentityProviderSvc';
import {Observable} from 'rxjs/Observable';
import {Router} from '@angular/router';

@Injectable()
export class IdentityInterceptorService implements HttpInterceptor {

  constructor(
    private router: Router, private identity: IdentityProviderSvc
  ) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const auth = this.identity.getAuth();
    if (!auth) {
      //this.router.navigate(['login']);
      return next.handle(req);
    }
    const _req = req.clone({
      setHeaders: {
        Authorization: `${auth.token_type} ${auth.access_token}`
      }
    });
    next.handle(_req).pipe(
      (res) => {
        return res;
      },
      err => {
        return err;
      }
    );
  }

}
