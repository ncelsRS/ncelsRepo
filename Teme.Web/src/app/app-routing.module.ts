import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';
import { HomeComponent } from './home/home.component';

export const routes: Routes = [
    { path: '', pathMatch: 'full', redirectTo: 'home' },
    { path: 'home', component: HomeComponent },
    { path: 'ext', loadChildren: './ext/ext.module#ExtModule' },
    // { path: 'int', loadChildren: '' },
    { path: 'metronic', loadChildren: './metronic/metronic.module#MetronicModule' }
];

export const routing: ModuleWithProviders = RouterModule.forRoot(routes);
