import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { IconsService } from './icons.service';

@Component({
  selector: 'app-icons',
  templateUrl: './icons.component.html',
  styleUrls: ['./icons.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [ IconsService ]
})
export class IconsComponent implements OnInit {
  public icons:any;
  constructor(private iconsService:IconsService) { 
    this.icons = iconsService.getIcons();
  }

  ngOnInit() {
  }

}
