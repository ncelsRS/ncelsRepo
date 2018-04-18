import {Routes, RouterModule} from '@angular/router';
import {ModuleWithProviders} from '@angular/core';

import {BekComponent} from './bek.component';
//import {BlankComponent} from './test/blank/blank.component';
//import {SearchComponent} from './test/search/search.component';
import {CostInfoComponent} from "./cost-info/cost-info.component";
import {DataComponent} from "./data/data.component";
import { EquipmentComponent } from './equipment/equipment.component';
import { ManufacturerComponent } from './manufacturer/manufacturer.component';
import { SubjectComponent } from './subject/subject.component';
import { AttachmentsComponent } from './attachments/attachments.component';
import { SigningComponent } from './signing/signing.component';


export const routes: Routes = [
  {
    path: '',
    component: BekComponent,
    children: [
      {path: '', redirectTo: 'cost-info', pathMatch: 'full'},
      {path: 'cost-info', component: CostInfoComponent, data: {breadcrumb: 'Сведение о стоимости'}},
      {path: 'data', component: DataComponent, data: {breadcrumb: 'Данные ИМН/МТ'}},
      {path: 'equipment', component: EquipmentComponent, data: {breadcrumb: 'Комплектация ИМН и МТ'}},
      {path: 'manufacturer', component: ManufacturerComponent, data: {breadcrumb: 'Производитель ИМН и МТ и участок производства'}},
      {path: 'subject', component: SubjectComponent, data: {breadcrumb: 'Субъект, осуществляющий оплату за проведение экспертизы'}},
      {path: 'attachments', component: AttachmentsComponent, data: {breadcrumb: 'Вложение'}},
      {path: 'signing', component: SigningComponent, data: {breadcrumb: 'Подписание'}},
      // {
      //   path: 'cost-info',
      //   loadChildren: 'app/bek/cost-info/cost-info.module#CostInfoModule',
      //   data: {breadcrumb: 'Сведение о стоимости'}
      // },
      // {
      //   path: 'dashboard',
      //   loadChildren: 'app/bek/test/dashboard/dashboard.module#DashboardModule',
      //   data: {breadcrumb: 'Dashboard'}
      // },
      // {
      //   path: 'membership',
      //   loadChildren: 'app/bek/test/membership/membership.module#MembershipModule',
      //   data: {breadcrumb: 'Membership'}
      // },
      // {path: 'ui', loadChildren: 'app/bek/test/ui/ui.module#UiModule', data: {breadcrumb: 'UI'}},
      // {
      //   path: 'form-elements',
      //   loadChildren: 'app/bek/test/form-elements/form-elements.module#FormElementsModule',
      //   data: {breadcrumb: 'Form Elements'}
      // },
      // {path: 'tables', loadChildren: 'app/bek/test/tables/tables.module#TablesModule', data: {breadcrumb: 'Tables'}},
      // {path: 'tools', loadChildren: 'app/bek/test/tools/tools.module#ToolsModule', data: {breadcrumb: 'Tools'}},
      // {
      //   path: 'calendar',
      //   loadChildren: 'app/bek/test/calendar/app-calendar.module#AppCalendarModule',
      //   data: {breadcrumb: 'Calendar'}
      // },
      // {path: 'mailbox', loadChildren: 'app/bek/test/mailbox/mailbox.module#MailboxModule', data: {breadcrumb: 'Mailbox'}},
      // {path: 'maps', loadChildren: 'app/bek/test/maps/maps.module#MapsModule', data: {breadcrumb: 'Maps'}},
      // {path: 'charts', loadChildren: 'app/bek/test/charts/charts.module#ChartsModule', data: {breadcrumb: 'Charts'}},
      // {
      //   path: 'dynamic-menu',
      //   loadChildren: 'app/bek/test/dynamic-menu/dynamic-menu.module#DynamicMenuModule',
      //   data: {breadcrumb: 'Dynamic Menu'}
      // },
      // {path: 'blank', component: BlankComponent, data: {breadcrumb: 'Blank page'}},
      // {path: 'search', component: SearchComponent, data: {breadcrumb: 'Search'}}
    ]
  }
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
