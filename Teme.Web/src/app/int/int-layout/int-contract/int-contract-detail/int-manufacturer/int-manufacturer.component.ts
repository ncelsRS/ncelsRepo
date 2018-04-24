import {Component, Input, OnInit, ViewEncapsulation} from '@angular/core';

@Component({
  selector: 'app-int-manufacturer',
  templateUrl: './int-manufacturer.component.html',
  styleUrls: ['./int-manufacturer.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class IntManufacturerComponent implements OnInit {
  isAddOrgForm = false;
  isAddBankName = false;
  public isRes : string;
  @Input() prnRegisterType: string;

  constructor() { }

  ngOnInit() {
  }

  addOrgForm()
  {
    this.isAddOrgForm = true;
  }

  saveOrgForm()
  {
    this.isAddOrgForm = false;
  }

  addBankName()
  {
    this.isAddBankName = true;
  }

  saveBankName()
  {
    this.isAddBankName = false;
  }

}
