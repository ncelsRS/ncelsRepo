import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {RouterModule, Routes} from "@angular/router";
import {Ng2SmartTableModule} from "ng2-smart-table";
import {ModuleWithProviders} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {FileUploadModule} from "ng2-file-upload";
import {IntCardComponent} from './int-card/int-card.component';
import {IntGeneralInformationComponent} from './int-general-information/int-general-information.component';
import {IntImnSetComponent} from './int-imn-set/int-imn-set.component';
import {IntAttachmentsComponent} from './int-attachments/int-attachments.component';
import {IntJournalComponent} from './int-journal/int-journal.component';
import {IntJournalListComponent} from './int-journal/int-journal-list/int-journal-list.component';
import {IntProducerComponent} from './int-producer/int-producer.component';
import {IntAgreementComponent} from './int-agreement/int-agreement.component';
import {IntHistoryComponent} from './int-history/int-history.component';
import { IntSubjectComponent } from './int-subject/int-subject.component';

const routes: Routes = [
  {
    path: 'card',
    component: IntCardComponent,
  },
  {
    path: 'attachments',
    component: IntAttachmentsComponent,
  },
  {
    path: 'history',
    component:IntHistoryComponent,
  }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    Ng2SmartTableModule,
    FormsModule,
    FileUploadModule,
    NgbModule.forRoot()
  ],
  declarations: [
    IntCardComponent,
    IntGeneralInformationComponent,
    IntImnSetComponent,
    IntAttachmentsComponent,
    IntJournalComponent,
    IntJournalListComponent,
    IntProducerComponent,
    IntAgreementComponent,
    IntHistoryComponent,
    IntSubjectComponent],
  exports: [RouterModule],
  entryComponents: [
    IntJournalListComponent
  ],
})

export class IntDeclarationDetailModule {
}

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
