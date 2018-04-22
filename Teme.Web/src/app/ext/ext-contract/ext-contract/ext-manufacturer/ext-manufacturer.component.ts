import {Component, Input, forwardRef, ViewChild, Injectable} from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
  ValidationErrors,
  Validator
} from "@angular/forms";
import {TemplateValidation} from '../../../../shared/TemplateValidation';
import {RefService} from '../ext-ref-sevice';

@Component({
  selector: 'app-ext-manufacturer',
  templateUrl: './ext-manufacturer.component.html',
  styleUrls: ['./ext-manufacturer.component.css'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => ExtManufacturerComponent),
    multi: true
  }, {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => ExtManufacturerComponent),
    multi: true
  }, RefService]
})
@Injectable()
export class ExtManufacturerComponent extends TemplateValidation{

   isAddOrgForm = false;
   isAddBankName = false;
  //private http: HttpClient;


  @Input() prnRegisterType: string;
   @Input() showErrors = false;
  constructor(private refService: RefService){
    super();
   // this.getOrgForm();

  }


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

  private url = "http://10.20.44.57:81/api/Reference/SaveOrganizationForm";
  //constructor(private http: HttpClient){ }

  getOrgForm(){
    this.refService.saveOrgForm();
  }


}
