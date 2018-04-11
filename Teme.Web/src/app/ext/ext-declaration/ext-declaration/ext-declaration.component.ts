import { Component, OnInit } from '@angular/core';
import { REGISTER_TYPES } from './RegisterTypes';
import { RegisterType } from './RegisterType';


@Component({
    selector: 'app-ext-declaration',
    templateUrl: './ext-declaration.component.html',
    styleUrls: ['./ext-declaration.component.css']
})

export class ExtDeclarationComponent implements OnInit {
    RegisterTypes: RegisterType[];
    type: string;
    heroes = ['Windstorm', 'Bombasto', 'Magneta', 'Tornado'];

    constructor() {
        this.type = 'general';
        this.RegisterTypes = REGISTER_TYPES;
    }

    setDeclarationTab(name: string) {
        this.type = name;
    }

    ngOnInit() {
    }

}
