import {Component, forwardRef, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {TemplateValidation} from "../../../../shared/TemplateValidation";
import {DeclarationReferenceService} from "../../declaration-reference-service";
import {NG_VALIDATORS, NG_VALUE_ACCESSOR} from "@angular/forms";
import {ExtProducerComponent} from "../ext-producer/ext-producer.component";

@Component({
  selector: 'app-ext-subject',
  templateUrl: './ext-subject.component.html',
  styleUrls: ['./ext-subject.component.scss'],
  encapsulation: ViewEncapsulation.None,
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
export class ExtSubjectComponent extends TemplateValidation implements OnInit {
  @Input() showErrors = false;

  constructor(private referenceService: DeclarationReferenceService) {
    super()
  }

  ngOnInit() {
  }

}
