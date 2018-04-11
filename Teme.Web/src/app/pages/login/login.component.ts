import { Component, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormControl, AbstractControl, FormBuilder, Validators} from '@angular/forms';
import { CustomValidators } from 'ng2-validation';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class LoginComponent {
  public router: Router;
  public form:FormGroup;
  public email:AbstractControl;
  public password:AbstractControl;

  constructor(router:Router, fb:FormBuilder) {
      this.router = router;
      this.form = fb.group({
          'email': ['', Validators.compose([Validators.required, CustomValidators.email])],
          'password': ['', Validators.compose([Validators.required, Validators.minLength(6)])]
      });

      this.email = this.form.controls['email'];
      this.password = this.form.controls['password'];
  }

  public onSubmit(values:Object):void {
      if (this.form.valid) {
          this.router.navigate(['pages/dashboard']);
      }
  }

  ngAfterViewInit(){
      document.getElementById('preloader').classList.add('hide');                 
  }

}
