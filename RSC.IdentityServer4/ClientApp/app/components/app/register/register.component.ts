import { Component } from '@angular/core';
import { Register } from './Register';
import { RegisterSvc } from './register.svc';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-login',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
export class RegisterComponent
{
    register: Register = new Register();
    type: 'fl' | 'ul';

    constructor(private registerSvc: RegisterSvc)
    {
        this.type = 'fl';
    }

    get diagnostic() { return JSON.stringify(this.register); }

    ngOnInit(): void
    {

    }

    onSubmit(form: FormsModule)
    {

        this.registerSvc.register(this.register).subscribe(data =>
        {

        });

    }

}
