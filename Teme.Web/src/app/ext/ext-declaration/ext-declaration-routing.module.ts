import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {ExtDeclarationsComponent} from './ext-declarations/ext-declarations.component';
import {ExtDeclarationComponent} from './ext-declaration/ext-declaration.component';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Заявления'
    },
    children: [
      {
        path: '',
        component: ExtDeclarationsComponent,
        data: {title: 'Заявления'}
      },
      {
        path: ':id',
        data: {title: 'Заявление'},
        component: ExtDeclarationComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class ExtDeclarationRoutingModule{
}
