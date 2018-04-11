import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DynamicMenuComponent } from './dynamic-menu.component';

export const routes = [
  { path: '', component: DynamicMenuComponent, pathMatch: 'full' }
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    NgbModule
  ],
  declarations: [
    DynamicMenuComponent
  ]
})
export class DynamicMenuModule { }
