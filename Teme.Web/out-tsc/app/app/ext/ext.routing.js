import { RouterModule } from '@angular/router';
import { ExtLayoutHomeComponent } from './ext-layout-pages/ext-layout-home/ext-layout-home.component';
import { ExtLayoutComponent } from './ext-layout/ext-layout.component';
export var routes = [
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
            },
            {
                path: 'contracts',
                data: { title: 'contracts' },
                loadChildren: './ext-contract/ext-contract.module#ExtContractModule'
            },
            {
                path: 'declarations',
                data: { title: 'declarations' },
                loadChildren: './ext-declaration/ext-declaration.module#ExtDeclarationModule'
            }
        ]
    }
];
export var routing = RouterModule.forChild(routes);
//# sourceMappingURL=ext.routing.js.map