import {Component, OnInit, ViewEncapsulation, HostListener, ViewChild} from '@angular/core';
import {FormsModule} from '@angular/forms';


@Component({
  selector: 'app-pages',
  templateUrl: './ext-payment.component.html',
  styleUrls: ['./ext-payment.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ExtPaymentComponent implements OnInit {
  public showAllErr = false;
  type: string;

  constructor() {
    this.type = 'cost-info';
  }

  ngOnInit() {
  }
  setDeclarationTab(name: string) {
    this.type = name;
  }
  sendPaymentRequest(validate) {
    this.showAllErr = true;
  }

  onSubmit(form: FormsModule) {
    // element.all(by.tagName('app-hero-parent'))
  }



  public declaration: any = {
    costInfo: {
      contractForm: "",             //Тип регистрации
      contractNumber: null, 			    //Номер договора
      conclusionBeginDate: null,	    //Дата заключения
      conclusionEndDate: null,		    //Срок Действия
      registrationType: null,	        //Вид регистрации
      registrationNumber: null,		    //№ регистрационного удостоверения
      isTypeImnMt: null,				      //Тип ИМН/МТ
      tradeName: null,					      //Торговое название
      typeServices: null,			        //Тип услуги
      additionalServices: null,	      //Дополнительные услуги
      priceManufacturerType: null,		//Cтоимость отечественного или зарубежного производителя
      numberServices:null,           //Количество
    },
    data: {
      nameKz: null,					          //Наименование на государственном языке
      nameRu: null,					          //Наименование на русском языке
      applicationAreaKz: null,		      //Область применения на государственном языке
      applicationAreaRu: null,		      //Область применения на русском языке
      appointmentAreaKz: null,			    //Назначение на государственном языке
      appointmentAreaRu: null,			    //Назначение на русском языке
      iSclosedSystem: null,			        //Закрытая система
      rationaleManufacturer: null,		  //Назначение на русском языке
      classRisk: null,				          //Класс в зависимости от степени потенциального риска применения
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
      manufacturer: null,						//Производитель
      organizationalForm: null,				//Организационная форма
      country: null,							//Страна
      nameKz: null,								//Наименование на государственном языке
      nameRu: null,								//Наименование на русском языке
      nameEn: null,								//Наименование на английском языке
      legalAddress: null,						//Юридический адрес
      actualAddress: null,						//Фактический адрес
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
      testSelect: null,
      contractForm: null
    }
  };
  //отключение заставки
  ngAfterViewInit(){
    document.getElementById('preloader').classList.add('hide');
  }



  getTestVar(){
    console.log(this.declaration);
  }
}
