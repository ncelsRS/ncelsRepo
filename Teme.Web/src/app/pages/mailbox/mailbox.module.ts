import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PipesModule } from '../../theme/pipes/pipes.module';
import { MailboxComponent } from './mailbox.component';

export const routes = [
  { path: '', component: MailboxComponent, pathMatch: 'full' }
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild(routes),
    PerfectScrollbarModule,
    PipesModule
  ],
  declarations: [
    MailboxComponent
  ]
})
export class MailboxModule { }
