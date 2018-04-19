import {Component, forwardRef, Input} from '@angular/core';
import {
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
} from "@angular/forms";
import {TemplateValidation} from "../../../../shared/TemplateValidation";

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
  }]
})

export class ExtProducerComponent extends TemplateValidation {
  @Input() showErrors = false;

  constructor() {
    super();
  }

  ngOnInit() {
  }

}
