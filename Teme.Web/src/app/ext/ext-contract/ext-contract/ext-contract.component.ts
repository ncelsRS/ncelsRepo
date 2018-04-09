import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-ext-contract',
  templateUrl: './ext-contract.component.html',
  styleUrls: ['./ext-contract.component.css']
})
export class ExtContractComponent implements OnInit {

  public id: string;

  constructor(private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.route.params
      .subscribe(params => {
        this.id = params.id;
      });
  }

}
