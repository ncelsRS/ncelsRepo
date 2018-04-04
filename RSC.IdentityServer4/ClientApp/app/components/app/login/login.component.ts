import { Component } from '@angular/core';
import { ParamMap, Router, ActivatedRoute } from '@angular/router';

import { Login } from './Login';
import { LoginSvc } from './login.svc';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent {

    login: Login = new Login();

    //returnUrl: string;

    returnUrl = "./bekbol/test";

    res: any;

    constructor(
        private route: ActivatedRoute,
        private loginSvc: LoginSvc) {

        this.returnUrl = this.route.snapshot.queryParams.returnUrl;
        let clientId = this.route.snapshot.queryParams.clientId;
        let clientSecret = this.route.snapshot.queryParams.clientSecret;

        this.login.client_id = clientId || this.login.client_id;
        this.login.client_secret = clientSecret || this.login.client_secret;
    }

    onSubmit(loginForm: any) {
        //this.res = this.loginSvc.post(this.login, new URL(this.returnUrl));
    }

}
