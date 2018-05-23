import {Component, forwardRef, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {IconExtModal} from 'app/shared/icon/icon-ext-modal';
import {DataComponent} from '../data/data.component';
import {NG_VALIDATORS, NG_VALUE_ACCESSOR} from '@angular/forms';
import {TemplateValidation} from '../../../shared/TemplateValidation';
import {ExtPaymentService} from '../ext-payment.service';
import {MeasureDropDownComponent} from './measure-drop-down/measure-drop-down.component';
import {DropDownLocalComponent} from '../../../shared/drop-down-local/drop-down-local.component';
import {DropDownRenderComponent} from '../../../shared/drop-down-local/drop-down-render';
import {PackagingTypeDropDownComponent} from './packaging-type-drop-down/packaging-type-drop-down.component';
import {EquipmentTypeDropDownComponent} from './equipment-type-drop-down/equipment-type-drop-down.component';
import {CountryDropDownComponent} from './country-drop-down/country-drop-down.component';

@Component({
  selector: 'app-equipment',
  templateUrl: './equipment.component.html',
  styleUrls: ['./equipment.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => EquipmentComponent),
    multi: true
  }, {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => EquipmentComponent),
    multi: true
  },
    IconExtModal,
    ExtPaymentService
  ]
})
export class EquipmentComponent extends TemplateValidation implements OnInit {

  @Input() showErrors = false;
  @Input() paymentId: string;
  @Input() public boxData;
  @Input() public equipmentData;
  public measure = [];
  boxSettings;

  ngOnInit() {}

  constructor(public iconModal: IconExtModal, private extPaymentService: ExtPaymentService) {
    super();
  }
}
