import { Component, OnInit } from '@angular/core';
import { RegisterType } from './RegisterType';


@Component({
    selector: 'app-ext-declaration',
    templateUrl: './ext-declaration.component.html',
    styleUrls: ['./ext-declaration.component.css']
})

export class ExtDeclarationComponent implements OnInit {
    RegisterTypes: RegisterType[];
    type: string;
    constructor() {
        this.type = 'general';
    }

    setDeclarationTab(name: string) {
        this.type = name;
    }

    ngOnInit() {
    }

}
