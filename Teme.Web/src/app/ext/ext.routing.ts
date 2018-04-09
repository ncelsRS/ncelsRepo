import {Routes, RouterModule} from '@angular/router';
import {ModuleWithProviders} from '@angular/core';
import {ExtLayoutHomeComponent} from './ext-layout-pages/ext-layout-home/ext-layout-home.component';
import {ExtLayoutComponent} from './ext-layout/ext-layout.component';

export const routes: Routes = [
  {
    path: '',
    data: {
      title: 'home'
    },
    component: ExtLayoutComponent,
    children: [
      {
        path: '',
        component: ExtLayoutHomeComponent
      }
    ]
  }
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
