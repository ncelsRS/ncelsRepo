import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {IconModal} from 'app/shared/IconModal';

@Component({
  selector: 'app-int-subject',
  templateUrl: './int-subject.component.html',
  styleUrls: ['./int-subject.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers:[IconModal]
})
export class IntSubjectComponent implements OnInit {

  constructor(public iconModal:  IconModal) { }

  ngOnInit() {
  }

}
