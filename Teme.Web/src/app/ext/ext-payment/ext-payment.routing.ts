import {Routes, RouterModule} from '@angular/router';
import {ModuleWithProviders} from '@angular/core';

import {ExtPaymentComponent} from './ext-payment.component';
import {CostInfoComponent} from "./cost-info/cost-info.component";
import {DataComponent} from "./data/data.component";
import { EquipmentComponent } from './equipment/equipment.component';
import { ManufacturerComponent } from './manufacturer/manufacturer.component';
import { SubjectComponent } from './subject/subject.component';
import { AttachmentsComponent } from './attachments/attachments.component';
import { SigningComponent } from './signing/signing.component';
import {ExtContractComponent} from '../ext-contract/ext-contract/ext-contract.component';


export const routes: Routes = [
  {
    path: '',
    component: ExtPaymentComponent,
    data: { title: 'Заявка на платеж' },
    // children: [
    //   //{path: '', redirectTo: 'cost-info', pathMatch: 'full'},
    //   {path: 'cost-info', component: CostInfoComponent, data: {breadcrumb: 'Сведение о стоимости'}},
    //   {path: 'data', component: DataComponent, data: {breadcrumb: 'Данные ИМН/МТ'}},
    //   {path: 'equipment', component: EquipmentComponent, data: {breadcrumb: 'Комплектация ИМН и МТ'}},
    //   {path: 'manufacturer', component: ManufacturerComponent, data: {breadcrumb: 'Производитель ИМН и МТ и участок производства'}},
    //   {path: 'subject', component: SubjectComponent, data: {breadcrumb: 'Субъект, осуществляющий оплату за проведение экспертизы'}},
    //   {path: 'attachments', component: AttachmentsComponent, data: {breadcrumb: 'Вложение'}},
    //   {path: 'signing', component: SigningComponent, data: {breadcrumb: 'Подписание'}},
    // ]
  }
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
