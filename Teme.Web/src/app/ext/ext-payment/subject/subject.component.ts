import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {IconModal} from '../../../shared/IconModal';

@Component({
  selector: 'app-subject',
  templateUrl: './subject.component.html',
  styleUrls: ['./subject.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [
    IconModal
  ]
})
export class SubjectComponent implements OnInit {

  constructor(public iconModal:  IconModal) { }

  ngOnInit() {
  }

}
