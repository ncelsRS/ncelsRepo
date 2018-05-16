function updateCompleteness($scope, $http) {
    $scope.object = {};
    loadEnums($scope, 'CompletenessType', $http);

    var id = $("#completenessId").val();
    $http({
        method: 'GET',
        url: '/Expertise/Completeness/GetData?id=' + id,
        data: 'JSON'
    }).success(function (result) {
        $scope.object = result;
        $scope.object.Date = new Date(parseInt(result.Date.slice(6, -2)));
    });


    $scope.update = function () {
        $http({
            url: '/Expertise/Completeness/Update',
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

function createCompleteness($scope, $http, notify) {


    loadEnums($scope, 'CompletenessType', $http);
    $scope.createCompleten = function () {

        if ($scope.completenForm.$valid) {
            $http({
                url: '/Expertise/Completeness/Create',
                method: 'POST',
                data: JSON.stringify($scope.completen)
            }).success(function (response) {
                if (!response.IsError) {
                    notify({ message: 'Комплектность исполненна!', classes: 'alert-info', templateUrl: '/Content/templates/notify.html' });
                    $scope.initCompletenUpdate(response.Id);
                } else {
                    alert(response.Message);
                }

            });
        }
    }
    $scope.saveCompleten = function () {
        if ($scope.editCompletenForm.$valid) {
            $http({
                url: '/Expertise/Completeness/Update',
                method: 'POST',
                data: JSON.stringify($scope.completen)
            }).success(function (response) {
                if (!response.IsError) {
                    notify({ message: 'Комплектность измененна!', classes: 'alert-info', templateUrl: '/Content/templates/notify.html' });
                    $scope.completen = response.Obj;
                    $scope.htmlContentReportIn = $scope.htmlContentReportInTemp;
                } else {
                    alert(response.Message);
                }
            });
        }
    }
    $scope.detail = function () {
        $http({
            method: 'GET',
            url: '/Expertise/Calculation/Details?id=' + $scope.completen.Id,
            data: 'JSON'
        }).success(function (result) {
            $scope.htmlContentReportIn = result;
        });
    }

    $scope.editCompleten = function () {
        $http({
            method: 'GET',
            url: '/Expertise/Completeness/Edit?id=' + $scope.completen.Id,
            data: 'JSON'
        }).success(function (result) {
            $scope.htmlContentReportIn = result;
        });
    }
    $scope.initCompleten = function (attach, taskId, projectId) {
        $scope.completen = {
            Date: new Date(),
            TaskId: taskId,
            ProjectId: projectId
        };
        $scope.completen.AttachPath = attach;
    }

    $scope.initCompletenUpdate = function (taskId) {
        $http({
            method: 'GET',
            url: '/Expertise/Completeness/Details?id=' + taskId,
            data: 'JSON'
        }).success(function (result) {
            $scope.htmlContentReportIn = result;
            $scope.htmlContentReportInTemp = result;
        });
        $http({
            method: 'GET',
            url: '/Expertise/Completeness/ReadCompleteness?id=' + taskId,
            data: 'JSON'
        }).success(function (result) {
            $scope.completen = result;
        });
    }
    $scope.detailCompleten = function () {
        $scope.htmlContentReportIn = '';
    }
}



function completenessList($scope, $http, DTOptionsBuilder, DTColumnBuilder) {
    $scope.dtColumns = [
        DTColumnBuilder.newColumn("ProjectNumber", "Номер проекта").withOption('name', 'ProjectNumber').renderWith(actionsCompletenessLink),
        DTColumnBuilder.newColumn("Date", "Дата").withOption('name', 'Date').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("AuthorEmployeeName", "Автор").withOption('name', 'AuthorEmployeeName'),
        DTColumnBuilder.newColumn("CompletenessTypeName", "Комплектность").withOption('name', 'CompletenessTypeName'),
        DTColumnBuilder.newColumn("Text", "Текст").withOption('name', 'Text'),
        //DTColumnBuilder.newColumn("Id", "Действия").withOption('name', 'Id').renderWith(actionsCompletenessCommand)
    ];
}

function actionsCompletenessLink(data, type, full, meta) {
    return '<a   href="/Expertise/Project/DetailCard?id=' + full.ProjectId + '" >' +
   full.ProjectNumber +
   '</a>';
}

function actionsCompletenessCommand(data, type, full, meta) {
    return '<a  class="btn btn-outline btn-warning btn-xs" href="/Expertise/Completeness/Edit?id=' + data + '">' +
   '   <i class="fa fa-edit"></i>' +
   '</a>&nbsp;' +
    '<a  class="btn btn-outline btn-success btn-xs" href="/Expertise/Completeness/Details?id=' + data + '">' +
    '   <i class="fa fa-search"></i>' +
    '</a>';
}
function completenessProjectCtrl($scope, $http, DTOptionsBuilder, DTColumnBuilder, SweetAlert) {
    $scope.dtColumns = [
        DTColumnBuilder.newColumn("Date", "Дата").withOption('name', 'Date').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("AuthorEmployeeName", "Автор").withOption('name', 'AuthorEmployeeName'),
        DTColumnBuilder.newColumn("CompletenessTypeName", "Комплектность").withOption('name', 'CompletenessTypeName'),
        DTColumnBuilder.newColumn("Text", "Текст").withOption('name', 'Text'),
       // DTColumnBuilder.newColumn("Id", "Действия").withOption('name', 'Id').renderWith(actionsCompletenessCommand)
    ];

    $scope.selectFun = function (data) {
        $http({
            method: 'GET',
            url: '/Expertise/Completeness/DetailsOrEdit?id=' + data.Id,
            data: 'JSON'
        }).success(function (result) {
            $scope.htmlContent = result;
        });


    }


}
angular
    .module('app')
    .controller('completenessProjectCtrl', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder', 'SweetAlert', completenessProjectCtrl])
    .controller('createCompleteness', ['$scope', '$http','notify', createCompleteness])
    .controller('completenessList', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder', completenessList])
    .controller('updateCompleteness', ['$scope', '$http', updateCompleteness])