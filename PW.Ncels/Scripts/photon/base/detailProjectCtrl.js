function actionsLoadFiles(data, type, full, meta, $http) {
    var files = '';
    angular.forEach(full.Items, function (value, key) {
        files = files + '<a href="/Upload/Download?id=' + value.documentId + '&name=' + value.name + '" >' + value.name + '&nbsp;<i class="fa fa-file"></i></a><br/>';
    });
    return files;
}
function priceReadonlyIsIncluded(data, type, full, meta) {
    var tag = '<input type="checkbox" disabled="disabled" price-id="' + full.Id + '" projectId="' + full.PriceProjectId + '" countryId="' + full.CountryId + '"';

    if (full.IsIncluded === true) {
        tag += ' checked ';
    }
    tag += '>';
    return tag;
}
function priceLsDetail($scope, DTColumnBuilder, $http, $uibModal) {
    $scope.view = function (id) {
        var modalInstance = $uibModal.open({
            templateUrl: '/Upload/FileView?id=' + id,
            controller: ModalregisterInstanceCtrl
        });
    };
    $scope.selectGridPrice = function (data) {
        console.log(data);
        $scope.object.Price = data;
    };
    $scope.dtColumns1 = [
        DTColumnBuilder.newColumn("Type", "Тип").withOption("name", "Number").notVisible(),
        DTColumnBuilder.newColumn("CountryName", "Страна"),
        DTColumnBuilder.newColumn("ManufacturerPriceWithLink", "Зарегистрированная цена производителя (указать год, ссылку на источник информации)"),
        DTColumnBuilder.newColumn("LimitPriceWithLink", "Предельная цена (указать год, ссылку на источник информации)"),
        DTColumnBuilder.newColumn("AvgOptPriceWithLink", "Средняя оптовая цена (за последний квартал) Указать ссылку на источник информации"),
        DTColumnBuilder.newColumn("AvgRozPriceWithLink", "Средняя розничная цена (за последний квартал) Указать ссылку на источник информации"),
        DTColumnBuilder.newColumn("IsIncluded", "Поставляет").renderWith(priceReadonlyIsIncluded)
    ];

    $scope.dtColumns10 = [
        DTColumnBuilder.newColumn("reg_number", "Номер регистрационного удостоверения"),
        DTColumnBuilder.newColumn("C_int_name", "МНН"),
        DTColumnBuilder.newColumn("name", "Наименование ЛС"),
        DTColumnBuilder.newColumn("concentration", "Концентрация"),
        DTColumnBuilder.newColumn("dosage_value", "Дозировка"),
        DTColumnBuilder.newColumn("C_dosage_form_name", "Лекарственная форма"),
        DTColumnBuilder.newColumn("C_atc_code", "АТХ код")
    ];

    $scope.dtColumnsCor = [
  DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
  DTColumnBuilder.newColumn("DocumentDate", "Дата").withOption('name', 'Number').renderWith(dateformatHtml),
  DTColumnBuilder.newColumn("Summary", "Краткое содержание").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("Id", "Файлы").renderWith(actionsLoadFiles)

    ];
}


function priceImnDetail($scope, DTColumnBuilder, $http, $uibModal) {
    $scope.view = function (id) {
        var modalInstance = $uibModal.open({
            templateUrl: '/Upload/FileView?id=' + id,
            controller: ModalregisterInstanceCtrl
        });
    };
    
    $scope.dtColumns1 = [
         DTColumnBuilder.newColumn("PartsName", "Наименование").withOption('name', 'Name'),
         DTColumnBuilder.newColumn("ManufacturerPrice", "Цена производителя").withClass("dtc_manufacturerPrice"),
         DTColumnBuilder.newColumn("CipPrice", "CIP цена").withClass("dtc_cipPrice"),
         DTColumnBuilder.newColumn("RefPriceTypeName", "Тип референтной цены"),
         DTColumnBuilder.newColumn("RefPrice", "Референтная цена").withClass("dtc_refPrice"),
         DTColumnBuilder.newColumn("UnitPrice", "Цена заявителя").withClass("dtc_unitPrice")
    ];

    $scope.dtColumns2 = [
        DTColumnBuilder.newColumn("CountryName", "Страна").withOption('name', 'CountryName'),
        DTColumnBuilder.newColumn("ManufacturerPrice", "Цена производителя").withOption('name', 'ManufacturerPrice'),
        DTColumnBuilder.newColumn("ManufacturerPriceNote", "Ссылка на источник информации").withOption('name', 'ManufacturerPriceNote'),
    ];
    $scope.dtColumns10 = [
        DTColumnBuilder.newColumn("reg_number", "Номер регистрационного удостоверения"),
        DTColumnBuilder.newColumn("C_int_name", "МНН"),
        DTColumnBuilder.newColumn("name", "Наименование ЛС"),
        DTColumnBuilder.newColumn("concentration", "Концентрация"),
        DTColumnBuilder.newColumn("dosage_value", "Дозировка"),
        DTColumnBuilder.newColumn("C_dosage_form_name", "Лекарственная форма"),
        DTColumnBuilder.newColumn("C_atc_code", "АТХ код")
    ];

    $scope.dtColumnsCor = [
  DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
  DTColumnBuilder.newColumn("DocumentDate", "Дата").withOption('name', 'Number').renderWith(dateformatHtml),
  DTColumnBuilder.newColumn("Summary", "Краткое содержание").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("Id", "Файлы").renderWith(actionsLoadFiles)

    ];
}


function rePriceLsDetail($scope, DTColumnBuilder, $http, $uibModal) {
    $scope.view = function (id) {
        var modalInstance = $uibModal.open({
            templateUrl: '/Upload/FileView?id=' + id,
            controller: ModalregisterInstanceCtrl
        });
    };
    $scope.dtColumns1 = [
        DTColumnBuilder.newColumn("Type", "Тип").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("CountryName", "Страна"),
        DTColumnBuilder.newColumn("ManufacturerPrice", "Зарегистрированная цена производителя"),
        DTColumnBuilder.newColumn("ManufacturerPriceNote", "Год, ссылка на источник информации"),
        DTColumnBuilder.newColumn("LimitPrice", "Предельная цена"),
        DTColumnBuilder.newColumn("LimitPriceNote", "Год, ссылка на источник информации"),
        DTColumnBuilder.newColumn("AvgOptPrice", "Средняя оптовая цена (за последний квартал)")
    ];
    $scope.dtColumns10 = [
        DTColumnBuilder.newColumn("reg_number", "Номер регистрационного удостоверения"),
        DTColumnBuilder.newColumn("C_int_name", "МНН"),
        DTColumnBuilder.newColumn("name", "Наименование ЛС"),
        DTColumnBuilder.newColumn("concentration", "Концентрация"),
        DTColumnBuilder.newColumn("dosage_value", "Дозировка"),
        DTColumnBuilder.newColumn("C_dosage_form_name", "Лекарственная форма"),
        DTColumnBuilder.newColumn("C_atc_code", "АТХ код")
    ];

    $scope.dtColumnsCor = [
  DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
  DTColumnBuilder.newColumn("DocumentDate", "Дата").withOption('name', 'Number').renderWith(dateformatHtml),
  DTColumnBuilder.newColumn("Summary", "Краткое содержание").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("Id", "Файлы").renderWith(actionsLoadFiles)

    ];
}


function rePriceImnDetail($scope, DTColumnBuilder, $http, $uibModal) {
    $scope.view = function (id) {
        var modalInstance = $uibModal.open({
            templateUrl: '/Upload/FileView?id=' + id,
            controller: ModalregisterInstanceCtrl
        });
    };
    $scope.dtColumns1 = [
        DTColumnBuilder.newColumn("PartsName", "Наименование").withOption('name', 'PartsName'),
        DTColumnBuilder.newColumn("ManufacturerPrice", "Цена производителя"),
        DTColumnBuilder.newColumn("CipPrice", "CIP цена"),
        DTColumnBuilder.newColumn("RefPriceTypeName", "Тип референтной цены"),
        DTColumnBuilder.newColumn("RefPrice", "Референтная цена"),
        DTColumnBuilder.newColumn("UnitPrice", "Цена заявителя")
    ];

    $scope.dtColumns2 = [
        DTColumnBuilder.newColumn("CountryName", "Страна").withOption('name', 'CountryName'),
        DTColumnBuilder.newColumn("ManufacturerPrice", "Цена производителя").withOption('name', 'ManufacturerPrice'),
        DTColumnBuilder.newColumn("ManufacturerPriceNote", "Ссылка на источник информации").withOption('name', 'ManufacturerPriceNote'),
    ];

    $scope.dtColumns10 = [
        DTColumnBuilder.newColumn("reg_number", "Номер регистрационного удостоверения"),
        DTColumnBuilder.newColumn("C_int_name", "МНН"),
        DTColumnBuilder.newColumn("name", "Наименование ЛС"),
        DTColumnBuilder.newColumn("concentration", "Концентрация"),
        DTColumnBuilder.newColumn("dosage_value", "Дозировка"),
        DTColumnBuilder.newColumn("C_dosage_form_name", "Лекарственная форма"),
        DTColumnBuilder.newColumn("C_atc_code", "АТХ код")
    ];

    $scope.dtColumnsCor = [
  DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
  DTColumnBuilder.newColumn("DocumentDate", "Дата").withOption('name', 'Number').renderWith(dateformatHtml),
  DTColumnBuilder.newColumn("Summary", "Краткое содержание").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("Id", "Файлы").renderWith(actionsLoadFiles)

    ];
}


function registerLsDetail($scope, DTColumnBuilder, $http, $uibModal) {
    $scope.view = function (id) {
        var modalInstance = $uibModal.open({
            templateUrl: '/Upload/FileView?id=' + id,
            controller: ModalregisterInstanceCtrl
        });
    };
    $scope.dtColumns1 = [
        DTColumnBuilder.newColumn("CountryName", "Страна").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("NameKz", "Наименование на государственном языке"),
        DTColumnBuilder.newColumn("NameRu", "Наименование на русском языке"),
        DTColumnBuilder.newColumn("NameEn", "Наименование на английском языке")
    ];

    $scope.dtColumns2 = [
        DTColumnBuilder.newColumn("PackageTypeName", "Вид").withOption('name', 'PackageTypeName'),
        DTColumnBuilder.newColumn("PackageNameAuto", "Наименование"),
        DTColumnBuilder.newColumn("Size", "Размер (при наличии)"),
        DTColumnBuilder.newColumn("Volume", "Объем (при наличии)"),
        DTColumnBuilder.newColumn("Count", "Кол-во единиц в упаковке"),
        DTColumnBuilder.newColumn("Note", "Краткое описание")
    ];

    $scope.dtColumns3 = [
        DTColumnBuilder.newColumn("MaterialTypeName", "Тип вещества").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("MaterialName", "Наименование").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("Count", "Количество на единицу лекарственной формы").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("NormativeDocument", "Нормативный докуент").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("CountryName", "Страна").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("MaterialGainName", "Тип").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("MaterialGain", "Место произрастания").withOption('name', 'Number'),
    ];

    $scope.dtColumns4 = [
        DTColumnBuilder.newColumn("CountryName", "Страна").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("DocNumber", "№ регистрационного удостоверения").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("DocDate", "Дата выдачи").withOption('name', 'DocDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("DocExpiryDate", "Срок действия").withOption('name', 'DocExpiryDate').renderWith(dateformatHtml)
    ];

    $scope.dtColumns5 = [
        DTColumnBuilder.newColumn("OrgManufactureTypeName", "Тип производителя").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("NameRu", "Наименование на русском языке").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("DocNumber", "№ разрешительного документа").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("AddressLegal", "Юридический адрес").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("AddressFact", "Фактический адрес").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("BossFio", "Руководитель").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("ContactFio", "Контактное лицо").withOption('name', 'Number'),
    ];

    $scope.dtColumnsCor = [
        DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("DocumentDate", "Дата").withOption('name', 'Number').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("Summary", "Краткое содержание").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("Id", "Файлы").renderWith(actionsLoadFiles)
    ];
}


function reRegisterLsDetail($scope, DTColumnBuilder, $http, $uibModal) {
    $scope.view = function (id) {
        var modalInstance = $uibModal.open({
            templateUrl: '/Upload/FileView?id=' + id,
            controller: ModalregisterInstanceCtrl
        });
    };
    $scope.dtColumns1 = [
        DTColumnBuilder.newColumn("CountryName", "Страна").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("NameKz", "Наименование на государственном языке"),
        DTColumnBuilder.newColumn("NameRu", "Наименование на русском языке"),
        DTColumnBuilder.newColumn("NameEn", "Наименование на английском языке")
    ];

    $scope.dtColumns2 = [
        DTColumnBuilder.newColumn("PackageTypeName", "Вид").withOption('name', 'PackageTypeName'),
        DTColumnBuilder.newColumn("PackageNameAuto", "Наименование"),
        DTColumnBuilder.newColumn("Size", "Размер (при наличии)"),
        DTColumnBuilder.newColumn("Volume", "Объем (при наличии)"),
        DTColumnBuilder.newColumn("Count", "Кол-во единиц в упаковке"),
        DTColumnBuilder.newColumn("Note", "Краткое описание")
    ];

    $scope.dtColumns3 = [
        DTColumnBuilder.newColumn("MaterialTypeName", "Тип вещества").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("MaterialName", "Наименование").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("Count", "Количество на единицу лекарственной формы").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("NormativeDocument", "Нормативный докуент").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("CountryName", "Страна").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("MaterialGainName", "Тип").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("MaterialGain", "Место произрастания").withOption('name', 'Number'),
    ];

    $scope.dtColumns4 = [
        DTColumnBuilder.newColumn("CountryName", "Страна").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("DocNumber", "№ регистрационного удостоверения").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("DocDate", "Дата выдачи").withOption('name', 'DocDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("DocExpiryDate", "Срок действия").withOption('name', 'DocExpiryDate').renderWith(dateformatHtml)
    ];

    $scope.dtColumns5 = [
        DTColumnBuilder.newColumn("OrgManufactureTypeName", "Тип производителя").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("NameRu", "Наименование на русском языке").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("DocNumber", "№ разрешительного документа").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("AddressLegal", "Юридический адрес").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("AddressFact", "Фактический адрес").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("BossFio", "Руководитель").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("ContactFio", "Контактное лицо").withOption('name', 'Number'),
    ];

    $scope.dtColumnsCor = [
  DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
  DTColumnBuilder.newColumn("DocumentDate", "Дата").withOption('name', 'Number').renderWith(dateformatHtml),
  DTColumnBuilder.newColumn("Summary", "Краткое содержание").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("Id", "Файлы").renderWith(actionsLoadFiles)

    ];
}


function chRegisterLsDetail($scope, DTColumnBuilder, $http, $uibModal) {
    $scope.view = function (id) {
        var modalInstance = $uibModal.open({
            templateUrl: '/Upload/FileView?id=' + id,
            controller: ModalregisterInstanceCtrl
        });
    };
    $scope.dtColumns1 = [
        DTColumnBuilder.newColumn("CountryName", "Страна").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("NameKz", "Наименование на государственном языке"),
        DTColumnBuilder.newColumn("NameRu", "Наименование на русском языке"),
        DTColumnBuilder.newColumn("NameEn", "Наименование на английском языке")
    ];

    $scope.dtColumns2 = [
       DTColumnBuilder.newColumn("PackageTypeName", "Вид").withOption('name', 'PackageTypeName'),
       DTColumnBuilder.newColumn("PackageNameAuto", "Наименование"),
       DTColumnBuilder.newColumn("Size", "Размер (при наличии)"),
       DTColumnBuilder.newColumn("Volume", "Объем (при наличии)"),
       DTColumnBuilder.newColumn("Count", "Кол-во единиц в упаковке"),
       DTColumnBuilder.newColumn("Note", "Краткое описание")
    ];

    $scope.dtColumns3 = [
        DTColumnBuilder.newColumn("MaterialTypeName", "Тип вещества").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("MaterialName", "Наименование").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("Count", "Количество на единицу лекарственной формы").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("NormativeDocument", "Нормативный докуент").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("CountryName", "Страна").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("MaterialGainName", "Тип").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("MaterialGain", "Место произрастания").withOption('name', 'Number'),
    ];

    $scope.dtColumns4 = [
        DTColumnBuilder.newColumn("CountryName", "Страна").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("DocNumber", "№ регистрационного удостоверения").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("DocDate", "Дата выдачи").withOption('name', 'DocDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("DocExpiryDate", "Срок действия").withOption('name', 'DocExpiryDate').renderWith(dateformatHtml)
    ];

    $scope.dtColumns5 = [
        DTColumnBuilder.newColumn("OrgManufactureTypeName", "Тип производителя").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("NameRu", "Наименование на русском языке").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("DocNumber", "№ разрешительного документа").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("AddressLegal", "Юридический адрес").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("AddressFact", "Фактический адрес").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("BossFio", "Руководитель").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("ContactFio", "Контактное лицо").withOption('name', 'Number'),
    ];

    $scope.dtColumns6 = [
        DTColumnBuilder.newColumn("Name", "Изменение").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("Conditions", "Условия/значения").withOption('name', 'Number'),
    ];

    $scope.dtColumnsCor = [
  DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
  DTColumnBuilder.newColumn("DocumentDate", "Дата").withOption('name', 'Number').renderWith(dateformatHtml),
  DTColumnBuilder.newColumn("Summary", "Краткое содержание").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("Id", "Файлы").renderWith(actionsLoadFiles)

    ];
}





angular
    .module('app')    
    .controller('priceLsDetail', ['$scope', 'DTColumnBuilder', '$http', '$uibModal', priceLsDetail])
    .controller('priceImnDetail', ['$scope', 'DTColumnBuilder', '$http', '$uibModal', priceImnDetail])
    .controller('rePriceLsDetail', ['$scope', 'DTColumnBuilder', '$http', '$uibModal', rePriceLsDetail])
    .controller('rePriceImnDetail', ['$scope', 'DTColumnBuilder', '$http', '$uibModal', rePriceImnDetail])
    .controller('registerLsDetail', ['$scope', 'DTColumnBuilder', '$http', '$uibModal', registerLsDetail])
    .controller('reRegisterLsDetail', ['$scope', 'DTColumnBuilder', '$http', '$uibModal', reRegisterLsDetail])
    .controller('chRegisterLsDetail', ['$scope', 'DTColumnBuilder', '$http', '$uibModal', chRegisterLsDetail]);