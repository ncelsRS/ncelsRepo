import {Component, EventEmitter, Input, OnInit, Output, ViewEncapsulation} from '@angular/core';
import {ViewCell} from 'ng2-smart-table';
import {Router} from "@angular/router";

@Component({
  selector: 'app-ext-manufactur-action',
  templateUrl: './ext-manufactur-action.component.html',
  styleUrls: ['./ext-manufactur-action.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ExtManufacturActionComponent implements OnInit ,  ViewCell{
  renderValue: string;

  @Input() value: string | number;
  @Input() rowData: any;

  @Output() smart : EventEmitter<any> = new EventEmitter();
  @Output() view: EventEmitter<any> = new EventEmitter();
  @Output() edit: EventEmitter<any> = new EventEmitter();
  @Output() dlte: EventEmitter<any> = new EventEmitter();

  ngOnInit() {
    this.renderValue = this.value.toString().toUpperCase();
  }

  constructor(private router: Router) { }

  viewData()
  {
    this.view.emit(this.rowData);
    this.router.navigate(['ext/contracts/6','view',this.rowData.id,'null' ]);
  }

  editData()
  {
    this.edit.emit(this.rowData);
  }

  deleteData()
  {
    this.dlte.emit(this.rowData);
  }




}
