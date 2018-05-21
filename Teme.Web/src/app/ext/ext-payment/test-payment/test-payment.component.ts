import {Component, forwardRef, Output, Input, OnInit, EventEmitter} from '@angular/core';
import {TemplateValidation} from 'app/shared/TemplateValidation';
import {IconIntModal} from '../../../shared/icon/icon-int-modal';
import { ViewCell  } from 'ng2-smart-table';
import {SmartTableButtonViewComponent} from '../../../shared/smart-table-button-view.component';
import {NgbDateStruct} from '@ng-bootstrap/ng-bootstrap';
import {SmartTableReferenceComponent} from '../../../shared/smart-table-reference.component';


function padNumber(value: number) {
  if (isNumber(value)) {
    return `0${value}`.slice(-2);
  } else {
    return '';
  }
}


function isNumber(value: any): boolean {
  return !isNaN(toInteger(value));
}

function toInteger(value: any): number {
  return parseInt(`${value}`, 10);
}

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

  moduleType = "1";
  objectId = "1";
  fieldName = "test.field2";
  valueField = "знасение поля 3";
  displayField = "значение на экране 3"

  public icons = [];
  myDate = new Date();

  reference = [{ value: '1', title: 'Казахстан' }, { value: '2', title: 'Россия' }, { value: '3', title: 'Белорусия'}]

  constructor(public iconModal:  IconIntModal) {

    super()

  }
  ngOnInit() {

  }

  dateToString() {
    let ddd = new Date( "13-01-2011".replace( /(\d{2})-(\d{2})-(\d{4})/, "$2/$1/$3") );
  }




  settings = {
    columns: {
      id: {
        title: 'ID',
      },
      countryId: {
        title: 'Страна',
        type: 'custom',
        editor: {
          type: 'list',
          config: {
            list: this.reference
          }
        },
        renderComponent: SmartTableReferenceComponent,
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
      countryId: 1,
      username: 'Bret',
      email: 'Sincere@april.biz',
    },
    {
      id: 2,
      countryId: 2,
      username: 'Antonette',
      email: 'Shanna@melissa.tv',
    },
    {
      id: 3,
      countryId: 1,
      username: 'Samantha',
      email: 'Nathan@yesenia.net',
    },
    {
      id: 4,
      countryId: 1,
      username: 'Karianne',
      email: 'Julianne.OConner@kory.org',
    },
    {
      id: 5,
      countryId: 2,
      username: 'Kamren',
      email: 'Lucio_Hettinger@annie.ca',
    },
  ];

}


