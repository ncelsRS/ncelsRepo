import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-ext-contract',
  templateUrl: './ext-contract.component.html',
  styleUrls: ['./ext-contract.component.css']
})
export class ExtContractComponent implements OnInit {
  type: string;
  public id: string;

  constructor(private route: ActivatedRoute) {
    this.type = 'manufacturer';
  }

  childChanged(event) {
    event = null;
  }

  setDeclarationTab(name: string) {
    this.type = name;
  }
  ngOnInit() {
    this.route.params
      .subscribe(params => {
        this.id = params.id;
      });
  }

}
