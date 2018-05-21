import {Component, OnInit, ViewEncapsulation} from '@angular/core';
import {DefaultEditor} from 'ng2-smart-table';

@Component({
  selector: 'app-measure-drop-down',
  templateUrl: './measure-drop-down.component.html',
  styleUrls: ['./measure-drop-down.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class MeasureDropDownComponent extends DefaultEditor implements OnInit {
  refName = 'ClassifierMedicalArea';

  constructor() {
    super();
  }

  ngOnInit() {
  }

}
