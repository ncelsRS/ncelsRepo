import {Routes, RouterModule} from '@angular/router';
import {ModuleWithProviders} from '@angular/core';
import {IntHomeComponent} from './int-home/int-home.component'
import {IntLayoutComponent} from "./int-layout/int-layout.component";

export const routes: Routes = [
  {path: 'home', component: IntHomeComponent},
  {
    path: 'spa',
    component: IntLayoutComponent,
    loadChildren: './int-layout/int-layout.module#IntLayoutModule'
  }
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
