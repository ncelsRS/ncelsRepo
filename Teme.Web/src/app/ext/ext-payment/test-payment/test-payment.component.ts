import {Component, forwardRef, Output, Input, OnInit, EventEmitter} from '@angular/core';
import {TemplateValidation} from 'app/shared/TemplateValidation';
import {IconIntModal} from '../../../shared/icon/icon-int-modal';
import { ViewCell  } from 'ng2-smart-table';
import {SmartTableButtonViewComponent} from '../../../shared/smart-table-button-view.component';

@Component({
  selector: 'app-test-payment',
  templateUrl: './test-payment.component.html',
  styleUrls: ['./test-payment.component.scss'],
  providers: [
    IconIntModal
  ]
})
export class TestPaymentComponent extends TemplateValidation{
  @Input() showErrors = false;

  public icons = [];

  constructor(public iconModal:  IconIntModal) {

    super()

  }
  ngOnInit() {
  }


  settings = {
    columns: {
      id: {
        title: 'ID',
      },
      name: {
        title: 'Full Name',
      },
      username: {
        title: 'User Name',
      },
      email: {
        title: 'Email',
      },
      button: {
        title: 'Button',
        type: 'custom',
        renderComponent: SmartTableButtonViewComponent,
        onComponentInitFunction(instance) {
          instance.edit.subscribe(row => {
            alert(`${row.name} saved!`)
          });
        },
      },
    },
  };

  data = [
    {
      id: 1,
      name: 'Leanne Graham',
      username: 'Bret',
      email: 'Sincere@april.biz',
    },
    {
      id: 2,
      name: 'Ervin Howell',
      username: 'Antonette',
      email: 'Shanna@melissa.tv',
    },
    {
      id: 3,
      name: 'Clementine Bauch',
      username: 'Samantha',
      email: 'Nathan@yesenia.net',
    },
    {
      id: 4,
      name: 'Patricia Lebsack',
      username: 'Karianne',
      email: 'Julianne.OConner@kory.org',
    },
    {
      id: 5,
      name: 'Chelsey Dietrich',
      username: 'Kamren',
      email: 'Lucio_Hettinger@annie.ca',
    },
  ];

}


@Component({
  selector: 'button-view',
  template: `
    <button (click)="onClick()">{{ renderValue }}</button>
  `,
})
export class ButtonViewComponent implements ViewCell, OnInit {
  renderValue: string;

  @Input() value: string | number;
  @Input() rowData: any;

  @Output() save: EventEmitter<any> = new EventEmitter();

  ngOnInit() {
    this.renderValue = this.value.toString().toUpperCase();
  }

  onClick() {
    this.save.emit(this.rowData);
  }
}
