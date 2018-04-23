import {Component, Input, forwardRef, ViewChild, Injectable} from '@angular/core';
import {Observable} from 'rxjs/Rx';
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
import {DeclarantDocType} from '../ext-declarant/DeclarantDocType';

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
export class ExtManufacturerComponent extends TemplateValidation {

   isAddOrgForm = false;
   isAddBankName = false;

  selectedOrgForm: string;
  public orgFormData ;
  public orgFormData2=[] ;

  //private http: HttpClient;


  @Input() prnRegisterType: string;
   @Input() showErrors = false;
  constructor(private refService: RefService){
    super();
  }


  addOrgForm()
  {
    this.isAddOrgForm = true;
  }

  // saveOrgForm()
  // {
  //
  // }

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

  saveOrgForm(nameKz:string,nameRu:string){
    this.refService.saveManufacturOrgForm(nameKz,nameRu);
    this.isAddOrgForm = false;
  }

  // getOrgForm(){
  //   // this.OrgFormlevels =
  //   console.log( this.refService.getManufacturOrgForm());
  // }

  getOrgForm() {
       this.refService.getManufacturOrgForm().subscribe(
            data=> { this.orgFormData = data; console.log(data)},
          err => console.error(err),
            () => console.log('done loading orgForm')
        );
       //this.orgFormData2 = JSON..parse(this.orgFormData);
       //this.orgFormData2.push("id: 1, code: null, nameRu: 'Best', nameKz: 'Best'");
       //console.log(this.orgFormData2);

  }


}
