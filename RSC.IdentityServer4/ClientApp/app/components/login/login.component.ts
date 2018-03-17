import { Component } from '@angular/core';

import { Login } from './Login';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent {

    login: Login = new Login();
    teststring: string = 'test string';

    counter: number = 0;

    onSubmit() {
        this.teststring = (++this.counter).toString();
    }

}
