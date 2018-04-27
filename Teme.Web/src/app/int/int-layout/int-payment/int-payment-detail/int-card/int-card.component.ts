import { Component, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-int-card',
  templateUrl: './int-card.component.html',
  styleUrls: ['./int-card.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class IntCardComponent implements OnInit {
  type: string;

  constructor() { this.type = 'cost-info'}

  ngOnInit() {
  }

  setDeclarationTab(name: string) {
    this.type = name;
  }

}
