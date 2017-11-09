function actionsContractListHtmlAction(data, type, full, meta, $scope) {
    debugger;
    if (data == null) {
        data = "б/н";
    }
    if (full.Type == 1)
        return '<a  class="pw-task-link" href="/Contract/ContractDetails?id=' + full.Id + '&listAction=' + $scope.listAction + '" >' + data + '</a>';
    else if (full.Type == 2)
        return '<a  class="pw-task-link" href="/Contract/ContractAdditionDetails?id=' + full.Id + '&listAction=' + $scope.listAction + '" >' + data + '</a>';
    return '';
}

function loadDictionaryByOrderYear($scope, name, $http) {
    $http({
        method: 'GET',
        url: '/Dictionaries/GetDicByOrderYear',
        data: 'JSON',
        params: { type: name }
    }).success(function (result) {
        $scope[name] = result;
    });
}

function loadDictionaryCurrency($scope, $http) {
    $http({
        method: 'GET',
        url: '/Dictionaries/GetCurrencies',
        data: 'JSON'
    }).success(function (result) {
        $scope["Currency"] = result;
        result.forEach(function (curr) {
            $scope["Currency"][curr.Id] = curr;
        });
    });
}

function ModalSendContract($scope, $http, $uibModalInstance) {
    debugger;
    $scope.sendProject = function () {
        var contractId;
        if ($scope.object.ContractAddition)
            contractId = $scope.object.ContractAddition.Id;
        else
            contractId = $scope.object.Contract.Id;
        $http({
            url: '/Contract/SendContractInProcessing?id=' + contractId,
            method: 'POST'
        }).success(function (response) {
            debugger;
            if ($scope.object.ContractAddition) {
                $scope.object.ContractAddition.StatusId = response.Id;
                $scope.object.ContractAddition.ContractStatus = response;
            } else {
                $scope.object.Contract.StatusId = response.Id;
                $scope.object.Contract.ContractStatus = response;
            }
            $uibModalInstance.close();
        });
    };

    $scope.cancelSend = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function actionsContractSignedAction(data, type, full, meta) {

    if (full.Type == 0) {
        return '<a  class="pw-task-link" href="/Contract/ContractDetails?id=' + full.DocumentId + '" >' +
            data +
            '</a>';
    }
    if (full.Type == 1) {
        return '<a  class="pw-task-link" href="/Contract/ContractExDetails?id=' + full.DocumentId + '" >' +
            data +
            '</a>';
    }
    return '';

}

function actionsContractSignAction(data, type, full, meta) {

    return '<button  class="btn btn-success" onclick="signTask(' + "'" + full.Id + "'" + ')" >' +
        'Подписать' +
        '</button>';

}

function signTask(id) {
    $.ajax({
        type: 'POST',
        url: '/Contract/SigningConfirm?taskId=' + id,
        success: function (result) {
            alert("Ok!");
        }
    });
}

function contractForm($scope, DTColumnBuilder, $http, $uibModal) {
    var contractHolderChangedModal = null;
    $scope.emailPattern = ".+@.+\\..+";
    $scope.phoneMaxLength = 30;
    $scope.phoneMinLength = 6;
    $scope.phoneNumberPattern = "[0-9]*";
    var id = $("#projectId").val();
    $scope.object = {};
    $scope.selection = {
        payerType: null,
        payerTranslation: null
    };
    $scope.manufacturesSource = {
        selected: null,
        page: 1,
        items: [],
        lastPage: 2,
        loading: false
    };
    $scope.applicantSource = {
        selected: null,
        page: 1,
        items: [],
        lastPage: 2,
        loading: false
    };
    $scope.holderSource = {
        selected: null,
        page: 1,
        items: [],
        lastPage: 2,
        loading: false
    };
    $scope.payerSource = {
        selected: null,
        page: 1,
        items: [],
        lastPage: 2,
        loading: false
    };
    $scope.payerTranslationSource = {
        selected: null,
        page: 1,
        items: [],
        lastPage: 2,
        loading: false
    };
    $scope.fetchOrganizations = function ($select, sourceName, $event) {
        if (!$event) {
            $scope[sourceName].page = 1;
            $scope[sourceName].items = [];
            $scope[sourceName].lastPage = 1;
        } else {
            $event.stopPropagation();
            $event.preventDefault();
            $scope[sourceName].page++;
        }
        if ($select.search === "") return;
        $scope[sourceName].loading = true;

        $http({
            method: 'GET',
            url: '/Dictionaries/Organizations',
            params: {
                search: $select.search,
                page: $scope[sourceName].page
            }
        }).then(function (resp) {
            $scope[sourceName].items = $scope[sourceName].items.concat(resp.data.items);
            $scope[sourceName].lastPage = resp.data.lastPage;
        })['finally'](function () {
            $scope[sourceName].loading = false;
        });
    }
    $scope.fetchOrganization = function (sourceName, currentOrg, copySpecificFieldsFn) {
        debugger;
        var source = $scope[sourceName];
        $http({
            method: 'GET',
            url: '/Dictionaries/GetOrganization',
            params: {
                id: source.selected.Id
            }
        }).then(function (resp) {
            if (resp.data) {
                $scope.copyOrg(resp.data, currentOrg, copySpecificFieldsFn);
                switch (sourceName) {
                    case 'payerSource':
                        setPayerType('payerType', 'Payer');
                        break;
                    case 'payerTranslationSource':
                        setPayerType('payerTranslation', 'PayerTranslation');
                        break;
                };
            }
        });
    }

    $scope.copyOrg = function (sourceOrg, destOrg, copySpecificFieldsFn) {
        angular.copy(sourceOrg, destOrg);
        if (copySpecificFieldsFn) {
            copySpecificFieldsFn(sourceOrg, destOrg);
        }
    }
    $scope.copyManufactureFields = function (sourceOrg, destOrg) {
        $scope.setSelectedCode('OpfType', 'manufactureOpfType', destOrg.OpfTypeDicId);
        $scope.setSelectedCode('Country', 'manufactureCountry', destOrg.CountryDicId);
    }
    $scope.copyApplicantFields = function (sourceOrg, destOrg) {
        $scope.setSelectedCode('OpfType', 'applicantOpfType', destOrg.OpfTypeDicId);
        $scope.setSelectedCode('Country', 'applicantCountry', destOrg.CountryDicId);
    }
    $scope.copyHolderFields = function (sourceOrg, destOrg) {
        $scope.setSelectedCode('OpfType', 'holderOpfType', destOrg.OpfTypeDicId);
        $scope.setSelectedCode('Country', 'holderCountry', destOrg.CountryDicId);
    }

    $scope.contractHolderChanged = function ($select) {
        debugger;
        $scope.object.Contract.HolderTypeCode = $select.selected.Code;
        if ($select.selected.Code === "holder") {
            contractHolderChangedModal = $uibModal.open({
                templateUrl: 'contractHolderChanged.html',
                controller: contractHolderChangedModalController,
                scope: $scope,
                size: 'size-custom'
            });
        }
    }

    $scope.BoolDic = [{
        Id: true,
        Name: "Да"
    }, {
        Id: false,
        Name: "Нет"
    }];
    $scope.PayerType = [
        {
            Id: 1,
            Name: 'Производитель',
            Code: 'manufacture'

        },
        {
            Id: 2,
            Name: 'Заявитель',
            Code: 'applicant'

        },
        {
            Id: 3,
            Name: 'Держатель регистрационного удостоверения',
            Code: 'holder'

        },
        {
            Id: 4,
            Name: 'Третье лицо',
            Code: 'other'

        }
    ];

    $scope.dtColumns = [
        DTColumnBuilder.newColumn("CreateDate", "Дата").withOption('name', 'CreateDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("Comment", "Замечание").withOption('name', 'Comment'),
        DTColumnBuilder.newColumn("Author", "Автор").withOption('name', 'Author').notSortable()
    ];

    $scope.setSelectedCode = function (list, name, id) {
        angular.forEach($scope[list], function (value, key) {
            if (value.Id === id) {
                $scope[name] = value.Code;
            }
        });
    }

    $scope.selectPayer = function (payerType, payer) {
        var payerCode = payerType.Code;
        var source;
        if (payerCode === 'manufacture') {
            source = $scope.object.Manufacture;
        }
        else if (payerCode === 'applicant') {
            source = $scope.object.Applicant;
        }
        else if (payerCode === 'holder') {
            source = $scope.object.Holder;
        }
        if (source) {
            $scope.copyOrg(source, payer);
        }
        else {
            payer.NameRu = '';
            payer.AddressLegal = '';
            payer.BankName = '';
            payer.BankIik = '';
            payer.PaymentBill = '';
            payer.BankCurencyDicId = '';
            payer.BankSwift = '';
            payer.BankBik = '';
            payer.Payer.Bin = '';
            payer.Payer.Iin = '';
            payer.Payer.BossPosition = '';
            payer.Payer.BossLastName = '';
            payer.Payer.BossFirstName = '';
            payer.Payer.BossMiddleName = '';
            payer.Payer.OpfTypeDicId = null;
            payer.Payer.CountryDicId = null;
        }
    }

    function getEqualOrgs(org, orgType) {
        var orgs = [];
        if ($scope.object.Manufacture.Id === org.Id && orgType !== "manufacture")
            orgs.push({
                org: $scope.object.Manufacture,
                copyField: $scope.copyManufactureFields
            });
        if ($scope.object.Applicant.Id === org.Id && orgType !== "applicant")
            orgs.push({
                org: $scope.object.Applicant,
                copyField: $scope.copyApplicantFields
            });
        if ($scope.object.Holder.Id === org.Id && orgType !== "holder")
            orgs.push({
                org: $scope.object.Holder,
                copyField: $scope.copyHolderFields
            });
        if ($scope.object.Payer.Id === org.Id && orgType !== "payer")
            orgs.push({
                org: $scope.object.Payer
            });
        if ($scope.object.PayerTranslation.Id === org.Id && orgType !== "payerTranslation")
            orgs.push({
                org: $scope.object.PayerTranslation
            });
        return orgs;
    }
    $scope.syncOrgs = function (sourceOrg, orgType) {
        var equalOrgs = getEqualOrgs(sourceOrg, orgType);
        equalOrgs.forEach(function (destOrg) {
            var org = destOrg.org;
            org.OpfTypeDicId = sourceOrg.OpfTypeDicId;
            org.NameRu = sourceOrg.NameRu;
            org.NameEn = sourceOrg.NameEn;
            org.CountryDicId = sourceOrg.CountryDicId;
            org.AddressLegal = sourceOrg.AddressLegal;
            org.AddressFact = sourceOrg.AddressFact;
            org.Phone = sourceOrg.Phone;
            org.Email = sourceOrg.Email;
            org.BossLastName = sourceOrg.BossLastName;
            org.BossFirstName = sourceOrg.BossFirstName;
            org.BossMiddleName = sourceOrg.BossMiddleName;
            org.BossPosition = sourceOrg.BossPosition;
            org.BankName = sourceOrg.BankName;
            org.BankIik = sourceOrg.BankIik;
            org.PaymentBill = sourceOrg.PaymentBill;
            org.BankCurencyDicId = sourceOrg.BankCurencyDicId;
            org.BankSwift = sourceOrg.BankSwift;
            org.BankBik = sourceOrg.BankBik;
            org.Bin = sourceOrg.Bin;
            org.Iin = sourceOrg.Iin;

            if (org.copyField) {
                org.copyField(sourceOrg, org);
            }
        });
    }

    $scope.editProject = function () {
        if ($scope.contractCreateForm.$valid) {
            $http({
                url: '/Contract/ContractSave',
                method: 'POST',
                data: JSON.stringify($scope.object)
            }).success(function (response) {
                debugger;
                $scope.object.Contract.StatusId = response.Id;
                $scope.object.Contract.ContractStatus = response;
                alert('Ок');
            });
            $scope.isSendProjectVisible = true;
            $scope.isEnableDownload = true;
        } else {
            alert("Заполните обязательные поля");
        }
    }
    $scope.sendProject = function () {

        $http({
            url: '/Contract/SendProjectContract',
            method: 'POST',
            data: JSON.stringify($scope.object)
        }).success(function (response) {
            alert('Ок');
        });
    }

    $scope.viewContract = function (id) {
        debugger;
        var modalInstance = $uibModal.open({
            templateUrl: '/Contract/ContractTemplate?id=' + id,
            controller: ModalregisterInstanceCtrl
        });
    };

    $scope.open = function () {
        var modalInstance = $uibModal.open({
            templateUrl: '/Home/ModalSing',
            controller: ModalregisterInstanceCtrl
        });
    };

    $scope.sendInProcessing = function () {
        var modalInstance = $uibModal.open({
            templateUrl: '/Project/Agreement',
            controller: ModalSendContract,
            scope: $scope,
            size: 'size-custom'
        });
    };

    $http({
        method: 'GET',
        url: '/Contract/LoadContract?id=' + id,
        data: 'JSON'
    }).success(function (response) {
        $scope.object = response;
        setPayerType('payerType', 'Payer');
        setPayerType('payerTranslation', 'PayerTranslation');
    });
    function setPayerType(payerTypeField, payerField) {
        if ($scope.object.Manufacture.Id == $scope.object[payerField].Id) {
            $scope.selection[payerTypeField] = $scope.PayerType[0];
        }
        else if ($scope.object.Applicant.Id == $scope.object[payerField].Id) {
            $scope.selection[payerTypeField] = $scope.PayerType[1];
        }
        else if ($scope.object.Holder.Id == $scope.object[payerField].Id) {
            $scope.selection[payerTypeField] = $scope.PayerType[2];
        } else {
            $scope.selection[payerTypeField] = $scope.PayerType[3];
        }
    }
    loadDictionary($scope, 'ContractHolderType', $http);
    loadDictionary($scope, 'OpfType', $http);
    loadDictionary($scope, 'Country', $http);
    loadDictionaryByOrderYear($scope, 'Currency', $http);
    loadDictionary($scope, 'DoverennostType', $http);
    $http({
        method: 'GET',
        url: '/Contract/GetSigners',
        data: 'JSON'
    }).success(function (result) {
        debugger;
        $scope.Signers = result;
    });

}

function contractGrid($scope, DTColumnBuilder) {
    function renderNumFunc(data, type, full, meta) {
        return actionsContractListHtmlAction(data, type, full, meta, $scope);
    };
    $scope.dtColumns = [
        DTColumnBuilder.newColumn("Number", "№ договора").withOption('name', 'Number').renderWith(renderNumFunc),
        DTColumnBuilder.newColumn("CreatedDate", "Дата").withOption('name', 'CreatedDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("StatusName", "Статус").withOption('name', 'StatusName'),
        DTColumnBuilder.newColumn("ManufactureOrgName", "Производитель").withOption('name', 'ManufactureOrgName')
    ];
}

function contractCompletedGrid($scope, DTColumnBuilder) {
    function renderNumFunc(data, type, full, meta) {
        return actionsContractListHtmlAction(data, type, full, meta, $scope);
    };
    $scope.dtColumns = [
        DTColumnBuilder.newColumn("Number", "№ договора").withOption('name', 'Number').renderWith(renderNumFunc),
        DTColumnBuilder.newColumn("CreatedDate", "Дата").withOption('name', 'CreatedDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("ManufactureOrgName", "Производитель").withOption('name', 'ManufactureOrgName')
    ];
}

function contractHolderChangedModalController($scope, $uibModalInstance) {
    $scope.fillManufacturerFromHolder = function () {
        $uibModalInstance.close();
        $scope.copyOrg($scope.object.Holder, $scope.object.Manufacture, $scope.copyManufactureFields);
    };

    $scope.cancelFillManufacturerFromHolder = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function contractDetail($scope, DTColumnBuilder, $http, $uibModal) {
    $scope.canSign = true;
    $scope.view = function (id) {
        var modalInstance = $uibModal.open({
            templateUrl: '/Contract/ContractTemplate?id=' + id,
            controller: ModalregisterInstanceCtrl
        });
    };
    $scope.dtColumnsCor = [
        DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("DocumentDate", "Дата").withOption('name', 'Number').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("Summary", "Краткое содержание").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("Id", "Файлы").renderWith(actionsLoadFiles)

    ];
    $scope.dtColumns = [
        DTColumnBuilder.newColumn("CreateDate", "Дата").withOption('name', 'CreateDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("Comment", "Замечание").withOption('name', 'Comment'),
        DTColumnBuilder.newColumn("Author", "Автор").withOption('name', 'Author').notSortable()
    ];


    $scope.sendProject = function (id) {
        $http({
            url: '/Contract/SendProjectContractById',
            method: 'POST',
            data: JSON.stringify({ id: id })
        }).success(function (response) {
            alert('Ок');
        });
    }
    $scope.sign = function (id) {
        debugger;
        startSign('/Contract/SignData', id, signData);

        function signData() {
            debugger;
            $.blockUI({ message: '<h1><img src="../../Content/css/plugins/slick/ajax-loader.gif"/> Выполняется подпись...</h1>', css: { opacity: 1 } });
            signXmlCall(function () {
                $http({
                    url: '/Contract/SignContract',
                    method: 'POST',
                    data: JSON.stringify({ contractId: id, signedData: $("#Certificate").val() })
                }).success(function (response) {
                    $scope.canSign = false;
                    $.unblockUI();
                }).error(function () {
                    $.unblockUI();
                });
            });
        };
    }
}

function contracAdditiontForm($scope, DTColumnBuilder, $http, $uibModal) {
    var id = $("#projectId").val();
    $scope.object = {};
    $scope.contractAdditionChanged = function (contractAdditionType) {
        debugger;
        if (contractAdditionType.Code === "1")
            $scope.object.AttachCodes = ["0", "1"];
        if (contractAdditionType.Code === "2")
            $scope.object.AttachCodes = ["0", "2"];
        if (contractAdditionType.Code === "3")
            $scope.object.AttachCodes = ["0", "3", "4"];
        if (contractAdditionType.Code === "4")
            $scope.object.AttachCodes = ["0", "5"];
        else
            $scope.object.AttachCodes["0"];
    }

    $scope.dtColumnsCor = [
        DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("DocumentDate", "Дата").withOption('name', 'Number').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("Summary", "Краткое содержание").withOption('name', 'Number'),
    ];
    $scope.dtColumns = [
        DTColumnBuilder.newColumn("CreateDate", "Дата").withOption('name', 'CreateDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("Comment", "Замечание").withOption('name', 'Comment'),
        DTColumnBuilder.newColumn("Author", "Автор").withOption('name', 'Author').notSortable()
    ];

    $scope.editProject = function () {
        debugger;
        if ($scope.contracAdditiontForm.$valid) {
            $http({
                url: '/Contract/ContractAdditionSave',
                method: 'POST',
                data: JSON.stringify($scope.object.ContractAddition)
            }).success(function (response) {
                debugger;
                $scope.object.ContractAddition.StatusId = response.Id;
                $scope.object.ContractAddition.ContractStatus = response;
                alert('Ок');
            });
            $scope.isSendProjectVisible = true;
            $scope.isEnableDownload = true;
        } else {
            alert("Заполните обязательные поля");
        }
    }
    $scope.sendInProcessing = function () {
        var modalInstance = $uibModal.open({
            templateUrl: '/Project/Agreement',
            controller: ModalSendContract,
            scope: $scope,
            size: 'size-custom'
        });
    };

    $scope.viewContract = function (id) {
        debugger;
        var modalInstance = $uibModal.open({
            templateUrl: '/Contract/ContractTemplate?id=' + id,
            controller: ModalregisterInstanceCtrl
        });
    };

    $http({
        method: 'GET',
        url: '/Contract/LoadContractAddition?id=' + id,
        data: 'JSON'
    }).success(function (response) {
        debugger;
        $scope.object = response;
        $scope.isEnableDownload = $scope.object.ContractSaved;
    });
    $http({
        method: 'GET',
        url: '/Contract/GetContract',
        data: 'JSON'
    }).success(function (result) {
        $scope.ContractId = result;
    });
    loadDictionary($scope, 'ContractAddition', $http);
    $http({
        method: 'GET',
        url: '/Contract/GetSigners',
        data: 'JSON'
    }).success(function (result) {
        debugger;
        $scope.Signers = result;
    });

}

angular
    .module('app')
    .controller('contractForm', ['$scope', 'DTColumnBuilder', '$http', '$uibModal', contractForm])
    .controller('contracAdditiontForm', ['$scope', 'DTColumnBuilder', '$http', '$uibModal', contracAdditiontForm])
    .controller('contractDetail', ['$scope', 'DTColumnBuilder', '$http', '$uibModal', contractDetail])
    .controller('contractGrid', ['$scope', 'DTColumnBuilder', contractGrid])
    .controller('contractCompletedGrid', ['$scope', 'DTColumnBuilder', contractCompletedGrid]);
