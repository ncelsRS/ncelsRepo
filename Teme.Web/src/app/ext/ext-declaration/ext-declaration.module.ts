import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ExtDeclarationRoutingModule} from './ext-declaration-routing.module';
import {ExtDeclarationsComponent} from './ext-declarations/ext-declarations.component';
import {ExtDeclarationComponent} from './ext-declaration/ext-declaration.component';
import {ExtGeneralInformationComponent} from './ext-declaration/ext-general-information/ext-general-information.component';
import {ExtImnSetComponent} from './ext-declaration/ext-imn-set/ext-imn-set.component';
import {FormsModule} from '@angular/forms';
import {ExtProducerComponent} from './ext-declaration/ext-producer/ext-producer.component';
import {ExtAgreementComponent} from './ext-declaration/ext-agreement/ext-agreement.component';
import {Ng2SmartTableModule} from 'ng2-smart-table';
import {ExtJournalComponent} from './ext-declaration/ext-journal/ext-journal.component';
import {ExtAttachmentsComponent} from './ext-declaration/ext-attachments/ext-attachments.component';
import {FileUploadModule} from 'ng2-file-upload';
import { ExtDeclarationsActionsComponent } from './ext-declarations/ext-declarations-actions/ext-declarations-actions.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  imports: [
    CommonModule,
    ExtDeclarationRoutingModule,
    FormsModule,
    Ng2SmartTableModule,
    FileUploadModule,
    NgbModule.forRoot()
  ],
  declarations: [
    ExtDeclarationsComponent,
    ExtDeclarationComponent,
    ExtGeneralInformationComponent,
    ExtImnSetComponent,
    ExtProducerComponent,
    ExtAgreementComponent,
    ExtJournalComponent,
    ExtAttachmentsComponent,
    ExtDeclarationsActionsComponent],
  entryComponents: [
    ExtDeclarationsActionsComponent,
  ],
  exports: [ ExtDeclarationsActionsComponent ]
})
export class ExtDeclarationModule {
}
