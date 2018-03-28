import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Register } from './Register';
import { RegisterSvc } from './register.svc';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-login',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
export class RegisterComponent {

    register: Register = new Register();
    type: 'fl' | 'ul';

    registerForm: FormGroup;

    constructor(private registerSvc: RegisterSvc)
    {
        this.type = 'fl';
    }


    ngOnInit(): void
    {
        this.registerForm = new FormGroup({
            'fl': new FormControl(this.type),
            'ul': new FormControl(this.type),
            'Password': new FormControl(this.register.Password, [
                Validators.required
            ]),
            'Bin': new FormControl(this.register.Bin),
            'CompanyName': new FormControl(this.register.CompanyName, Validators.required),
            'HasIin': new FormControl(this.register.HasIin, Validators.required),
            'Iin': new FormControl(this.register.Iin, Validators.required),
            'ConfirmPassword': new FormControl(this.register.ConfirmPassword, Validators.required),
            'LastName': new FormControl(this.register.LastName, Validators.required),
            'FirstName': new FormControl(this.register.FirstName, Validators.required),
            'MiddleName': new FormControl(this.register.MiddleName, Validators.required),
            'Email': new FormControl(this.register.Email, Validators.required)
        });
    }

    get Password() { return this.registerForm.get('Password'); }

    onSubmit() {

        this.registerSvc.register(this.register).subscribe(data =>
            {
            
            });
    }

}
