import {Component, forwardRef, Input} from '@angular/core';
import {
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
} from "@angular/forms";
import {TemplateValidation} from "../../../../shared/TemplateValidation";
import {DeclarationReferenceService} from "../../declaration-reference-service";

@Component({
  selector: 'app-ext-producer',
  templateUrl: './ext-producer.component.html',
  styleUrls: ['./ext-producer.component.css'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => ExtProducerComponent),
    multi: true
  }, {
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => ExtProducerComponent),
    multi: true
  },DeclarationReferenceService]
})

export class ExtProducerComponent extends TemplateValidation {
  @Input() showErrors = false;
  organizationForms;
  countries;
  constructor(private referenceService: DeclarationReferenceService) {
    super();
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
  }

}
