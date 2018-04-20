import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {IntContractComponent} from "./int-contract/int-contract.component";
import {IntDeclarationComponent} from "./int-declaration/int-declaration.component";
import {RouterModule, Routes} from "@angular/router";

const routes: Routes = [
  {
    path: 'contracts',
    component:IntContractComponent
  },
  {
    path: 'declarations',
    component:IntDeclarationComponent
  }
];


@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    NgbModule.forRoot()
  ],
  declarations: [
    IntContractComponent,
    IntDeclarationComponent
  ],
  exports: [RouterModule]
})

export class IntLayoutModule {
}
