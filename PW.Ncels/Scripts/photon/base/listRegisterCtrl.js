function ModalregisterInstanceCtrl($scope, $uibModalInstance) {
    
    $scope.ok = function () {
        $uibModalInstance.close();
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}


function ModalSendProject($scope, $http, $uibModalInstance) {
    var type = $scope.object.Drug.Type;
    $scope.sendProject = function () {
        $http({
            url: '/Project/SendProjectRegister',
            method: 'POST',
            data: JSON.stringify($scope.object)
        }).success(function (response) {
            if (type == 0) {
                window.location.href = '/Project/RegisterLsDetails/' + $scope.object.Drug.Id;
            } else if (type == 1) {
                window.location.href = '/Project/ReRegisterLsDetails/' + $scope.object.Drug.Id;
            } else {
                window.location.href = '/Project/ChRegisterLsDetails/' + $scope.object.Drug.Id;
            }
        });
    };

    $scope.cancelSend = function () {
        $uibModalInstance.dismiss('cancel');
    };
}
function doSign() {
    $.blockUI({ message: '<h1><img src="../../Content/css/plugins/slick/ajax-loader.gif"/> Идет подпись отчета...</h1>', css: { opacity: 1 } });
    signXmlCall(function () {
        var model = { preambleId: $("#paramcontroller").val(), xmlAuditForm: $("#Certificate").val() };
        $.ajax({
            url: '/Project/SignForm',
            type: "POST",
            dataType: 'json',
            contentType: "application/json",
            async: false,
            data: JSON.stringify(model),
            success: function (data) {
                if (data.success) {
                    $("#signBtn").attr('disabled', 'disabled');
     
                }
//                    window.location = data.url;
                else {
                    $("#formCertValidation").show();
                }
                $.unblockUI();
//                window.location.reload();
            },
            error: function (data) {
                $.unblockUI();
            }
        });
    }, $("#hfXmlToSign").val());
}
function registerLsGrid($scope, DTColumnBuilder, $http, $uibModal) {
    $scope.BoolDic = [{
        Id: true,
        Name: "Да"
    }, {
        Id: false,
        Name: "Нет"
    }];
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
    ];

    var id = $("#projectId").val();
    $scope.object = { Package: { Id: null } };

    $scope.setSelectedCode = function (list, name, id) {
        angular.forEach($scope[list], function (value, key) {
            if (value.Id === id) {
                $scope[name] = value.Code;
            }
        });
    }

    $scope.setSelectedAtx = function (id) {
        angular.forEach($scope.Atx, function (value, key) {
            if (value.Id === id) {
                $scope.object.Drug.AtxCode = value.Code;
                $scope.object.Drug.AtxNameKz = value.NameKz;
                $scope.object.Drug.AtxNameRu = value.Name;
            }
        });
    }

    $scope.setContractDate = function (list, name, id) {
        angular.forEach($scope[list], function (value, key) {
            if (value.Id === id) {
                $scope[name] = value.EndDate;
            }
        });
    }

    $scope.editProject = function () {

        $http({
            url: '/Project/RegisterLsSave',
            method: 'POST',
            data: JSON.stringify($scope.object)
        }).success(function (response) {
            alert('Ок');
            $scope.isEnableDownload = true;
        });
    }
    $scope.sendProject = function () {

        $http({
            url: '/Project/SendProjectRegister',
            method: 'POST',
            data: JSON.stringify($scope.object)
        }).success(function (response) {
            window.location.href = '/Project/RegisterLsDetails/' + $scope.object.Drug.Id;
            alert('Ок');
        });
    }
    $scope.sign = function () {
        $("#paramcontroller").val(id);
        startSign('/Project/SignOperation',id);
        /*   var modalInstance = $uibModal.open({
               templateUrl: '/Home/ModalSing',
               controller: ModalregisterInstanceCtrl
           });*/
    };


    $scope.view = function (id) {
        var modalInstance = $uibModal.open({
            templateUrl: '/Upload/FileView?id=' + id,
            controller: ModalregisterInstanceCtrl
        });
    };

    $scope.sendAgree = function () {
        var modalInstance = $uibModal.open({
            templateUrl: '/Project/Agreement',
            controller: ModalSendProject,
            scope:$scope
        });
    };

    $scope.cancelSend = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.addOrganizationType121 = function () {
        $scope.object.Export = angular.copy($scope.defaultOrganizationType12);
    }
    $scope.addOrganizationType13 = function () {
        $scope.object.Organization = angular.copy($scope.defaultOrganizationType13);
    }
    $scope.addOrganizationType14 = function () {
        $scope.object.Manufacture = angular.copy($scope.defaultOrganizationType14);
    }
    $scope.addPackage = function () {
        calculateCount($scope);
        $scope.object.Package = angular.copy($scope.defaultPackage);
    }
    $scope.addComposition = function () {
        $scope.object.Composition = angular.copy($scope.defaultComposition);
    }
    $http({
        method: 'GET',
        url: '/Project/LoadRegisterLs?id=' + id,
        data: 'JSON'
    }).success(function (response) {
        $scope.object = response;
        $scope.defaultOrganizationType12 = $scope.object.Export;
        $scope.defaultOrganizationType13 = $scope.object.Organization;
        $scope.defaultOrganizationType14 = $scope.object.Manufacture;
        $scope.defaultPackage = $scope.object.Package;
        $scope.defaultComposition = $scope.object.Composition;
        $scope.object.Export = angular.copy($scope.defaultOrganizationType12);
        $scope.object.Organization = angular.copy($scope.defaultOrganizationType13);
        $scope.object.Manufacture = angular.copy($scope.defaultOrganizationType14);
        $scope.object.Package = angular.copy($scope.defaultPackage);
        $scope.object.Composition = angular.copy($scope.defaultComposition);
    });
    $scope.selectGridOrganizationType12 = function (data) {
        console.log(data);
        $scope.object.Export = data;
    }
    $scope.selectGridOrganizationType13 = function (data) {
        console.log(data);
        $scope.object.Organization = data;
    }
    $scope.selectGridOrganizationType14 = function (data) {
        console.log(data);
        $scope.object.Manufacture = data;
    }
    $scope.selectGridPackage = function (data) {
        console.log(data);
        $scope.object.Package = data;
    }
    $scope.selectGridComposition = function (data) {
        console.log(data);
        $scope.object.Composition = data;
    }


    $scope.deleteOrganizationType12 = function () {
        $http({
            url: '/Organization/OrganizationDelete',
            method: 'POST',
            data: JSON.stringify($scope.object.Export)
        }).success(function (response) {
            $scope.reloadGridOrganizationType12();
            $scope.addOrganizationType121();
        });

    }
    $scope.saveOrganizationType12 = function () {
        $http({
            url: '/Organization/OrganizationSave',
            method: 'POST',
            data: JSON.stringify($scope.object.Export)
        }).success(function (response) {
            $scope.addOrganizationType121();
            $scope.reloadGridOrganizationType12();
            alert('Ок');
        });
    }
    $scope.deleteOrganizationType13 = function () {
        $http({
            url: '/Organization/OrganizationDelete',
            method: 'POST',
            data: JSON.stringify($scope.object.Organization)
        }).success(function (response) {
            $scope.reloadGridOrganizationType13();
            $scope.addOrganizationType13();
        });

    }
    $scope.saveOrganizationType13 = function () {
        $http({
            url: '/Organization/OrganizationSave',
            method: 'POST',
            data: JSON.stringify($scope.object.Organization)
        }).success(function (response) {
            $scope.addOrganizationType13();
            $scope.reloadGridOrganizationType13();
            alert('Ок');
        });
    }
    $scope.deleteOrganizationType14 = function () {
        $http({
            url: '/Organization/OrganizationDelete',
            method: 'POST',
            data: JSON.stringify($scope.object.Manufacture)
        }).success(function (response) {
            $scope.reloadGridOrganizationType14();
            $scope.addOrganizationType14();
        });

    }
    $scope.saveOrganizationType14 = function () {
        $http({
            url: '/Organization/OrganizationSave',
            method: 'POST',
            data: JSON.stringify($scope.object.Manufacture)
        }).success(function (response) {
            $scope.addOrganizationType14();
            $scope.reloadGridOrganizationType14();
            alert('Ок');
        });
    }
    $scope.deletePackage = function () {
        $http({
            url: '/Packag/PackageDelete',
            method: 'POST',
            data: JSON.stringify($scope.object.Package)
        }).success(function (response) {
            $scope.reloadGridPackage();
            $scope.addPackage();
        });

    }
    $scope.savePackage = function () {
        $http({
            url: '/Packag/PackageSave',
            method: 'POST',
            data: JSON.stringify($scope.object.Package)
        }).success(function (response) {
            $scope.defaultPackage.Id = response;
            $scope.addPackage();
            $scope.reloadGridPackage();
            alert('Ок');
        });
    }
    $scope.deleteComposition = function () {
        $http({
            url: '/Composition/CompositionDelete',
            method: 'POST',
            data: JSON.stringify($scope.object.Composition)
        }).success(function (response) {
            $scope.reloadGridComposition();
            $scope.addComposition();
        });

    }
    $scope.saveComposition = function () {
        $http({
            url: '/Composition/CompositionSave',
            method: 'POST',
            data: JSON.stringify($scope.object.Composition)
        }).success(function (response) {
            $scope.addComposition();
            $scope.reloadGridComposition();
            alert('Ок');
        });
    }
    $http({
        method: 'GET',
        url: '/Contract/GetContract',
        data: 'JSON'
    }).success(function (result) {
        $scope.ContractId = result;
    });
    loadDictionary($scope, 'Atx', $http);
    loadDictionary($scope, 'Country', $http);
    loadDictionary($scope, 'ImnSecuryType', $http);
    loadDictionary($scope, 'Currency', $http);
    loadDictionary($scope, 'IntroducingMethod', $http);
    loadDictionary($scope, 'LsType', $http);
    loadDictionary($scope, 'RefPriceType', $http);
    loadDictionary($scope, 'ManufactureType', $http);
    loadDictionary($scope, 'BestBeforeMeasureType', $http);
    loadDictionary($scope, 'OrgManufactureType', $http);
    loadDictionary($scope, 'AccelerationType', $http);
    loadDictionary($scope, 'MaterialOrigin', $http);
    loadDictionary($scope, 'MaterialGain', $http);
    loadDictionary($scope, 'MaterialName', $http);
    loadDictionary($scope, 'MaterialType', $http);
    loadDictionary($scope, 'MeasureType', $http);
    loadDictionary($scope, 'PackageName', $http);
    loadDictionary($scope, 'PackageType', $http);
    loadDictionary($scope, 'IntroducingMethod', $http);
    loadDictionary($scope, 'SaleType', $http);
    loadDictionary($scope, 'LsType2', $http);
    loadDictionary($scope, 'LsType', $http);
}

function reRegisterLsGrid($scope, DTColumnBuilder, $http, $uibModal) {
    $scope.BoolDic = [{
        Id: true,
        Name: "Да"
    }, {
        Id: false,
        Name: "Нет"
    }];
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
    ];

    $scope.setSelectedCode = function (list, name, id) {
        angular.forEach($scope[list], function (value, key) {
            if (value.Id === id) {
                $scope[name] = value.Code;
            }
        });
    }

    $scope.setSelectedAtx = function (id) {
        angular.forEach($scope.Atx, function (value, key) {
            if (value.Id === id) {
                $scope.object.Drug.AtxCode = value.Code;
                $scope.object.Drug.AtxNameKz = value.NameKz;
                $scope.object.Drug.AtxNameRu = value.Name;
            }
        });
    }

    $scope.setContractDate = function (list, name, id) {
        angular.forEach($scope[list], function (value, key) {
            if (value.Id === id) {
                $scope[name] = value.EndDate;
            }
        });
    }

    var id = $("#projectId").val();
    $scope.object = {};

    $scope.editProject = function () {

        $http({
            url: '/Project/ReRegisterLsSave',
            method: 'POST',
            data: JSON.stringify($scope.object)
        }).success(function (response) {
            alert('Ок');
            $scope.isEnableDownload = true;
        });
    }

    $scope.sendProject = function () {

        $http({
            url: '/Project/SendProjectRegister',
            method: 'POST',
            data: JSON.stringify($scope.object)
        }).success(function (response) {
            alert('Ок');
        });
    }
    $scope.open = function () {

        var modalInstance = $uibModal.open({
            templateUrl: '/Home/ModalSing',
            controller: ModalregisterInstanceCtrl
        });
    };


    $scope.view = function (id) {
        var modalInstance = $uibModal.open({
            templateUrl: '/Upload/FileView?id=' + id,
            controller: ModalregisterInstanceCtrl
        });
    };

    $scope.sendAgree = function () {
        var modalInstance = $uibModal.open({
            templateUrl: '/Project/Agreement',
            controller: ModalSendProject,
            scope: $scope
        });
    };

    $scope.addOrganizationType15 = function () {
        $scope.object.Export = angular.copy($scope.defaultOrganizationType15);
    }
    $scope.addOrganizationType17 = function () {
        $scope.object.Manufacture = angular.copy($scope.defaultOrganizationType17);
    }
    $scope.addOrganizationType16 = function () {
        $scope.object.Organization = angular.copy($scope.defaultOrganizationType16);
    }
    $scope.addPackage = function () {
        calculateCount($scope);
        $scope.object.Package = angular.copy($scope.defaultPackage);
    }
    $scope.addComposition = function () {
        $scope.object.Composition = angular.copy($scope.defaultComposition);
    }
    $http({
        method: 'GET',
        url: '/Project/LoadReRegisterLs?id=' + id,
        data: 'JSON'
    }).success(function (response) {
        $scope.object = response;
        $scope.defaultOrganizationType15 = $scope.object.Export;
        $scope.defaultOrganizationType17 = $scope.object.Manufacture;
        $scope.defaultOrganizationType16 = $scope.object.Organization;
        $scope.defaultPackage = $scope.object.Package;
        $scope.defaultComposition = $scope.object.Composition;
        addOrganizationType15();
        addOrganizationType16();
        addOrganizationType17();
        addPackage();
        addComposition();
    });

    $scope.selectGridOrganizationType15 = function (data) {
        console.log(data);
        $scope.object.Export = data;
    }
    $scope.selectGridOrganizationType17 = function (data) {
        console.log(data);
        $scope.object.Manufacture = data;
    }
    $scope.selectGridOrganizationType16 = function (data) {
        console.log(data);
        $scope.object.Organization = data;
    }
    $scope.selectGridPackage = function (data) {
        console.log(data);
        $scope.object.Package = data;
    }
    $scope.selectGridComposition = function (data) {
        console.log(data);
        $scope.object.Composition = data;
    }


    $scope.deleteOrganizationType15 = function () {
        $http({
            url: '/Organization/OrganizationDelete',
            method: 'POST',
            data: JSON.stringify($scope.object.Export)
        }).success(function (response) {
            $scope.reloadGridOrganizationType15();
            $scope.addOrganizationType15();
        });

    }
    $scope.saveOrganizationType15 = function () {
        $http({
            url: '/Organization/OrganizationSave',
            method: 'POST',
            data: JSON.stringify($scope.object.Export)
        }).success(function (response) {
            $scope.addOrganizationType15();
            $scope.reloadGridOrganizationType15();
            alert('Ок');
        });
    }
    $scope.deleteOrganizationType17 = function () {
        $http({
            url: '/Organization/OrganizationDelete',
            method: 'POST',
            data: JSON.stringify($scope.object.Manufacture)
        }).success(function (response) {
            $scope.reloadGridOrganizationType17();
            $scope.addOrganizationType17();
        });

    }
    $scope.saveOrganizationType17 = function () {
        $http({
            url: '/Organization/OrganizationSave',
            method: 'POST',
            data: JSON.stringify($scope.object.Manufacture)
        }).success(function (response) {
            $scope.addOrganizationType17();
            $scope.reloadGridOrganizationType17();
            alert('Ок');
        });
    }
    $scope.deleteOrganizationType16 = function () {
        $http({
            url: '/Organization/OrganizationDelete',
            method: 'POST',
            data: JSON.stringify($scope.object.Organization)
        }).success(function (response) {
            $scope.reloadGridOrganizationType16();
            $scope.addOrganizationType16();
        });

    }
    $scope.saveOrganizationType16 = function () {
        $http({
            url: '/Organization/OrganizationSave',
            method: 'POST',
            data: JSON.stringify($scope.object.Organization)
        }).success(function (response) {
            $scope.addOrganizationType16();
            $scope.reloadGridOrganizationType16();
            alert('Ок');
        });
    }
    $scope.deletePackage = function () {
        $http({
            url: '/Packag/PackageDelete',
            method: 'POST',
            data: JSON.stringify($scope.object.Package)
        }).success(function (response) {
            $scope.reloadGridPackage();
            $scope.addPackage();
        });

    }
    $scope.savePackage = function () {
        $http({
            url: '/Packag/PackageSave',
            method: 'POST',
            data: JSON.stringify($scope.object.Package)
        }).success(function (response) {
            $scope.addPackage();
            $scope.reloadGridPackage();
            alert('Ок');
        });
    }
    $scope.deleteComposition = function () {
        $http({
            url: '/Composition/CompositionDelete',
            method: 'POST',
            data: JSON.stringify($scope.object.Composition)
        }).success(function (response) {
            $scope.reloadGridComposition();
            $scope.addComposition();
        });

    }
    $scope.saveComposition = function () {
        $http({
            url: '/Composition/CompositionSave',
            method: 'POST',
            data: JSON.stringify($scope.object.Composition)
        }).success(function (response) {
            $scope.addComposition();
            $scope.reloadGridComposition();
            alert('Ок');
        });
    }
    $http({
        method: 'GET',
        url: '/Contract/GetContract',
        data: 'JSON'
    }).success(function (result) {
        $scope.ContractId = result;
    });
    loadDictionary($scope, 'Atx', $http);
    loadDictionary($scope, 'Country', $http);
    loadDictionary($scope, 'ImnSecuryType', $http);
    loadDictionary($scope, 'Currency', $http);
    loadDictionary($scope, 'IntroducingMethod', $http);
    loadDictionary($scope, 'LsType', $http);
    loadDictionary($scope, 'RefPriceType', $http);
    loadDictionary($scope, 'ManufactureType', $http);
    loadDictionary($scope, 'BestBeforeMeasureType', $http);
    loadDictionary($scope, 'OrgManufactureType', $http);
    loadDictionary($scope, 'AccelerationType', $http);
    loadDictionary($scope, 'MaterialGain', $http);
    loadDictionary($scope, 'MaterialName', $http);
    loadDictionary($scope, 'MaterialType', $http);
    loadDictionary($scope, 'MeasureType', $http);
    loadDictionary($scope, 'PackageName', $http);
    loadDictionary($scope, 'PackageType', $http);
    loadDictionary($scope, 'IntroducingMethod', $http);
    loadDictionary($scope, 'SaleType', $http);
    loadDictionary($scope, 'LsType2', $http);
    loadDictionary($scope, 'LsType', $http);

    $scope.dtColumns10 = [
        DTColumnBuilder.newColumn("reg_number", "Номер регистрационного удостоверения"),
        DTColumnBuilder.newColumn("C_int_name", "МНН"),
        DTColumnBuilder.newColumn("name", "Наименование ЛС"),
        DTColumnBuilder.newColumn("concentration", "Концентрация"),
        DTColumnBuilder.newColumn("dosage_value", "Дозировка"),
        DTColumnBuilder.newColumn("C_dosage_form_name", "Лекарственная форма"),
        DTColumnBuilder.newColumn("C_atc_code", "АТХ код")
    ];

    $scope.selectGridIntegration = function (data) {
        console.log(data);
        $scope.curentReg = data;
    }

    $scope.SetObjectReg = function () {
        $scope.object.Drug.RegDocNumber = $scope.curentReg.reg_number;
        $scope.object.Drug.NameRu = $scope.curentReg.name;
        $scope.object.Drug.RegDate = $scope.curentReg.reg_date;
        $scope.object.Drug.MnnRu = $scope.curentReg.C_int_name;
        $scope.object.Drug.Dosage = $scope.curentReg.dosage_value;
        $scope.object.Drug.AtxCode = $scope.curentReg.C_atc_code;
        alert('Данные заполнены');
    }
}

function chRegisterLsGrid($scope, DTColumnBuilder, $http, $uibModal) {
    $scope.BoolDic = [{
        Id: true,
        Name: "Да"
    }, {
        Id: false,
        Name: "Нет"
    }];
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
    DTColumnBuilder.newColumn("Summary", "Краткое содержание").withOption('name', 'Number')
    ];

    $scope.setSelectedAtx = function (id) {
        angular.forEach($scope.Atx, function (value, key) {
            if (value.Id === id) {
                $scope.object.Drug.AtxCode = value.Code;
                $scope.object.Drug.AtxNameKz = value.NameKz;
                $scope.object.Drug.AtxNameRu = value.Name;
            }
        });
    }

    $scope.setSelectedCode = function (list, name, id) {
        angular.forEach($scope[list], function (value, key) {
            if (value.Id === id) {
                $scope[name] = value.Code;
            }
        });
    }

    $scope.setContractDate = function (list, name, id) {
        angular.forEach($scope[list], function (value, key) {
            if (value.Id === id) {
                $scope[name] = value.EndDate;
            }
        });
    }

    var id = $("#projectId").val();
    $scope.object = {};

    $scope.editProject = function () {

        $http({
            url: '/Project/ChRegisterLsSave',
            method: 'POST',
            data: JSON.stringify($scope.object)
        }).success(function (response) {
            alert('Ок');
            $scope.isEnableDownload = true;
        });
    }
    $scope.sendProject = function () {

        $http({
            url: '/Project/SendProjectRegister',
            method: 'POST',
            data: JSON.stringify($scope.object)
        }).success(function (response) {
            alert('Ок');
        });
    }
    $scope.open = function () {

        var modalInstance = $uibModal.open({
            templateUrl: '/Home/ModalSing',
            controller: ModalregisterInstanceCtrl
        });
    };


    $scope.view = function (id) {
        var modalInstance = $uibModal.open({
            templateUrl: '/Upload/FileView?id=' + id,
            controller: ModalregisterInstanceCtrl
        });
    };

    $scope.sendAgree = function () {
        var modalInstance = $uibModal.open({
            templateUrl: '/Project/Agreement',
            controller: ModalSendProject,
            scope: $scope
        });
    };

    $scope.addOrganizationType18 = function () {
        $scope.object.Export = angular.copy($scope.defaultOrganizationType18);
    }
    $scope.addOrganizationType20 = function () {
        $scope.object.Manufacture = angular.copy($scope.defaultOrganizationType20);
    }
    $scope.addOrganizationType19 = function () {
        $scope.object.Organization = angular.copy($scope.defaultOrganizationType19);
    }
    $scope.addPackage = function () {
        calculateCount($scope);
        $scope.object.Package = angular.copy($scope.defaultPackage);
    }
    $scope.addComposition = function () {
        $scope.object.Composition = angular.copy($scope.defaultComposition);
    }
    $scope.addChanges = function () {
        $scope.object.Changes = angular.copy($scope.defaultChanges);
    }
    $http({
        method: 'GET',
        url: '/Project/LoadChRegisterLs?id=' + id,
        data: 'JSON'
    }).success(function (response) {
        $scope.object = response;
        $scope.defaultOrganizationType18 = $scope.object.Export;
        $scope.defaultOrganizationType20 = $scope.object.Manufacture;
        $scope.defaultOrganizationType19 = $scope.object.Organization;
        $scope.defaultPackage = $scope.object.Package;
        $scope.defaultComposition = $scope.object.Composition;
        $scope.defaultChanges = $scope.object.Changes;
        addOrganizationType18();
        addOrganizationType19();
        addOrganizationType20();
        addPackage();
        addComposition();
        addChanges();
    });


    $scope.selectGridOrganizationType18 = function (data) {
        console.log(data);
        $scope.object.Export = data;
    }
    $scope.selectGridOrganizationType20 = function (data) {
        console.log(data);
        $scope.object.Manufacture = data;
    }
    $scope.selectGridOrganizationType19 = function (data) {
        console.log(data);
        $scope.object.Organization = data;
    }
    $scope.selectGridPackage = function (data) {
        console.log(data);
        $scope.object.Package = data;
    }
    $scope.selectGridComposition = function (data) {
        console.log(data);
        $scope.object.Composition = data;
    }
    $scope.selectGridChanges = function (data) {
        console.log(data);
        $scope.object.Changes = data;
    }

    $scope.deleteChanges = function () {
        $http({
            url: '/Chang/ChangeDelete',
            method: 'POST',
            data: JSON.stringify($scope.object.Changes)
        }).success(function (response) {
            $scope.reloadGridChanges();
            $scope.addChanges();
        });

    }
    $scope.saveChanges = function () {
        $http({
            url: '/Chang/ChangeSave',
            method: 'POST',
            data: JSON.stringify($scope.object.Changes)
        }).success(function (response) {
            $scope.addChanges();
            $scope.reloadGridChanges();
            alert('Ок');
        });
    }
    $scope.deleteOrganizationType18 = function () {
        $http({
            url: '/Organization/OrganizationDelete',
            method: 'POST',
            data: JSON.stringify($scope.object.Export)
        }).success(function (response) {
            $scope.reloadGridOrganizationType18();
            $scope.addOrganizationType18();
        });

    }
    $scope.saveOrganizationType18 = function () {
        $http({
            url: '/Organization/OrganizationSave',
            method: 'POST',
            data: JSON.stringify($scope.object.Export)
        }).success(function (response) {
            $scope.addOrganizationType18();
            $scope.reloadGridOrganizationType18();
            alert('Ок');
        });
    }
    $scope.deleteOrganizationType20 = function () {
        $http({
            url: '/Organization/OrganizationDelete',
            method: 'POST',
            data: JSON.stringify($scope.object.Manufacture)
        }).success(function (response) {
            $scope.reloadGridOrganizationType20();
            $scope.addOrganizationType20();
        });

    }
    $scope.saveOrganizationType20 = function () {
        $http({
            url: '/Organization/OrganizationSave',
            method: 'POST',
            data: JSON.stringify($scope.object.Manufacture)
        }).success(function (response) {
            $scope.addOrganizationType20();
            $scope.reloadGridOrganizationType20();
            alert('Ок');
        });
    }
    $scope.deleteOrganizationType19 = function () {
        $http({
            url: '/Organization/OrganizationDelete',
            method: 'POST',
            data: JSON.stringify($scope.object.Organization)
        }).success(function (response) {
            $scope.reloadGridOrganizationType19();
            $scope.addOrganizationType19();
        });

    }
    $scope.saveOrganizationType19 = function () {
        $http({
            url: '/Organization/OrganizationSave',
            method: 'POST',
            data: JSON.stringify($scope.object.Organization)
        }).success(function (response) {
            $scope.addOrganizationType19();
            $scope.reloadGridOrganizationType19();
            alert('Ок');
        });
    }
    $scope.deletePackage = function () {
        $http({
            url: '/Packag/PackageDelete',
            method: 'POST',
            data: JSON.stringify($scope.object.Package)
        }).success(function (response) {
            $scope.reloadGridPackage();
            $scope.addPackage();
        });

    }
    $scope.savePackage = function () {
        $http({
            url: '/Packag/PackageSave',
            method: 'POST',
            data: JSON.stringify($scope.object.Package)
        }).success(function (response) {
            $scope.addPackage();
            $scope.reloadGridPackage();
            alert('Ок');
        });
    }
    $scope.deleteComposition = function () {
        $http({
            url: '/Composition/CompositionDelete',
            method: 'POST',
            data: JSON.stringify($scope.object.Composition)
        }).success(function (response) {
            $scope.reloadGridComposition();
            $scope.addComposition();
        });

    }
    $scope.saveComposition = function () {
        $http({
            url: '/Composition/CompositionSave',
            method: 'POST',
            data: JSON.stringify($scope.object.Composition)
        }).success(function (response) {
            $scope.addComposition();
            $scope.reloadGridComposition();
            alert('Ок');
        });
    }
    $http({
        method: 'GET',
        url: '/Contract/GetContract',
        data: 'JSON'
    }).success(function (result) {
        $scope.ContractId = result;
    });
    loadDictionary($scope, 'Atx', $http);
    loadDictionary($scope, 'Country', $http);
    loadDictionary($scope, 'ImnSecuryType', $http);
    loadDictionary($scope, 'Currency', $http);
    loadDictionary($scope, 'IntroducingMethod', $http);
    loadDictionary($scope, 'LsType', $http);
    loadDictionary($scope, 'RefPriceType', $http);
    loadDictionary($scope, 'ManufactureType', $http);
    loadDictionary($scope, 'BestBeforeMeasureType', $http);
    loadDictionary($scope, 'OrgManufactureType', $http);
    loadDictionary($scope, 'AccelerationType', $http);
    loadDictionary($scope, 'MaterialGain', $http);
    loadDictionary($scope, 'MaterialName', $http);
    loadDictionary($scope, 'MaterialType', $http);
    loadDictionary($scope, 'MeasureType', $http);
    loadDictionary($scope, 'PackageName', $http);
    loadDictionary($scope, 'PackageType', $http);
    loadDictionary($scope, 'IntroducingMethod', $http);
    loadDictionary($scope, 'SaleType', $http);
    loadDictionary($scope, 'LsType2', $http);
    loadDictionary($scope, 'LsType', $http);

    $scope.dtColumns10 = [
        DTColumnBuilder.newColumn("reg_number", "Номер регистрационного удостоверения"),
        DTColumnBuilder.newColumn("C_int_name", "МНН"),
        DTColumnBuilder.newColumn("name", "Наименование ЛС"),
        DTColumnBuilder.newColumn("concentration", "Концентрация"),
        DTColumnBuilder.newColumn("dosage_value", "Дозировка"),
        DTColumnBuilder.newColumn("C_dosage_form_name", "Лекарственная форма"),
        DTColumnBuilder.newColumn("C_atc_code", "АТХ код")
    ];
    

    $scope.selectGridIntegration = function (data) {
        console.log(data);
        $scope.curentReg = data;
    }

    $scope.SetObjectReg = function () {
        $scope.object.Drug.RegDocNumber = $scope.curentReg.reg_number;
        $scope.object.Drug.NameRu = $scope.curentReg.name;
        $scope.object.Drug.RegDate = $scope.curentReg.reg_date;
        $scope.object.Drug.MnnRu = $scope.curentReg.C_int_name;
        $scope.object.Drug.Dosage = $scope.curentReg.dosage_value;
        $scope.object.Drug.AtxCode = $scope.curentReg.C_atc_code;
        alert('Данные заполнены');
    }
}


function calculateCount($scope) {
    if ($scope.object.Package.Count != null) {
        $scope.countLs *= $scope.object.Package.Count;
        $scope.isCountLsEnable = true;
    }
}

angular
    .module('app')
    .controller('registerLsGrid', ['$scope', 'DTColumnBuilder', '$http', '$uibModal', registerLsGrid])
    .controller('reRegisterLsGrid', ['$scope', 'DTColumnBuilder', '$http', '$uibModal', reRegisterLsGrid])
    .controller('chRegisterLsGrid', ['$scope', 'DTColumnBuilder', '$http', '$uibModal', chRegisterLsGrid]);