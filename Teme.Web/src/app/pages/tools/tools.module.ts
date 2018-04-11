import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { DragulaModule } from 'ng2-dragula';
import { ResizableModule } from 'angular-resizable-element';
import { DirectivesModule } from '../../theme/directives/directives.module';
import { DragDropComponent } from './drag-drop/drag-drop.component';
import { ToasterComponent } from './toaster/toaster.component';
import { ResizableComponent } from './resizable/resizable.component';


export const routes = [
  { path: '', redirectTo: 'drag-drop', pathMatch: 'full'},
  { path: 'drag-drop', component: DragDropComponent, data: { breadcrumb: 'Drag and Drop' } },
  { path: 'resizable', component: ResizableComponent, data: { breadcrumb: 'Resizable' } },
  { path: 'toaster', component: ToasterComponent, data: { breadcrumb: 'Toaster' } }  
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FormsModule,
    PerfectScrollbarModule,
    DragulaModule,
    ResizableModule,
    DirectivesModule
  ],
  declarations: [
    DragDropComponent, 
    ToasterComponent, 
    ResizableComponent
  ]
})
export class ToolsModule { }
