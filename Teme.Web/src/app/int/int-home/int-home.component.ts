import { Component, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-int-home',
  templateUrl: './int-home.component.html',
  styleUrls: ['./int-home.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class IntHomeComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  ngAfterViewInit(){
    document.getElementById('preloader').classList.add('hide');
  }

}
