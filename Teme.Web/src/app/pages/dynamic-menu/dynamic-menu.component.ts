import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators} from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Settings } from '../../app.settings.model';
import { AppSettings } from '../../app.settings';
import { Menu } from '../../theme/components/menu/menu.model';
import { MenuService } from '../../theme/components/menu/menu.service';
import 'rxjs/add/operator/debounceTime';


@Component({
  selector: 'app-dynamic-menu',
  templateUrl: './dynamic-menu.component.html',
  encapsulation: ViewEncapsulation.None,
  providers: [ MenuService ]
})
export class DynamicMenuComponent implements OnInit {
  public form:FormGroup;
  public targets = ['_blank', '_self'];

  public icons = [
    { name: 'address-card-o', unicode: '&#xf2bc'},
    { name: 'bars', unicode: '&#xf0c9'},
    { name: 'bell-o', unicode: '&#xf0a2'},
    { name: 'calendar', unicode: '&#xf073'},
    { name: 'circle', unicode: '&#xf111'},
    { name: 'circle-o', unicode: '&#xf10c'},
    { name: 'cog', unicode: '&#xf013'},
    { name: 'comment', unicode: '&#xf075'},
    { name: 'comment-o', unicode: '&#xf0e5'},
    { name: 'credit-card', unicode: '&#xf09d'},
    { name: 'desktop', unicode: '&#xf108'},
    { name: 'exclamation-triangle', unicode: '&#xf071'},
    { name: 'folder', unicode: '&#xf07b'},
    { name: 'folder-o', unicode: '&#xf114'},
    { name: 'heart', unicode: '&#xf004'},
    { name: 'search', unicode:'&#xf002'}
  ]
 
  public menuItems:Array<Menu>;
  public settings: Settings;
  constructor(public fb:FormBuilder, 
              public toastrService: ToastrService,
              public appSettings:AppSettings, 
              private menuService:MenuService) {
      this.settings = this.appSettings.settings;
      if(this.settings.theme.menu == 'vertical'){
        this.menuItems = this.menuService.getVerticalMenuItems();
      } 
      if(this.settings.theme.menu == 'horizontal'){
        this.menuItems = this.menuService.getHorizontalMenuItems();
      } 
  }

  ngOnInit() {
    this.form = this.fb.group({
        title: ['', Validators.compose([Validators.required, Validators.minLength(3)])],
        routerLink: null,
        href: null,
        icon: null,
        target: null,
        hasSubMenu: false,
        parentId: 0
    });

  }

  ngAfterViewInit() {
    this.form.valueChanges.debounceTime(500).subscribe(menu => {  
      if(menu.routerLink && menu.routerLink != ''){
        this.form.controls['href'].setValue(null);
        this.form.controls['href'].disable();
        this.form.controls['target'].setValue(null);
        this.form.controls['target'].disable();
      }
      else{
        this.form.controls['href'].enable();
        this.form.controls['target'].enable();
      }

      if(menu.href && menu.href != ''){
        this.form.controls['routerLink'].setValue(null);
        this.form.controls['routerLink'].disable();
        this.form.controls['hasSubMenu'].setValue(false);
        this.form.controls['hasSubMenu'].disable();
      }
      else{
        this.form.controls['routerLink'].enable();
        this.form.controls['hasSubMenu'].enable();
      }
    })
  }

  public onSubmit(menu:Object):void {
    if (this.form.valid) {
      let lastId = this.menuItems[this.menuItems.length-1].id;
      let newMenuItem = new Menu(lastId+1, menu['title'], menu['routerLink'], menu['href'], menu['icon'], menu['target'], menu['hasSubMenu'], parseInt(menu['parentId']));
      this.menuService.addNewMenuItem(this.menuItems, newMenuItem, this.settings.theme.menu);      
      this.toastrService.success('New menu item successfully added !', menu['title'] );
      this.form.reset({
        hasSubMenu:false,
        parentId:0
      });     
    }
    if(this.settings.theme.menuType=='mini'){
        jQuery('.menu-item-link').tooltip('enable');
    }else{
        jQuery('.menu-item-link').tooltip('disable');
    }
  } 

}
