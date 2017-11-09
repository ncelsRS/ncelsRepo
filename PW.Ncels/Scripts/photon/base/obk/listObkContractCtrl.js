function obkContractForm($scope, $http, $interval, $uibModal, $window) {
    $scope.flag = false;
    // Patterns start
    $scope.emailPattern = ".+@.+\\..+";
    $scope.iikPattern = "[a-z0-9]+";
    $scope.bankBikPattern = "[a-z0-9]+";
    $scope.bankBikMinLength = 8;
    $scope.bankBikMaxLength = 11;
    $scope.phonePattern = "[0-9 ()+,]+";
    $scope.iinPattern = "[0-9]+";
    $scope.iinMinLength = 12;
    $scope.iinMaxLength = 12;
    // Patterns end



    $scope.ExpertOrganizations = [];
    $scope.ContractSigners = [];
    $scope.factory = {};

    $scope.showContactInformation = false;

    $scope.declarantNotFound = false;

    $scope.iinSearchActive = false;

    $scope.searchViewMode = "drugform";

    $scope.mtParts = [];
    $scope.drugForms = [];
    $scope.selectedMtParts = [];

    var curDate = new Date();
    $scope.mode = 0; // 0 - unknown, 1 - add, 2 - edit
    $scope.declarant = {};
    $scope.object = {};

    $scope.object.Status = 1;
    $scope.object.Number = "б/н";

    $scope.searchDrugWorking = false;

    $scope.showComments = false;
    $scope.viewMpde = false;

    $scope.declarant.IsResident = true;
    $scope.object.seriesValue = "";
    $scope.object.seriesCreateDate = curDate.getTime();
    $scope.object.seriesExpireDate = curDate.getTime();

    $scope.object.BossDocCreatedDate = "";
    $scope.object.BossDocEndDate = "";
    $scope.object.BossDocUnlimited = false;
    $scope.object.SignDocCreatedDate = "";
    $scope.object.SignDocEndDate = "";
    $scope.object.SignDocUnlimited = false;

    $scope.enableCompanyData = false;
    $scope.showBin = false;
    $scope.showResidentsBlock = false;

    $scope.productSeries = [];

    $scope.addedProducts = [];

    $scope.product = {};

    $scope.gridOptions = {
        enableRowSelection: true,
        enableRowHeaderSelection: false,
        multiSelect: false,
        noUnselect: true
    };

    $scope.gridOptions.onRegisterApi = function (gridApi) {
        $scope.gridOptionsApi = gridApi;

        gridApi.selection.on.rowSelectionChanged($scope, function (row) {
            $scope.product.ProductId = row.entity.ProductId;
            $scope.product.RegTypeId = row.entity.RegTypeId;
            $scope.product.DegreeRiskId = row.entity.DegreeRiskId;
            $scope.product.NameRu = row.entity.Name;
            $scope.product.NameKz = row.entity.NameKz;
            $scope.product.ProducerNameRu = row.entity.ProducerName;
            $scope.product.ProducerNameKz = row.entity.ProducerNameKz;
            $scope.product.CountryNameRu = row.entity.CountryName;
            $scope.product.CountryNameKz = row.entity.CountryNameKz;
            $scope.product.TnvedCode = row.entity.TnvedCode;
            $scope.product.KpvedCode = row.entity.KpvedCode;
            $scope.product.Price = row.entity.Price;
            $scope.product.Currency = row.entity.Currency;
            $scope.product.RegisterId = row.entity.RegisterId;
            $scope.product.RegNumber = row.entity.RegNumber;
            $scope.product.RegNumberKz = row.entity.RegNumberKz;
            $scope.product.RegDate = row.entity.RegDate;
            $scope.product.ExpirationDate = row.entity.ExpireDate;
            $scope.product.NdName = row.entity.NdName;
            $scope.product.NdNumber = row.entity.NdNumber;

            $scope.selectedMtParts.length = 0;

            $scope.loadDrugFormsAndMtParts();

            $scope.loadProductServiceNames();

            $scope.object.ProductServiceName = null;
        });
    };

    $scope.gridOptionsDrugForm = {
        enableRowSelection: true,
        enableRowHeaderSelection: false,
        multiSelect: false,
        noUnselect: true
    };

    $scope.gridOptionsDrugForm.columnDefs = [
        { name: 'Id', displayName: 'ИД', visible: false },
        { name: 'RegisterId', displayName: 'ИД продукта', visible: false },
        { name: 'BoxCount', displayName: 'Кол-во в потр.уп.', visible: true },
        { name: 'FullName', displayName: 'Полное наименование', visible: true },
        { name: 'FullNameKz', displayName: 'Полное наименование', visible: false }
    ];

    $scope.gridOptionsDrugForm.data = $scope.drugForms;

    $scope.gridOptionsDrugForm.onRegisterApi = function (gridApi) {
        $scope.gridOptionsDrugFormApi = gridApi;

        $scope.gridOptionsDrugFormApi.selection.on.rowSelectionChanged($scope, function (row) {
            $scope.product.DrugFormId = row.entity.Id;
            $scope.product.DrugFormRegisterId = row.entity.RegisterId;
            $scope.product.DrugFormBoxCount = row.entity.BoxCount;
            $scope.product.DrugFormFullName = row.entity.FullName;
            $scope.product.DrugFormFullNameKz = row.entity.FullNameKz;
        });
    };

    $scope.clearDrugFormComponents = function () {
        $scope.product.DrugFormId = null;
        $scope.product.DrugFormRegisterId = null;
        $scope.product.DrugFormBoxCount = null;
        $scope.product.DrugFormFullName = null;
        $scope.product.DrugFormFullNameKz = null;
    }

    $scope.gridOptionsMtPart = {
    };

    $scope.gridOptionsMtPart.columnDefs = [
        { name: "Selected", displayName: "Выбор", visible: true, type: "boolean", cellTemplate: '<input type="checkbox" ng-model="row.entity.Selected" ng-change="grid.appScope.cBoxChange(row);">' },
        { name: 'Id', displayName: 'ИД', visible: false },
        { name: 'RegisterId', displayName: 'ИД продукции', visible: false },
        { name: 'PartNumber', displayName: '№', visible: true },
        { name: 'Model', displayName: 'Модель', visible: true },
        { name: 'Specification', displayName: 'Тех.характеристика', visible: true },
        { name: 'SpecificationKz', displayName: 'Тех.характеристика', visible: false },
        { name: 'Name', displayName: 'Наименование изделия', visible: true },
        { name: 'NameKz', displayName: 'Наименование изделия', visible: false },
        { name: 'ProducerName', displayName: 'Производитель', visible: true },
        { name: 'ProducerNameKz', displayName: 'Производитель', visible: false },
        { name: 'CountryName', displayName: 'Страна', visible: true },
        { name: 'CountryNameKz', displayName: 'Страна', visible: false }
    ];

    $scope.gridOptionsMtPart.data = $scope.mtParts;

    $scope.gridOptionsMtPart.onRegisterApi = function (gridApi) {
        $scope.gridOptionsMtPartApi = gridApi;
    };

    $scope.cBoxChange = function (row) {
        if (row.entity.Selected) {
            $scope.addItemToSelectedMtParts(row.entity);
        }
        else {
            $scope.removeItemFromSelectedMtParts(row.entity);
        }
    }

    $scope.changeViewMode = function () {
        // 1 Черновик
        if ($scope.object.Status == 1) {
            $scope.object.viewMpde = false;
        }
            // 7 На корректировке у заявителя
        else if ($scope.object.Status == 7) {
            $scope.object.viewMpde = false;
            $scope.showComments = true;
        }
        else {
            $scope.object.viewMpde = true;
        }
    }

    $scope.addItemToSelectedMtParts = function (item) {
        if (!$scope.selectedMtPartsContainsItem(item)) {
            var mtPart = {
                Id: item.Id,
                RegisterId: item.RegisterId,
                PartNumber: item.PartNumber,
                PartNumber: item.PartNumber,
                Model: item.Model,
                Specification: item.Specification,
                SpecificationKz: item.SpecificationKz,
                Name: item.Name,
                NameKz: item.NameKz,
                ProducerName: item.ProducerName,
                ProducerNameKz: item.ProducerNameKz,
                CountryName: item.CountryName,
                CountryNameKz: item.CountryNameKz
            };
            $scope.selectedMtParts.push(mtPart);
        }
    }

    $scope.removeItemFromSelectedMtParts = function (item) {
        for (var i = $scope.selectedMtParts.length - 1; i >= 0; i--) {
            if ($scope.selectedMtParts[i].Id == item.Id) {
                $scope.selectedMtParts.splice(i, 1);
            }
        }
    }

    $scope.selectedMtPartsContainsItem = function (item) {
        var res = false;
        for (var i = 0; i < $scope.selectedMtParts.length; i++) {
            if ($scope.selectedMtParts[i].Id === item.Id) {
                res = true;
                break;
            }
        }
        return res;
    }


    $scope.gridOptionsSelectedMtPart = {
        enableRowSelection: true,
        enableRowHeaderSelection: false,
        multiSelect: false,
        noUnselect: true
    };

    $scope.gridOptionsSelectedMtPart.columnDefs = [
        { name: 'Id', displayName: 'ИД', visible: false },
        { name: 'RegisterId', displayName: 'ИД продукции', visible: false },
        { name: 'PartNumber', displayName: '№', visible: true },
        { name: 'Model', displayName: 'Модель', visible: true },
        { name: 'Specification', displayName: 'Тех.характеристика', visible: true },
        { name: 'SpecificationKz', displayName: 'Тех.характеристика', visible: false },
        { name: 'Name', displayName: 'Наименование изделия', visible: true },
        { name: 'NameKz', displayName: 'Наименование изделия', visible: false },
        { name: 'ProducerName', displayName: 'Производитель', visible: true },
        { name: 'ProducerNameKz', displayName: 'Производитель', visible: false },
        { name: 'CountryName', displayName: 'Страна', visible: true },
        { name: 'CountryNameKz', displayName: 'Страна', visible: false }
    ];

    $scope.gridOptionsSelectedMtPart.data = $scope.selectedMtParts;

    $scope.gridOptionsSelectedMtPart.onRegisterApi = function (gridApi) {
        $scope.gridOptionsSelectedMtPartApi = gridApi;

        $scope.gridOptionsSelectedMtPartApi.selection.on.rowSelectionChanged($scope, function (row) {
        });
    };

    $scope.loadDrugFormsAndMtParts = function () {
        if ($scope.product.ProductId) {
            if ($scope.product.RegTypeId == 1) {
                $scope.loadDrugForms($scope.product.ProductId);
                $scope.loadMtParts(null);
            }
            else if ($scope.product.RegTypeId == 2) {
                $scope.loadDrugForms($scope.product.ProductId);
                $scope.loadMtParts($scope.product.ProductId);
            }
        }
        else {
            $scope.loadDrugForms(null);
            $scope.loadMtParts(null);
        }
    }

    $scope.loadDrugForms = function (productId) {
        $scope.drugForms.length = 0;
        if (productId) {
            $http({
                method: 'GET',
                url: '/OBKContract/GetDrugForms',
                params: {
                    productId: productId
                }
            }).then(function (resp) {
                if (resp.data) {
                    $scope.drugForms.push.apply($scope.drugForms, resp.data);
                }
            });
        }
    }

    $scope.loadMtParts = function (productId) {
        $scope.mtParts.length = 0;
        if (productId) {
            $http({
                method: 'GET',
                url: '/OBKContract/GetMtParts',
                params: {
                    productId: productId
                }
            }).then(function (resp) {
                if (resp.data) {
                    $scope.mtParts.push.apply($scope.mtParts, resp.data);
                }
            });
        }
    }


    $scope.gridOptions.columnDefs = [
    { name: 'ProductId', displayName: 'ProductId', visible: false },
    { name: 'RegNumber', displayName: 'Рег. номер' },
    { name: 'RegNumberKz', displayName: 'Рег. номер - KZ', visible: false },
    { name: 'RegTypeName', displayName: 'Тип' },
    { name: 'RegTypeId', displayName: 'Тип - ИД', visible: false },
    { name: 'Name', displayName: 'Торговое название' },
    { name: 'NameKz', displayName: 'Торговое название на казахском', visible: false },
    { name: 'RegDate', displayName: 'Дата регистрации' },
    { name: 'ExpireDate', displayName: 'Дата истечения' },
    { name: 'ProducerName', displayName: 'Производитель' },
    { name: 'ProducerNameKz', displayName: 'Производитель на казахском', visible: false },
    { name: 'CountryName', displayName: 'Страна' },
    { name: 'CountryNameKz', displayName: 'Страна на казахском', visible: false },
    { name: 'TnvedCode', displayName: 'ТН ВЭД', visible: false },
    { name: 'KpvedCode', displayName: 'КП ВЭД', visible: false },
    { name: 'Price', displayName: 'Цена', visible: false },
    { name: 'Currency', displayName: 'Валюта', visible: false },
    { name: 'DegreeRiskId', displayName: 'Класс ИМН', visible: false },
    { name: 'NdName', displayName: 'NdName', visible: false },
    { name: 'NdNumber', displayName: 'NdNumber', visible: false },
    { name: 'RegisterId', displayName: 'RegisterId', visible: false }
    ];

    // gridSeries
    $scope.gridOptionsSeries = {
        enableRowSelection: true,
        enableRowHeaderSelection: false,
        multiSelect: false,
        noUnselect: true
    };

    $scope.selectedSeriesIndex = null;

    $scope.gridOptionsSeries.onRegisterApi = function (gridApi) {
        $scope.gridOptionsSeriesApi = gridApi;
        gridApi.selection.on.rowSelectionChanged($scope, function (row) {
            $scope.selectedSeriesIndex = $scope.gridOptionsSeries.data.indexOf(row.entity);
        });
    };

    $scope.gridOptionsSeries.columnDefs = [
{ name: 'Id', displayName: 'ID', visible: false },
{ name: 'Series', displayName: 'Номер серии' },
{ name: 'CreateDate', displayName: 'Произведена' },
{ name: 'ExpireDate', displayName: 'Истекает' },
{ name: 'Part', displayName: 'Размер партии' },
{ name: 'UnitName', displayName: 'Ед. измерения' },
{ name: 'UnitId', displayName: 'Ед. измерения - код', visible: false },
{ name: 'ButtonComments', displayName: '', cellTemplate: '<span class="input-group-addon"><a valval="{{row.entity.Id}}" class="obkproductseriedialog" href="#"><i class="glyphicon glyphicon-info-sign"></i></a></span>' }
    ];

    $scope.gridOptionsSeries.data = $scope.productSeries;

    // Grid SEries End

    // Grid Products

    $scope.gridOptionsProducts = {
        enableRowSelection: true,
        enableRowHeaderSelection: false,
        multiSelect: false,
        noUnselect: true
    };

    $scope.selectedProductIndex = null;

    $scope.gridOptionsProducts.onRegisterApi = function (gridApi) {
        $scope.gridOptionsProductsApi = gridApi;

        gridApi.selection.on.rowSelectionChanged($scope, function (row) {
            $scope.selectedProductIndex = $scope.gridOptionsProducts.data.indexOf(row.entity);
        });
    };

    $scope.gridOptionsProducts.columnDefs = [
        { name: 'Id', displayName: 'Id', visible: false },
        { name: 'ProductId', displayName: 'ProductId', visible: false },
        { name: 'NameRu', displayName: 'Наименование' },
        { name: 'ProducerNameRu', displayName: 'Производитель' },
        { name: 'CountryNameRu', displayName: 'Страна-производитель' },
        { name: 'ButtonComments', displayName: '', cellTemplate: '<span class="input-group-addon"><a valval="{{row.entity.Id}}" class="obkproductdialog" href="#"><i class="glyphicon glyphicon-info-sign"></i></a></span>' }
    ];

    $scope.addedProducts = [];

    $scope.gridOptionsProducts.data = $scope.addedProducts;

    // Grid Products End

    $scope.showAddEditDrugBlock = false;
    $scope.showSearchDrugInReestr = false;
    $scope.object.drugRegType = 1;
    $scope.object.drugEndDateExpired = false;
    $scope.searchResults = null;

    // Grid Factory begin
    $scope.gridOptionsFactories = {
        enableRowSelection: true,
        enableRowHeaderSelection: false,
        multiSelect: false,
        noUnselect: true
    };

    $scope.gridOptionsFactories.columnDefs = [
        { name: 'Id', displayName: 'Id', visible: false },
        { name: 'Name', displayName: 'Наименование цеха' },
        { name: 'Location', displayName: 'Месторасположение цеха' }
    ]

    $scope.selectedFactoryIndex = null;

    $scope.gridOptionsFactories.onRegisterApi = function (gridApi) {
        gridApi.selection.on.rowSelectionChanged($scope, function (row) {
            $scope.selectedFactoryIndex = $scope.gridOptionsFactories.data.indexOf(row.entity);
        });
    };

    $scope.factories = [];

    $scope.gridOptionsFactories.data = $scope.factories;

    // Grid Factory end

    // Grid Calculator Additional begin
    $scope.addedServicesAdditional = [];

    $scope.gridOptionsCalculatorAdditional = {
        enableRowSelection: true,
        enableRowHeaderSelection: false,
        multiSelect: false,
        noUnselect: true
    };

    $scope.gridOptionsCalculatorAdditional.onRegisterApi = function (gridApi) {
        $scope.gridOptionsCalculatorAdditionalApi = gridApi;

        $interval(function () {
            $scope.gridOptionsCalculatorAdditionalApi.core.handleWindowResize();
        }, 500, 10);
    };

    $scope.gridOptionsCalculatorAdditional.columnDefs = [
        { name: 'Id', displayName: 'ИД', width: "*", visible: false },
        { name: 'ServiceName', displayName: 'Тип услуги', width: "*", cellTemplate: '<div class="ui-grid-cell-contents" >{{grid.getCellValue(row, col)}}</div>' },
		{ name: 'ServiceId', displayName: 'Тип услуги - ИД', width: "*", visible: false },
        { name: "ProductId", displayName: "Продукция - ИД", width: "*", visible: false },
        { name: "ProductName", displayName: "Продукция", width: "*", visible: false },
        { name: 'UnitOfMeasurementName', displayName: 'Единица измерения', width: "*", visible: false },
		{ name: 'UnitOfMeasurementId', displayName: 'Единица измерения - ИД', width: "*", visible: false },
        { name: 'PriceWithoutTax', displayName: 'Цена в тенге, без НДС', width: "*", visible: false },
        { name: 'Count', displayName: 'Количество услуг (работ)', width: "*" },
        { name: 'FinalCostWithoutTax', displayName: 'Итоговая стоимость услуги, в тенге без НДС', width: "*" },
        { name: 'FinalCostWithTax', displayName: 'Итоговая стоимость услуги, в тенге с НДС', width: "*" }
    ];

    $scope.gridOptionsCalculatorAdditional.data = $scope.addedServicesAdditional;

    // Grid Calculator Additional end

    loadExpertOrganizations($scope, $http);
    loadContractSigners($scope, $http);
    loadDictionary($scope, 'Currency', $http);
    loadObkRefTypes($scope, $http);
    loadObkOrganizations($scope, $http);
    loadDictionary($scope, 'OpfType', $http);
    loadDictionary($scope, 'Country', $http);
    loadDictionaryOBKContractDocumentType($scope, $http);
    loadDictionaryMeasure($scope, $http);

    $scope.BoolDic = [{
        Id: true,
        Name: "Да"
    }, {
        Id: false,
        Name: "Нет"
    }];

    $scope.loadNamesNonResidents = function () {
        $scope.NamesNonResidents = [];

        $http({
            method: 'GET',
            url: '/OBKContract/GetNamesNonResidents',
            params: {
                countryId: $scope.object.Country
            }
        }).then(function (resp) {
            if (resp.data) {
                $scope.NamesNonResidents = resp.data;
            }
        });
    }

    $scope.loadNamesNonResidents();

    $scope.nonResidentCountryChange = function () {
        $scope.loadNamesNonResidents();
        $scope.object.NameNonResident = null;
        $scope.declarantNotFound = false;
        $scope.showContactInformation = false;

        $scope.clearDeclarantForm();
        $scope.removeDeclarantId();

        $scope.clearContactForm();
        $scope.clearContactData();
    }

    $scope.searchDrug = function () {
        var drugRegType = $scope.object.drugRegType;
        var drugNumber = $scope.object.drugNumber;
        var drugTradeName = $scope.object.drugTradeName;
        var drugEndDateExpired = $scope.object.drugEndDateExpired;

        $scope.searchViewMode = "drugform";
        $scope.mtParts.length = 0;
        $scope.drugForms.length = 0;

        $scope.searchDrugWorking = true;

        $http({
            method: 'GET',
            url: '/OBKContract/SearchDrug',
            params: {
                regType: drugRegType,
                drugNumber: drugNumber,
                drugTradeName: drugTradeName,
                drugEndDateExpired: drugEndDateExpired
            }
        }).then(function (resp) {
            if (resp.data) {
                $scope.formatArray(resp.data);

                $scope.searchResults = resp.data;
                $scope.gridOptions.data = resp.data;
            }
            else {
                $scope.searchResults = null;
                $scope.gridOptions.data = null;
            }
            $scope.searchDrugWorking = false;
        }, function (response) {
            $scope.searchDrugWorking = false;
            alert(JSON.stringify(response));
        });

        $scope.clearDrugFormComponents();
    };

    $scope.clearSearchDrugArea = function () {
        $scope.object.drugNumber = null;
        $scope.object.drugRegType = 1;
        $scope.object.drugEndDateExpired = false;
        $scope.object.drugTradeName = null;
    }

    $scope.formatArray = function (arr) {
        if (arr && arr.length) {
            for (var i = 0; i < arr.length; i++) {
                if (arr[i].RegDate) {
                    arr[i].RegDate = convertDateToStringDDMMYYYY(arr[i].RegDate);
                }
                if (arr[i].ExpireDate) {
                    arr[i].ExpireDate = convertDateToStringDDMMYYYY(arr[i].ExpireDate);
                }
            }
        }
    }

    $scope.addDrug = function addDrug() {
        $scope.showAddEditDrugBlock = true;
        $scope.showSearchDrugInReestr = true;
        $scope.mode = 1;
    };

    $scope.editDrug = function editDrug() {
        if ($scope.selectedProductIndex != null) {
            $scope.deletedServicesId = [];
            $scope.mode = 2;

            $scope.showAddEditDrugBlock = true;
            $scope.showSearchDrugInReestr = false;

            var selectedObj = $scope.addedProducts[$scope.selectedProductIndex];

            $scope.product.Id = selectedObj.Id;
            $scope.product.ProductId = selectedObj.ProductId;
            $scope.product.RegTypeId = selectedObj.RegTypeId;
            $scope.product.DegreeRiskId = selectedObj.DegreeRiskId;
            $scope.product.NameRu = selectedObj.NameRu;
            $scope.product.NameKz = selectedObj.NameKz;
            $scope.product.ProducerNameRu = selectedObj.ProducerNameRu;
            $scope.product.ProducerNameKz = selectedObj.ProducerNameKz;
            $scope.product.CountryNameRu = selectedObj.CountryNameRu;
            $scope.product.CountryNameKz = selectedObj.CountryNameKz;
            $scope.product.Price = selectedObj.Price;
            $scope.product.Currency = selectedObj.Currency;
            $scope.product.RegisterId = selectedObj.RegisterId;
            $scope.product.RegNumber = selectedObj.RegNumber;
            $scope.product.RegNumberKz = selectedObj.RegNumberKz;
            $scope.product.RegDate = selectedObj.RegDate;
            $scope.product.ExpirationDate = selectedObj.ExpirationDate;
            $scope.product.NdName = selectedObj.NdName;
            $scope.product.NdNumber = selectedObj.NdNumber;

            $scope.product.DrugFormId = selectedObj.DrugFormId;
            $scope.product.DrugFormRegisterId = selectedObj.DrugFormRegisterId;
            $scope.product.DrugFormBoxCount = selectedObj.DrugFormBoxCount;
            $scope.product.DrugFormFullName = selectedObj.DrugFormFullName;
            $scope.product.DrugFormFullNameKz = selectedObj.DrugFormFullNameKz;

            $scope.product.ServiceName = selectedObj.ServiceName;

            $scope.productSeries.push.apply($scope.productSeries, selectedObj.Series);
            $scope.selectedMtParts.push.apply($scope.selectedMtParts, selectedObj.MtParts);

            $scope.loadProductServiceNames();
        }
        else {
            alert("Выберите продукцию для изменения");
        }
    }

    $scope.deleteDrug = function deleteDrug() {
        if ($scope.selectedProductIndex != null) {
            var deleteConfirmed = confirm("Вы подтверждаете удаление продукции со списка?");
            if (deleteConfirmed) {
                var selectedObj = $scope.addedProducts[$scope.selectedProductIndex];
                var id = selectedObj.Id;
                for (var i = $scope.addedServices.length - 1; i >= 0; i--) {
                    if ($scope.addedServices[i].ProductId == selectedObj.ProductId) {
                        $scope.addedServices.splice(i, 1);
                    }
                }
                

                $scope.deleteProductInformation(id);

                $scope.addedProducts.splice($scope.selectedProductIndex, 1);
                $scope.selectedProductIndex = null;
            }
        }
        else {
            alert("Выберите продукцию для удаления");
        }
    }

    $scope.displayDrug = function displayDrug() {
        if ($scope.selectedProductIndex != null) {
            $scope.mode = 3;
            $scope.showAddEditDrugBlock = true;

            var selectedObj = $scope.addedProducts[$scope.selectedProductIndex];

            $scope.product.Id = selectedObj.Id;
            $scope.product.ProductId = selectedObj.ProductId;
            $scope.product.RegTypeId = selectedObj.RegTypeId;
            $scope.product.DegreeRiskId = selectedObj.DegreeRiskId;
            $scope.product.NameRu = selectedObj.NameRu;
            $scope.product.NameKz = selectedObj.NameKz;
            $scope.product.ProducerNameRu = selectedObj.ProducerNameRu;
            $scope.product.ProducerNameKz = selectedObj.ProducerNameKz;
            $scope.product.CountryNameRu = selectedObj.CountryNameRu;
            $scope.product.CountryNameKz = selectedObj.CountryNameKz;
            $scope.product.Price = selectedObj.Price;
            $scope.product.Currency = selectedObj.Currency;
            $scope.product.RegisterId = selectedObj.RegisterId;
            $scope.product.RegNumber = selectedObj.RegNumber;
            $scope.product.RegNumberKz = selectedObj.RegNumberKz;
            $scope.product.RegDate = selectedObj.RegDate;
            $scope.product.ExpirationDate = selectedObj.ExpirationDate;
            $scope.product.NdName = selectedObj.NdName;
            $scope.product.NdNumber = selectedObj.NdNumber;
            $scope.product.DrugFormId = selectedObj.DrugFormId;
            $scope.product.DrugFormRegisterId = selectedObj.DrugFormRegisterId;
            $scope.product.DrugFormBoxCount = selectedObj.DrugFormBoxCount;
            $scope.product.DrugFormFullName = selectedObj.DrugFormFullName;
            $scope.product.DrugFormFullNameKz = selectedObj.DrugFormFullNameKz;

            $scope.productSeries.push.apply($scope.productSeries, selectedObj.Series);
            $scope.selectedMtParts.push.apply($scope.selectedMtParts, selectedObj.MtParts);

            $scope.loadProductServiceNames();
        }
        else {
            alert("Выберите продукцию для просмотра");
        }
    }

    $scope.closeDisplayDrug = function () {
        $scope.showAddEditDrugBlock = false;
        $scope.showSearchDrugInReestr = false;
        $scope.clearSearchAndProductFields();
        $scope.mode = 0;
    }

    $scope.addSeries = function addSeries() {
        var createDate = convertDateToString($scope.object.seriesCreateDate);
        var expireDate = convertDateToString($scope.object.seriesExpireDate);

        if (!$scope.object.seriesValue ||
            !createDate ||
            !expireDate ||
            !$scope.object.partValue ||
            !$scope.object.seriesUnit
            ) {
            alert("Заполните поля серии");
        }
        else {
            var obj = { Id: null, Series: $scope.object.seriesValue, CreateDate: createDate, ExpireDate: expireDate, Part: $scope.object.partValue, UnitId: $scope.object.seriesUnit.Id, UnitName: $scope.object.seriesUnit.Name };
            $scope.productSeries.push(obj);
            $scope.object.seriesValue = null;
            $scope.object.partValue = null;
            $scope.object.seriesUnit = null;
        }
    };

    $scope.deleteSeries = function deleteSeries() {
        if ($scope.selectedSeriesIndex != null) {
            $scope.productSeries.splice($scope.selectedSeriesIndex, 1);
            $scope.selectedSeriesIndex = null;
        }
        else {
            alert("Выберите серию для удаления");
        }
    }

    $scope.saveProduct = function saveProduct() {
        if ($scope.mode == 1) {
            var id = $scope.product.ProductId;
            if (id) {
                if (!$scope.existInArray($scope.addedProducts, id)) {
                    if ($scope.drugForms.length == 0 || ($scope.drugForms.length > 0 && $scope.product.DrugFormId)) {
                        if ($scope.productSeries.length > 0) {
                            if ($scope.product.ServiceName) {
                                var product = {
                                    Id: null,
                                    ProductId: $scope.product.ProductId,
                                    RegTypeId: $scope.product.RegTypeId,
                                    DegreeRiskId: $scope.product.DegreeRiskId,
                                    NameRu: $scope.product.NameRu,
                                    NameKz: $scope.product.NameKz,
                                    ProducerNameRu: $scope.product.ProducerNameRu,
                                    ProducerNameKz: $scope.product.ProducerNameKz,
                                    CountryNameRu: $scope.product.CountryNameRu,
                                    CountryNameKz: $scope.product.CountryNameKz,
                                    Price: $scope.product.Price,
                                    Currency: $scope.product.Currency,
                                    RegisterId: $scope.product.RegisterId,
                                    RegNumber: $scope.product.RegNumber,
                                    RegNumberKz: $scope.product.RegNumberKz,
                                    RegDate: $scope.product.RegDate,
                                    ExpirationDate: $scope.product.ExpirationDate,
                                    NdName: $scope.product.NdName,
                                    NdNumber: $scope.product.NdNumber,
                                    DrugFormBoxCount: $scope.product.DrugFormBoxCount,
                                    DrugFormFullName: $scope.product.DrugFormFullName,
                                    DrugFormFullNameKz: $scope.product.DrugFormFullNameKz,
                                    Series: [],
                                    MtParts: [],
                                    ServiceName: $scope.product.ServiceName
                                }
                                product.Series = $scope.productSeries.slice();
                                product.MtParts = $scope.selectedMtParts.slice();

                                $scope.addedProducts.push(product);

                                $scope.saveProductInformation(product);

                                $scope.showAddEditDrugBlock = false;
                                $scope.showSearchDrugInReestr = false;
                                $scope.mode = 0;

                                alert("Информация о продукции добавлена");
                            }
                            else {
                                alert("Введите информацию о типе услуги");
                            }
                        }
                        else {
                            alert("Введите информацию о серии продукта");
                        }
                    }
                    else {
                        alert("Выберите форму выпуска продукции");
                    }
                }
                else {
                    alert("Выбранная продукция уже имеется в таблице");
                }
            }
            else {
                alert("Выберите продукт");
            }
        }
        if ($scope.mode == 2) {
            if ($scope.productSeries.length > 0) {
                if ($scope.product.ServiceName) {
                    var selectedObj = $scope.addedProducts[$scope.selectedProductIndex];
                    selectedObj.Id = $scope.product.Id;
                    selectedObj.ProductId = $scope.product.ProductId;
                    selectedObj.RegTypeId = $scope.product.RegTypeId;
                    selectedObj.DegreeRiskId = $scope.product.DegreeRiskId;
                    selectedObj.NameRu = $scope.product.NameRu;
                    selectedObj.NameKz = $scope.product.NameKz;
                    selectedObj.ProducerNameRu = $scope.product.ProducerNameRu;
                    selectedObj.ProducerNameKz = $scope.product.ProducerNameKz;
                    selectedObj.CountryNameRu = $scope.product.CountryNameRu;
                    selectedObj.CountryNameKz = $scope.product.CountryNameKz;
                    selectedObj.Price = $scope.product.Price;
                    selectedObj.Currency = $scope.product.Currency;
                    selectedObj.RegisterId = $scope.product.RegisterId;
                    selectedObj.RegNumber = $scope.product.RegNumber;
                    selectedObj.RegNumberKz = $scope.product.RegNumberKz;
                    selectedObj.RegDate = $scope.product.RegDate;
                    selectedObj.ExpirationDate = $scope.product.ExpirationDate;
                    selectedObj.NdName = $scope.product.NdName;
                    selectedObj.NdNumber = $scope.product.NdNumber;
                    selectedObj.ServiceName = $scope.product.ServiceName;
                    selectedObj.Series.length = 0;
                    selectedObj.Series.push.apply(selectedObj.Series, $scope.productSeries);

                    $scope.saveProductInformation(selectedObj);

                    $scope.showAddEditDrugBlock = false;
                    $scope.showSearchDrugInReestr = false;
                    $scope.mode = 0;
                    alert("Информация о продукции обновлена");
                }
                else {
                    alert("Введите информацию о типе услуги");
                }
            }
            else {
                alert("Введите информацию о серии продукта");
            }
        }
    }

    $scope.saveProductInformation = function (product) {
        var projectId = $scope.object.Id;
        if (projectId) {
            $http({
                url: '/OBKContract/SaveProduct',
                method: 'POST',
                data: { contractId: projectId, product: product }
            }).success(function (response) {
                if (response.ProductId && response.Id) {
                    $scope.updateIdOfProduct(response.ProductId, response.Id);
                }

                $scope.loadContractPrices();

                $scope.clearSearchAndProductFields();
            });
        }
    }

    $scope.deleteProductInformation = function (id) {
        var projectId = $scope.object.Id;
        if (projectId) {
            $http({
                url: '/OBKContract/DeleteProduct',
                method: 'POST',
                data: { contractId: projectId, productId: id }
            }).success(function (response) {
                $scope.loadContractPrices();
            });
        }
    }

    $scope.updateIdOfProduct = function (productId, id) {
        for (var i = 0; i < $scope.addedProducts.length; i++) {
            if ($scope.addedProducts[i].ProductId == productId && $scope.addedProducts[i].Id == null) {
                $scope.addedProducts[i].Id = id;
            }
        }
    }

    $scope.cancelSaveProduct = function cancelSaveProduct() {
        $scope.showAddEditDrugBlock = false;
        $scope.showSearchDrugInReestr = false;
        $scope.clearSearchAndProductFields();
    }

    $scope.existInArray = function existInArray(arr, id) {
        if (arr && id) {
            for (var i = 0; i < arr.length; i++) {
                if (arr[i].Id == id) {
                    return true;
                }
            }
        }
        return false;
    }

    $scope.clearSearchAndProductFields = function clearSearchAndProductFields() {
        $scope.object.drugNumber = null;
        $scope.object.drugRegType = 1;
        $scope.object.drugEndDateExpired = false;
        $scope.object.drugTradeName = null;

        $scope.gridOptions.data.length = 0;
        //
        $scope.mtParts.length = 0;
        $scope.drugForms.length = 0;

        $scope.product.ProductId = null;
        $scope.product.NameRu = null;
        $scope.product.NameKz = null;
        $scope.product.ProducerNameRu = null;
        $scope.product.ProducerNameKz = null;
        $scope.product.CountryNameRu = null;
        $scope.product.CountryNameKz = null;
        $scope.product.TnvedCode = null;
        $scope.product.KpvedCode = null;
        $scope.product.Price = null;
        $scope.product.Currency = null;
        $scope.product.RegisterId = null;
        $scope.product.RegNumber = null;
        $scope.product.RegNumberKz = null;
        $scope.product.RegDate = null;
        $scope.product.ExpirationDate = null;
        $scope.product.NdName = null;
        $scope.product.NdNumber = null;

        $scope.clearDrugFormComponents();

        $scope.product.ServiceName = null;

        $scope.object.seriesValue = null;
        $scope.object.partValue = null;
        $scope.object.seriesUnit = null;

        // clear series table
        $scope.productSeries.length = 0;

        // clear mt parts
        $scope.selectedMtParts.length = 0;
    }

    $scope.resetMode = function resetMode() {
        $scope.mode = 0; // 0 - unknown, 1 - add, 2 - edit, 3 - view
    }

    $scope.refTypeChange = function (save) {
        $scope.loadServiceNames();
        $scope.loadProductServiceNames();
        if (save) {
            $scope.editProject();
        }
    }

    $scope.loadServiceNames = function loadServiceNames() {
        $scope.serviceNames = [];
        if ($scope.object.Type) {
            $http({
                method: "GET",
                url: "/OBKDictionaries/GetServiceNamesServiceTypeDocument",
                data: "JSON",
                params: {
                    type: $scope.object.Type
                }
            }).success(function (result) {
                $scope.serviceNames = result;
            });
        }
    }

    $scope.saveBtnClick = function () {
        var formValid = $scope.contractCreateForm.$valid;
        var filesValid = $scope.checkFileValidation();
        $scope.editProject();
    }

    $scope.editProject = function () {
        var generatedGuid = $("#generatedGuid").val();
        $http({
            url: '/OBKContract/ContractSave',
            method: 'POST',
            data: { Guid: generatedGuid, contractViewModel: $scope.object }
        }).success(function (response) {
            $scope.object.Id = response.Id;
        });
    }


    $scope.companyChange = function (loadOrgData) {

    }

    $scope.loadOrganizationData = function (id) {
        if (id) {
            $http({
                method: 'GET',
                url: '/OBKContract/GetOrganizationData',
                params: {
                    guid: id
                }
            }).then(function (resp) {
                if (resp.data) {
                    $scope.declarant.Id = resp.data.Id;
                    $scope.declarant.OrganizationFormId = resp.data.OrganizationFormId;
                    $scope.declarant.NameKz = resp.data.NameKz;
                    $scope.declarant.NameRu = resp.data.NameRu;
                    $scope.declarant.NameEn = resp.data.NameEn;
                    $scope.declarant.CountryId = resp.data.CountryId;

                    $scope.object.AddressLegalRu = resp.data.AddressLegalRu;
                    $scope.object.AddressLegalKz = resp.data.AddressLegalKz
                    $scope.object.AddressFact = resp.data.AddressFact;
                    $scope.object.Phone = resp.data.Phone;
                    $scope.object.Email = resp.data.Email;
                    $scope.object.BossLastName = resp.data.BossLastName;
                    $scope.object.BossFirstName = resp.data.BossFirstName;
                    $scope.object.BossMiddleName = resp.data.BossMiddleName;
                    $scope.object.BossPosition = resp.data.BossPosition;
                    $scope.object.BossPositionKz = resp.data.BossPositionKz;
                    $scope.object.BossDocType = resp.data.BossDocType;
                    $scope.object.IsHasBossDocNumber = resp.data.IsHasBossDocNumber;
                    $scope.object.BossDocNumber = resp.data.BossDocNumber;
                    $scope.object.BossDocCreatedDate = getDate(resp.data.BossDocCreatedDate);
                    $scope.object.BossDocEndDate = getDate(resp.data.BossDocEndDate);
                    $scope.object.BossDocUnlimited = resp.data.BossDocUnlimited;
                    $scope.object.SignerIsBoss = resp.data.SignerIsBoss;
                    $scope.object.SignLastName = resp.data.SignLastName;
                    $scope.object.SignFirstName = resp.data.SignFirstName;
                    $scope.object.SignMiddleName = resp.data.SignMiddleName;
                    $scope.object.SignPosition = resp.data.SignPosition;
                    $scope.object.SignPositionKz = resp.data.SignPositionKz;
                    $scope.object.SignDocType = resp.data.SignDocType;
                    $scope.object.IsHasSignDocNumber = resp.data.IsHasSignDocNumber;
                    $scope.object.SignDocNumber = resp.data.SignDocNumber;
                    $scope.object.SignDocCreatedDate = getDate(resp.data.SignDocCreatedDate);
                    $scope.object.SignDocEndDate = getDate(resp.data.SignDocEndDate);
                    $scope.object.SignDocUnlimited = resp.data.SignDocUnlimited;
                    $scope.object.BankIik = resp.data.BankIik;
                    $scope.object.BankBik = resp.data.BankBik;
                    $scope.object.CurrencyId = resp.data.CurrencyId;
                    $scope.object.BankNameRu = resp.data.BankNameRu;
                    $scope.object.BankNameKz = resp.data.BankNameKz;

                    $scope.saveDeclarantId(resp.data.Id);
                    $scope.editProject();

                    $scope.showContactInformation = true;
                }
            }, function (response) {
                alert(JSON.stringify(response));
            });
        }
        else {
            $scope.declarant.Id = null;
            $scope.declarant.OrganizationFormId = null;
            $scope.declarant.NameKz = null;
            $scope.declarant.NameRu = null;
            $scope.declarant.NameEn = null;
            $scope.declarant.CountryId = null;

            $scope.object.AddressLegalRu = null;
            $scope.object.AddressLegalKz = null
            $scope.object.AddressFact = null;
            $scope.object.Phone = null;
            $scope.object.Email = null;
            $scope.object.BossLastName = null;
            $scope.object.BossFirstName = null;
            $scope.object.BossMiddleName = null;
            $scope.object.BossPosition = null;
            $scope.object.BossPositionKz = null;
            $scope.object.BossDocType = null;
            $scope.object.IsHasBossDocNumber = null;
            $scope.object.BossDocNumber = null;
            $scope.object.BossDocCreatedDate = getDate(null);
            $scope.object.BossDocEndDate = getDate(null);
            $scope.object.BossDocUnlimited = false;
            $scope.object.SignerIsBoss = false;
            $scope.object.SignLastName = null;
            $scope.object.SignFirstName = null;
            $scope.object.SignMiddleName = null;
            $scope.object.SignPosition = null;
            $scope.object.SignPositionKz = null;
            $scope.object.SignDocType = null;
            $scope.object.IsHasSignDocNumber = null;
            $scope.object.SignDocNumber = null;
            $scope.object.SignDocCreatedDate = getDate(null);
            $scope.object.SignDocEndDate = getDate(null);
            $scope.object.SignDocUnlimited = false;
            $scope.object.BankIik = null;
            $scope.object.BankBik = null;
            $scope.object.CurrencyId = null;
            $scope.object.BankNameRu = null;
            $scope.object.BankNameKz = null;
        }
    }

    $scope.clearContactForm = function () {
        $scope.object.AddressLegalRu = null;
        $scope.object.AddressLegalKz = null
        $scope.object.AddressFact = null;
        $scope.object.Phone = null;
        $scope.object.Email = null;
        $scope.object.BossLastName = null;
        $scope.object.BossFirstName = null;
        $scope.object.BossMiddleName = null;
        $scope.object.BossPosition = null;
        $scope.object.BossPositionKz = null;
        $scope.object.BossDocType = null;
        $scope.object.IsHasBossDocNumber = null;
        $scope.object.BossDocNumber = null;
        $scope.object.BossDocCreatedDate = getDate(null);
        $scope.object.BossDocEndDate = getDate(null);
        $scope.object.BossDocUnlimited = false;
        $scope.object.SignerIsBoss = false;
        $scope.object.SignLastName = null;
        $scope.object.SignFirstName = null;
        $scope.object.SignMiddleName = null;
        $scope.object.SignPosition = null;
        $scope.object.SignPositionKz = null;
        $scope.object.SignDocType = null;
        $scope.object.IsHasSignDocNumber = null;
        $scope.object.SignDocNumber = null;
        $scope.object.SignDocCreatedDate = getDate(null);
        $scope.object.SignDocEndDate = getDate(null);
        $scope.object.SignDocUnlimited = false;
        $scope.object.BankIik = null;
        $scope.object.BankBik = null;
        $scope.object.CurrencyId = null;
        $scope.object.BankNameRu = null;
        $scope.object.BankNameKz = null;
    }

    $scope.clearContactData = function () {
        if ($scope.object.Id) {
            $http({
                url: '/OBKContract/ClearContactData',
                method: 'POST',
                data: { contractId: $scope.object.Id }
            }).success(function (response) {
            });
        }
    }

    $scope.showHideBin = function () {
        if ($scope.declarant.IsResident == true) {
            $scope.showBin = true;
            $scope.cancelFindDeclarant();
        }
        else {
            $scope.showBin = false;
            $scope.object.Country = null;
            $scope.object.NameNonResident = null;
            $scope.clearDeclarantForm();
            $scope.removeDeclarantId();
        }
        $scope.declarant.Bin = null;
        $scope.showContactInformation = false;
        $scope.declarantNotFound = false;
        $scope.clearContactForm();
        $scope.clearContactData();
    }

    $scope.hideBin = function () {
        $scope.showBin = false;
    }

    $scope.residentChange = function () {
        $scope.showHideBin();
    }

    $scope.signerIsLeaderCheckBoxChanged = function () {
        if ($scope.object.SignerIsBoss == true) {
            $scope.object.SignLastName = $scope.object.BossLastName;
            $scope.object.SignFirstName = $scope.object.BossFirstName;
            $scope.object.SignMiddleName = $scope.object.BossMiddleName;
            $scope.object.SignPosition = $scope.object.BossPosition;
            $scope.object.SignPositionKz = $scope.object.BossPositionKz;
            $scope.object.SignDocType = $scope.object.BossDocType;
            $scope.object.SignDocUnlimited = $scope.object.BossDocUnlimited;
            $scope.object.IsHasSignDocNumber = $scope.object.IsHasBossDocNumber;
            $scope.object.SignDocNumber = $scope.object.BossDocNumber;
            $scope.object.SignDocCreatedDate = $scope.object.BossDocCreatedDate;
            $scope.object.SignDocEndDate = $scope.object.BossDocEndDate;
        }
        $scope.editProject();
    }

    initProductServiceModule($scope, $http, $interval);

    initCalculator($scope, $interval, $http);

    $scope.loadContractPrices = function () {
        var projectId = $scope.object.Id;
        if (!projectId) {
            projectId = $("#projectId").val();
        }

        if (projectId) {
            $http({
                method: 'GET',
                url: '/OBKContract/GetContractPrices',
                params: {
                    contractId: projectId
                }
            }).then(function (resp) {
                if (resp.data) {
                    $scope.addedServices.length = 0;
                    $scope.addedServices.push.apply($scope.addedServices, resp.data);

                    $interval(function () {
                        $scope.gridOptionsCalculatorApi.core.handleWindowResize();
                    }, 500, 10);
                }
            });

            $http({
                method: 'GET',
                url: '/OBKContract/GetContractPricesAdditional',
                params: {
                    contractId: projectId
                }
            }).then(function (resp) {
                if (resp.data) {
                    $scope.addedServicesAdditional.length = 0;
                    $scope.addedServicesAdditional.push.apply($scope.addedServicesAdditional, resp.data);

                    $interval(function () {
                        $scope.gridOptionsCalculatorAdditionalApi.core.handleWindowResize();
                    }, 500, 10);
                }
            });

            $scope.calcTotalCostCalculator();
        }
    }

    $scope.loadFactories = function () {
        var projectId = $scope.object.Id;
        if (!projectId) {
            projectId = $("#projectId").val();
        }

        if (projectId) {
            $http({
                method: 'GET',
                url: '/OBKContract/GetFactories',
                params: {
                    contractId: projectId
                }
            }).then(function (resp) {
                if (resp.data) {
                    $scope.factories.push.apply($scope.factories, resp.data);
                }
            });
        }
    }

    $scope.addFactory = function () {
        if ($scope.factory.Name && $scope.factory.Location) {
            var factory = {
                Id: null,
                Name: $scope.factory.Name,
                Location: $scope.factory.Location
            }
            $scope.postFactory(factory);
        }
        else {
            alert("Введите наименование и месторасположение цеха");
        }
    }

    $scope.postFactory = function (factory) {
        var projectId = $scope.object.Id;
        if (projectId) {
            $http({
                url: '/OBKContract/AddFactory',
                method: 'POST',
                data: { contractId: projectId, factory: factory }
            }).success(function (response) {
                if (response) {
                    factory.Id = response;
                    $scope.factories.push(factory);
                    $scope.clearFactoryFields();
                    $scope.loadContractPrices();
                }
            });
        }
    }

    $scope.deleteFactory = function () {
        if ($scope.selectedFactoryIndex != null) {
            var deleteConfirmed = confirm("Вы подтверждаете удаление цеха со списка?");
            if (deleteConfirmed) {
                var factory = $scope.factories[$scope.selectedFactoryIndex];
                $scope.postDeleteFactory(factory);
            }
        }
        else {
            alert("Выберите цех");
        }
    }

    $scope.clearFactoryFields = function () {
        $scope.factory.Name = null;
        $scope.factory.Location = null;
    }

    $scope.postDeleteFactory = function (factory) {
        var projectId = $scope.object.Id;
        if (projectId) {
            $http({
                url: '/OBKContract/DeleteFactory',
                method: 'POST',
                data: { contractId: projectId, factory: factory }
            }).success(function (response) {
                if (response) {
                    $scope.factories.splice($scope.selectedFactoryIndex, 1);
                    $scope.selectedFactoryIndex = null;
                    $scope.loadContractPrices();
                }
            });
        }
    }

    $scope.loadContract = function () {
        var projectId = $("#projectId").val();

        $http({
            method: 'GET',
            url: '/OBKContract/LoadContract',
            params: {
                id: projectId
            }
        }).then(function (resp) {
            if (resp.data) {
                $scope.object.Id = resp.data.Id;
                $scope.object.Type = resp.data.Type;
                $scope.object.ExpertOrganization = resp.data.ExpertOrganization;
                $scope.object.Signer = resp.data.Signer;
                $scope.object.Status = resp.data.Status;
                $scope.object.Number = resp.data.Number;
                $scope.changeViewMode();


                $scope.refTypeChange(false);

                $http({
                    method: 'GET',
                    url: '/OBKContract/GetDeclarant',
                    params: {
                        contractId: projectId
                    }
                }).then(function (resp) {
                    if (resp.data) {
                        $scope.declarant.Id = resp.data.Id;
                        $scope.declarant.IsResident = resp.data.IsResident;
                        $scope.declarant.OrganizationFormId = resp.data.OrganizationFormId;
                        $scope.declarant.NameKz = resp.data.NameKz;
                        $scope.declarant.NameRu = resp.data.NameRu;
                        $scope.declarant.NameEn = resp.data.NameEn;
                        $scope.declarant.CountryId = resp.data.CountryId;
                        $scope.declarant.Bin = resp.data.Bin;

                        if ($scope.declarant.IsResident) {
                            $scope.showBin = true;
                            if (resp.data.IsConfirmed) {
                                $scope.iinSearchActive = true;
                                $scope.declarantNotFound = false;
                                $scope.enableCompanyData = false;
                                $scope.showContactInformation = true;
                            }
                            else {
                                $scope.iinSearchActive = true;
                                $scope.declarantNotFound = true;
                                $scope.showContactInformation = true;
                                $scope.enableCompanyData = true;
                                $scope.addDeclarantDisabled = true;
                            }
                        }
                        else {
                            $scope.showBin = false;
                            if (resp.data.IsConfirmed) {
                                $scope.object.Country = $scope.declarant.CountryId;
                                $scope.loadNamesNonResidents();
                                $scope.object.NameNonResident = $scope.declarant.Id;

                                $scope.addDeclarantDisabled = true;
                                $scope.showContactInformation = true;
                            }
                            else {
                                $scope.object.Country = $scope.declarant.CountryId;
                                $scope.loadNamesNonResidents();
                                $scope.object.NameNonResident = "00000000-0000-0000-0000-000000000000";
                                $scope.showContactInformation = true;
                                $scope.enableCompanyData = true;
                                $scope.addDeclarantDisabled = true;
                            }
                        }
                    }
                });

                $scope.object.AddressLegalRu = resp.data.AddressLegalRu;
                $scope.object.AddressLegalKz = resp.data.AddressLegalKz;
                $scope.object.AddressFact = resp.data.AddressFact;
                $scope.object.Phone = resp.data.Phone;
                $scope.object.Email = resp.data.Email;
                $scope.object.BossLastName = resp.data.BossLastName;
                $scope.object.BossFirstName = resp.data.BossFirstName;
                $scope.object.BossMiddleName = resp.data.BossMiddleName;
                $scope.object.BossPosition = resp.data.BossPosition;
                $scope.object.BossPositionKz = resp.data.BossPositionKz;
                $scope.object.BossDocType = resp.data.BossDocType;
                $scope.object.IsHasBossDocNumber = resp.data.IsHasBossDocNumber;
                $scope.object.BossDocNumber = resp.data.BossDocNumber;
                $scope.object.BossDocCreatedDate = getDate(resp.data.BossDocCreatedDate);
                $scope.object.BossDocEndDate = getDate(resp.data.BossDocEndDate);
                $scope.object.BossDocUnlimited = resp.data.BossDocUnlimited;
                $scope.object.SignerIsBoss = resp.data.SignerIsBoss;
                $scope.object.SignLastName = resp.data.SignLastName;
                $scope.object.SignFirstName = resp.data.SignFirstName;
                $scope.object.SignMiddleName = resp.data.SignMiddleName;
                $scope.object.SignPosition = resp.data.SignPosition;
                $scope.object.SignPositionKz = resp.data.SignPositionKz;
                $scope.object.SignDocType = resp.data.SignDocType;
                $scope.object.IsHasSignDocNumber = resp.data.IsHasSignDocNumber;
                $scope.object.SignDocNumber = resp.data.SignDocNumber;
                $scope.object.SignDocCreatedDate = getDate(resp.data.SignDocCreatedDate);
                $scope.object.SignDocEndDate = getDate(resp.data.SignDocEndDate);
                $scope.object.SignDocUnlimited = resp.data.SignDocUnlimited;
                $scope.object.BankIik = resp.data.BankIik;
                $scope.object.BankBik = resp.data.BankBik;
                $scope.object.CurrencyId = resp.data.CurrencyId;
                $scope.object.BankNameRu = resp.data.BankNameRu;
                $scope.object.BankNameKz = resp.data.BankNameKz;
            }
        }, function (response) {

        });

        $http({
            method: 'GET',
            url: '/OBKContract/GetProducts',
            params: {
                contractId: projectId
            }
        }).then(function (resp) {
            if (resp.data) {
                for (var i = 0; i < resp.data.length; i++) {
                    resp.data[i].RegDate = getDate(resp.data[i].RegDate);
                    resp.data[i].ExpirationDate = getDate(resp.data[i].ExpirationDate);
                }
                $scope.addedProducts.push.apply($scope.addedProducts, resp.data);
            }
        });

        $scope.loadFactories();

        $scope.loadContractPrices();
    }

    var projectId = $("#projectId").val();
    if (projectId) {
        $scope.loadContract();
    }
    else {

    }


    $scope.checkFileValidation = function () {
        var invalidFiles = 0;

        var containerName = "";
        if ($scope.declarant.IsResident === true) {
            containerName = "#filesResident";
        }
        else {
            containerName = "#filesNonResident";
        }

        $(containerName + ' .file-validation').text("");
        $(containerName + ' .file-validation').each(function () {
            var ct = $(this).attr('countFile');
            var attcode = $(this).attr('attcode');
            var count = parseInt(ct, 10) || 0;
            if (count === 0 && attcode === "1") {
                $(this).text("Необходимо вложить файлы");
                invalidFiles++;
            } else {
                $(this).text("");
            }
        });
        return invalidFiles === 0;
    }

    $scope.findDeclarant = function () {
        if ($scope.declarant.Bin && $scope.declarant.Bin.length == 12) {
            $http({
                method: 'GET',
                url: '/OBKContract/FindDeclarant',
                params: {
                    bin: $scope.declarant.Bin
                }
            }).then(function (resp) {
                $scope.iinSearchActive = true;
                if (resp.data) {
                    $scope.declarant.Id = resp.data.Id;
                    $scope.declarant.OrganizationFormId = resp.data.OrganizationFormId;
                    $scope.declarant.IsResident = resp.data.IsResident;
                    $scope.declarant.NameKz = resp.data.NameKz;
                    $scope.declarant.NameRu = resp.data.NameRu;
                    $scope.declarant.NameEn = resp.data.NameEn;
                    $scope.declarant.CountryId = resp.data.CountryId;

                    $scope.object.AddressLegalRu = resp.data.AddressLegalRu;
                    $scope.object.AddressLegalKz = resp.data.AddressLegalKz
                    $scope.object.AddressFact = resp.data.AddressFact;
                    $scope.object.Phone = resp.data.Phone;
                    $scope.object.Email = resp.data.Email;
                    $scope.object.BossLastName = resp.data.BossLastName;
                    $scope.object.BossFirstName = resp.data.BossFirstName;
                    $scope.object.BossMiddleName = resp.data.BossMiddleName;
                    $scope.object.BossPosition = resp.data.BossPosition;
                    $scope.object.BossPositionKz = resp.data.BossPositionKz;
                    $scope.object.BossDocType = resp.data.BossDocType;
                    $scope.object.IsHasBossDocNumber = resp.data.IsHasBossDocNumber;
                    $scope.object.BossDocNumber = resp.data.BossDocNumber;
                    $scope.object.BossDocCreatedDate = getDate(resp.data.BossDocCreatedDate);
                    $scope.object.BossDocEndDate = getDate(resp.data.BossDocEndDate);
                    $scope.object.BossDocUnlimited = resp.data.BossDocUnlimited;
                    $scope.object.SignerIsBoss = resp.data.SignerIsBoss;
                    $scope.object.SignLastName = resp.data.SignLastName;
                    $scope.object.SignFirstName = resp.data.SignFirstName;
                    $scope.object.SignMiddleName = resp.data.SignMiddleName;
                    $scope.object.SignPosition = resp.data.SignPosition;
                    $scope.object.SignPositionKz = resp.data.SignPositionKz;
                    $scope.object.SignDocType = resp.data.SignDocType;
                    $scope.object.IsHasSignDocNumber = resp.data.IsHasSignDocNumber;
                    $scope.object.SignDocNumber = resp.data.SignDocNumber;
                    $scope.object.SignDocCreatedDate = getDate(resp.data.SignDocCreatedDate);
                    $scope.object.SignDocEndDate = getDate(resp.data.SignDocEndDate);
                    $scope.object.SignDocUnlimited = resp.data.SignDocUnlimited;
                    $scope.object.BankIik = resp.data.BankIik;
                    $scope.object.BankBik = resp.data.BankBik;
                    $scope.object.CurrencyId = resp.data.CurrencyId;
                    $scope.object.BankNameRu = resp.data.BankNameRu;
                    $scope.object.BankNameKz = resp.data.BankNameKz;
                    $scope.saveDeclarantId(resp.data.Id);
                    $scope.editProject();

                    $scope.showContactInformation = true;
                    $scope.declarantNotFound = false;
                    $scope.enableCompanyData = false;
                }
                else {
                    $scope.declarantNotFound = true;
                    $scope.addDeclarantDisabled = false;
                    $scope.enableCompanyData = true;

                    $scope.declarant.Id = null;
                }
            }, function (response) {

            });
        }
    }

    $scope.saveDeclarantId = function (declarantId) {
        if ($scope.object.Id) {
            $http({
                url: '/OBKContract/SaveDeclarantId',
                method: 'POST',
                data: { contractId: $scope.object.Id, declarantId: declarantId }
            }).success(function (response) {
            });
        }
    }

    $scope.clearDeclarantForm = function () {
        $scope.declarant.Id = null;
        $scope.declarant.OrganizationFormId = null;
        $scope.declarant.NameKz = null;
        $scope.declarant.NameRu = null;
        $scope.declarant.NameEn = null;
        $scope.declarant.CountryId = null;
    }

    $scope.removeDeclarantId = function () {
        if ($scope.object.Id) {
            $http({
                url: '/OBKContract/RemoveDeclarantId',
                method: 'POST',
                data: { contractId: $scope.object.Id }
            }).success(function (response) {
            });
        }
    }

    $scope.addDeclarant = function () {
        $scope.showContactInformation = true;
        $scope.enableCompanyData = true;
        $scope.addDeclarantDisabled = true;

        if ($scope.declarant.IsResident == false) {
            $scope.declarant.CountryId = $scope.object.Country;
        }
        else if ($scope.declarant.IsResident == true) {
            $scope.declarant.CountryId = null;
        }
    }

    $scope.cancelFindDeclarant = function () {
        $scope.iinSearchActive = false;
        $scope.declarantNotFound = false;
        $scope.showContactInformation = false;
        $scope.addDeclarantDisabled = false;

        $scope.clearDeclarantForm();

        $scope.removeDeclarantId();

        $scope.clearContactForm();

        $scope.clearContactData();
    }

    $scope.editDeclarant = function ($event) {

        if ($scope.object.Id) {
            $http({
                url: '/OBKContract/ContractDeclarantSave',
                method: 'POST',
                data: { guid: $scope.object.Id, declarantViewModel: $scope.declarant }
            }).success(function (response) {
                if (!response.Exist) {
                    $scope.declarant.Id = response.DeclarantId;
                }
                else {
                    if (response.IsResident) {
                        var message = "Заявитель с указанным ИИН/БИН уже существует!";
                        var title = "Сообщение";

                        if ($scope.flag == true) {
                            return;
                        }

                        $scope.flag = true;
                        var title = "Сообщение";
                        $('<div></div>').html(message).dialog({
                            title: title,
                            resizable: false,
                            modal: true,
                            buttons: {
                                'Ok': function () {
                                    $(this).dialog('close');
                                    $scope.flag = false;
                                }
                            },
                            close: function () {
                                $scope.flag = false;
                            },
                            dialogClass: 'no-close alert-dialog'
                        });
                    }
                    else {
                        var message = "Заявитель с указанной страной и наименованием уже существует!";

                        if ($scope.flag == true) {
                            return;
                        }

                        $scope.flag = true;
                        var title = "Сообщение";
                        $('<div></div>').html(message).dialog({
                            title: title,
                            resizable: false,
                            modal: true,
                            buttons: {
                                'Ok': function () {
                                    $(this).dialog('close');
                                    $scope.flag = false;
                                }
                            },
                            close: function () {
                                $scope.flag = false;
                            },
                            dialogClass: 'no-close alert-dialog'
                        });
                    }
                }
            });
        }
    }

    $scope.nameNonResidentChange = function () {
        $scope.showContactInformation = false;
        if ($scope.object.NameNonResident) {
            if ($scope.object.NameNonResident == "00000000-0000-0000-0000-000000000000") {
                $scope.declarantNotFound = true;
                $scope.addDeclarantDisabled = false;
                $scope.clearDeclarantForm();
                $scope.removeDeclarantId();
                $scope.clearContactForm();
                $scope.clearContactData();
            }
            else {
                $scope.declarantNotFound = false;
                $scope.addDeclarantDisabled = true;
                $scope.loadOrganizationData($scope.object.NameNonResident);
            }
        }
    }


    $scope.sendWithoutDigitalSign = function () {
        var formValid = $scope.contractCreateForm.$valid;
        var productInfoExist = $scope.addedProducts.length > 0;
        var outputErrors = true;
        if (!formValid && $scope.contractCreateForm.$error && outputErrors) {
            //var errors = [];

            //for (var key in $scope.contractCreateForm.$error) {
            //    for (var index = 0; index < $scope.contractCreateForm.$error[key].length; index++) {
            //        errors.push($scope.contractCreateForm.$error[key][index].$name + ' is required.');
            //    }
            //}

            //for (var i = 0; i < errors.length; i++) {
            //    alert(errors[i]);
            //}
        }
        var filesValid = $scope.checkFileValidation();
        if (formValid && productInfoExist && filesValid) {
            var modalInstance = $uibModal.open({
                templateUrl: '/Project/Agreement',
                controller: modalSendContract,
                scope: $scope,
                size: 'size-custom'
            });
        }
        else {
            alert("Заполните обязательные поля, информацию о продукции и загрузите вложения!");
        }
    }

    $scope.sendWithDigitalSign = function () {
        var formValid = $scope.contractCreateForm.$valid;
        var productInfoExist = $scope.addedProducts.length > 0;
        var filesValid = $scope.checkFileValidation();
        if (formValid && productInfoExist && filesValid) {
            $scope.doSign();
        }
        else {
            alert("Заполните обязательные поля, информацию о продукции и загрузите вложения!");
        }
    }

    $scope.doSign = function () {
        var id = $scope.object.Id;
        if (id) {

            var funcSign = function signData() {
                debugger;
                $.blockUI({ message: '<h1><img src="../../Content/css/plugins/slick/ajax-loader.gif"/> Выполняется подпись...</h1>', css: { opacity: 1 } });
                signXmlCall(function () {
                    $http({
                        url: '/OBKContract/SignContract',
                        method: 'POST',
                        data: JSON.stringify({ contractId: id, signedData: $("#Certificate").val() })
                    }).success(function (response) {
                        $scope.object.Status = response;
                        $scope.changeViewMode();
                        $window.location.href = '/OBKContract';
                    }).error(function () {
                        alert("error");
                        $.unblockUI();
                    });
                });
            };

            startSign('/OBKContract/SignData', id, funcSign);
        }
    }

    $scope.viewContract = function (id) {
        debugger;
        if ($scope.object.Type == null) {
            alert("Выберите тип договра");
            return;
        }
        var modalInstance = $uibModal.open({
            templateUrl: '/OBKContract/ContractTemplate?Id=' + id + "&Url=" + "GetContractTemplatePdf",
            controller: ModalRegisterInstanceCtrl
        });
    };

    $scope.tab3click = function () {
        $interval(function () {
            $scope.gridOptionsCalculatorApi.core.handleWindowResize();
        }, 500, 10);

        $interval(function () {
            $scope.gridOptionsCalculatorAdditionalApi.core.handleWindowResize();
        }, 500, 10);
    }

    $scope.viewInvoicePayment = function (id) {
        debugger;
        var modalInstance = $uibModal.open({
            templateUrl: '/OBKContract/ContractTemplate?Id=' + id + "&Url=" + "GetPaymentTemplatePdf",
            controller: ModalRegisterInstanceCtrl
        });
    };

    $scope.validatePhone = function () {
        if (!$scope.contractCreateForm.Phone.$valid && $scope.object.Phone && $scope.object.Phone.length > 0) {
            var message = "Поле должно содержать числовое значение и знаки \"+\", \",\", \"(\", \")\"";

            if ($scope.flag == true) {
                return;
            }

            $scope.flag = true;
            var title = "Предупреждение";
            $('<div></div>').html(message).dialog({
                title: title,
                resizable: false,
                modal: true,
                buttons: {
                    'Ok': function () {
                        $(this).dialog('close');
                        $scope.flag = false;
                    }
                },
                close: function () {
                    $scope.flag = false;
                },
                dialogClass: 'no-close alert-dialog'
            });
        }
    }

    $scope.validateBik = function () {
        if (!$scope.contractCreateForm.BankBik.$valid && $scope.object.BankBik && $scope.object.BankBik.length > 0) {
            var message = "Поле должно содержать только латинские буквы и цифры. Длина поля должна быть от 8 до 11 символов";

            if ($scope.flag == true) {
                return;
            }

            $scope.flag = true;
            var title = "Предупреждение";
            $('<div></div>').html(message).dialog({
                title: title,
                resizable: false,
                modal: true,
                buttons: {
                    'Ok': function () {
                        $(this).dialog('close');
                        $scope.flag = false;
                    }
                },
                close: function () {
                    $scope.flag = false;
                },
                dialogClass: 'no-close alert-dialog'
            });
        }
    }

    $scope.validateIik = function () {
        if (!$scope.contractCreateForm.BankIik.$valid && $scope.object.BankIik && $scope.object.BankIik.length > 0) {
            var message = "Поле должно содержать только латинские буквы и цифры";

            if ($scope.flag == true) {
                return;
            }

            $scope.flag = true;
            var title = "Предупреждение";
            $('<div></div>').html(message).dialog({
                title: title,
                resizable: false,
                modal: true,
                buttons: {
                    'Ok': function () {
                        $(this).dialog('close');
                        $scope.flag = false;
                    }
                },
                close: function () {
                    $scope.flag = false;
                },
                dialogClass: 'no-close alert-dialog'
            });
        }
    }
}

function ModalRegisterInstanceCtrl($scope, $uibModalInstance) {
    debugger;
    $scope.ok = function () {
        $uibModalInstance.close();
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function modalSendContract($scope, $http, $window, $uibModalInstance) {
    $scope.sendProject = function () {
        var projectId = $scope.object.Id;
        if (projectId) {
            $http({
                url: '/OBKContract/SendContractInProcessing',
                method: 'POST',
                data: { contractId: projectId }
            }).success(function (response) {
                $scope.object.Status = response;
                $scope.changeViewMode();
                $uibModalInstance.close();
                $window.location.href = '/OBKContract';
            });
        }
    };

    $scope.cancelSend = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function getDate(value) {
    if (value) {
        var dt = new Date(parseInt(value.substr(6)));
        return dt;
    }
    return "";
}

function convertDateToString(dateMilliseconds) {
    var d = new Date();
    d.setTime(dateMilliseconds);
    if (!d) {
        return null;
    }
    var yyyy = d.getFullYear();
    var mm = d.getMonth() < 9 ? "0" + (d.getMonth() + 1) : (d.getMonth() + 1);
    return mm + "." + yyyy;
}

function convertDateToStringDDMMYYYY(value) {
    if (value) {
        var d = new Date(parseInt(value.substr(6)));
        var yyyy = d.getFullYear();
        var mm = d.getMonth() < 9 ? "0" + (d.getMonth() + 1) : (d.getMonth() + 1);
        var dd = d.getDate() < 10 ? "0" + d.getDate() : d.getDate();
        return dd + "." + mm + "." + yyyy;
    }
    return "";
}

function loadExpertOrganizations($scope, $http) {
    $http({
        method: "GET",
        url: "/OBKContract/GetExpertOrganizations",
        data: "JSON"
    }).success(function (result) {
        $scope.ExpertOrganizations = result;
    });
}

function loadContractSigners($scope, $http) {
    $http({
        method: "GET",
        url: "/OBKContract/GetSigners",
        data: "JSON"
    }).success(function (result) {
        $scope.ContractSigners = result;
    });
}

function loadObkRefTypes($scope, $http) {
    var name = "ObkContractTypes";
    $http({
        method: "GET",
        url: "/OBKDictionaries/GetObkContractTypes",
        data: "JSON"
    }).success(function (result) {
        $scope[name] = result;
    });
}

function loadObkOrganizations($scope, $http) {
    var name = "Organizations";
    $http({
        method: "GET",
        url: "/OBKDictionaries/GetObkOrganizations",
        data: "JSON"
    }).success(function (result) {
        $scope[name] = result;
    });
}

function loadDictionaryOBKContractDocumentType($scope, $http) {
    var name = "OBKContractDocumentType";
    $http({
        method: "GET",
        url: "/OBKDictionaries/GetOBKContractDocumentTypeDictionary",
        data: "JSON"
    }).success(function (result) {
        $scope[name] = result;
    });
}

function loadDictionaryMeasure($scope, $http) {
    var name = "SRMeasures";
    $http({
        method: "GET",
        url: "/OBKDictionaries/GetMeasureDictionary",
        data: "JSON"
    }).success(function (result) {
        $scope[name] = result;
    });
}

function initCalculator($scope, $interval, $http) {

    $scope.totalCostCalculator = 0;

    $scope.selectedServiceIndex = null;

    $scope.addedServices = [];

    $scope.gridOptionsCalculator = {
        enableRowSelection: true,
        enableRowHeaderSelection: false,
        multiSelect: false,
        noUnselect: true
    };

    $scope.gridOptionsCalculator.onRegisterApi = function (gridApi) {
        $scope.gridOptionsCalculatorApi = gridApi;

        $interval(function () {
            $scope.gridOptionsCalculatorApi.core.handleWindowResize();
        }, 500, 10);

        $scope.gridOptionsCalculatorApi.selection.on.rowSelectionChanged($scope, function (row) {
            $scope.selectedServiceIndex = $scope.gridOptionsCalculator.data.indexOf(row.entity);
        });
    };

    $scope.gridOptionsCalculator.columnDefs = [
        { name: 'Id', displayName: 'ИД', width: "*", visible: false },
        { name: 'ServiceName', displayName: 'Тип услуги', width: "*", cellTemplate: '<div class="ui-grid-cell-contents" >{{grid.getCellValue(row, col)}}</div>' },
		{ name: 'ServiceId', displayName: 'Тип услуги - ИД', width: "*", visible: false },
        { name: "ProductId", displayName: "Продукция - ИД", width: "*", visible: false },
        { name: "ProductName", displayName: "Продукция", width: "*" },
        { name: 'UnitOfMeasurementName', displayName: 'Единица измерения', width: "*" },
		{ name: 'UnitOfMeasurementId', displayName: 'Единица измерения - ИД', width: "*", visible: false },
        { name: 'PriceWithoutTax', displayName: 'Цена в тенге, без НДС', width: "*" },
        { name: 'Count', displayName: 'Количество услуг (работ)', width: "*" },
        { name: 'FinalCostWithoutTax', displayName: 'Итоговая стоимость услуги, в тенге без НДС', width: "*" },
        { name: 'FinalCostWithTax', displayName: 'Итоговая стоимость услуги, в тенге с НДС', width: "*" },
        { name: 'ButtonComments', displayName: '', width: '*', cellTemplate: '<span class="input-group-addon"><a valval="{{row.entity.Id}}" class="obkpricedialog" href="#"><i class="glyphicon glyphicon-info-sign"></i></a></span>' }
    ];

    $scope.gridOptionsCalculator.data = $scope.addedServices;

    $scope.serviceTypeChange = function () {
        if ($scope.object.ServiceName && $scope.object.ServiceName.Id) {
            $http({
                method: 'GET',
                url: '/OBKContract/GetService',
                params: {
                    service: $scope.object.ServiceName.Id
                }
            }).then(function (resp) {
                if (resp.data) {
                    $scope.object.UnitOfMeasurementName = resp.data.UnitOfMeasurementName;
                    $scope.object.UnitOfMeasurementId = resp.data.UnitOfMeasurementId;
                    $scope.object.PriceWithoutTax = resp.data.Price;
                    $scope.object.CountServices = null;
                    $scope.object.ResultPriceWithoutTax = null;
                    $scope.object.ResultPriceWithTax = null;
                    $scope.calcPrice();
                }
                else {
                    $scope.object.UnitOfMeasurementName = null;
                    $scope.object.UnitOfMeasurementId = null;
                    $scope.object.PriceWithoutTax = null;
                    $scope.object.CountServices = null;
                    $scope.object.ResultPriceWithoutTax = null;
                    $scope.object.ResultPriceWithTax = null;
                    $scope.calcPrice();
                }
            }, function (response) {

            });
        }

    }

    $scope.calcPrice = function () {
        if ($scope.object.PriceWithoutTax && $scope.taxValue && $scope.object.CountServices) {
            var sum = $scope.object.PriceWithoutTax * $scope.object.CountServices;
            var res = sum + sum * ($scope.taxValue * 0.01);
            $scope.object.ResultPriceWithoutTax = sum;
            $scope.object.ResultPriceWithTax = res;
        }
        else {
            $scope.object.ResultPriceWithoutTax = null;
            $scope.object.ResultPriceWithTax = null;
        }
    }

    $scope.countServiceChange = function () {
        $scope.calcPrice();
    }

    $scope.calcTotalCostCalculator = function () {
        $scope.totalCostCalculator = 0;

        var sum = 0.0;
        var projectId = $scope.object.Id;
        if (!projectId) {
            projectId = $("#projectId").val();
        }

        if (projectId) {
            $http({
                method: 'GET',
                url: '/OBKContract/GetContractPricesSum',
                params: {
                    contractId: projectId
                }
            }).then(function (resp) {
                if (resp.data) {
                    sum = resp.data;
                    $scope.totalCostCalculator = sum;
                }
            });
        }
    }
}

function initProductServiceModule($scope, $http, $interval) {
    $scope.selectedProductServiceIndex = null;

    $scope.gridOptionsProductServices = {
        enableRowSelection: true,
        enableRowHeaderSelection: false,
        multiSelect: false,
        noUnselect: true
    };

    $scope.gridOptionsProductServices.onRegisterApi = function (gridApi) {
        $scope.gridOptionsProductServicesApi = gridApi;

        $interval(function () {
            $scope.gridOptionsProductServicesApi.core.handleWindowResize();
        }, 500, 10);

        $scope.gridOptionsProductServicesApi.selection.on.rowSelectionChanged($scope, function (row) {
            $scope.selectedProductServiceIndex = $scope.gridOptionsProductServices.data.indexOf(row.entity);
        });
    };

    $scope.gridOptionsProductServices.columnDefs = [
        { name: 'Id', displayName: 'ИД', width: "*", visible: false },
        { name: 'ServiceName', displayName: 'Тип услуги', width: "*" },
		{ name: 'ServiceId', displayName: 'Тип услуги - ИД', width: "*", visible: false },
        { name: 'UnitOfMeasurementName', displayName: 'Единица измерения', width: "*" },
		{ name: 'UnitOfMeasurementId', displayName: 'Единица измерения - ИД', width: "*", visible: false },
        { name: 'PriceWithoutTax', displayName: 'Цена в тенге, без НДС', width: "*" },
        { name: 'Count', displayName: 'Количество услуг (работ)', width: "*" },
        { name: 'FinalCostWithoutTax', displayName: 'Итоговая стоимость услуги, в тенге без НДС', width: "*" },
        { name: 'FinalCostWithTax', displayName: 'Итоговая стоимость услуги, в тенге с НДС', width: "*" }
    ];

    $scope.gridOptionsProductServices.data = null;

    $scope.loadProductServiceNames = function loadProductServiceNames() {
        $scope.productServiceNames = [];
        if ($scope.object.Type && $scope.product.RegTypeId) {
            $http({
                method: "GET",
                url: "/OBKDictionaries/GetServiceNames",
                data: "JSON",
                params: {
                    type: $scope.object.Type,
                    productType: $scope.product.RegTypeId,
                    degreeRiskId: $scope.product.DegreeRiskId
                }
            }).success(function (result) {
                $scope.productServiceNames = result;
            });
        }
    }
}

angular
    .module('app')
    .controller('obkContractForm', ['$scope', '$http', '$interval', '$uibModal', '$window', obkContractForm])