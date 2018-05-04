import {Component, OnInit, ViewChild} from '@angular/core';
import {RegisterType} from './RegisterType';
import {FormsModule} from '@angular/forms';
import {ExtGeneralInformationComponent} from './ext-general-information/ext-general-information.component';

@Component({
  selector: 'app-ext-declaration',
  templateUrl: './ext-declaration.component.html',
  styleUrls: ['./ext-declaration.component.css']
})

export class ExtDeclarationComponent implements OnInit {
  public showAllErr = false;
  RegisterTypes: RegisterType[];
  type: string;

  public declaration: any = {
    general: {
      val: null,
      val2: null,
      val3: null,
      val4: null,
      val6: null,
      numberRegistrationDocument: null,
      registrationDate: null,
      term: null,
      normativeDocNumber: null,
      letterDate: null,
      imnOfficialLanguage: null,
      GMDN: null,
      imnRussianLanguage: null,
      NomenclatureCodeMedProduct: null,
      codeNomenclatureOfficial: null,
      codeNomenclatureRussian: null,
      descrNomenclatureRussian: null,
      descrNomenclatureOfficial: null,
      useAreOfficial: null,
      useAreRussian: null,
      purposeAreOfficial: null,
      purposeAreRussian: null,
      systemType: null,
      techDescriptionOfficial: null,
      techDescriptionRussian: null,
      dependencyClass: null,
      signType: null,
      sterile: null,
      measureDevice: null,
      medicineExistence: null,
      vitroDiagnostic: null,
      withoutAE: null,
      transportationCondition: null,
      storageCondition: null,
      closedSystem: null,
    },
    producer: {
      producer: null,
      organizationForm: null,
      displayRu: null,
      displayOfficial: null,
      displayRussian: null,
      displayEnglish: null,
      permittedDocument: null,
      extraditionDate: null,
      termDate: null,
      leaderLastname: null,
      leaderFirstname: null,
      leaderMiddlename: null,
      leaderPosition: null,
      leaderPhone: null
    },
    journal: {
      additional: null,
      storage: null,
      materialName: null,
      storageCondition: null,
      materialQuantity: null,
      manufactureDate: null,
      measure: null,
      expirationDate: null,
      serieParty: null
    },
    subject: {
      subject: null,
      resident: null,
      notResident: null,
      individual: null,
      legalEntity: null,
      firstChiefSurname: null,
      firstChiefName: null,
      bin: null,
      middleName: null,
      firstLeaderPositionRu: null,
      firstLeaderPositionKz: null,
      bankName: null,
      IIK: null,
      currency: null,
      leaderBin: null,
      leaderBik: null,
      leaderCode: null,
      subjectNameRu: null,
      subjectCountry: null,
      legalAddress: null,
      factualAddress: null,
      phoneNumber: null,
      email: null,
      fax: null,
      organizationForm: null,
    }
  };


  @ViewChild(ExtGeneralInformationComponent)
  private ExtGeneral: ExtGeneralInformationComponent;

  constructor() {
    this.type = 'general';
  }

  setDeclarationTab(name: string) {
    this.type = name;
  }

  onSubmit(form: FormsModule) {
    // element.all(by.tagName('app-hero-parent'))
  }

  ngOnInit() {
  }

  sendToNcels(valid) {
    this.showAllErr = true;
    console.log(this.declaration);
  }

  diagnostic() {
    return JSON.stringify(this.declaration);
  }

}
