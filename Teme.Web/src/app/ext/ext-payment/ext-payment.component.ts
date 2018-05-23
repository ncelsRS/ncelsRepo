import {Component, OnInit, ViewEncapsulation, HostListener, ViewChild} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {ActivatedRoute, Routes} from '@angular/router';
import {ExtPaymentService} from './ext-payment.service';
import {PaymentDto} from './payment-dto';
import {isBoolean} from 'util';


@Component({
  selector: 'app-ext-payment',
  templateUrl: './ext-payment.component.html',
  styleUrls: ['./ext-payment.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [ExtPaymentService]
})
export class ExtPaymentComponent implements OnInit {
  public showAllErr = false;
  type: string;
  paymentId:string = "0";
  //paymentData:PaymentDto;

  constructor(private route: ActivatedRoute, private paymentService: ExtPaymentService) {
    this.type = 'cost-info';
  }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.paymentId = params.paymentId;
    });
    this.paymentService.getPayment(this.paymentId).subscribe((data:PaymentDto)=> {
      //this.paymentData = data
      this.paymentModel.costInfo.contractForm = (data.contractForm==null)? "" :data.contractForm ;
      this.paymentModel.costInfo.contractNumber = data.contractNumber;

      this.paymentModel.costInfo.contractDateCreate = new Date(data.contractDateCreate);
      let contractDateInterval:Date = new Date(data.contractDateCreate);
      contractDateInterval.setDate( contractDateInterval.getDate() + 364);
      this.paymentModel.costInfo.contractDateInterval = contractDateInterval;
      this.paymentModel.costInfo.cardNumber = data.cardNumber;
      this.paymentModel.costInfo.cardBeginDate = data.cardBeginDate;
      this.paymentModel.costInfo.cardEndDate = data.cardEndDate;
      this.paymentModel.costInfo.isTypeImnMt = data.isTypeImnMt;
      this.paymentModel.costInfo.tradeName = data.tradeName;

      this.paymentModel.data.nameKz = data.nameKz;
      this.paymentModel.data.nameRu = data.nameRu;
      this.paymentModel.data.applicationAreaKz = data.applicationAreaKz;
      this.paymentModel.data.applicationAreaRu = data.applicationAreaRu;
      this.paymentModel.data.appointmentKz = data.appointmentKz;
      this.paymentModel.data.appointmentRu = data.appointmentRu;
      this.paymentModel.data.isClosedSystem = data.isClosedSystem;
      this.paymentModel.data.rationaleManufacturer = data.rationaleManufacturer;
      this.paymentModel.data.degreeRiskClassId = data.degreeRiskClassId;
      this.paymentModel.data.isBlank = data.isBlank;
      this.paymentModel.data.isMeasures = data.isMeasures;
      this.paymentModel.data.isDiagnostics = data.isDiagnostics;
      this.paymentModel.data.isStyryl = data.isStyryl;
      this.paymentModel.data.isPresenceMedicinalProduct = data.isPresenceMedicinalProduct;
      this.paymentModel.data.numberModificationImn = data.numberModificationImn;

      this.paymentModel.manufacturer.revisionBeforeChanges = data.revisionBeforeChanges;
      this.paymentModel.manufacturer.changesMade = data.changesMade;

    });

  }
  setTab(name: string) {
    this.type = name;
  }
  sendPaymentRequest(validate) {
    this.showAllErr = true;
  }
  getChangeModel(){
    console.log("paymentModel",this.paymentModel );
    // let fields= {cardNumber: "333"};
    // this.changeModel(fields);
  }

  changeModel(evnt:any){
    //console.log("evnt","-" + evnt.name + "-", evnt.value, evnt);
    let fields = {[evnt.name]: evnt.value};
    console.log("fields",fields);
    var changeModelHead = ({'id': this.paymentId, 'classname': 'Teme.Shared.Data.Context.Payment', 'fields': fields});
    this.paymentService.changeModel(changeModelHead)
      .subscribe((data)=>{},error => console.log(error));
  }

  onSubmit(form: FormsModule) {
    // element.all(by.tagName('app-hero-parent'))
  }

  public paymentModel: any = {
    costInfo: {
      contractForm: null,             //Тип регистрации
      contractNumber: null, 			    //Номер договора
      contractDateCreate: null,	    //Дата заключения
      contractDateInterval: null,		    //Срок Действия
      cardNumber: null,		            //№ регистрационного удостоверения
      cardBeginDate: null,
      cardEndDate: null,
      isTypeImnMt: null,				      //Тип ИМН/МТ
      tradeName: null,					      //Торговое название
      typeServices: null,			        //Тип услуги
      // additionalServices: null,	      //Дополнительные услуги
      // priceManufacturerType: null,		//Cтоимость отечественного или зарубежного производителя
      // numberServices:null,           //Количество
    },
    data: {
      nameKz: null,					          //Наименование на государственном языке
      nameRu: null,					          //Наименование на русском языке
      applicationAreaKz: null,		      //Область применения на государственном языке
      applicationAreaRu: null,		      //Область применения на русском языке
      appointmentKz: null,			    //Назначение на государственном языке
      appointmentRu: null,			    //Назначение на русском языке
      isClosedSystem: null,			        //Закрытая система
      rationaleManufacturer: null,		  //Назначение на русском языке
      degreeRiskClassId: null,				          //Класс в зависимости от степени потенциального риска применения
      isBlank: null,							      //Бланк
      isMeasures: null,						      //Средство измерения
      isDiagnostics: null,					    //ИМН и МТ для ин витро диагностики
      isStyryl : null,						      //Стирильное
      isPresenceMedicinalProduct: null,		//В наличие лекарственное средства
      numberModificationImn: null,				  //Количество модификации ИМН
    },
    equipment: {
      equipmentImnMt: {							//Комплектация ИМН и МТ
        id: null,							      //П/п
        type: null,					        //Тип
        name: null,							    //Наименование
        idCode: null,								//ID
        model: null,							  //Мордель
        manufacturer: null,					//Производитель
        country: null,						  //Страна
      },
      numberModificationImn: null,				//Количество модификации ИМН
      packaging: {								      //Упаковка
        id: null,   							      //П/п
        type: null,						          //Вид (первичная или вторичная)
        name: null,							        //Наименование
        SizeWidth: null,							//Размер Ширина
        SizeHeight: null,							//Размер Высота
        SizeLength: null,							//Размер Длина
        SizeMeasure: null,						//Еденица измерения
        NumberUnitsInPackaging: null,					//Кол-во ед. в упаковке
        ShortDescription: null,						//Краткое описание
      },
    },
    manufacturer: {
      // manufacturer: null,						  //Производитель
      // organizationalForm: null,				//Организационная форма
      // country: null,							    //Страна
      // nameKz: null,								    //Наименование на государственном языке
      // nameRu: null,								    //Наименование на русском языке
      // nameEn: null,								    //Наименование на английском языке
      // legalAddress: null,						  //Юридический адрес
      // actualAddress: null,						//Фактический адрес
      revisionBeforeChanges: null,				//Редакция до внесения изменений
      changesMade: null,							//Вносимые изменения
    },
    subject: {
      subject: null,						    //Субъект
      isResident: null,						  //Резидент или не Резидент
      binId: null,									  //БИН
      organizationalForm: null,				//Организационная форма
      nameRu: null,								  //Наименование на русском языке
      country: null,							  //Страна
      legalAddress: null,						//Юридический адрес
      actualAddress: null,						//Фактический адрес
      phoneNumber: null,						//номер телефона
      emailAddress: null,						//Адрес электронной почты
      fax: null,								    //Факс

      directorLastName: null,					//Фамилия первого руководителя
      directorFirstName: null,					//Имя первого руководителя
      directorMidleName: null,					//Отчество первого руководителя
      directorPositionRu: null,					//Должность первого руководителя на русском языке
      directorPositionKz: null,					//Должность первого руководителя на государственном языке
      bank: null,							          //Название банка
      iik: null,								        //ИИК //Рассчетный счет
      currency: null,							      //Валюта
      bin: null,									      //БИН
      bik: null,									      //БИК
      code: null,								        //Код
      SWIFT: null,								      //SWIFT
    },
    attacments: {
      classSecurity: null,						        //Документ, подтверждающий класс безопасности в зависимости от степени потенциального риска применения (Декларация соответствия; письмо-обоснование от производителя и т.д) с аутентичным переводом на русский язык, заверенный нотариально
      registrationСountryOrigin: null,			//Документ, удостоверяющий регистрацию в стране производителе
      сompositionProduct: null,					      //Информация о введенных в медицинское изделие лекарственных средствах: состав, количество.
      technicalСharacteristics: null,			  //Спецификация с указанием технических характеристик, перечня комплектующих и расходных материалов (по утвержденной форме)
      exploitativeDocument: null,				     //Эксплуатационный документ медицинской техники на русском языке, в том числе инструкция по медицинскому применению расходных материалов и комплектующих к медицинской технике, являющихся изделиями медицинского назначения
      InstructionApplication: null,				  //Инструкция по применению изделия медицинского назначения, утвержденная в стране-производителе с аутентичным переводом на русский язык
      TypeSystem: null,							          //Письмо – обоснование о типе медицинской техники (открытая или закрытая система)
      ChangesMade: null,						          //Письмо – обоснование от производителя о вносимых изменениях
    },
    signing: {
      confirmation: null,						    //Подтвержден
    },
    testPayment: {
      contractForm: "2",
      testSelect: "hhh",
    }
  };
  //отключение заставки
  // ngAfterViewInit(){
  //   document.getElementById('preloader').classList.add('hide');
  // }



  getTestVar(){
    let fff = new Date('2018-05-04T12:43:56.4566667');
    console.log("getTestVar", fff.toString());
  }
}
