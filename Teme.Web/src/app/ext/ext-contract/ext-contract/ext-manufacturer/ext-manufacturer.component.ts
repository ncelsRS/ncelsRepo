import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-ext-manufacturer',
  templateUrl: './ext-manufacturer.component.html',
  styleUrls: ['./ext-manufacturer.component.css']
})
export class ExtManufacturerComponent implements OnInit {
  @Output() change: EventEmitter<any> = new EventEmitter();
  constructor() { }

  ngOnInit() {
  }

}
