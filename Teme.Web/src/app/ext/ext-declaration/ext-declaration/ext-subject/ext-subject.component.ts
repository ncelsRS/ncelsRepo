import {Component, forwardRef, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {TemplateValidation} from "../../../../shared/TemplateValidation";
import {DeclarationReferenceService} from "../../declaration-reference-service";
import {NG_VALIDATORS, NG_VALUE_ACCESSOR} from "@angular/forms";
import {Subject} from "../../../../shared/models/Subject";
import {RefService} from "../../../ext-contract/ext-contract/ext-ref-sevice";

@Component({
  selector: 'app-ext-subject',
  templateUrl: './ext-subject.component.html',
  styleUrls: ['./ext-subject.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => ExtSubjectComponent),
    multi: true
  }, {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => ExtSubjectComponent),
    multi: true
  }, DeclarationReferenceService, RefService]
})
export class ExtSubjectComponent extends TemplateValidation implements OnInit {
  @Input() showErrors = false;
  isAddBankName = false;
  public bankVar;
  bankId:any;
  organizationForms;countries;currencies;
  public subjects: Array<any> = [
    new Subject(12, "Заявитель", "Заявитель", "declarant"),
    new Subject(13, "Производитель", "Производитель", "producer"),
    new Subject(14, "Третье лицо", "Третье лицо", "third")
  ]

  constructor(private referenceService: DeclarationReferenceService,
              private refService: RefService) {
    super()
  }

  ngOnInit() {
    this.referenceService.getOrganizationForm().then(
      res => { // Success
        this.organizationForms = res;
        return res;
      }
    ).catch((err) => console.error(err));

    this.referenceService.getCountry().then(
      res => { // Success
        this.countries = res;
        return res;
      }
    ).catch((err) => console.error(err));

    this.referenceService.getCurrency().then(
      res => { // Success
        this.currencies = res;
        return res;
      }
    ).catch((err) => console.error(err));

    this.getBanks();
  }

  addBankName()
  {
    this.isAddBankName = true;
  }

  saveBank(nameKz:string,nameRu:string){
    var res =  this.refService.saveBank(nameKz,nameRu)
      .toPromise()
      .then(response => {
        console.log(response);
        this.bankId =response;
        return this.bankId;
      })
      .then (response => {
          this.bankVar.push({id: response as number, code: null, nameRu: nameKz, nameKz: nameRu});
          console.log(res);
          this.isAddBankName = false;
        }
      )
      .catch (err=>
        {
          console.error(err);
        }
      )
    ;
    console.log("step 1");
  }

  declineBank()
  {
    this.isAddBankName = false;
  }

  getBanks() {
    this.refService.getBank()
      .subscribe(
        data=> {
          this.bankVar = data;      },
        (err) =>
          console.error(err),
        () =>
          console.log('done loading Bank')
      );

  }

}
