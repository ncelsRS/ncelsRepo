import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-ext-layout',
    templateUrl: './ext-layout.component.html',
    styleUrls: ['./ext-layout.component.css']
})
export class ExtLayoutComponent implements OnInit {

    constructor() { }

    ngOnInit() {
    }

    ngAfterViewInit(){
        document.getElementById('preloader').classList.add('hide');
    }

}
