import { Component } from '@angular/core';

import { Register } from './Register';

@Component({
    selector: 'app-login',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
export class RegisterComponent {

    register: Register = new Register();
    teststring: string = 'test string';

    counter: number = 0;

    onSubmit() {
        this.teststring = (++this.counter).toString();
    }

}
