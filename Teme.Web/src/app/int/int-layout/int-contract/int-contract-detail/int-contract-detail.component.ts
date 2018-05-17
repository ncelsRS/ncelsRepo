import {Component, OnInit, ViewEncapsulation} from '@angular/core';
import {Menu} from "app/theme/components/menu/menu.model";
import {RegisterType} from "../../../../ext/ext-contract/ext-contract/RegisterType";
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-int-contract-detail',
  templateUrl: './int-contract-detail.component.html',
  styleUrls: ['./int-contract-detail.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class IntContractDetailComponent implements OnInit {
  public menuItems: Array<any>;
  idContract:any;

  constructor(private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.route.params
      .subscribe(params => {
        console.log("here"+params.id);
        this.idContract = params.id;
        console.log(params.id);
      })
    this.menuItems = this.getMenuItems();

  }

  getMenuItems() {
    return [
      new Menu(112312, 'Карточка договора', '/int/spa/contracts/'+this.idContract+'/card', null, 'tachometer', null, false, 0),
      new Menu(212312, 'Вложения', '/int/spa/contracts/:id/attachments', null,'keyboard-o', null, false, 0),
      new Menu(334343, 'История согласования', '/int/spa/contracts/:id/history', null, 'creative-commons', null, false, 0),
    ]
  }


}
