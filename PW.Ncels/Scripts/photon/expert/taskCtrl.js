function taskNewCtrl($scope, DTColumnBuilder) {
    $scope.dtColumns = [
        DTColumnBuilder.newColumn("ProjectNumber", "Номер проекта").withOption('name', 'ProjectNumber').renderWith(actionsTask),
        DTColumnBuilder.newColumn("ApplicationDate", "Дата проекта").withOption('name', 'ApplicationDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("ClientOrgName", "Заказчик").withOption('name', 'ClientOrgName'),
        DTColumnBuilder.newColumn("ProjectName", "Наименование объекта").withOption('name', 'ProjectName'),
        DTColumnBuilder.newColumn("TaskTypeName", "Тип поручения").withOption('name', 'TaskTypeName'),
        DTColumnBuilder.newColumn("AuthorName", "Автор поручения").withOption('name', 'AuthorName'),
        DTColumnBuilder.newColumn("ExecutionDate", "Срок исполнения").withOption('name', 'ExecutionDate').renderWith(dateformatHtml)
    ];
}

function taskWorkCtrl($scope, DTColumnBuilder) {
    $scope.dtColumns = [
       DTColumnBuilder.newColumn("ProjectNumber", "Номер проекта").withOption('name', 'ProjectNumber').renderWith(actionsTask),
        DTColumnBuilder.newColumn("ApplicationDate", "Дата проекта").withOption('name', 'ApplicationDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("ClientOrgName", "Заказчик").withOption('name', 'ClientOrgName'),
        DTColumnBuilder.newColumn("ProjectName", "Наименование объекта").withOption('name', 'ProjectName'),
        DTColumnBuilder.newColumn("TaskTypeName", "Тип поручения").withOption('name', 'TaskTypeName'),
        DTColumnBuilder.newColumn("AuthorName", "Автор поручения").withOption('name', 'AuthorName'),
        DTColumnBuilder.newColumn("ExecutionDate", "Срок исполнения").withOption('name', 'ExecutionDate').renderWith(dateformatHtml)
    ];
}

function taskSuccessCtrl($scope, DTColumnBuilder) {
    $scope.dtColumns = [
        DTColumnBuilder.newColumn("ProjectNumber", "Номер проекта").withOption('name', 'ProjectNumber').renderWith(actionsTask),
        DTColumnBuilder.newColumn("ApplicationDate", "Дата проекта").withOption('name', 'ApplicationDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("ClientOrgName", "Заказчик").withOption('name', 'ClientOrgName'),
        DTColumnBuilder.newColumn("ProjectName", "Наименование объекта").withOption('name', 'ProjectName'),
        DTColumnBuilder.newColumn("TaskTypeName", "Тип поручения").withOption('name', 'TaskTypeName'),
        DTColumnBuilder.newColumn("AuthorName", "Автор поручения").withOption('name', 'AuthorName'),
        DTColumnBuilder.newColumn("ExecutionDate", "Срок исполнения").withOption('name', 'ExecutionDate').renderWith(dateformatHtml)
    ];
}
function taskResolutionByProjectCtrl($scope, DTColumnBuilder) {
    $scope.dtColumns = [
        DTColumnBuilder.newColumn("Id", "Номер").withOption('name', 'Id'),
        DTColumnBuilder.newColumn("ExecutionDate", "Дата исполнения").withOption('name', 'ExecutionDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("TaskTypeName", "Тип").withOption('name', 'TaskTypeName'),
        DTColumnBuilder.newColumn("AuthorName", "Автор").withOption('name', 'AuthorName'),
    ];


}
function actionsTaskLink(data, type, full, meta) {
    return '<a   href="/Expertise/Project/DetailCard?id=' + full.ProjectId + '" >' +
   full.ProjectNumber +
   '</a>';
}
function actionsTask(data, type, full, meta) {
    return '<a  class="pw-task-link" href="/Expertise/Project/DetailTasks?id=' + full.ProjectId + '&taskId=' + full.Id + '" >' + data + '</a>';
}


function taskTreeCtrl($scope, $http, SweetAlert, notify) {
    loadEnums($scope, 'CalculationType', $http);

    $scope.htmlContentData = '';
    var projectId = '';
    var attachPath = '';
    $scope.selectFun = function (data) {
        var id = data[0];
        if (id != undefined) {

            $http({
                method: 'GET',
                url: '/Expertise/Task/Details?id=' + id,
                data: 'JSON'
            }).success(function (result) {
                $scope.htmlContent = result;
                $scope.htmlContentData = result;
            });

            $http({
                method: 'GET',
                url: '/Expertise/Task/ReadTask?id=' + id,
                data: 'JSON'
            }).success(function (result) {

                $scope.task = result.Data;
                $scope.getReport($scope.task.TaskTypeCode, $scope.task.TaskStateCode);
            });
            $scope.htmlContentReport = '';
        }
    }

    $scope.editTask = function () {
        $http({
            method: 'GET',
            url: '/Expertise/Task/Edit?id=' + $scope.task.Id,
            data: 'JSON'
        }).success(function (result) {
            $scope.htmlContent = result;
            $scope.htmlContentReport = '';
        });
    }
    $scope.update = function () {
        if ($scope.editTaskForm.$valid) {
            $http({
                url: '/Expertise/Task/Update',
                method: 'POST',
                data: JSON.stringify($scope.task)
            }).success(function (result) {
                notify({
                    message: 'Резолюция измененна!', classes: 'alert-info', templateUrl: '/Content/templates/notify.html'
                });
                $scope.task = result.Data;
                $scope.htmlContent = $scope.htmlContentData;
            });
        }
    }

    $scope.create = function () {

        if ($scope.taskForm.$valid) {
            $scope.htmlContentReport = '';
            $scope.reassignment.AttachPath = attachPath;
            $http({
                url: '/Expertise/Task/Create',
                method: 'POST',
                data: JSON.stringify($scope.reassignment)
            }).success(function (response) {
                if (!response.IsError) {
                    notify({
                        message: 'Резолюция создана!', classes: 'alert-info', templateUrl: '/Content/templates/notify.html'
                    });
                    $scope.htmlContent = $scope.htmlContentData;
                    $scope.reloadTree(response.Result);
                } else {
                    alert(response.Message);
                }
            });
        }
    }
    $scope.reassignmentTask = function () {
        $scope.reassignment = {};
        $scope.reassignment.ProjectId = projectId;

        $scope.reassignment.ExecutionDate = new Date();
        $scope.reassignment.Text = '';
        $scope.reassignment.Executors = [];
        $scope.reassignment.ParentId = $scope.task.Id;
        $http({
            method: 'GET',
            url: '/Expertise/Task/Create',
            data: 'JSON'
        }).success(function (result) {
          
            $scope.htmlContent = result;
        });
    }
    $scope.detail = function () {
        $scope.htmlContent = $scope.htmlContentData;
    }
    $scope.detailTask = function () {
        $http({
            method: 'GET',
            url: '/Expertise/Task/ReadTask?id=' + $scope.task.Id,
            data: 'JSON'
        }).success(function (result) {
            $scope.htmlContent = $scope.htmlContentData;
            $scope.task = result.Data;
        });
    }

    $scope.initTree = function (id, attach) {
        projectId = id;

        $http({
            method: 'GET',
            url: '/Dictionaries/GetExecutorByProject?id=' + id,
            data: 'JSON'
        }).success(function (result) {
            $scope.Executors = result;
        });
        loadEnums($scope, 'TaskType', $http);
    }

    $scope.initAttach = function (attach) {
        attachPath = attach;
    }


    $scope.deleteTask = function () {
        $scope.htmlContentReport = '';
        SweetAlert.swal({
            title: "Удалить задание?",
            text: "После удаление задание его не возможно восстановить!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Да удалить!",
            cancelButtonText: "Нет, отмена!",
            closeOnConfirm: false,
            closeOnCancel: false
        },
            function (isConfirm) {
                if (isConfirm) {
                    $http({
                        method: 'POST',
                        url: '/Expertise/Task/Delete',
                        data: { id: $scope.task.Id }
                    }).success(function (result) {
                        if (!result.IsError) {
                            $scope.deleteNode($scope.task.Id);
                            SweetAlert.swal("Удаление!", "Задание удалено.", "success");
                            $scope.htmlContent = '<p>Задание удалено</p>';
                        } else {
                            SweetAlert.swal("Отмена", "Ошибка удаления :)", "error");
                        }
                    });

                } else {
                    SweetAlert.swal("Отмена", "Вы отменили удаление :)", "error");
                }
            });
    }
    $scope.executeTask = function () {
        var url = "";
        switch ($scope.task.TaskTypeCode) {
            case "contract":
                url = '/Expertise/Contract/Create?taskId=' + $scope.task.Id + '&projectId=' + $scope.task.ProjectId;
                break;
            case "conclusion":
                url = '/Expertise/Conclusion/Create?taskId=' + $scope.task.Id + '&projectId=' + $scope.task.ProjectId;
                break;
            case "completeness":
                url = '/Expertise/Completeness/Create?taskId=' + $scope.task.Id + '&projectId=' + $scope.task.ProjectId;
                break;
            case "expertise":
                url = '/Expertise/Expertise/Create?taskId=' + $scope.task.Id + '&projectId=' + $scope.task.ProjectId;
                break;
            case "calculation":
                url = '/Expertise/Calculation/Create?taskId=' + $scope.task.Id + '&projectId=' + $scope.task.ProjectId;
                break;
            case "agreement":
                url = '/Expertise/Agreement/Execute?taskId=' + $scope.task.Id + '&projectId=' + $scope.task.ProjectId;
                break;
            case "sing":
                url = '/Expertise/Signing/Execute?taskId=' + $scope.task.Id + '&projectId=' + $scope.task.ProjectId;
                break;
            default:
                break;
        }

        //get execute view
        if (url !== "") {
            $http({
                method: 'GET',
                url: url,
                data: 'JSON'
            }).success(function (result) {
                $scope.htmlContentReport = result;
            });
        }
    }

    $scope.getReport = function (type, code) {
        if (code !== 'executed') {
            return;
        }
        var url = "";
        switch (type) {
            case "contract":
                url = '/Expertise/Contract/DetailsOrEditTree?id=' + $scope.task.Id;
                break;
            case "conclusion":
                url = '/Expertise/Conclusion/DetailsOrEditTree?id=' + $scope.task.Id;
                break;
            case "completeness":
                url = '/Expertise/Completeness/DetailsOrEditTree?id=' + $scope.task.Id;
                break;
            case "expertise":
                url = '/Expertise/Expertise/DetailsOrEditTree?id=' + $scope.task.Id;
                break;
            case "calculation":
                url = '/Expertise/Calculation/DetailsOrEditTree?id=' + $scope.task.Id;
                break;
            case "agreement":
            case "sing":
                url = '/Expertise/Task/Report?taskId=' + $scope.task.Id;
                break;
            default:
                break;
        }

        //get execute view
        if (url != "") {
            $http({
                method: 'GET',
                url: url,
                data: 'JSON'
            }).success(function (result) {
                $scope.htmlContentReport = result;
            });
        }
    }
}


angular
    .module('app')
    .controller('taskTreeCtrl', ['$scope', '$http', 'SweetAlert', 'notify', taskTreeCtrl])
    .controller('taskNewCtrl', ['$scope', 'DTColumnBuilder', taskNewCtrl])
    .controller('taskSuccessCtrl', ['$scope', 'DTColumnBuilder', taskSuccessCtrl])
    .controller('taskResolutionByProjectCtrl', ['$scope', 'DTColumnBuilder', taskResolutionByProjectCtrl])
    .controller('taskWorkCtrl', ['$scope', 'DTColumnBuilder', taskWorkCtrl]);