import {ExtLayoutComponent} from './ext-layout/ext-layout.component';
import {Routes, RouterModule} from '@angular/router';
import {ModuleWithProviders} from '@angular/core';
import {TestComponent} from './ext-layout/test/test.component';

export const routes: Routes = [
  {path: '', component: ExtLayoutComponent}, // default route of the module
  {path: 'test', component: TestComponent}
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
