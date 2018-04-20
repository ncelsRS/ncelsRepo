import {Component, forwardRef, Input, OnInit, ViewChild} from '@angular/core';
import {DeclarantDocType} from './DeclarantDocType';
import {TemplateValidation} from '../../../../shared/TemplateValidation';
import {
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
} from "@angular/forms";



@Component({
    selector: 'app-ext-declarant',
    templateUrl: './ext-declarant.component.html',
    styleUrls: ['./ext-declarant.component.css'],
    providers: [{
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ExtDeclarantComponent),
      multi: true
    }, {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => ExtDeclarantComponent),
      multi: true
    }]
})
export class ExtDeclarantComponent  extends TemplateValidation{
  isAddOrgForm = false;
  isAddBankName = false;
  selectedDeclarantDocType: string;
  levels: Array<DeclarantDocType> = [
    {code: 'Procuration', name: 'Доверенность'},
    {code: 'organizationChart', name: 'Устав'},
  ]


  changeDocLevel(lev: DeclarantDocType) {
    this.selectedDeclarantDocType = lev.name;
  }

  constructor() {
    super();
    this.selectedDeclarantDocType = 'Procuration';
  }


    ngOnInit() {
  }

  @Input() showErrors = false;

  addOrgForm()
  {
    this.isAddOrgForm = true;
  }

  saveOrgForm()
  {
    this.isAddOrgForm = false;
  }

  addBankName()
  {
    this.isAddBankName = true;
  }

  saveBankName()
  {
    this.isAddBankName = false;
  }

}
