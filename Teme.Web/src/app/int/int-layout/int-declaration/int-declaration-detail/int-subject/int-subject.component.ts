import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {Subject} from "../../../../../shared/models/Subject";

@Component({
  selector: 'app-int-subject',
  templateUrl: './int-subject.component.html',
  styleUrls: ['./int-subject.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class IntSubjectComponent implements OnInit {
  public subjects: Array<any> = [
    new Subject(12, "Заявитель", "Заявитель", "declarant"),
    new Subject(13, "Производитель", "Производитель", "producer"),
    new Subject(14, "Третье лицо", "Третье лицо", "third")
  ]

  constructor() { }

  ngOnInit() {
  }

}
