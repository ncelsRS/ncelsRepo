import {Component, OnInit, ViewEncapsulation} from '@angular/core';

@Component({
  selector: 'app-int-producer',
  templateUrl: './int-producer.component.html',
  styleUrls: ['./int-producer.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class IntProducerComponent implements OnInit {

  constructor() {
  }

  ngOnInit() {
  }

  showErrors = false;
  producer = null;
  organizationForm = null;
  displayRu = {invalid: true};
  displayOfficial = null;
  displayRussian = null;
  displayEnglish = null;
  permittedDocument = {invalid: true};
  extraditionDate = null;
  termDate = null;
  leaderLastname = null;
  leaderFirstname = null;
  leaderMiddlename = {invalid: true};
  leaderPosition = {invalid: true};
  leaderPhone = {invalid: true};
  leaderEmail = {invalid: true};
  contact = {invalid: true};
}
