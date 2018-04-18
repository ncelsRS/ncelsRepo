import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {ExtContractsComponent} from './ext-contracts/ext-contracts.component';
import {ExtContractComponent} from './ext-contract/ext-contract.component';
import { ExtCostInfoComponent } from './ext-contract/ext-payment-tab/ext-cost-info/ext-cost-info.component';

const routes: Routes = [
    {
        path: '',
        data: {
            title: 'Договора'
        },
        children: [
            {
                path: '',
                component: ExtContractsComponent,
                data: { title: 'Договора' }
            },
            {
                path: ':id',
                data: { title: 'Договор' },
                component: ExtContractComponent
            },
            {
              path: 'create',
              data: {title: 'Заявление'},
              component: ExtContractComponent
            },
            {
                path: 'payment',
                component: ExtCostInfoComponent,
                data: { title: 'Заявка на платеж' }
            }
        ]
    }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class ExtContractRoutingModule {
}
