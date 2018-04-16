import { RouterModule, PreloadAllModules } from '@angular/router';
import { NotFoundComponent } from './pages/errors/not-found/not-found.component';
export var routes = [
    { path: '', redirectTo: 'pages', pathMatch: 'full' },
    { path: 'pages', loadChildren: 'app/pages/pages.module#PagesModule' },
    { path: 'ext', loadChildren: 'app/ext/ext.module#ExtModule' },
    { path: 'login', loadChildren: 'app/pages/login/login.module#LoginModule' },
    { path: 'register', loadChildren: 'app/pages/register/register.module#RegisterModule' },
    { path: '**', component: NotFoundComponent }
];
export var routing = RouterModule.forRoot(routes, {
    preloadingStrategy: PreloadAllModules,
});
//# sourceMappingURL=app.routing.js.map