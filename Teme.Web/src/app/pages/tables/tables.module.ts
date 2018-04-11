import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { DirectivesModule } from '../../theme/directives/directives.module';
import { BasicTablesComponent } from './basic-tables/basic-tables.component';

export const routes = [
  { path: '', redirectTo: 'basic-tables', pathMatch: 'full'},
  { path: 'basic-tables', component: BasicTablesComponent, data: { breadcrumb: 'Basic Tables' } },
  { path: 'dynamic-tables', loadChildren: 'app/pages/tables/dynamic-tables/dynamic-tables.module#DynamicTablesModule', data: { breadcrumb: 'Dynamic Tables' } }
];

@NgModule({
  imports: [
    CommonModule,
    NgxDatatableModule,
    DirectivesModule,
    RouterModule.forChild(routes)
  ],
  declarations: [
    BasicTablesComponent
  ]
})
export class TablesModule { }
