import {Component, OnInit} from '@angular/core';
import {ExtDeclarationsActionsComponent} from './ext-declarations-actions/ext-declarations-actions.component';

@Component({
  selector: 'app-ext-declarations',
  templateUrl: './ext-declarations.component.html',
  styleUrls: ['./ext-declarations.component.css']
})
export class ExtDeclarationsComponent implements OnInit {

  public data = [
    {
      view: 'sdfsdf',
      declarationType: 'Leanne Graham',
      name: 'Bret',
      number: 'Sincere@april.biz',
      currentStatus: '',
      sendDate: '',
      Action: 'Button #1',
      username: 'Nicholas.Stanton'
    }];
  public declarationsSettings = {
    selectMode: 'single',  //single|multi
    hideHeader: false,
    hideSubHeader: false,
    noDataMessage: 'Нет данных',
    actions: false,
    prop: {name: 'view', filter: false},
    columns: {
      view: {
        title: 'Вид',
        type: 'string'
      },
      declarationType: {
        title: 'Тип заявления',
        type: 'string'
      },
      name: {
        title: 'Наименование изделия\\материала\n',
        type: 'string'
      },
      number: {
        title: 'Номер',
        type: 'string'
      },
      currentStatus: {
        title: 'Текущий статус',
        type: 'string'
      },
      sendDate: {
        title: 'Дата направления',
        type: 'string'
      },
      Action: {
        title: 'Действия',
        type: 'custom',
        renderComponent: ExtDeclarationsActionsComponent,
        onComponentInitFunction(instance) {
          instance.save.subscribe(row => {
            alert(`${row.name} saved!`)
          });
        }
      },
    },
    pager: {
      display: true,
      perPage: 10
    }
  };

  constructor() {
  }

  ngOnInit() {
  }

  public getData(data) {
    const req = new XMLHttpRequest();
    req.open('GET', 'assets/data/users.json');
    req.onload = () => {
      data(JSON.parse(req.response));
    };
    req.send();
  }

  public onDeleteConfirm(event): void {
    if (window.confirm('Are you sure you want to delete?')) {
      event.confirm.resolve();
    } else {
      event.confirm.reject();
    }
  }

  public onRowSelect(event) {
    // console.log(event);
  }

  public onUserRowSelect(event) {
    //console.log(event);   //this select return only one page rows
  }

  public onRowHover(event) {
    //console.log(event);
  }

}
