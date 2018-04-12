import { Component, ViewEncapsulation } from '@angular/core';
import { AppSettings } from '../../../app.settings';
import { Settings } from '../../../app.settings.model';
import { countries } from '../dashboard.data';

@Component({
  selector: 'app-visitors',
  templateUrl: './visitors.component.html',
  styleUrls: ['./visitors.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class VisitorsComponent  {

  public countries: any[];
  public colorScheme = {
    domain: ['#378D3B', '#D22E2E', '#F47B00', '#AAAAAA']
  };
  public gradient:boolean = true;
  public tooltipDisabled:boolean = false;

  public visitorsLabelFormat(c): string {
    switch(c.label) {
      case 'Germany':
        return `<span class="flag-icon flag-icon-de mr-2"></span>${c.label}`;
      case 'France':
        return `<span class="flag-icon flag-icon-fr mr-2"></span>${c.label}`;
      case 'Great Britain':
        return `<span class="flag-icon flag-icon-gb mr-2"></span>${c.label}`;
      default:
        return c.label;
    }
  }


  public previousShowMenuOption:boolean;
  public previousMenuOption:string;
  public previousMenuTypeOption:string;
  public settings: Settings;
  constructor(public appSettings:AppSettings) {    
    this.settings = this.appSettings.settings;
    this.initPreviousSettings();
  }


  ngOnInit(){
    setTimeout(() => this.countries = countries); 
    this.countries = countries;
  }
  
  public onSelect(event) {
    console.log(event);
  }

  public ngDoCheck() {
    if(this.checkAppSettingsChanges()) {
      setTimeout(() => this.countries = [...countries] ); 
      this.initPreviousSettings();
    }
  }

  public checkAppSettingsChanges(){
    if(this.previousShowMenuOption != this.settings.theme.showMenu ||
      this.previousMenuOption != this.settings.theme.menu ||
      this.previousMenuTypeOption != this.settings.theme.menuType){
      return true;
    }
    return false;
  }

  public initPreviousSettings(){
    this.previousShowMenuOption = this.settings.theme.showMenu;
    this.previousMenuOption = this.settings.theme.menu;
    this.previousMenuTypeOption = this.settings.theme.menuType;
  }

}
