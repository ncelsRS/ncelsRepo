import { Component, ViewEncapsulation } from '@angular/core';
import { bubble } from './../charts.data';

@Component({
  selector: 'app-bubble',
  templateUrl: './bubble.component.html',
  encapsulation: ViewEncapsulation.None
})
export class BubbleComponent {

  public bubble: any[]; 
   
  public legendTitle = 'Legend';
  public showLegend = true;
  public tooltipDisabled = false;
  public showGridLines = true;
  public roundDomains = false;
  public maxRadius = 10;
  public minRadius = 3;
  public schemeType: string = 'ordinal';
  public showXAxis = true;
  public showYAxis = true;
  public showXAxisLabel = true;
  public xAxisLabel = 'Census Date';
  public showYAxisLabel = true;
  public yAxisLabel = 'Life expectancy [years]';
  public colorScheme = {
    domain: ['#2F3E9E', '#D22E2E', '#378D3B', '#0096A6', '#F47B00', '#606060']
  }; 
  public autoScale = true;
  
  constructor() {
    Object.assign(this, {bubble})   
  }
  
  onSelect(event) {
    console.log(event);
  }

}
