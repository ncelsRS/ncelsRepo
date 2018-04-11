import { Component, ViewEncapsulation } from '@angular/core';
import { AppSettings } from '../../../app.settings';
import { Settings } from '../../../app.settings.model';
import { cost } from '../dashboard.data';

function getNewTime(d){
  let h = (d.getHours()<10?'0':'') + d.getHours(),
      m = (d.getMinutes()<10?'0':'') + d.getMinutes(),
      s = (d.getSeconds()<10?'0':'') + d.getSeconds(),
      time = h + ":" + m + ":" + s;
  return time;
}

@Component({
  selector: 'app-cost',
  templateUrl: './cost.component.html',
  encapsulation: ViewEncapsulation.None
})
export class CostComponent {
  public cost: any[];
  public showXAxis = true;
  public showYAxis = true;
  public gradient:boolean = true;
  public tooltipDisabled:boolean = false;
  public showLegend = false;
  public showXAxisLabel = true;
  public xAxisLabel = 'Time';
  public showYAxisLabel = true;
  public yAxisLabel = 'Cost';
  public colorScheme = {
    domain: ['#0096A6', '#D22E2E']
  }; 
  public autoScale = true;
  
  public previousShowMenuOption:boolean;
  public previousMenuOption:string;
  public previousMenuTypeOption:string;
  public settings: Settings;
  constructor(public appSettings:AppSettings) {    
    this.settings = this.appSettings.settings;
    this.initPreviousSettings();
    setInterval(() => { 
      this.cost = [...this.addRandomValue()];
    }, 3000);
  }

  ngOnInit(){
    this.cost = cost;
  }
  
  public onSelect(event) {
    console.log(event);
  }

  public addRandomValue() {
    let value1 = Math.random() * 1000000;    
    this.cost[0].series.push({"name": getNewTime(new Date()), "value": value1});
    let value2 = Math.random() * 1000000;
    this.cost[1].series.push({"name": getNewTime(new Date()), "value": value2});
    if (this.cost[0].series.length > 5) this.cost[0].series.splice(0,1);
    if (this.cost[1].series.length > 5) this.cost[1].series.splice(0,1);
    return this.cost;
  } 
  
  
  ngOnDestroy(){
    this.cost[0].series.length = 0;
  }


  public ngDoCheck() {
    if(this.checkAppSettingsChanges()) {
      setTimeout(() => this.cost = [...cost] ); 
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
