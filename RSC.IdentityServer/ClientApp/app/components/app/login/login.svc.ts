import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { Login } from './Login';
import { WindowSvc } from '../../../windowSvc';

@Injectable()
export class LoginSvc {

    constructor(
        private http: Http,
        private winSvc: WindowSvc
    ) { }

    post(login: Login, returnUrl: URL) {
        return this.http.post('/account/login', login)
            .toPromise()
            .then(res => {
                var authData = JSON.stringify(res.json());
                if (returnUrl.searchParams.get("onetime") != null)
                    returnUrl.searchParams.delete("onetime");
                returnUrl.searchParams.append("onetime", authData);
                this.winSvc.nativeWindow.location = returnUrl.href;
            })
            .catch(err => {
                console.error(err);
                return err.json();
            });
    }

}