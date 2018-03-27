import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { Login } from './Login';
import { WindowSvc } from '../../../windowSvc';

@Injectable()
export class LoginSvc {

    constructor(
        private http: Http,
        private winSvc: WindowSvc
    ) { }

    private objToForm(obj: any): string {
        var keys = Object.keys(obj);
        if (keys.length === 0) return '';
        var keyVals: string[] = [];
        keys.forEach(key => {
            var val = obj[key];
            keyVals.push(`${key}=${val}`);
        });
        return keyVals.join('&');
    }

    post(login: Login, returnUrl: URL) {
        var headers = new Headers();
        headers.append('Content-Type', 'application/x-www-form-urlencoded');
        return this.http.post('/connect/token', this.objToForm(login), { headers: headers })
            .toPromise()
            .then(res => {
                var authData = JSON.stringify(res.json());
                if (returnUrl.searchParams.get("authData") != null)
                    returnUrl.searchParams.delete("authData");
                returnUrl.searchParams.append("authData", authData);
                this.winSvc.nativeWindow.location = returnUrl.href;
            })
            .catch(err => {
                console.error(err);
                return err.json();
            });
    }

}