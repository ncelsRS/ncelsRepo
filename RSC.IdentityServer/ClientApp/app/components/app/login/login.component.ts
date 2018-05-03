import {Component} from '@angular/core';
import {ParamMap, Router, ActivatedRoute, Params} from '@angular/router';

import {Login} from './Login';
import {LoginSvc} from './login.svc';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent {

    login: Login = new Login();

    returnUrl: string;

    res: any;

    constructor(
        private route: ActivatedRoute,
        private loginSvc: LoginSvc) {

        this.route.queryParams
            .subscribe(params => {
                this.returnUrl = params.returnUrl;
                this.login.client_id = params.client_id || this.login.client_id;
                this.login.client_secret = params.client_secret || this.login.client_secret;
            });

    }

    onSubmit(loginForm: any) {
        //this.res = this.loginSvc.post(this.login, new URL(this.returnUrl));
    }

}
