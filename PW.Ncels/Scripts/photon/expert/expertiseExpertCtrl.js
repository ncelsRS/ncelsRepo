//function updateExpertise($scope, $http) {
//    $scope.object = {};
//    loadEnums($scope, 'ExpertiseType', $http);

//    var id = $("#expertiseId").val();
//    $http({
//        method: 'GET',
//        url: '/Expertise/Expertise/GetData?id=' + id,
//        data: 'JSON'
//    }).success(function (result) {
//        $scope.object = result;
//        $scope.object.Date = new Date(parseInt(result.Date.slice(6, -2)));
//    });


//    $scope.update = function () {
//        $http({
//            url: '/Expertise/Expertise/Update',
//            method: 'POST',
//            data: JSON.stringify($scope.object)
//        }).success(function (response) {
//            if (!response.IsError) {
//                window.location = '/Home/Success';
//            } else {
//                alert(response.Message);
//            }
//        });
//    }
//}

function createExpertise($scope, $http,notify) {


    loadEnums($scope, 'ExpertiseType', $http);

    $scope.createExpert = function () {
        $http({
            url: '/Expertise/Expertise/Create',
            method: 'POST',
            data: JSON.stringify($scope.expert)
        }).success(function (response) {
            if (!response.IsError) {
                notify({ message: 'Экспертиза исполненна!', classes: 'alert-info', templateUrl: '/Content/templates/notify.html' });
                $scope.initExpertUpdate(response.Id);

            } else {
                alert(response.Message);
            }
        });
    }

    $scope.initExpert = function (attach, taskId, projectId) {
        $scope.expert = {
            Date: new Date(),
            TaskId: taskId,
            ProjectId: projectId
        };
        $scope.expert.AttachPath = attach;
    }
    $scope.saveExpertise = function () {
        if ($scope.editExpertiseForm.$valid) {

            $http({
                url: '/Expertise/Expertise/Update',
                method: 'POST',
                data: JSON.stringify($scope.expert)
            }).success(function (response) {
                if (!response.IsError) {
                    notify({ message: 'Экспертиза измененна!', classes: 'alert-info', templateUrl: '/Content/templates/notify.html' });

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
            url: '/Expertise/Expertise/Details?id=' + $scope.expert.Id,
            data: 'JSON'
        }).success(function (result) {
            $scope.htmlContentReportIn = result;
        });
    }
    $scope.editExpertise = function () {
        $http({
            method: 'GET',
            url: '/Expertise/Expertise/Edit?id=' + $scope.expert.Id,
            data: 'JSON'
        }).success(function (result) {
            $scope.htmlContentReportIn = result;
        });
    }
    $scope.initExpertUpdate = function (taskId) {
        $http({
            method: 'GET',
            url: '/Expertise/Expertise/Details?id=' + taskId,
            data: 'JSON'
        }).success(function (result) {
            $scope.htmlContentReportIn = result;
            $scope.htmlContentReportInTemp = result;
        });
        $http({
            method: 'GET',
            url: '/Expertise/Expertise/ReadExpertise?id=' + taskId,
            data: 'JSON'
        }).success(function (result) {
            $scope.expert = result;
        });
    }
    $scope.detail = function () {
        $scope.htmlContentReportIn = '';
    }
}



function expertiseList($scope, $http, DTOptionsBuilder, DTColumnBuilder) {
    $scope.dtColumns = [
        DTColumnBuilder.newColumn("ProjectNumber", "Номер проекта").withOption('name', 'ProjectNumber').renderWith(actionsExpertiseLink),
        DTColumnBuilder.newColumn("Date", "Дата").withOption('name', 'Date').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("AuthorEmployeeName", "Автор").withOption('name', 'AuthorEmployeeName'),
        DTColumnBuilder.newColumn("ExpertiseTypeName", "Экспертиза").withOption('name', 'ExpertiseTypeName'),
        DTColumnBuilder.newColumn("Text", "Текст").withOption('name', 'Text'),
        //DTColumnBuilder.newColumn("Id", "Действия").withOption('name', 'Id').renderWith(actionsExpertiseCommand)
    ];

}

function expertiseProjectCtrl($scope, $http, DTOptionsBuilder, DTColumnBuilder, SweetAlert) {
    $scope.dtColumns = [
        DTColumnBuilder.newColumn("AuthorEmployeeName", "Автор").withOption('name', 'AuthorEmployeeName'),
        DTColumnBuilder.newColumn("Date", "Дата").withOption('name', 'Date').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("ExpertiseTypeName", "Экспертиза").withOption('name', 'ExpertiseTypeName'),
        DTColumnBuilder.newColumn("Text", "Текст").withOption('name', 'Text'),
       // DTColumnBuilder.newColumn("Id", "Действия").withOption('name', 'Id').renderWith(actionsExpertiseCommand)
    ];
    $scope.selectFun = function (data) {
        $http({
            method: 'GET',
            url: '/Expertise/Expertise/DetailsOrEdit?Id=' + data.Id,
            data: 'JSON'
        }).success(function (result) {

            $scope.htmlContent = result;
        });


    }

}

function actionsExpertiseLink(data, type, full, meta) {
    return '<a   href="/Expertise/Project/DetailCard?id=' + full.ProjectId + '" >' +
   full.ProjectNumber +
   '</a>';
}

function actionsExpertiseCommand(data, type, full, meta) {
    return '<a  class="btn btn-outline btn-warning btn-xs" href="/Expertise/Expertise/Edit?id=' + data + '">' +
   '   <i class="fa fa-edit"></i>' +
   '</a>&nbsp;' +
    '<a  class="btn btn-outline btn-success btn-xs" href="/Expertise/Expertise/Details?id=' + data + '">' +
    '   <i class="fa fa-search"></i>' +
    '</a>';
}

angular
    .module('app')
   // .controller('updateExpertise',  ['$scope', '$http',updateExpertise])
    .controller('createExpertise', ['$scope', '$http', 'notify', createExpertise])
    .controller('expertiseList', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder', expertiseList])
    .controller('expertiseProjectCtrl', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder', 'notify', 'FileUploader', expertiseProjectCtrl])