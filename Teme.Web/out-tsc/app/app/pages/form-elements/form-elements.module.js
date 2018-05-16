var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MultiselectDropdownModule } from 'angular-2-dropdown-multiselect';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CustomFormsModule } from 'ng2-validation';
import { CKEditorModule } from 'ng2-ckeditor';
import { DirectivesModule } from '../../theme/directives/directives.module';
import { ControlsComponent } from './controls/controls.component';
import { FileUploaderComponent } from './controls/file-uploader/file-uploader.component';
import { ImageUploaderComponent } from './controls/image-uploader/image-uploader.component';
import { MultipleImageUploaderComponent } from './controls/multiple-image-uploader/multiple-image-uploader.component';
import { LayoutsComponent } from './layouts/layouts.component';
import { ValidationsComponent } from './validations/validations.component';
import { WizardComponent } from './wizard/wizard.component';
import { EditorComponent } from './editor/editor.component';
export var routes = [
    { path: '', redirectTo: 'controls', pathMatch: 'full' },
    { path: 'controls', component: ControlsComponent, data: { breadcrumb: 'Form Controls' } },
    { path: 'layouts', component: LayoutsComponent, data: { breadcrumb: 'Layouts' } },
    { path: 'validations', component: ValidationsComponent, data: { breadcrumb: 'Validations' } },
    { path: 'wizard', component: WizardComponent, data: { breadcrumb: 'Wizard' } },
    { path: 'editor', component: EditorComponent, data: { breadcrumb: 'Editor' } }
];
var FormElementsModule = (function () {
    function FormElementsModule() {
    }
    FormElementsModule = __decorate([
        NgModule({
            imports: [
                CommonModule,
                FormsModule,
                ReactiveFormsModule,
                MultiselectDropdownModule,
                NgbModule,
                CustomFormsModule,
                CKEditorModule,
                DirectivesModule,
                RouterModule.forChild(routes)
            ],
            declarations: [
                ControlsComponent,
                FileUploaderComponent,
                ImageUploaderComponent,
                MultipleImageUploaderComponent,
                LayoutsComponent,
                ValidationsComponent,
                WizardComponent,
                EditorComponent
            ]
        })
    ], FormElementsModule);
    return FormElementsModule;
}());
export { FormElementsModule };
//# sourceMappingURL=form-elements.module.js.map