
export class PaymentDto {
  public id: number;
  public contractId: number;
  public contractNumber: string;
  public contractDateCreate: string;
  public contractDateInterval: string;
  public contractForm: number;
  public cardNumber: string;
  public isTypeImnMt: boolean;
  public tradeName: string;
  public nameRu: string;
  public nameKz: string;
  public applicationAreaKz: string;
  public applicationAreaRu: string;

  public appointmentKz: string;
  public appointmentRu: string;
  public isClosedSystem: boolean;
  public rationaleManufacturer: string;
  public degreeRiskClassId: number;
  public isBlank: boolean;

  public isMeasures: boolean;
  public isDiagnostics: boolean;
  public isStyryl: boolean;
  public isPresenceMedicinalProduct: boolean;
  public numberModificationImn: string;
  public revisionBeforeChanges: string;
  public changesMade: string;

  public paymentEquipmentDtos: Array<PaymentEquipmentDto>;
  public paymentPackagingDtos: Array<PaymentPackagingDto>;
  public paymentPlatformDtos: Array<PaymentPlatformDto>;
}
export class PaymentEquipmentDto {
  public id: number;
  public equipmentTypeId: number;
  public name: string;
  public model: string;
  public manufacturer: string;
  public countryId: number;
}
export class PaymentPackagingDto {
  public id: number;
  public packagingtTypeId: number;
  public name: string;
  public sizeWidth: string;
  public sizeHeight: string;
  public sizeLength: string;
  public sizeMeasureId: number;
  public volumeValue: string;
  public volumeMeasureId: number;
  public numberUnitsInBox: string;
  public shortDescription: string;
}
export class PaymentPlatformDto {
  public id: number;
  public countryId: number;
  public nameRu: string;
  public nameKz: string;
  public nameEn: string;
  public legalAddress: string;
  public factAddress: string;
}
