import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ResizeEvent } from 'angular-resizable-element';

@Component({
  selector: 'app-resizable',
  templateUrl: './resizable.component.html',
  styleUrls: ['./resizable.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ResizableComponent implements OnInit {

  public style: Object = {};

  constructor() { }

  ngOnInit() {
  }

  public validate(event: ResizeEvent): boolean {
    const MIN_DIMENSIONS_PX: number = 50;
    if (event.rectangle.width < MIN_DIMENSIONS_PX || event.rectangle.height < MIN_DIMENSIONS_PX) {
      return false;
    }
    return true;
  }

  public onResizeEnd(event: ResizeEvent): void {
    this.style = {
      position: 'fixed',
      left: `${event.rectangle.left}px`,
      top: `${event.rectangle.top}px`,
      width: `${event.rectangle.width}px`,
      height: `${event.rectangle.height}px`
    };
  }

}
