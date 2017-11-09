function obkContracAdditiontForm($scope, $http, $interval, $uibModal, $window) {
    $scope.loadContracts = function () {
        $http({
            method: 'GET',
            url: '/OBKContract/GetActiveContracts',
            data: 'JSON'
        }).success(function (result) {
            $scope.Contracts = result;
        });
    }

    $scope.contractAdditionChanged = function (contractAdditionType) {
        $scope.contractAdditionTypeCode = contractAdditionType.Code;
        $scope.contractChanged();
    }

    $scope.contractChanged = function () {
        if ($scope.contractAddition.ContractId) {
            $scope.loadContract($scope.contractAddition.ContractId);
        }
    }

    $scope.loadContract = function (contractId) {
        $http({
            method: 'GET',
            url: '/OBKContract/LoadContract',
            params: {
                id: contractId
            }
        }).then(function (resp) {
            if (resp.data) {
                $scope.object.Type = resp.data.Type;
                $scope.object.ExpertOrganization = resp.data.ExpertOrganization;
                $scope.object.Signer = resp.data.Signer;

                $http({
                    method: 'GET',
                    url: '/OBKContract/GetDeclarant',
                    params: {
                        contractId: contractId
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
                //$scope.object.SignerIsBoss = resp.data.SignerIsBoss;
                //$scope.object.SignLastName = resp.data.SignLastName;
                //$scope.object.SignFirstName = resp.data.SignFirstName;
                //$scope.object.SignMiddleName = resp.data.SignMiddleName;
                //$scope.object.SignPosition = resp.data.SignPosition;
                //$scope.object.SignPositionKz = resp.data.SignPositionKz;
                //$scope.object.SignDocType = resp.data.SignDocType;
                //$scope.object.IsHasSignDocNumber = resp.data.IsHasSignDocNumber;
                //$scope.object.SignDocNumber = resp.data.SignDocNumber;
                //$scope.object.SignDocCreatedDate = getDate(resp.data.SignDocCreatedDate);
                //$scope.object.SignDocEndDate = getDate(resp.data.SignDocEndDate);
                //$scope.object.SignDocUnlimited = resp.data.SignDocUnlimited;
                $scope.object.BankIik = resp.data.BankIik;
                $scope.object.BankBik = resp.data.BankBik;
                $scope.object.CurrencyId = resp.data.CurrencyId;
                $scope.object.BankNameRu = resp.data.BankNameRu;
                $scope.object.BankNameKz = resp.data.BankNameKz;

                $scope.editProject();
            }
        }, function (response) {

        });
    }

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

    $scope.editProject = function () {
        var generatedGuid = $("#generatedGuid").val();
        $http({
            url: '/OBKContract/ContractAdditionSave',
            method: 'POST',
            data: { Guid: generatedGuid, contractViewModel: $scope.object, contractAddition: $scope.contractAddition }
        }).success(function (response) {
            $scope.contractAddition.Id = response.Id;
        });
    }

    $scope.loadContractAddition = function () {
        var projectId = $("#projectId").val();
        $http({
            method: 'GET',
            url: '/OBKContract/ContractAdditionGet',
            params: {
                id: projectId
            }
        }).then(function (resp) {
            if (resp.data) {
                $scope.contractAddition = resp.data.contractAddition;
                $scope.object.Status = resp.data.contract.Status;
                $scope.changeViewMode();
                $scope.object.Type = resp.data.contract.Type;
                $scope.object.ExpertOrganization = resp.data.contract.ExpertOrganization;
                $scope.object.Signer = resp.data.contract.Signer;


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

                        if (!$scope.declarant.IsResident) {
                            if (resp.data.IsConfirmed) {
                                $scope.object.Country = $scope.declarant.CountryId;
                                $scope.loadNamesNonResidents();
                                $scope.object.NameNonResident = $scope.declarant.Id;
                            }
                            else {
                                $scope.object.Country = $scope.declarant.CountryId;
                                $scope.loadNamesNonResidents();
                                $scope.object.NameNonResident = "00000000-0000-0000-0000-000000000000";
                            }
                        }
                    }
                });

                $scope.object.AddressLegalRu = resp.data.contract.AddressLegalRu;
                $scope.object.AddressLegalKz = resp.data.contract.AddressLegalKz;
                $scope.object.AddressFact = resp.data.contract.AddressFact;
                $scope.object.Phone = resp.data.contract.Phone;
                $scope.object.Email = resp.data.contract.Email;
                $scope.object.BossLastName = resp.data.contract.BossLastName;
                $scope.object.BossFirstName = resp.data.contract.BossFirstName;
                $scope.object.BossMiddleName = resp.data.contract.BossMiddleName;
                $scope.object.BossPosition = resp.data.contract.BossPosition;
                $scope.object.BossPositionKz = resp.data.contract.BossPositionKz;
                $scope.object.BossDocType = resp.data.contract.BossDocType;
                $scope.object.IsHasBossDocNumber = resp.data.contract.IsHasBossDocNumber;
                $scope.object.BossDocNumber = resp.data.contract.BossDocNumber;
                $scope.object.BossDocCreatedDate = getDate(resp.data.contract.BossDocCreatedDate);
                $scope.object.BossDocEndDate = getDate(resp.data.contract.BossDocEndDate);
                $scope.object.BossDocUnlimited = resp.data.contract.BossDocUnlimited;
                $scope.object.SignerIsBoss = resp.data.contract.SignerIsBoss;
                $scope.object.SignLastName = resp.data.contract.SignLastName;
                $scope.object.SignFirstName = resp.data.contract.SignFirstName;
                $scope.object.SignMiddleName = resp.data.contract.SignMiddleName;
                $scope.object.SignPosition = resp.data.contract.SignPosition;
                $scope.object.SignPositionKz = resp.data.contract.SignPositionKz;
                $scope.object.SignDocType = resp.data.contract.SignDocType;
                $scope.object.IsHasSignDocNumber = resp.data.contract.IsHasSignDocNumber;
                $scope.object.SignDocNumber = resp.data.contract.SignDocNumber;
                $scope.object.SignDocCreatedDate = getDate(resp.data.contract.SignDocCreatedDate);
                $scope.object.SignDocEndDate = getDate(resp.data.contract.SignDocEndDate);
                $scope.object.SignDocUnlimited = resp.data.contract.SignDocUnlimited;
                $scope.object.BankIik = resp.data.contract.BankIik;
                $scope.object.BankBik = resp.data.contract.BankBik;
                $scope.object.CurrencyId = resp.data.contract.CurrencyId;
                $scope.object.BankNameRu = resp.data.contract.BankNameRu;
                $scope.object.BankNameKz = resp.data.contract.BankNameKz;

                for (var i = 0; i < $scope.OBKContractAddition.length; i++) {
                    if ($scope.OBKContractAddition[i].Id == $scope.contractAddition.ContractAdditionTypeId) {
                        $scope.contractAdditionTypeCode = $scope.OBKContractAddition[i].Code;
                        break;
                    }
                }

            }
        }, function (response) {

        });
    }

    $scope.changeViewMode = function () {
        // 1 Черновик
        if ($scope.object.Status == 1) {
            $scope.object.viewMode = false;
        }
            // 7 На корректировке у заявителя
        else if ($scope.object.Status == 7) {
            $scope.object.viewMode = false;
            $scope.showComments = true;
        }
        else {
            $scope.object.viewMode = true;
        }
    }

    $scope.saveBtnClick = function () {
        var formValid = $scope.contracAdditiontForm.$valid;
        var filesValid = $scope.checkFileValidation();
        if (formValid && filesValid) {
            $scope.editProject();
        }
        else {
            alert("Заполните обязательные поля!");
        }
    }

    $scope.sendWithoutDigitalSign = function () {
        var formValid = $scope.contracAdditiontForm.$valid;
        var filesValid = $scope.checkFileValidation();
        if (formValid && filesValid) {
            var modalInstance = $uibModal.open({
                templateUrl: '/Project/Agreement',
                controller: modalSendContractAddition,
                scope: $scope,
                size: 'size-custom'
            });
        }
        else {
            alert("Заполните обязательные поля и загрузите вложения!");
        }
    }

    $scope.sendWithDigitalSign = function () {
        var formValid = $scope.contracAdditiontForm.$valid;
        var filesValid = $scope.checkFileValidation();
        if (formValid && filesValid) {
            $scope.doSign();
        }
        else {
            alert("Заполните обязательные поля и загрузите вложения!");
        }
    }

    $scope.doSign = function () {
        var id = $scope.contractAddition.Id;
        if (id) {

        }
    }

    $scope.checkFileValidation = function () {
        var invalidFiles = 0;

        var containerName = "";

        if ($scope.contractAdditionTypeCode == 3) {
            containerName = "#filesBank";
        }
        else if ($scope.contractAdditionTypeCode == 2) {
            containerName = "#filesManager";
        }
        else if ($scope.contractAdditionTypeCode == 1 && $scope.declarant.IsResident == true) {
            containerName = "#filesAddressResident";
        }
        else if ($scope.contractAdditionTypeCode == 1 && $scope.declarant.IsResident == false) {
            containerName = "#filesAddressNonResident";
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

    $scope.validatePhone = function () {
        if (!$scope.contracAdditiontForm.Phone.$valid && $scope.object.Phone && $scope.object.Phone.length > 0) {
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
        if (!$scope.contracAdditiontForm.BankBik.$valid && $scope.object.BankBik && $scope.object.BankBik.length > 0) {
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
        if (!$scope.contracAdditiontForm.BankIik.$valid && $scope.object.BankIik && $scope.object.BankIik.length > 0) {
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

    $scope.object = {};
    $scope.object.Status = 1;
    $scope.declarant = {};
    $scope.contractAddition = {};
    loadDictionary($scope, 'OBKContractAddition', $http);
    loadDictionary($scope, 'OpfType', $http);
    loadDictionary($scope, 'Country', $http);
    loadDictionary($scope, 'Currency', $http);
    loadObkRefTypes($scope, $http);
    loadContractSigners($scope, $http);
    loadExpertOrganizations($scope, $http);
    $scope.BoolDic = [{
        Id: true,
        Name: "Да"
    }, {
        Id: false,
        Name: "Нет"
    }];
    loadDictionaryOBKContractDocumentType($scope, $http);
    $scope.loadContracts();

    $scope.object.viewMode = false;

    // Patterns start
    $scope.emailPattern = ".+@.+\\..+";
    $scope.iikPattern = "[a-z0-9]+";
    $scope.bankBikPattern = "[a-z0-9]+";
    $scope.bankBikMinLength = 8;
    $scope.bankBikMaxLength = 11;
    $scope.phonePattern = "[0-9 ()+,]+";
    // Patterns end

    var projectId = $("#projectId").val();
    if (projectId) {
        $scope.loadContractAddition();
    }
}

function modalSendContractAddition($scope, $http, $window, $uibModalInstance) {
    $scope.sendProject = function () {
        var projectId = $scope.contractAddition.Id;
        if (projectId) {

        }
    };

    $scope.cancelSend = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

angular
    .module('app')
    .controller('obkContracAdditiontForm', ['$scope', '$http', '$interval', '$uibModal', '$window', obkContracAdditiontForm])