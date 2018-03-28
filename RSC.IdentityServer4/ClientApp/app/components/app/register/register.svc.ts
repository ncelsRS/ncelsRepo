import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { Register } from './Register';
import { WindowSvc } from '../../../windowSvc';

@Injectable()
export class RegisterSvc {

    private url = "/Auth/Register/";

    constructor(
        private http: Http,
        private winSvc: WindowSvc
    ) { }

    register(register: Register)
    {
        return this.http.post(this.url, register); 
    }

}