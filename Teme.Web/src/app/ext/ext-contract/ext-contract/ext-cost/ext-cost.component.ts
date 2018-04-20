import {Component, forwardRef, Input, OnInit, ViewChild} from '@angular/core';
import {
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR
} from "@angular/forms";
import {TemplateValidation} from '../../../../shared/TemplateValidation';


@Component({
  selector: 'app-ext-cost',
  templateUrl: './ext-cost.component.html',
  styleUrls: ['./ext-cost.component.css'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => ExtCostComponent),
    multi: true
  }, {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => ExtCostComponent),
    multi: true
  }]
})
export class ExtCostComponent extends TemplateValidation {
  isViewCost = false;
  @Input() showErrors = false;
  constructor() { super(); }

  ngOnInit() {
  }

  public costsSettings = {
    selectMode: 'single',
    hideHeader: false,
    hideSubHeader: false,
    noDataMessage: 'Нет данных',
    actions: false,
    prop: {name: 'view', filter: false},
    columns: {
      view: {
        title: '№',
        type: 'string'
      },
      declarationType: {
        title: 'Наименование работ по Прейскуранту',
        type: 'string'
      },
      name: {
        title: 'Цена в тенге, без НДС',
        type: 'string'
      },
      number: {
        title: 'Количество',
        type: 'string'
      },
      currentStatus: {
        title: 'Всего',
        type: 'string'
      },
      // sendDate: {
      //   title: 'Тип',
      //   type: 'string'
      // },
      // Action: {
      //   title: 'Действия',
      //   type: 'custom',
      //   renderComponent: ExtDeclarationsActionsComponent,
      //   onComponentInitFunction(instance) {
      //     instance.save.subscribe(row => {
      //       alert(`${row.name} saved!`)
      //     });
      //   }
      // },
    },
    pager: {
      display: true,
      perPage: 10
    }
  };

  viewCost()
  {
    this.isViewCost = true;
  }



}
