import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CalendarModule } from 'angular-calendar';
import { DirectivesModule } from '../../theme/directives/directives.module';
import { AppCalendarComponent } from './app-calendar.component';

export const routes = [
  { path: '', component: AppCalendarComponent, pathMatch: 'full' }
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    CalendarModule,
    DirectivesModule,
    RouterModule.forChild(routes)
  ],
  declarations: [
    AppCalendarComponent
  ]
})
export class AppCalendarModule { }
