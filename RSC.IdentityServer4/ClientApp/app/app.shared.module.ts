import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { LoginComponent } from './components/app/login/login.component';
import { LoginSvc } from './components/app/login/login.svc';
import { RegisterSvc } from './components/app/register/register.svc';
import { WindowSvc } from './windowSvc';
import { RegisterComponent } from './components/app/register/register.component';
import { ReactiveFormsModule } from '@angular/forms'; 

@NgModule({
    declarations: [
        AppComponent,
        LoginComponent,
        RegisterComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'login', pathMatch: 'full' },
            { path: 'login', component: LoginComponent },
            { path: 'register', component: RegisterComponent },
            { path: '**', redirectTo: 'login' }
        ])
    ],
    providers: [
        WindowSvc,
        LoginSvc,
        RegisterSvc
    ]
})
export class AppModuleShared {
}
