import { Component, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-components',
  templateUrl: './components.component.html',
  styleUrls: ['./components.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ComponentsComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
      jQuery('.tt').tooltip();
      jQuery('.pp').popover();
  }

}
