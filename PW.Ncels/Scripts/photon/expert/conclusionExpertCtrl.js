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

function createConclusion($scope, $http,notify) {

    loadEnums($scope, 'ConclusionType', $http);




    $scope.generateConclusion = function () {
        if ($scope.conclusionForm.$valid) {
            $scope.conclusion.TepS = parseFloat($scope.conclusion.TepS);
            $scope.conclusion.TepV = parseFloat($scope.conclusion.TepV);
            $scope.conclusion.TepL = parseFloat($scope.conclusion.TepL);
            $http({
                url: '/Expertise/Conclusion/Generate',
                method: 'POST',
                data: JSON.stringify($scope.conclusion)
            }).success(function (response) {
                if (!response.IsError) {
                    notify({ message: 'Заключение сгенерировано!', classes: 'alert-info', templateUrl: '/Content/templates/notify.html' });
                } else {
                    alert(response.Message);
                }
            });
        }
    }

    $scope.saveConclusion = function () {
        if ($scope.editConclusionForm.$valid) {
            $scope.conclusion.TepS = parseFloat($scope.conclusion.TepS);
            $scope.conclusion.TepV = parseFloat($scope.conclusion.TepV);
            $scope.conclusion.TepL = parseFloat($scope.conclusion.TepL);
            $http({
                url: '/Expertise/Conclusion/Update',
                method: 'POST',
                data: JSON.stringify($scope.conclusion)
            }).success(function (response) {
                if (!response.IsError) {
                    $scope.htmlContentReportIn = $scope.htmlContentReportInTemp;
                } else {
                    alert(response.Message);
                }
            });
        }
    }
    $scope.createConclusion = function () {
        if ($scope.conclusionForm.$valid) {
            $scope.conclusion.TepS = parseFloat($scope.conclusion.TepS);
            $scope.conclusion.TepV = parseFloat($scope.conclusion.TepV);
            $scope.conclusion.TepL = parseFloat($scope.conclusion.TepL);

            $http({
                url: '/Expertise/Conclusion/Create',
                method: 'POST',
                data: JSON.stringify($scope.conclusion)
            }).success(function (response) {
                if (!response.IsError) {
                    notify({ message: 'Заключение исполнено!', classes: 'alert-info', templateUrl: '/Content/templates/notify.html' });
                    $scope.initConclusionUpdate(response.Id);
                } else {
                    alert(response.Message);
                }
            });
        }
    }

    $scope.initConclusion = function (attach, taskId, projectId) {
        $scope.conclusion = {
            Date: new Date(),
            TaskId: taskId,
            ProjectId: projectId
        };
        $scope.conclusion.AttachPath = attach;

        $scope.getProjectNumber = function () {
            $http({
                url: '/Expertise/Project/GetProjectNumber',
                method: 'Get',
                params: { id: projectId }
            }).success(function (response) {
                $scope.conclusion.Number = response;

            });
        }
    }

    $scope.initConclusionUpdate = function (projectId) {
        $http({
            method: 'GET',
            url: '/Expertise/Conclusion/Details?id=' + projectId,
            data: 'JSON'
        }).success(function (result) {
            $scope.htmlContentReportIn = result;
            $scope.htmlContentReportInTemp = result;
        });

        $http({
            method: 'GET',
            url: '/Expertise/Conclusion/ReadConclusion?projectId=' + projectId,
            data: 'JSON'
        }).success(function (result) {
            $scope.conclusion = result;

        });
    }
    $scope.detailConclusion = function () {
        $scope.htmlContentReportIn = '';
            $scope.annulContent = '';
    }
    $scope.detail = function () {
        $http({
            method: 'GET',
            url: '/Expertise/Conclusion/Details?id=' + $scope.conclusion.Id,
            data: 'JSON'
        }).success(function (result) {
            $scope.htmlContentReportIn = result;
        });
    }
    $scope.editConclusion = function () {
        $http({
            method: 'GET',
            url: '/Expertise/Conclusion/Edit?id=' + $scope.conclusion.Id,
            data: 'JSON'
        }).success(function (result) {
            $scope.htmlContentReportIn = result;
        });
    }

    $scope.getExecutors = function () {
        $http({
            method: 'GET',
            url: '/Dictionaries/GetExecutorByProject?id=' + $scope.conclusion.ProjectId,
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
        $scope.agreementObj.ProjectId = $scope.conclusion.ProjectId;
        $scope.agreementObj.Id = $scope.conclusion.TaskId;
        $scope.agreementObj.Executors = [];
        $http({
            method: 'GET',
            url: '/Expertise/Agreement/Create',
            data: 'JSON',
            params: { taskId: $scope.conclusion.TaskId, projectId: $scope.conclusion.ProjectId }
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
                $scope.htmlContentReportIn = '<p>Все хорошо</p>';
            } else {
                alert(response.Message);
            }
        });

    };


    $scope.sing = function () {
        $scope.getExecutors();
        $scope.singObj = {};
        $scope.singObj.Text = '';
        $scope.singObj.ExecutionDate = new Date();
        $scope.singObj.ProjectId = $scope.conclusion.ProjectId;
        $scope.singObj.Executors = [];
        $scope.singObj.Id = $scope.conclusion.TaskId;
        $http({
            method: 'GET',
            url: '/Expertise/Signing/Create',
            data: 'JSON',
            params: { taskId: $scope.conclusion.TaskId, projectId: $scope.conclusion.ProjectId }
        }).success(function (result) {
            $scope.htmlContentReportIn = result;
        });

    };

    $scope.createSing = function () {
        $http({
            url: '/Expertise/Signing/Create',
            method: 'POST',
            data: JSON.stringify($scope.singObj)
        }).success(function (response) {
            if (!response.IsError) {
                $scope.htmlContentReportIn = '<p>Все хорошо</p>';
            } else {
                alert(response.Message);
            }
        });

    };

    $scope.annul = function () {
        $scope.conclusion.AnnulDate = new Date();
        $scope.conclusion.AnnulNumber = $scope.conclusion.Number;
        $http({
            method: 'GET',
            url: '/Expertise/Conclusion/Annul',
            data: 'JSON'
        }).success(function (result) {
            $scope.annulContent = result;
        });
    }

    $scope.give = function () {
        $http({
            url: '/Expertise/Conclusion/Give',
            method: 'POST',
            data: JSON.stringify($scope.conclusion)
        }).success(function (response) {
            if (!response.IsError) {
                notify({ message: 'Заключение выданно!', classes: 'alert-info', templateUrl: '/Content/templates/notify.html' });
            } else {
                alert(response.Message);
            }
        });
    }

    $scope.annulConclusion = function () {
        if ($scope.annulConclusionForm.$valid) {
            $scope.conclusion.Number = null;
            $http({
                url: '/Expertise/Conclusion/Annul',
                method: 'POST',
                data: JSON.stringify($scope.conclusion)
            }).success(function (response) {
                if (!response.IsError) {
                    notify({ message: 'Заключение аннулированно!', classes: 'alert-info', templateUrl: '/Content/templates/notify.html' });
                } else {
                    alert(response.Message);
                }
            });
        }
    }

    $scope.spinOptionP = {
        postfix: '%',
        verticalbuttons: true,
        min: 0,
        max: 100,
        step: 1,
        decimals: 0,
        boostat: 5,
        maxboostedstep: 5
    }

    $scope.spinOptionY = {
        postfix: 'год',
        verticalbuttons: true,
        min: 1900,
        max: 2999,
        step: 1,
        decimals: 0,
        boostat: 5,
        maxboostedstep: 5
    }

    $scope.spinOptionS = {
        postfix: 'м2',
        verticalbuttons: true,
        min: 0,
        max: 999999999,
        step: 0.01,
        decimals: 2,
        boostat: 5,
        maxboostedstep: 100
    }

    $scope.spinOptionV = {
        postfix: 'м3',
        verticalbuttons: true,
        min: 0,
        max: 999999999,
        step: 0.01,
        decimals: 2,
        boostat: 5,
        maxboostedstep: 100
    }

    $scope.spinOptionL = {
        postfix: 'км',
        verticalbuttons: true,
        min: 0,
        max: 999999999,
        step: 0.01,
        decimals: 2,
        boostat: 5,
        maxboostedstep: 100
    }

    $scope.spinOptionH = {
        postfix: 'этажей',
        verticalbuttons: true,
        min: 0,
        max: 999999999,
        step: 1,
        decimals: 0,
        boostat: 5,
        maxboostedstep: 100
    }

    $scope.spinOptionT = {
        postfix: 'месяц',
        verticalbuttons: true,
        min: 0,
        max: 999999999,
        step: 1,
        decimals: 0,
        boostat: 5,
        maxboostedstep: 100
    }
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

function conclusionProjectCtrl($scope, $http, DTOptionsBuilder, DTColumnBuilder, notify, FileUploader) {
    //$scope.dtColumns = [
    //    DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
    //    DTColumnBuilder.newColumn("Date", "Дата").withOption('name', 'Date').renderWith(dateformatHtml),
    //    DTColumnBuilder.newColumn("AuthorEmployeeName", "Автор").withOption('name', 'AuthorEmployeeName'),
    //    DTColumnBuilder.newColumn("ConclusionTypeName", "Заключение").withOption('name', 'ConclusionTypeName'),
    //    DTColumnBuilder.newColumn("SignEmployeeName", "Подписал").withOption('name', 'SignEmployeeName'),
    //    DTColumnBuilder.newColumn("Id", "Действия").withOption('name', 'Id').renderWith(actionsConclusionCommand)
    //];

    $scope.init = function (id) {
        $http({
            method: 'GET',
            url: '/Expertise/Conclusion/DetailsOrEdit?id=' + id,
            data: 'JSON'
        }).success(function (result) {
            $scope.htmlContent = result;
        });
    }

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
    .controller('updateConclusion', ['$scope', '$http', updateConclusion])
    .controller('createConclusion', ['$scope', '$http', 'notify', createConclusion])
    .controller('conclusionList', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder', conclusionList])
    .controller('conclusionProjectCtrl', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder', 'notify', 'FileUploader', conclusionProjectCtrl])