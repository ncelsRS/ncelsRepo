import {ModuleWithProviders, NgModule, Optional, SkipSelf} from '@angular/core';
import {CommonModule} from '@angular/common';
import {IdentityProviderSvc} from './IdentityProviderSvc';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {IdentityInterceptorService} from './identity-interceptor.service';

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule
  ],
  declarations: [],
  providers: [
    IdentityProviderSvc,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: IdentityInterceptorService,
      multi: true
    }
  ]
})
export class IdentityModule {
}
