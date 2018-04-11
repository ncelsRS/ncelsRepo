import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { DirectivesModule } from '../../theme/directives/directives.module';
import { ButtonsComponent } from './buttons/buttons.component';
import { CardsComponent } from './cards/cards.component';
import { ComponentsComponent } from './components/components.component';
import { TabsAccordionsComponent } from './tabs-accordions/tabs-accordions.component';
import { IconsComponent } from './icons/icons.component';
import { ListGroupComponent } from './list-group/list-group.component';
import { MediaObjectsComponent } from './media-objects/media-objects.component';
import { TypographyComponent } from './typography/typography.component';


export const routes = [
  { path: '', redirectTo: 'buttons', pathMatch: 'full'},
  { path: 'buttons', component: ButtonsComponent, data: { breadcrumb: 'Buttons' } },
  { path: 'cards', component: CardsComponent, data: { breadcrumb: 'Cards' } },
  { path: 'components', component: ComponentsComponent, data: { breadcrumb: 'Components' } },
  { path: 'icons', component: IconsComponent, data: { breadcrumb: 'Icons' } },
  { path: 'list-group', component: ListGroupComponent, data: { breadcrumb: 'List Group' } },
  { path: 'media-objects', component: MediaObjectsComponent, data: { breadcrumb: 'Media Objects' } },
  { path: 'tabs-accordions', component: TabsAccordionsComponent, data: { breadcrumb: 'Tabs & Accordions' } },
  { path: 'typography', component: TypographyComponent, data: { breadcrumb: 'Typography' } }
];

@NgModule({
  imports: [
    CommonModule,
    DirectivesModule,
    RouterModule.forChild(routes)
  ],
  declarations: [
    ButtonsComponent,
    CardsComponent,
    ComponentsComponent,
    TabsAccordionsComponent,
    IconsComponent,
    ListGroupComponent,
    MediaObjectsComponent,
    TypographyComponent
  ]
})
export class UiModule { }
