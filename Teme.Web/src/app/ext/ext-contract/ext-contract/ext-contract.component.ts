import { Component, OnInit , Output, EventEmitter } from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {FormsModule} from '@angular/forms';
import {RegisterType} from './RegisterType'

@Component({
  selector: 'app-ext-contract',
  templateUrl: './ext-contract.component.html',
  styleUrls: ['./ext-contract.component.css']
})
export class ExtContractComponent implements OnInit {
  selectedLevel: string;
  @Output() selectedLevelChange = new EventEmitter<string>();
  levels: Array<RegisterType> = [
    {code: 'Registration', name: 'Регистрация'},
    {code: 'Reregistration', name: 'Перерегистрация'},
    {code: 'Edit', name: 'Внесение изменений'},
  ];
  type: string;
  public id: string;

  constructor(private route: ActivatedRoute) {
    this.type = 'manufacturer';
    this.selectedLevel = 'Registration';
  }

   setDeclarationTab(name: string) {
    this.type = name;
  }

  changeLevel(lev: RegisterType) {
    this.selectedLevel = lev.name;
  }

  onNameChange(lev: string){

    this.selectedLevel = lev;
    this.selectedLevelChange.emit(lev);
  }

  ngOnInit() {
    this.route.params
      .subscribe(params => {
        this.id = params.id;
      });
  }

}
