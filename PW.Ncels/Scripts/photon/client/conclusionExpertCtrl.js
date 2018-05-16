function updateConclusion($scope, $http) {
    $scope.object = {};
    loadEnums($scope, 'ConclusionType', $http);

    var id = $("#conclusionId").val();
    $http({
        method: 'GET',
        url: '/Expertise/Conclusion/GetData?id=' + id,
        data: 'JSON'
    }).success(function (result) {
        $scope.object = result;
        $scope.object.Date = new Date(parseInt(result.Date.slice(6, -2)));
    });


    $scope.update = function () {
        $http({
            url: '/Expertise/Conclusion/Update',
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

function createConclusion($scope, $http) {
    $scope.object = {
        Date: new Date()
    };

    loadEnums($scope, 'ConclusionType', $http);

    $scope.Create = function () {
        $http({
            url: '/Expertise/Conclusion/Create',
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

function loadEnums($scope, name, $http) {
    $http({
        method: 'GET',
        url: '/Enums/GetReference',
        data: 'JSON',
        params: { type: name }
    }).success(function (result) {
        $scope[name] = result;
    });
}

function conclusionList($scope, $http, DTOptionsBuilder, DTColumnBuilder) {
    $scope.dtColumns = [
        DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("ProjectNumber", "Номер проекта").withOption('name', 'ProjectNumber').renderWith(actionsConclusionLink),
        DTColumnBuilder.newColumn("Date", "Дата").withOption('name', 'Date').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("AuthorEmployeeName", "Автор").withOption('name', 'AuthorEmployeeName'),
        DTColumnBuilder.newColumn("ConclusionTypeName", "Заключение").withOption('name', 'ConclusionTypeName'),
        DTColumnBuilder.newColumn("SignEmployeeName", "Подписал").withOption('name', 'SignEmployeeName'),
        //DTColumnBuilder.newColumn("Id", "Действия").withOption('name', 'Id').renderWith(actionsConclusionCommand)
    ];
 
}

function conclusionProjectCtrl($scope, $http, DTOptionsBuilder, DTColumnBuilder) {
    //$scope.dtColumns = [
    //    DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
    //    DTColumnBuilder.newColumn("Date", "Дата").withOption('name', 'Date').renderWith(dateformatHtml),
    //    DTColumnBuilder.newColumn("AuthorEmployeeName", "Автор").withOption('name', 'AuthorEmployeeName'),
    //    DTColumnBuilder.newColumn("ConclusionTypeName", "Заключение").withOption('name', 'ConclusionTypeName'),
    //    DTColumnBuilder.newColumn("SignEmployeeName", "Подписал").withOption('name', 'SignEmployeeName'),
    //    DTColumnBuilder.newColumn("Id", "Действия").withOption('name', 'Id').renderWith(actionsConclusionCommand)
    //];
    var projectId = $('#projectId').val();

    $http({
        method: 'GET',
        url: '/Client/Conclusion/Details',
        data: 'JSON'
    }).success(function (result) {
        $scope.detailContent = result;
        $scope.htmlContent = result;
    });

    $http({
        method: 'GET',
        url: '/Client/Conclusion/ReadConclusion?id=' + projectId,
        data: 'JSON'
    }).success(function (result) {
        result.Date = new Date(parseInt(result.Date.slice(6, -2)));
        $scope.object = result;
    });

    $scope.detailConclusion = function () {
        $scope.htmlContent = $scope.detailContent;
    }
    $scope.editConclusion = function () {
        $http({
            method: 'GET',
            url: '/Expertise/Conclusion/Edit',
            data: 'JSON'
        }).success(function (result) {
            $scope.htmlContent = result;
        });
    }
    $scope.saveConclusion = function () {
        if ($scope.editConclusionForm.$valid) {
            $http({
                url: '/Expertise/Conclusion/Update',
                method: 'POST',
                data: JSON.stringify($scope.object)
            }).success(function(response) {
                if (!response.IsError) {
                    response.Obj.Date = new Date(parseInt(response.Obj.Date.slice(6, -2)));
                    $scope.object = response.Obj;
                    $scope.htmlContent = $scope.detailContent;
                } else {
                    alert(response.Message);
                }
            });
        }
    }
    loadEnums($scope, 'ConclusionType', $http);
}

function actionsConclusionLink(data, type, full, meta) {
    return '<a   href="/Expertise/Project/DetailCard?id=' + full.ProjectId + '" >' +
   full.ProjectNumber +
   '</a>';
}

function actionsConclusionCommand(data, type, full, meta) {
    return '<a  class="btn btn-outline btn-warning btn-xs" href="/Expertise/Conclusion/Edit?id=' + data + '">' +
   '   <i class="fa fa-edit"></i>' +
   '</a>&nbsp;' +
    '<a  class="btn btn-outline btn-success btn-xs" href="/Expertise/Conclusion/Details?id=' + data + '">' +
    '   <i class="fa fa-search"></i>' +
    '</a>';
}

angular
    .module('app')
    .controller('updateConclusion', ['$scope', '$http',updateConclusion])
    .controller('createConclusion', ['$scope', '$http',createConclusion])
    .controller('conclusionList', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder',conclusionList])
    .controller('conclusionProjectCtrl', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder',conclusionProjectCtrl])