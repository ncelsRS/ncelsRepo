function updateCalculation($scope, $http) {
    $scope.object = {};

    var id = $("#calculationId").val();
    $http({
        method: 'GET',
        url: '/Expertise/Calculation/GetData?id=' + id,
        data: 'JSON'
    }).success(function (result) {
        $scope.object = result;
        $scope.object.Date = new Date(parseInt(result.Date.slice(6, -2)));
    });


    $scope.update = function () {
        $http({
            url: '/Expertise/Calculation/Update',
            method: 'POST',
            data: JSON.stringify($scope.object)
        }).success(function (response) {
            if (!response.IsError) {
                window.location = '/Home/Success';
            } else {
                alert(response.Message);
            }
        });
    }
}

function createCalculation($scope, $http, notify) {
    loadEnums($scope, 'CalculationType', $http);

    $scope.generateCalculation = function () {
        if ($scope.calculationForm.$valid || $scope.editCalculationForm.$valid) {

            $scope.calculation.Cost = parseFloat($scope.calculation.Cost);
            $scope.calculation.Pir = parseFloat($scope.calculation.Pir);
            $scope.calculation.MrpPriv = parseFloat($scope.calculation.MrpPriv);
            $scope.calculation.MrpCalc = parseFloat($scope.calculation.MrpCalc);
            $scope.calculation.PirCalc = parseFloat($scope.calculation.PirCalc);
            $scope.calculation.Norm = parseFloat($scope.calculation.Norm);
            $scope.calculation.NdsPercent = parseFloat($scope.calculation.NdsPercent);
            $scope.calculation.CostNds = parseFloat($scope.calculation.CostNds);
            $http({
                url: '/Expertise/Calculation/Generate',
                method: 'POST',
                data: JSON.stringify($scope.calculation)
            }).success(function (response) {
                if (!response.IsError) {

                    notify({
                        message: 'Расчет сгенерирован!', classes: 'alert-info', templateUrl: '/Content/templates/notify.html'
                    });
                } else {
                    alert(response.Message);
                }
            });
        }
    }
    $scope.createCalculation = function () {
        if ($scope.calculationForm.$valid) {
            $scope.calculation.Cost = parseFloat($scope.calculation.Cost);
            $scope.calculation.Pir = parseFloat($scope.calculation.Pir);
            $scope.calculation.MrpPriv = parseFloat($scope.calculation.MrpPriv);
            $scope.calculation.MrpCalc = parseFloat($scope.calculation.MrpCalc);
            $scope.calculation.PirCalc = parseFloat($scope.calculation.PirCalc);
            $scope.calculation.Norm = parseFloat($scope.calculation.Norm);
            $scope.calculation.NdsPercent = parseFloat($scope.calculation.NdsPercent);
            $scope.calculation.CostNds = parseFloat($scope.calculation.CostNds);
            $http({
                url: '/Expertise/Calculation/Create',
                method: 'POST',
                data: JSON.stringify($scope.calculation)
            }).success(function (response) {
                if (!response.IsError) {
                    notify({ message: 'Расчет исполнен!', classes: 'alert-info', templateUrl: '/Content/templates/notify.html' });
                    $scope.initCalculationUpdate(response.Id);

                } else {
                    alert(response.Message);
                }
            });
        }
    }
    $scope.updateCalculation = function () {
        if ($scope.editCalculationForm.$valid) {
            $scope.calculation.Cost = parseFloat($scope.calculation.Cost);
            $scope.calculation.Pir = parseFloat($scope.calculation.Pir);
            $scope.calculation.MrpPriv = parseFloat($scope.calculation.MrpPriv);
            $scope.calculation.MrpCalc = parseFloat($scope.calculation.MrpCalc);
            $scope.calculation.PirCalc = parseFloat($scope.calculation.PirCalc);
            $scope.calculation.Norm = parseFloat($scope.calculation.Norm);
            $scope.calculation.NdsPercent = parseFloat($scope.calculation.NdsPercent);
            $scope.calculation.CostNds = parseFloat($scope.calculation.CostNds);
            $http({
                url: '/Expertise/Calculation/Update',
                method: 'POST',
                data: JSON.stringify($scope.calculation)
            }).success(function (response) {
                if (!response.IsError) {
                    notify({ message: 'Расчет изменен!', classes: 'alert-info', templateUrl: '/Content/templates/notify.html' });

                    $scope.htmlContentReportIn = $scope.htmlContentReportInTemp;
                } else {
                    alert(response.Message);
                }
            });
        }
    }
    $scope.initCalculation = function (attach, taskId, projectId) {
        $scope.calculation = {
            Date: new Date(),
            TaskId: taskId,
            ProjectId: projectId,
            Year: 2016,
            MrpPriv: 1982
        };
        $scope.calculation.AttachPath = attach;
        loadDictionary($scope, 'CalculationPeriodDict', $http);
        loadDictionary($scope, 'CalculationMrpDict', $http);
    }
    $scope.initCalculationUpdate = function (taskId) {
        $http({
            method: 'GET',
            url: '/Expertise/Calculation/Details?id=' + taskId,
            data: 'JSON'
        }).success(function (result) {
            $scope.htmlContentReportIn = result;
            $scope.htmlContentReportInTemp = result;
        });
        $http({
            method: 'GET',
            url: '/Expertise/Calculation/ReadCalculation?id=' + taskId,
            data: 'JSON'
        }).success(function (result) {
            $scope.calculation = result;
            $scope.calcType = result.CalculationTypeCode;
            loadDictionary($scope, 'CalculationPeriodDict', $http);
            loadDictionary($scope, 'CalculationMrpDict', $http);
        });
    }
    $scope.detailCalculation = function () {
        $scope.htmlContentReportIn = '';
    }

    $scope.getExecutors = function () {
        $http({
            method: 'GET',
            url: '/Dictionaries/GetExecutorByProject?id=' + $scope.calculation.ProjectId,
            data: 'JSON'
        }).success(function (result) {
            $scope.Executors = result;
        });
    }

    $scope.agreement = function () {
        $scope.getExecutors();
        $scope.agreementObj = {};
        $scope.agreementObj.Text = '';
        $scope.agreementObj.ExecutionDate = new Date();
        $scope.agreementObj.ProjectId = $scope.calculation.ProjectId;
        $scope.agreementObj.Id = $scope.calculation.TaskId;
        $scope.agreementObj.Executors = [];
        $http({
            method: 'GET',
            url: '/Expertise/Agreement/Create',
            data: 'JSON',
            params: { taskId: $scope.calculation.TaskId, projectId: $scope.calculation.ProjectId }
        }).success(function (result) {
            $scope.htmlContentReportIn = result;
        });

    };

    $scope.createAgreement = function () {
        $http({
            url: '/Expertise/Agreement/Create',
            method: 'POST',
            data: JSON.stringify($scope.agreementObj)
        }).success(function (response) {
            if (!response.IsError) {
                notify({ message: 'Расчет отправлен на согласование!', classes: 'alert-info', templateUrl: '/Content/templates/notify.html' });
                $scope.htmlContentReportIn = '<p>Все хорошо</p>';
            } else {
                alert(response.Message);
            }
        });

    };

    $scope.detail = function () {
        $http({
            method: 'GET',
            url: '/Expertise/Calculation/Details?id=' + $scope.calculation.Id,
            data: 'JSON'
        }).success(function (result) {
            $scope.htmlContentReportIn = result;
        });
    }
    $scope.editCalculation = function () {
        $http({
            method: 'GET',
            url: '/Expertise/Calculation/Edit?id=' + $scope.calculation.Id,
            data: 'JSON'
        }).success(function (result) {
            $scope.htmlContentReportIn = result;
        });
    }

    $scope.setPirCalc = function () {
        if ($scope.calculation.Pir != '' && $scope.calculation.MrpPriv != '' && $scope.calculation.MrpCalc != '' && $scope.calculation.MrpCalc != 0) {
            var pir = $scope.calculation.Pir;
            var mrpPriv = $scope.calculation.MrpPriv;
            var mrpCalc = $scope.calculation.MrpCalc;
            var norm;
            $scope.calculation.PirCalc = (pir * mrpPriv / mrpCalc).toFixed(3);
            var pirCalc = $scope.calculation.PirCalc;
            if (pirCalc <= 3682.08) {
                $scope.calculation.Norm = (30.28 + ((pirCalc - 3682.08) / ((0 - 3682.08) * (0 - 30.28)))).toFixed(2);
                norm = $scope.calculation.Norm;
            } else {
                $scope.calculation.Norm = (1.1431 * (Math.pow(pirCalc, 0.39906))).toFixed(2);
                norm = $scope.calculation.Norm;
            }
            var psod = 23982;
            $scope.calculation.Cost = (psod * norm).toFixed(2);
            var cost = $scope.calculation.Cost;
            $scope.calculation.CostNds = (cost * 1.12).toFixed(2);
            $scope.calculation.Nds = (cost * 0.12).toFixed(2);
        }
    }

    $scope.setCode = function (calcType) {
        $scope.calcType = calcType;
    }

    $scope.setDefault = function (list, res, code) {
        $scope.calcType = code;
        angular.forEach($scope[list], function (value, key) {
            if (value.Code === 'second') {
                $scope.calculation.CalculationTypeId = value.Id;
            }
        });
    }



    function loadDictionary($scope, name, $http) {
        $http({
            method: 'GET',
            url: '/Dictionaries/GetCalculation',
            data: 'JSON',
            params: { type: name }
        }).success(function (result) {
            $scope[name] = result;
        });
    }

    $scope.setMrpCalc = function (name, id) {
        $http({
            method: 'GET',
            url: '/Dictionaries/GetCalculation',
            data: 'JSON',
            params: { type: name, id: id }
        }).success(function (result) {
            $scope.calculation.MrpCalc = result[0].Code;
        });
    }


    $scope.spinOption1003 = {
        postfix: 'тысяч тенге',
        verticalbuttons: true,
        min: 0,
        max: 9999999999,
        step: 0.001,
        decimals: 3,
        boostat: 5,
        maxboostedstep: 100,
    }

    $scope.spinOption1002 = {
        postfix: 'тысяч тенге',
        verticalbuttons: true,
        min: 0,
        max: 9999999999,
        step: 0.01,
        decimals: 2,
        boostat: 5,
        maxboostedstep: 100
    }
    $scope.spinOption1 = {
        postfix: 'тенге',
        verticalbuttons: true,
        min: 0,
        max: 999999999,
        step: 0.01,
        decimals: 2,
        boostat: 5,
        maxboostedstep: 100
    }

    $scope.spinOption1000 = {
        postfix: 'тысяч тенге',
        verticalbuttons: true,
        min: 0,
        max: 999999999,
        step: 0.01,
        decimals: 2,
        boostat: 5,
        maxboostedstep: 100
    }


}



function calculationList($scope, $http, DTOptionsBuilder, DTColumnBuilder) {
    $scope.dtColumns = [
    DTColumnBuilder.newColumn("ProjectNumber", "Номер проекта").withOption('name', 'ProjectNumber').renderWith(actionsCalculationLink),
    DTColumnBuilder.newColumn("CalculationTypeName", "Расчет").withOption('name', 'CalculationTypeName'),
                    DTColumnBuilder.newColumn("Date", "Дата").withOption('name', 'Date').renderWith(dateformatHtml),
                    DTColumnBuilder.newColumn("Cost", "Стоимость").withOption('name', 'Cost'),
                    DTColumnBuilder.newColumn("AuthorEmployeeName", "Автор").withOption('name', 'AuthorEmployeeName'),
                        //DTColumnBuilder.newColumn("Id", "Действия").withOption('name', 'Id').renderWith(actionsCalculation)
    ];

}
function calculationProjectCtrl($scope, $http, DTOptionsBuilder, DTColumnBuilder, SweetAlert, notify, FileUploader) {
    $scope.dtColumns = [
    DTColumnBuilder.newColumn("Date", "Дата").withOption('name', 'Date').renderWith(dateformatHtml),
    DTColumnBuilder.newColumn("CalculationTypeName", "Расчет").withOption('name', 'CalculationTypeName'),
DTColumnBuilder.newColumn("Cost", "Стоимость").withOption('name', 'Cost'),
DTColumnBuilder.newColumn("AuthorEmployeeName", "Автор").withOption('name', 'AuthorEmployeeName'),
//   DTColumnBuilder.newColumn("Id", "Действия").withOption('name', 'Id').renderWith(actionsCalculation)
    ];

    $scope.selectFun = function (data) {

        $http({
            method: 'GET',
            url: '/Expertise/Calculation/DetailsOrEdit?id=' + data.Id,
            data: 'JSON'
        }).success(function (result) {
            $scope.htmlContent = result;
        });
    }


}
function actionsCalculationLink(data, type, full, meta) {
    return '<a   href="/Expertise/Project/DetailCard?id=' + full.ProjectId + '" >' +
   full.ProjectNumber +
   '</a>';
}

function actionsCalculation(data, type, full, meta) {
    return '<a  class="btn btn-outline btn-warning btn-xs" href="/Expertise/Calculation/Edit?id=' + data + '">' +
   '   <i class="fa fa-edit"></i>' +
   '</a>&nbsp;' +
   '<a  class="btn btn-outline btn-success btn-xs" href="/Expertise/Calculation/Details?id=' + data + '">' +
   '   <i class="fa fa-search"></i>' +
   '</a>';
}

angular
    .module('app')
    .controller('calculationProjectCtrl', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder', 'SweetAlert', 'notify', 'FileUploader', calculationProjectCtrl])
    .controller('createCalculation', ['$scope', '$http', 'notify', createCalculation])
    .controller('calculationList', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder', calculationList])
    .controller('updateCalculation', ['$scope', '$http', updateCalculation])
