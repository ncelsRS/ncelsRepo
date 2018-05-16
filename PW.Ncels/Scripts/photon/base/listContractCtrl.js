function deleteContract(id) {
    var success = function () {
        window.location.href = '/Contract/Delete/' + id + '?returnAction=' + $('#contractListMode').val();
    }
    var cancel = function () {
    };
    showConfirmation("Сохранить", "Вы уверены, что хотите удалить черновик договора?", success, cancel);
}
function actionsContractListNumberHtmlAction(data, type, full, meta, $scope) {
    debugger;   
    if (data == null) {
        return "б/н";
    }    
    return data;
}
function actionsContractListHtmlAction(data, type, full, meta, $scope) {
    var editBtn = "";
    var deleteBtn = "";
    debugger;
    if (full.StatusCode === '0' || full.StatusCode === '1') {
        editBtn = '<li class="btn-warning">' 
            + (full.Type === 1 ?'<a href="/Contract/Contract?id=' + full.Id + '&listAction=' + $scope.listAction
                : '<a href="/Contract/ContractAddition?id=' + full.Id + '&listAction=' + $scope.listAction)
            + '" class="link-object" ><span class="glyphicon glyphicon-edit" aria-hidden="true"></span> Редактировать</a></li>';

    }
    if (full.StatusCode === '0') {
        deleteBtn = "<li class=\"btn-danger\"><a href=\"#\" onclick=\"deleteContract('" + full.Id + "');\">" +
            '<span class="glyphicon glyphicon-remove" aria-hidden="true"></span> Удалить</a></li>' +
            '';

    }
    return '<div class="btn-group" style="float: right;margin-right: 40px">' +
            '<button type="button" class="btn btn-success dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Операции <span class="caret"></span></button>' +
            '<ul class="dropdown-menu btnmenu"> ' +
            '<li class="btn-info" ><a '
             + (full.Type===1? 'href="/Contract/ContractDetails?id=' + full.Id + '&listAction=' + $scope.listAction
                : 'href="/Contract/ContractAdditionDetails?id=' + full.Id + '&listAction=' + $scope.listAction)
             + '" class="link-object"><span class="glyphicon glyphicon-search" aria-hidden="true"></span> Просмотр</a></li>' +
            editBtn +
            deleteBtn + '</ul></div>';    
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
    $scope.requiredAttachCodes = ['2', '3', '5'];
    $scope.applicantIsManufacture = false;
    $scope.applicantIsManufactureChanged = function (applicantIsManufacture) {
        debugger;
        if (applicantIsManufacture === true) {
            $scope.copyOrg($scope.object.Manufacture, $scope.object.Applicant, $scope.copyApplicantFields);
        } else {
            $scope.clearOrgFields('Applicant', $scope.object.NewApplicantId);
        }
    };
    //DatePicker    
    $scope.datePicker = {
        doverennostCreatedDateStatus: { opened: false },
        doverennostCreatedDateOpen: function ($event) {
            $scope.datePicker.doverennostCreatedDateStatus.opened = true;
        },
        doverennostCreatedDateOptions: {                        
        },
        doverennostExpiryDateStatus: { opened: false },
        doverennostExpiryDateOpen: function ($event) {
            $scope.datePicker.doverennostExpiryDateStatus.opened = true;
        },
        doverennostExpiryDateOptions: {
        },
        agentDocCreatedDateStatus: { opened: false },
        agentDocCreatedDateOpen: function ($event) {
            $scope.datePicker.agentDocCreatedDateStatus.opened = true;
        },
        agentDocCreatedDateOptions: {
        },
        agentDocExpiryDateStatus: { opened: false },
        agentDocExpiryDateOpen: function ($event) {
            $scope.datePicker.agentDocExpiryDateStatus.opened = true;
        },
        agentDocExpiryDateOptions: {
        }
    };
    $scope.clearOrgFields = function (org, newId) {
        if (newId)
            $scope.object[org].Id = newId;
        $scope.object[org].NameRu = null;
        $scope.object[org].NameEn = null;
        $scope.object[org].AddressLegal = null;
        $scope.object[org].AddressFact = null;
        $scope.object[org].Phone = null;
        $scope.object[org].Email = null;
        $scope.object[org].BankName = null;
        $scope.object[org].BankIik = null;
        $scope.object[org].PaymentBill = null;
        $scope.object[org].BankCurencyDicId = null;
        $scope.object[org].BankSwift = null;
        $scope.object[org].BankBik = null;
        $scope.object[org].Bin = null;
        $scope.object[org].Iin = null;
        $scope.object[org].BossPosition = null;
        $scope.object[org].BossLastName = null;
        $scope.object[org].BossFirstName = null;
        $scope.object[org].BossMiddleName = null;
        $scope.object[org].OpfTypeDicId = null;
        $scope.object[org].CountryDicId = null;
        $scope.agentAttachValidation();
    }
    $scope.clearOrgTabData = function (orgSource, org, orgType, newId) {
        orgSource.selected = null;
        $scope.clearOrgFields(org, newId);
        $scope.syncOrgs($scope.object[org], orgType);
    }
    $scope.emailPattern = ".+@.+\\..+";
    $scope.phoneMaxLength = 30;
    $scope.phoneMinLength = 6;
    $scope.phoneNumberPattern = "[0-9]*";
    $scope.enName = "[ A-Za-z_0-9@./#&+\\-,;!?><|*'\"«»]*";
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
    $scope.agentSource = {
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
    $scope.fetchOrganization = function (sourceName, currentOrg, copySpecificFieldsFn, orgType) {
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
                var equalOrgs = getEqualOrgs(currentOrg, orgType);
                $scope.copyOrg(resp.data, currentOrg, copySpecificFieldsFn);
                equalOrgs.forEach(function (destOrg) {
                    $scope.copyOrg(resp.data, destOrg.org, destOrg.copyField);
                });
                switch (sourceName) {
                    case 'payerSource':
                        setPayerType('payerType', 'Payer');
                        break;
                    case 'payerTranslationSource':
                        setPayerType('payerTranslation', 'PayerTranslation');
                        break;
                };
                $scope.agentAttachValidation();
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
    $scope.copyAgentFields = function (sourceOrg, destOrg) {
        $scope.setSelectedCode('OpfType', 'agentOpfType', destOrg.OpfTypeDicId);
        $scope.setSelectedCode('Country', 'agentCountry', destOrg.CountryDicId);
    }

    $scope.contractHolderChanged = function ($select) {
        debugger;
        $scope.object.Contract.HolderTypeCode = $select.selected.Code;
        if ($select.selected.Code === "holder") {
            $scope.requiredAttachCodes = ['2', '3', '4', '5'];
            contractHolderChangedModal = $uibModal.open({
                templateUrl: 'contractHolderChanged.html',
                controller: contractHolderChangedModalController,
                scope: $scope,
                size: 'size-custom'
            });
        } else {
            $scope.requiredAttachCodes = ['2', '3', '5'];
        }
        $scope.agentAttachValidation();
    }

    $scope.agentAttachValidation = function () {        
        if (($scope.object.Agent.NameRu && $scope.object.Agent.NameRu !== '') || ($scope.object.Agent.NameEn && $scope.object.Agent.NameEn !== '')) {
            if ($scope.requiredAttachCodes.indexOf('6') < 0)
                $scope.requiredAttachCodes.push('6');
        } else {
            var index = $scope.requiredAttachCodes.indexOf('6');
            if (index > -1) {
                $scope.requiredAttachCodes.splice(index, 1);
            }
        }
    };

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
        //{
        //    Id: 4,
        //    Name: 'Доверенное лицо',
        //    Code: 'agent'

        //},
        {
            Id: 5,
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
        debugger;
        var payerCode = payerType.Code;
        var source;
        var countryCodeField;
        if (payerCode === 'manufacture') {
            source = $scope.object.Manufacture;
            countryCodeField = 'manufactureCountry';
        }
        else if (payerCode === 'applicant') {
            source = $scope.object.Applicant;
            countryCodeField = 'applicantCountry';
        }
        else if (payerCode === 'holder') {
            source = $scope.object.Holder;
            countryCodeField = 'holderCountry';
        }
        else if (payerCode === 'agent') {
            source = $scope.object.Agent;
            countryCodeField = 'agentCountry';
        }
        if (source) {
            $scope.copyOrg(source, payer);
            if ($scope[countryCodeField] === 'KZ') {
                payer.IsResident = true;
            } else {
                payer.IsResident = false;
            }
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
            payer.Bin = '';
            payer.Iin = '';
            payer.BossPosition = '';
            payer.BossLastName = '';
            payer.BossFirstName = '';
            payer.BossMiddleName = '';
            payer.OpfTypeDicId = null;
            payer.CountryDicId = null;
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
        if ($scope.object.Agent.Id === org.Id && orgType !== "agent")
            orgs.push({
                org: $scope.object.Agent,
                copyField: $scope.copyAgentFields
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
        debugger;
        $('.file-validation').text('');
        $('[name="contractCreateForm"] .nav-tabs a').removeClass('invalid-tab');
        var filesValid = checkAttachFile();
        if ($scope.contractCreateForm.$valid && filesValid) {
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
            function showErrors(errors) {
                angular.forEach(errors, function (error) {
                    if (error.$name && error.$name != '') {
                        var tabPaneId = $($('[name="' + error.$name + '"]').closest('.tab-pane')).attr('id');
                        $('.nav-tabs a[href="#' + tabPaneId + '"]').addClass('invalid-tab');
                    }
                });
            }
            showErrors($scope.contractCreateForm.$error.required);
            showErrors($scope.contractCreateForm.$error.pattern);
            if (!filesValid)
                $('.nav-tabs a[href="#tab-7"]').addClass('invalid-tab');
        }
    }
    function checkAttachFile() {
        var validFile = true;
        $('.file-validation').each(function () {
            var ct = $(this).attr('countFile');
            var attcode = $(this).attr('attcode');
            var count = parseInt(ct, 10) || 0;
            if (count === 0 && $scope.requiredAttachCodes.includes(attcode)) {
                $(this).text("Необходимо вложить файлы");
                validFile = false;
            } else {
                $(this).text("");
            }
        });
        return validFile;
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
        if ($scope.object.Manufacture.Id == $scope.object.Applicant.Id) {
            $scope.applicantIsManufacture = true;
        }
        if ($scope.object.Contract.DoverennostCreatedDate) {
            $scope.object.Contract.DoverennostCreatedDate = new Date($scope.object.Contract.DoverennostCreatedDate);
        }
        if ($scope.object.Contract.DoverennostExpiryDate) {
            $scope.object.Contract.DoverennostExpiryDate = new Date($scope.object.Contract.DoverennostExpiryDate);
        }
        if ($scope.object.Contract.AgentDocCreateDate) {
            $scope.object.Contract.AgentDocCreateDate = new Date($scope.object.Contract.AgentDocCreateDate);
        }
        if ($scope.object.Contract.AgentDocExpiryDate) {
            $scope.object.Contract.AgentDocExpiryDate = new Date($scope.object.Contract.AgentDocExpiryDate);
        }
        $scope.agentAttachValidation();
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
        } /*else if ($scope.object.Agent.Id == $scope.object[payerField].Id) {
            $scope.selection[payerTypeField] = $scope.PayerType[3];
        }*/
        else {
            $scope.selection[payerTypeField] = $scope.PayerType[3];
        }
    }
    loadDictionary($scope, 'ContractHolderType', $http);
    loadDictionary($scope, 'OpfType', $http);
    loadDictionary($scope, 'Country', $http, true);
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
        return actionsContractListNumberHtmlAction(data, type, full, meta, $scope);
    };
    function renderActionFunc(data, type, full, meta) {
        return actionsContractListHtmlAction(data, type, full, meta, $scope);
    };
    $scope.dtColumns = [
        DTColumnBuilder.newColumn("Number", "№ договора").withOption('name', 'Number').renderWith(renderNumFunc),
        DTColumnBuilder.newColumn("CreatedDate", "Дата").withOption('name', 'CreatedDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("StatusName", "Статус").withOption('name', 'StatusName'),
        DTColumnBuilder.newColumn("ManufactureOrgName", "Производитель").withOption('name', 'ManufactureOrgName'),
        DTColumnBuilder.newColumn("Id", "").withOption('name', 'Id').renderWith(renderActionFunc)
    ];
}

function contractCompletedGrid($scope, DTColumnBuilder) {
    function renderNumFunc(data, type, full, meta) {
        return actionsContractListNumberHtmlAction(data, type, full, meta, $scope);
    };
    function renderActionFunc(data, type, full, meta) {
        return actionsContractListHtmlAction(data, type, full, meta, $scope);
    };
    $scope.dtColumns = [
        DTColumnBuilder.newColumn("Number", "№ договора").withOption('name', 'Number').renderWith(renderNumFunc),
        DTColumnBuilder.newColumn("CreatedDate", "Дата").withOption('name', 'CreatedDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("ManufactureOrgName", "Производитель").withOption('name', 'ManufactureOrgName'),
        DTColumnBuilder.newColumn("Id", "").withOption('name', 'Id').renderWith(renderActionFunc)
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
    $scope.BoolDic = [{
        Id: true,
        Name: "Да"
    }, {
        Id: false,
        Name: "Нет"
    }];
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
        DTColumnBuilder.newColumn("Summary", "Краткое содержание").withOption('name', 'Number')
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
