import { Component } from '@angular/core';
import { ParamMap, Router, ActivatedRoute, Params } from '@angular/router';

import { Login } from './Login';
import { LoginSvc } from './login.svc';
import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

    login: Login = new Login();

    returnUrl: string;

    res: any;

    constructor(
        private route: ActivatedRoute,
        private loginSvc: LoginSvc) {
    }

    ngOnInit() {
        this.route.queryParams
            .subscribe(params => {
                this.returnUrl = params.returnUrl;
            });
    }

    onSubmit(isValid: boolean) {
        if (!isValid) return;
        this.res = this.loginSvc
            .post(this.login, new URL(this.returnUrl));
    }

}
