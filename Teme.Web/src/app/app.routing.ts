import {Routes, RouterModule, PreloadAllModules} from '@angular/router';
import {ModuleWithProviders} from '@angular/core';

import {NotFoundComponent} from './pages/errors/not-found/not-found.component';

export const routes: Routes = [
  {path: '', redirectTo: 'pages', pathMatch: 'full'},
  // {path: 'payment', loadChildren: 'app/ext/ext-payment/ext-payment.module#ExtPaymentModule'},
  {path: 'pages', loadChildren: 'app/pages/pages.module#PagesModule'},
  {path: 'ext', loadChildren: 'app/ext/ext.module#ExtModule'},
  {path: 'login', loadChildren: 'app/pages/login/login.module#LoginModule'},
  {path: 'register', loadChildren: 'app/pages/register/register.module#RegisterModule'},
  {path: '**', component: NotFoundComponent}
];

export const routing: ModuleWithProviders = RouterModule.forRoot(routes, {
  preloadingStrategy: PreloadAllModules,
  // useHash: true
});
