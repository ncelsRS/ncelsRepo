function updateContract($scope, $http) {
    $scope.object = {};
    loadEnums($scope, 'ContractType', $http);

    var id = $("#contractId").val();
    $http({
        method: 'GET',
        url: '/Expertise/Contract/GetData?id=' + id,
        data: 'JSON'
    }).success(function (result) {
        $scope.contract = result;
        $scope.contract.Date = new Date(parseInt(result.Date.slice(6, -2)));
    });

    $http({
        method: 'GET',
        url: '/Expertise/Contract/GetCalculation',
        data: 'JSON',
        params: { id: 1 }
    }).success(function (result) {
        $scope.Calculations = result;
    });

    $scope.update = function () {
        $http({
            url: '/Expertise/Contract/Update',
            method: 'POST',
            data: JSON.stringify($scope.contract)
        }).success(function (response) {
            if (!response.IsError) {
                window.location = '/Home/Success';
            } else {
                alert(response.Message);
            }
        });
    }
}

function createContract($scope, $http) {

    //loadEnums($scope, 'ContractType', $http);

    //$scope.createContract = function () {
    //    if ($scope.contractForm.$valid) {
    //        $http({
    //            url: '/Expertise/Contract/Create',
    //            method: 'POST',
    //            data: JSON.stringify($scope.contract)
    //        }).success(function (response) {
    //            if (!response.IsError) {

    //                $scope.htmlContent = '<p>Все хорошо</p>';
    //            } else {
    //                alert(response.Message);
    //            }
    //        });
    //    }
    //}

    //$scope.initContract = function (attach, taskId, projectId) {

    //    $scope.contract = {
    //        Date: new Date(),
    //        TaskId: taskId,
    //        ProjectId: projectId
    //    };
    //    $scope.contract.AttachPath = attach;

    //    $http({
    //        method: 'GET',
    //        url: '/Expertise/Contract/GetCalculation',
    //        data: 'JSON',
    //        params: { id: projectId }
    //    }).success(function (result) {
    //        $scope.Calculations = result;
    //    });

    //    $http({
    //        url: '/Expertise/Contract/GetClientInfo',
    //        method: 'GET',
    //        params: { id: projectId }
    //    }).success(function (response) {
    //        $scope.contract.client = response;
    //    });
    //}
    //$scope.detailContract = function () {
    //    $scope.htmlContentReportIn = '';
    //}
}



function contractList($scope, $http, DTOptionsBuilder, DTColumnBuilder) {
    $scope.dtColumns = [
        DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("ProjectNumber", "Номер проекта").withOption('name', 'ProjectNumber').renderWith(actionsContractLink),
        DTColumnBuilder.newColumn("Date", "Дата").withOption('name', 'Date').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("AuthorEmployeeName", "Автор").withOption('name', 'AuthorEmployeeName'),
        DTColumnBuilder.newColumn("ContractTypeName", "Договор").withOption('name', 'ContractTypeName'),
        DTColumnBuilder.newColumn("ClientName", "Заказчик").withOption('name', 'ClientName'),
        //DTColumnBuilder.newColumn("Id", "Действия").withOption('name', 'Id').renderWith(actionsContractCommand)
    ];
}


function contractProjectCtrl($scope, $http, DTOptionsBuilder, DTColumnBuilder, SweetAlert) {
    $scope.dtColumns = [

        DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("Date", "Дата").withOption('name', 'Date').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("AuthorEmployeeName", "Автор").withOption('name', 'AuthorEmployeeName'),
        DTColumnBuilder.newColumn("ContractTypeName", "Договор").withOption('name', 'ContractTypeName'),
      //  DTColumnBuilder.newColumn("Id", "Действия").withOption('name', 'Id').renderWith(actionsContractCommand)
    ];

    $scope.selectFun = function (data) {
        $http({
            method: 'GET',
            url: '/Client/Contract/Details',
            data: 'JSON'
        }).success(function (result) {
            $scope.detailContent = result;
            $scope.htmlContent = result;
        });

        $http({
            method: 'GET',
            url: '/Client/Contract/ReadContract?id=' + data.Id,
            data: 'JSON'
        }).success(function (result) {
            result.contract.Date = new Date(parseInt(result.contract.Date.slice(6, -2)));
            $scope.object = result;
        });
    }
    $scope.editContract = function () {
        $http({
            method: 'GET',
            url: '/Expertise/Contract/Edit',
            data: 'JSON'
        }).success(function (result) {
            $scope.htmlContent = result;
        });
    }

    $scope.saveContract = function () {
        if ($scope.editContractForm.$valid) {
            $http({
                url: '/Expertise/Contract/Update',
                method: 'POST',
                data: JSON.stringify($scope.object.contract)
            }).success(function (response) {
                if (!response.IsError) {
                    response.Obj.Date = new Date(parseInt(response.Obj.Date.slice(6, -2)));
                    $scope.object.contract = response.Obj;
                    $scope.htmlContent = $scope.detailContent;
                } else {
                    alert(response.Message);
                }
            });
        }
    }

    $scope.detailContract = function () {
        $scope.htmlContent = $scope.detailContent;
    }
    $scope.deleteContract = function () {
        SweetAlert.swal({
            title: "Удалить договор?",
            text: "После удаления договора его не возможно восстановить!",
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
                        url: '/Expertise/Contract/Delete',
                        data: { id: $scope.object.contract.Id }
                    }).success(function (result) {
                        if (!result.IsError) {
                            $scope.removeRow();
                            SweetAlert.swal("Удаление!", "Договор удален.", "success");
                            $scope.htmlContent = '';
                        } else {
                            SweetAlert.swal("Отмена", "Ошибка удаления :)", "error");
                        }
                    });

                } else {
                    SweetAlert.swal("Отмена", "Вы отменили удаление :)", "error");
                }
            });
    }
    loadEnums($scope, 'ContractType', $http);

    //$http({
    //    method: 'GET',
    //    url: '/Expertise/Contract/GetCalculation',
    //    data: 'JSON',
    //    params: { id: $("#projectId").val() }
    //}).success(function (result) {
    //    $scope.Calculations = result;
    //});
}

function actionsContractLink(data, type, full, meta) {
    return '<a   href="/Expertise/Project/DetailCard?id=' + full.ProjectId + '" >' +
   full.ProjectNumber +
   '</a>';
}

function actionsContractCommand(data, type, full, meta) {
    return '<a  class="btn btn-outline btn-warning btn-xs" href="/Expertise/Contract/Edit?id=' + data + '">' +
        '   <i class="fa fa-edit"></i>' +
        '</a>&nbsp;' +
        '<a  class="btn btn-outline btn-success btn-xs" href="/Expertise/Contract/Details?id=' + data + '">' +
        '   <i class="fa fa-search"></i>' +
        '</a>';
}

angular
    .module('app')
    .controller('updateContract', ['$scope', '$http', updateContract])
    .controller('createContract', ['$scope', '$http', createContract])
    .controller('contractList', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder', contractList])
    .controller('contractProjectCtrl', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder', 'SweetAlert', contractProjectCtrl])