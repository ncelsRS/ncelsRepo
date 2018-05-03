import {Component, OnInit, ViewEncapsulation, HostListener} from '@angular/core';
import {trigger, state, style, transition, animate} from '@angular/animations';
import {AppSettings} from '../../app.settings';
import {Settings} from '../../app.settings.model';
import {MenuService} from '../../shared/menu/menu.service';
import {Menu} from "../../shared/menu/menu.model";

@Component({
  selector: 'app-int-menu',
  templateUrl: './int-menu.component.html',
  styleUrls: ['./int-menu.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [MenuService],
  animations: [
    trigger('showInfo', [
      state('1', style({transform: 'rotate(180deg)'})),
      state('0', style({transform: 'rotate(0deg)'})),
      transition('1 => 0', animate('400ms')),
      transition('0 => 1', animate('400ms'))
    ])
  ]
})
export class IntMenuComponent implements OnInit {
  public showHorizontalMenu: boolean = true;
  public showInfoContent: boolean = false;
  public settings: Settings;
  public menuItems: Array<any>;

  constructor(public appSettings: AppSettings, public menuService: MenuService) {
    this.settings = this.appSettings.settings;
    this.menuItems = this.getMenuItems();
  }

  ngOnInit() {
    if(window.innerWidth <= 768)
      this.showHorizontalMenu = false;
  }

  public closeSubMenus(){
    let menu = document.querySelector("#menu0");
    if(menu){
      for (let i = 0; i < menu.children.length; i++) {
        let child = menu.children[i].children[1];
        if(child){
          if(child.classList.contains('show')){
            child.classList.remove('show');
            menu.children[i].children[0].classList.add('collapsed');
          }
        }
      }
    }
  }

  getMenuItems() {
    return [
      new Menu (2, 'Отчеты', '/pages/membership', null, 'users', null, false, 0),
      new Menu (3, 'Договоры', null, null, 'laptop', null, true, 0),
      new Menu (4, 'Договоры', '/int/spa/contracts', null, 'keyboard-o', null, false, 3),
      new Menu (5, 'Договоры', '/int/spa/contracts', null, 'address-card-o', null, false, 3),
      new Menu (20, 'Заявки', null, null, 'pencil-square-o', null, true, 0),
      new Menu (21, 'Заявки', '/int/spa/declarations', null, 'check-square-o', null, false, 20),
      new Menu (22, 'Заявки', '/int/spa/declarations', null, 'th-large', null, false, 20),
      new Menu (30, 'Платеж', '/int/spa/payments', null, 'money', null, true, 0),
    ]
  }

  @HostListener('window:resize')
  public onWindowResize():void {
    if(window.innerWidth <= 768){
      this.showHorizontalMenu = false;
    }
    else{
      this.showHorizontalMenu = true;
    }
  }

}
