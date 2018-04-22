import { Component, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-int-layout',
  templateUrl: './int-layout.component.html',
  styleUrls: ['./int-layout.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class IntLayoutComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  ngAfterViewInit(){
    document.getElementById('preloader').classList.add('hide');
  }

}
