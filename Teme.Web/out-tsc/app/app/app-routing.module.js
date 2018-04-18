import { RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
export var routes = [
    { path: '', pathMatch: 'full', redirectTo: 'home' },
    { path: 'home', component: HomeComponent },
    { path: 'ext', loadChildren: './ext/ext.module#ExtModule' },
    // { path: 'int', loadChildren: '' },
    { path: 'metronic', loadChildren: './metronic/metronic.module#MetronicModule' }
];
export var routing = RouterModule.forRoot(routes);
//# sourceMappingURL=app-routing.module.js.map