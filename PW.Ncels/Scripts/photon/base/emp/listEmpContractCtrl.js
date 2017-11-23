function empContractForm($scope, $http, DTColumnBuilder, $interval, $uibModal, $window) {
    $scope.object = {};
    $scope.Calulator = {};

    $scope.Payer = {};

    $scope.iinSearchActiveManufactur = false;
    $scope.showContactInformationManufactur = true;
    $scope.showContactInformationDeclarant = true;
    $scope.showContactInformationPayer = true;
    $scope.addNewBankName = false;
    $scope.IsManufactur = null;

    //calculator
    //$scope.showServiceType = true;
    $scope.showServiceTypeName = true;
    $scope.showServiceTypeNameModif = true;
    $scope.showCalculatorForm = true;
    $scope.showRadioIsImport = true;
    $scope.Calulator.IsImport = false;
    $scope.Calulator.Count = 1;
    //$scope.declarant.IsResident = true;

    $scope.bankValid = true;

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
    $scope.HolderType = [];

    //$scope.manufacturObject = {};
    //$scope.object.manufactur = {};
    debugger;
    // loadRefs
    loadHolderTypes($scope, $http);
    loadExpertOrganizations($scope, $http);
    loadDictionary($scope, 'OpfType', $http);
    loadDictionary($scope, 'Country', $http);
    loadDictionary($scope, 'Currency', $http);
    loadBanks($scope, $http);

    loadServiceType($scope, $http);

    $scope.saveBtnClick = function () {
        debugger;
        //todo будет валидация
        $scope.editProject();
    }

    // saveContract
    $scope.editProject = function () {
        debugger;
        var modelGuid = $("#modelGuid").val();
        var object = $scope.object;
        $http({
            url: '/EMPContract/ContractSave',
            method: 'POST',
            data: { Guid: modelGuid, contractViewModel: $scope.object }
        }).success(function (response) {
            $scope.object.Id = response.Id;
        });
    }

    //choose Payer
    $scope.ChoosePayers = [{
        Id: 'Manufactur',
        Name: "Производитель"
    }, {
        Id: 'Declarant',
        Name: "Заявитель"
    }, {
        Id: 'Payer',
        Name: "Третье лицо"
        }
    ];

    $scope.editPayer = function () {
        debugger;
        if ($scope.object['Payer'] != null)
            $scope.clearForm('Payer');
        switch ($scope.Payer.ChoosePayer) {
            case 'Manufactur':
                $scope.object['Payer'] = angular.copy($scope.object['Manufactur']);
                $scope.showContactInformationPayer = true;
                $scope.object['Payer'].IsResident = null; //todo временно для тестирования
                break;
            case 'Declarant':
                $scope.object['Payer'] = angular.copy($scope.object['Declarant']);
                $scope.showContactInformationPayer = true;
                $scope.object['Payer'].IsResident = null; //todo временно для тестирования
                break;
            case 'Payer':
                $scope.showContactInformationPayer = false;
                break;
                default:
        }
    }

    $scope.isManufacturer = function () {
        if ($scope.IsManufactur) {
            $scope.object['Declarant'] = angular.copy($scope.object['Manufactur']);
            $scope.object['Declarant'].IsResident = null; //todo временно для тестирования
        }
        if (!$scope.IsManufactur) {
            $scope.clearForm('Declarant');
        }
    }

    $scope.residentChange = function (obj) {
        debugger;
        $scope.showHideBin(obj);
    }
    //check radioBtn
    $scope.showHideBin = function (obj) {
        debugger;
        if ($scope.object[obj].IsResident == true) {
            //$scope.object[obj].showBin = true;
        }
        else {
            //$scope.object[obj].showBin = false;
            $scope.object[obj].Country = null;
            $scope.object[obj].NameNonResident = null;
            //$scope.clearDeclarantForm();
            //$scope.removeDeclarantId();
        }
        $scope.cancelFind(obj);
        $scope.object[obj].Bin = null;
        

        switch(obj) {
            case 'Manufactur':
                $scope.showContactInformationManufactur = true;
                $scope.manufacturNotFound = false;
                break;
            case 'Declarant':
                $scope.showContactInformationDeclarant = true;
                $scope.declarantNotFound = false;
                break;
            case 'Payer':
                $scope.showContactInformationPayer = true;
                $scope.payerNotFound = false;
                break;
                default:
        }

        //$scope.clearContactForm();
        //$scope.clearContactData();
    }

    $scope.cancelFind = function (obj) {
        debugger;
        switch(obj) {
            case 'Manufactur':
                $scope.iinSearchActiveManufactur = false;
                $scope.manufacturNotFound = false;
                $scope.showContactInformationManufactur = true;
                $scope.addManufacturDisabled = false;
                break;
            case 'Declarant':
                $scope.iinSearchActiveDeclarant = false;
                $scope.declarantNotFound = false;
                $scope.showContactInformationDeclarant = true;
                $scope.addDeclarantDisabled = false;
                break;
            case 'Payer':
                $scope.iinSearchActivePayer = false;
                $scope.payerNotFound = false;
                $scope.showContactInformationPayer = true;
                $scope.addPayerDisabled = false;
                break;
            default:
        }
        $scope.clearForm(obj);
        //$scope.removeDeclarantId();

        //$scope.clearContactForm();

        //$scope.clearContactData();
    }

    $scope.clearForm = function (obj) {
        $scope.object[obj].Id = null;
        $scope.object[obj].OrganizationFormId = null;
        $scope.object[obj].NameKz = null;
        $scope.object[obj].NameRu = null;
        $scope.object[obj].NameEn = null;
        $scope.object[obj].CountryId = null;

        $scope.object[obj].AddressLegalRu = null;
        $scope.object[obj].AddressLegalKz = null;
        $scope.object[obj].AddressFact = null;
        $scope.object[obj].Phone = null;
        $scope.object[obj].Email = null;
        $scope.object[obj].BossLastName = null;
        $scope.object[obj].BossFirstName = null;
        $scope.object[obj].BossMiddleName = null;
        $scope.object[obj].BossPosition = null;
        $scope.object[obj].BossPositionKz = null;
        $scope.object[obj].BossDocType = null;
        $scope.object[obj].IsHasBossDocNumber = null;
        $scope.object[obj].BossDocNumber = null;
        $scope.object[obj].BossDocCreatedDate = getDate(null);
        $scope.object[obj].BossDocEndDate = getDate(null);
        $scope.object[obj].BossDocUnlimited = false;
        $scope.object[obj].SignerIsBoss = false;
        $scope.object[obj].SignLastName = null;
        $scope.object[obj].SignFirstName = null;
        $scope.object[obj].SignMiddleName = null;
        $scope.object[obj].SignPosition = null;
        $scope.object[obj].SignPositionKz = null;
        $scope.object[obj].SignDocType = null;
        $scope.object[obj].IsHasSignDocNumber = null;
        $scope.object[obj].SignDocNumber = null;
        $scope.object[obj].SignDocCreatedDate = getDate(null);
        $scope.object[obj].SignDocEndDate = getDate(null);
        $scope.object[obj].SignDocUnlimited = false;
        $scope.object[obj].BankId = null;
        $scope.object[obj].BankIik = null;
        $scope.object[obj].BankBik = null;
        $scope.object[obj].CurrencyId = null;
        $scope.object[obj].BankNameRu = null;
        $scope.object[obj].BankNameKz = null;
    }

    $scope.addManufactur = function (obj) {
        
        if ($scope.object[obj].IsResident == false) {
            $scope.object[obj].CountryId = $scope.object[obj].Country;
        }
        else if ($scope.object[obj].IsResident == true) {
            $scope.object[obj].CountryId = null;
        }
        switch (obj) {
            case 'Manufactur':
                $scope.showContactInformationManufactur = false;
                $scope.addManufacturDisabled = true;
                break;
            case 'Declarant':
                $scope.showContactInformationDeclarant = false;
                $scope.addDeclarantDisabled = true;
                break;
            case 'Payer':
                $scope.showContactInformationPayer = false;
                $scope.addPayerDisabled = true;
                break;
            default:
        }
    }

    $scope.loadNamesNonResidents = function (obj) {
        $scope.NamesNonResidents = [];

        $http({
            method: 'GET',
            url: '/OBKContract/GetNamesNonResidents',
            params: {
                countryId: $scope.object[obj].Country
            }
        }).then(function (resp) {
            if (resp.data) {
                $scope.NamesNonResidents = resp.data;
            }
        });
    }

    //$scope.loadNamesNonResidents();

    $scope.nonResidentCountryChange = function (obj) {
        
        $scope.loadNamesNonResidents(obj);
        $scope.object[obj].NameNonResident = null;
        
        switch (obj) {
            case 'Manufactur':
                $scope.manufacturNotFound = false;
                $scope.showContactInformationManufactur = true;
                break;
            case 'Declarant':
                $scope.declarantNotFound = false;
                $scope.showContactInformationDeclarant = true;
                break;
            case 'Payer':
                $scope.payerNotFound = false;
                $scope.showContactInformationPayer = true;
                break;
            default:
        }

        //$scope.clearDeclarantForm();
        //$scope.removeDeclarantId();

        //$scope.clearContactForm();
        //$scope.clearContactData();
    }

    $scope.nameNonResidentChange = function (obj) {
        //$scope.showContactInformationManufactur = false;
        if ($scope.object[obj].NameNonResident) {
            if ($scope.object[obj].NameNonResident == "00000000-0000-0000-0000-000000000000") {
                switch(obj) {
                    case 'Manufactur':
                        $scope.manufacturNotFound = true;
                        $scope.addManufacturDisabled = false;
                        break;
                    case 'Declarant':
                        $scope.declarantNotFound = true;
                        $scope.addDeclarantDisabled = false;
                        break;
                    case 'Payer':
                        $scope.payerNotFound = true;
                        $scope.addPayerDisabled = false;
                        default:
                }
                
                
                //$scope.clearDeclarantForm();
                //$scope.removeDeclarantId();
                //$scope.clearContactForm();
                //$scope.clearContactData();
            }
            else {
                switch (obj) {
                    case 'Manufactur':
                        $scope.manufacturNotFound = false;
                        $scope.addManufacturDisabled = true;
                        break;
                    case 'Declarant':
                        $scope.declarantNotFound = false;
                        $scope.addDeclarantDisabled = true;
                        break;
                    case 'Payer':
                        $scope.payerNotFound = false;
                        $scope.addPayerDisabled = true;
                        break;
                    default:
                }
                $scope.addManufacturDisabled = true;
                //$scope.loadOrganizationData($scope.manufacturObject.NameNonResident);
            }
        }
    }

    $scope.changeBank = function (obj) {
        debugger;
        if ($scope.object[obj].BankId == "999") {
            $scope.addNewBankName = true;
            $scope.bankValid = true;
        } else {
            $scope.addNewBankName = false;
            $scope.bankValid = false;
            //editProject();
        }
    }

    $scope.addNewBank = function(obj) {
        if ($scope.object[obj].BankNameRu == null || $scope.object[obj].BankNameKz == null)
            return alert("Заполните наименование банка");
        $http({
            url: '/EMPContract/SaveNewBank',
            method: 'POST',
            data: { BankNameRu: $scope.object[obj].BankNameRu, BankNameKz: $scope.object[obj].BankNameKz }
        }).success(function (response) {
            debugger;
            loadBanks($scope, $http);
            $scope.object[obj].BankId = response.Id;
            $scope.addNewBankName = false;
        });
        $scope.bankValid = false;
    }

    $scope.editServiceType = function () {
        debugger;
        if ($scope.Calulator.ServiceType == "00000000-0000-0000-0000-000000000000") {
            $scope.showServiceTypeName = true;
        } else {
            $scope.showServiceTypeName = false;
            $http({
                method: "GET",
                url: "/EMPContract/GetServiceTypeParentId",
                params: {
                    id: $scope.Calulator.ServiceType
                }
            }).success(function (result) {
                $scope.ServiceTypeNames = result;
            });
        }
    }


    $scope.editServiceTypeName = function() {
        debugger;
        if ($scope.Calulator.ServiceTypeName == "00000000-0000-0000-0000-000000000000") {
            $scope.showServiceTypeNameModif = true;
            $scope.showCalculatorForm = true;
        } else {
            $scope.showServiceTypeNameModif = false;
            $http({
                method: "GET",
                url: "/EMPContract/GetServiceTypeParentId",
                params: {
                    id: $scope.Calulator.ServiceTypeName
                }
            }).success(function (result) {
                $scope.ServiceTypeNameModifs = result;

            });
        }
    }

    $scope.editServiceTypeNameModif = function () {
        debugger;
        if ($scope.Calulator.ServiceTypeNameModif == "00000000-0000-0000-0000-000000000000") {
            $scope.showCalculatorForm = true;
            $scope.showRadioIsImport = false;
        } else {
            $scope.showCalculatorForm = false;
            $scope.showRadioIsImport = false;
        }
    }

    $scope.calulatorChange = function (bool) {
        debugger;
        $scope.Calulator.IsImport = bool;
    }

    $scope.calculation = function () {
        $scope.object.ServicePrice = [];
        debugger;
        var serviceType = null;
        if ($scope.Calulator.ServiceTypeNameModif == "00000000-0000-0000-0000-000000000000") {
            serviceType = $scope.Calulator.ServiceTypeName;
        } else {
            serviceType = $scope.Calulator.ServiceTypeNameModif;
        }
        $http({
            method: "GET",
            url: "/EMPContract/GetCalculation",
            params: {
                serviceTypeId: serviceType,
                isImport: $scope.Calulator.IsImport,
                count: $scope.Calulator.Count
            }
        }).success(function(response) {
            debugger;
            if (response) {
                $scope.object.ServicePrice = response;
                $scope.addServices.length = 0;
                $scope.addServices.push.apply($scope.addServices, response);
            }
        });
    };

    $scope.clearCalculation = function () {
        $scope.Calulator.ServiceType = null;//"00000000-0000-0000-0000-000000000000";
        $scope.Calulator.ServiceTypeName = null;
        $scope.Calulator.ServiceTypeNameModif = null;
        $scope.Calulator.Count = null;
        $scope.showServiceTypeName = true;
        $scope.showServiceTypeNameModif = true;
        $scope.showCalculatorForm = true;
        $scope.showRadioIsImport = true;
        $scope.Calulator.Count = 1;

        $scope.gridOptionsCalc.data = null;
    }

    //table datatable

    $scope.dtColumns2 = [
        DTColumnBuilder.newColumn("Id", "№").withOption("name", "Id"),
        DTColumnBuilder.newColumn("NameRu", "Наименование работ по Прейскуранту").withOption("name", "NameRu"),
        DTColumnBuilder.newColumn("Price", "Цена в тенге, без НДС").withOption("name", "Price"),
        DTColumnBuilder.newColumn("Count", "Количество").withOption("name", "Count"),
        DTColumnBuilder.newColumn("TotalCount", "Всего").withOption("name", "TotalCount")
    ];

    //table
    $scope.addServices = [];

    $scope.gridOptionsCalc = {
        enableRowSelection: true,
        enableRowHeaderSelection: false,
        multiSelect: false,
        noUnselect: true
    };

    $scope.gridOptionsCalc.columnDefs = [
        { name: 'Id', displayName: '№', width: '5%' },
        { name: 'NameRu', displayName: 'Наименование работ по Прейскуранту', width: "50%" },
        { name: 'Price', displayName: 'Цена в тенге, без НДС', width: "15%" },
        { name: "Сount", displayName: "Количество", width: "15%" },
        { name: "TotalCount", displayName: "Всего", width: "15%" }
    ];

    $scope.gridOptionsCalc.data = $scope.addServices;

    $scope.gridOptionsCalc.onRegisterApi = function (gridApi) {
        $scope.gridOptionsCalcApi = gridApi;
    };

    //todo незабыть перечислить поля
    $scope.findDeclarant = function (obj) {
        debugger;
        if ($scope.object[obj].Bin && $scope.object[obj].Bin.length == 12) {
            $http({
                method: 'GET',
                url: '/EMPContract/FindDeclarant',
                params: {
                    bin: $scope.object[obj].Bin
                }
            }).then(function (resp) {
                switch(obj) {
                    case 'Manufactur':
                        $scope.iinSearchActiveManufactur = true;
                        break;
                    case 'Declarant':
                        $scope.iinSearchActiveDeclarant = true;
                        break;
                    case 'Payer':
                        $scope.iinSearchActivePayer = true;
                        break;
                        default:
                }
                if (resp.data) {
                    $scope.object[obj].Id = resp.data.Id;
                    $scope.object[obj].OrganizationFormId = resp.data.OrganizationFormId;
                    $scope.object[obj].IsResident = resp.data.IsResident;
                    $scope.object[obj].NameKz = resp.data.NameKz;
                    $scope.object[obj].NameRu = resp.data.NameRu;
                    $scope.object[obj].NameEn = resp.data.NameEn;
                    $scope.object[obj].CountryId = resp.data.CountryId;

                    $scope.object[obj].AddressLegalRu = resp.data.AddressLegalRu;
                    $scope.object[obj].AddressLegalKz = resp.data.AddressLegalKz;
                    $scope.object[obj].AddressFact = resp.data.AddressFact;
                    $scope.object[obj].Phone = resp.data.Phone;
                    $scope.object[obj].Email = resp.data.Email;
                    $scope.object[obj].BossLastName = resp.data.BossLastName;
                    $scope.object[obj].BossFirstName = resp.data.BossFirstName;
                    $scope.object[obj].BossMiddleName = resp.data.BossMiddleName;
                    $scope.object[obj].BossPosition = resp.data.BossPosition;
                    $scope.object[obj].BossPositionKz = resp.data.BossPositionKz;
                    $scope.object[obj].BossDocType = resp.data.BossDocType;
                    $scope.object[obj].IsHasBossDocNumber = resp.data.IsHasBossDocNumber;
                    $scope.object[obj].BossDocNumber = resp.data.BossDocNumber;
                    $scope.object[obj].BossDocCreatedDate = getDate(resp.data.BossDocCreatedDate);
                    $scope.object[obj].BossDocEndDate = getDate(resp.data.BossDocEndDate);
                    $scope.object[obj].BossDocUnlimited = resp.data.BossDocUnlimited;
                    $scope.object[obj].SignerIsBoss = resp.data.SignerIsBoss;
                    $scope.object[obj].SignLastName = resp.data.SignLastName;
                    $scope.object[obj].SignFirstName = resp.data.SignFirstName;
                    $scope.object[obj].SignMiddleName = resp.data.SignMiddleName;
                    $scope.object[obj].SignPosition = resp.data.SignPosition;
                    $scope.object[obj].SignPositionKz = resp.data.SignPositionKz;
                    $scope.object[obj].SignDocType = resp.data.SignDocType;
                    $scope.object[obj].IsHasSignDocNumber = resp.data.IsHasSignDocNumber;
                    $scope.object[obj].SignDocNumber = resp.data.SignDocNumber;
                    $scope.object[obj].SignDocCreatedDate = getDate(resp.data.SignDocCreatedDate);
                    $scope.object[obj].SignDocEndDate = getDate(resp.data.SignDocEndDate);
                    $scope.object[obj].SignDocUnlimited = resp.data.SignDocUnlimited;
                    $scope.object[obj].BankIik = resp.data.BankIik;
                    $scope.object[obj].BankBik = resp.data.BankBik;
                    $scope.object[obj].CurrencyId = resp.data.CurrencyId;
                    $scope.object[obj].BankNameRu = resp.data.BankNameRu;
                    $scope.object[obj].BankNameKz = resp.data.BankNameKz;
                    //$scope.saveDeclarantId(resp.data.Id);
                    //$scope.editProject();
                    debugger;
                    switch (obj) {
                        case 'Manufactur':
                            $scope.showContactInformationManufactur = true;
                            $scope.manufacturNotFound = false;
                            break;
                        case 'Declarant':
                            $scope.showContactInformationDeclarant = true;
                            $scope.declarantNotFound = false;
                            break;
                        case 'Payer':
                            $scope.showContactInformationPayer = true;
                            $scope.payerNotFound = false;
                            break;
                    default:
                    }
                    
                }
                else {
                    switch (obj) {
                        case 'Manufactur':
                            $scope.manufacturNotFound = true;
                            $scope.addManufacturDisabled = false;
                            //$scope.declarant.Id = null;
                            break;
                        case 'Declarant':
                            $scope.manufacturNotFound = true;
                            $scope.addDeclarantDisabled = false;
                            break;
                        case 'Payer':
                            $scope.payerNotFound = true;
                            $scope.addPayerDisabled = false;
                            break;
                    default:
                    }
                   
                }
            }, function (response) {

            });
        }
    }

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
}

function loadHolderTypes($scope, $http) {
    $http({
        method: "GET",
        url: "/EMPContract/GetHolderTypes",
        data: "JSON"
    }).success(function (result) {
        $scope.HolderType = result;
    });
}

function loadExpertOrganizations($scope, $http) {
    $http({
        method: "GET",
        url: "/EMPContract/GetExpertOrganizations",
        data: "JSON"
    }).success(function (result) {
        $scope.ExpertOrganizations = result;
    });
}

function loadBanks($scope, $http) {
    $http({
        method: "GET",
        url: "/EMPContract/GetBanks",
        data: "JSON"
    }).success(function (result) {
        $scope.Bank = result;
    });
}

function loadServiceType($scope, $http) {
    $http({
        method: "GET",
        url: "/EMPContract/GetServiceType",
        data: "JSON"
    }).success(function(result) {
        $scope.ServiceTypes = result;
    });
}


angular
    .module('app')
    .controller('empContractForm', ['$scope', '$http', 'DTColumnBuilder', '$interval', '$uibModal', '$window', empContractForm])