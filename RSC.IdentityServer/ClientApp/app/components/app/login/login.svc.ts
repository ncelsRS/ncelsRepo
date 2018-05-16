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

    post(login: Login, returnUrl: string) {
        return this.http.post('/account/login', login)
            .toPromise()
            .then(res => {
                var authData = res.json().oneTimeToken;
                var params: any[] = [];
                var paramsArrStr = returnUrl.split('?');
                returnUrl = paramsArrStr[0];
                var paramsStr = paramsArrStr[1];
                if (paramsStr) {
                    var paramsKeyValues = [];
                    paramsStr.split('&').forEach(paramStr => {
                        var keyValue = paramStr.split('=');
                        if (keyValue[0] != 'auth')
                            params.push({ key: keyValue[0], value: keyValue[1] });
                    });
                }
                params.push({ key: 'onetime', value: authData });
                paramsArrStr = [];
                params.forEach(param => {
                    paramsArrStr.push(`${param.key}=${param.value}`);
                });
                paramsStr = `?${paramsArrStr.join('&')}`;
                this.winSvc.nativeWindow.location.href = returnUrl + paramsStr;
            })
            .catch(err => {
                console.error(err);
                return err.json();
            });
    }

}