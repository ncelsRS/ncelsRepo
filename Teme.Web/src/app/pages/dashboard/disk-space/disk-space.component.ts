import { Component, ViewEncapsulation } from '@angular/core';
import { AppSettings } from '../../../app.settings';
import { Settings } from '../../../app.settings.model';
import { disk_space } from '../dashboard.data';

@Component({
  selector: 'app-disk-space',
  templateUrl: './disk-space.component.html',
  encapsulation: ViewEncapsulation.None
})
export class DiskSpaceComponent {

  public data: any[];
 
  public showLegend = false;
  public gradient = true;
  public colorScheme = {
    domain: ['#2F3E9E', '#D22E2E', '#378D3B']
  }; 
  public showLabels = true;
  public explodeSlices = true;
  public doughnut = false;
 
  public previousShowMenuOption:boolean;
  public previousMenuOption:string;
  public previousMenuTypeOption:string;
  public settings: Settings;
  constructor(public appSettings:AppSettings) {
    this.settings = this.appSettings.settings;
    this.initPreviousSettings(); 
  }

  ngOnInit(){
    this.data = disk_space;  
  }
  
  public onSelect(event) {
    console.log(event);
  }

   public ngDoCheck() {
    if(this.checkAppSettingsChanges()) {
      setTimeout(() => this.data = [...disk_space] ); 
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
