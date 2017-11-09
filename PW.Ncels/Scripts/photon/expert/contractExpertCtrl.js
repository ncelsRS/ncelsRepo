function updateContract($scope, $http) {
    $scope.object = {};
    //loadEnums($scope, 'ContractType', $http);

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

function createContract($scope, $http, SweetAlert, notify) {

    loadEnums($scope, 'ContractType', $http);

    $scope.getCalculation = function() {
        $http({
            method: 'GET',
            url: '/Expertise/Contract/GetCalculation',
            data: 'JSON',
            params: { id: $scope.object.contract.ProjectId }
        }).success(function (result) {
            $scope.Calculations = result;
        });
    }

    $scope.createContract = function () {
        if ($scope.contractForm.$valid) {
            $http({
                url: '/Expertise/Contract/Create',
                method: 'POST',
                data: JSON.stringify($scope.contract)
            }).success(function (response) {
                if (!response.IsError) {
                    notify({ message: 'Договор исполнен!', classes: 'alert-info', templateUrl: '/Content/templates/notify.html' });
                    $scope.initContractUpdate(response.Id);
                } else {
                    alert(response.Message);
                }
            });
        }
    }
    $scope.saveContract = function () {
        if ($scope.editContractForm.$valid) {
            $http({
                url: '/Expertise/Contract/Update',
                method: 'POST',
                data: JSON.stringify($scope.object.contract)
            }).success(function (response) {
                if (!response.IsError) {
                    notify({ message: 'Договор изменен!', classes: 'alert-info', templateUrl: '/Content/templates/notify.html' });

                    $scope.htmlContentReportIn = $scope.htmlContentReportInTemp;
                } else {
                    alert(response.Message);
                }
            });
        }
    }
    $scope.initContract = function (attach, taskId, projectId) {

        $scope.contract = {
            Date: new Date(),
            TaskId: taskId,
            ProjectId: projectId
        };
        $scope.contract.AttachPath = attach;

        $http({
            method: 'GET',
            url: '/Expertise/Contract/GetCalculation',
            data: 'JSON',
            params: { id: projectId }
        }).success(function (result) {
            $scope.Calculations = result;
        });

        $http({
            url: '/Expertise/Contract/GetClientInfo',
            method: 'GET',
            params: { id: projectId }
        }).success(function (response) {
            $scope.contract.client = response;
        });



    }

    $scope.initContractUpdate = function (taskId) {

        $http({
            method: 'GET',
            url: '/Expertise/Contract/Details?id=' + taskId,
            data: 'JSON'
        }).success(function (result) {
            $scope.htmlContentReportIn = result;
            $scope.htmlContentReportInTemp = result;
        });
        $http({
            method: 'GET',
            url: '/Expertise/Contract/ReadContract?id=' + taskId,
            data: 'JSON'
        }).success(function (result) {
            $scope.object = result;
        });
    }
    $scope.detailContract = function () {
        $scope.htmlContentReportIn = '';
    }

    $scope.generateContract = function () {
        if ($scope.contractForm.$valid) {
            $http({
                url: '/Expertise/Contract/Generate',
                method: 'POST',
                data: JSON.stringify($scope.contract)
            }).success(function (response) {
                if (!response.IsError) {
                    notify({ message: 'Договор сгенерирован!', classes: 'alert-info', templateUrl: '/Content/templates/notify.html' });
                } else {
                    alert(response.Message);
                }
            });
        }
    }
    $scope.detail = function () {
        $http({
            method: 'GET',
            url: '/Expertise/Contract/Details?id=' + $scope.object.contract.Id,
            data: 'JSON'
        }).success(function (result) {
            $scope.htmlContentReportIn = result;
        });
    }
    $scope.editContract = function () {
        $http({
            method: 'GET',
            url: '/Expertise/Contract/Edit?id=' + $scope.object.contract.Id,
            data: 'JSON'
        }).success(function (result) {
            $scope.htmlContentReportIn = result;
        });
    }
    $scope.generateContractUpdate = function () {
        if ($scope.contractForm.$valid) {
            $http({
                url: '/Expertise/Contract/Generate',
                method: 'POST',
                data: JSON.stringify($scope.object.contract)
            }).success(function (response) {
                if (!response.IsError) {
                    notify({ message: 'Договор сгенерирован!', classes: 'alert-info', templateUrl: '/Content/templates/notify.html' });
                } else {
                    alert(response.Message);
                }
            });
        }
    }

    $scope.getExecutors = function () {
        $http({
            method: 'GET',
            url: '/Dictionaries/GetExecutorByProject?id=' + $scope.object.contract.ProjectId,
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
        $scope.agreementObj.ProjectId = $scope.object.contract.ProjectId;
        $scope.agreementObj.Id = $scope.object.contract.TaskId;
        $scope.agreementObj.Executors = [];
        $http({
            method: 'GET',
            url: '/Expertise/Agreement/Create',
            data: 'JSON',
            params: { taskId: $scope.object.contract.TaskId, projectId: $scope.object.contract.ProjectId }
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
        $scope.singObj.ProjectId = $scope.object.contract.ProjectId;
        $scope.singObj.Executors = [];
        $scope.singObj.Id = $scope.object.contract.TaskId;
        $http({
            method: 'GET',
            url: '/Expertise/Signing/Create',
            data: 'JSON',
            params: { taskId: $scope.object.contract.TaskId, projectId: $scope.object.contract.ProjectId }
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

    $scope.getContractSecond = function () {
        $http({
            method: 'GET',
            url: '/Dictionaries/GetContarctSecond',
            params: { id: $scope.object.contract.ProjectId },
            data: 'JSON'
        }).success(function (result) {
            $scope.ContarctSecond = result;
        });
    }

    $scope.annul = function () {
        $scope.object.contract.AnnulDate = new Date();
        $http({
            method: 'GET',
            url: '/Expertise/Contract/Annul'
        }).success(function (result) {
            $scope.htmlContentReportIn = result;
        });
    }

    $scope.prolong = function () {
        $scope.object.contract.ProlongDate = new Date();
        $http({
            method: 'GET',
            url: '/Expertise/Contract/Prolong'
        }).success(function (result) {
            $scope.htmlContentReportIn = result;
        });
    }

    $scope.dissolve = function () {
        $scope.object.contract.DissolveDate = new Date();
        $http({
            method: 'GET',
            url: '/Expertise/Contract/Dissolve'
        }).success(function (result) {
            $scope.htmlContentReportIn = result;
        });
    }

    $scope.annulContract = function () {
        if ($scope.annulContractForm.$valid) {
            $http({
                url: '/Expertise/Contract/Annul',
                method: 'POST',
                data: JSON.stringify($scope.object.contract)
            }).success(function (response) {
                if (!response.IsError) {

                    notify({ message: 'Договор аннулирован!', classes: 'alert-info', templateUrl: '/Content/templates/notify.html' });
                } else {
                    alert(response.Message);
                }
            });
        }
    }


    $scope.dissolveContract = function () {
        if ($scope.dissolveContractForm.$valid) {
            $http({
                url: '/Expertise/Contract/Disolve',
                method: 'POST',
                data: JSON.stringify($scope.object.contract)
            }).success(function (response) {
                if (!response.IsError) {
                    notify({ message: 'Договор расторгнут!', classes: 'alert-info', templateUrl: '/Content/templates/notify.html' });
                } else {
                    alert(response.Message);
                }
            });
        }
    }


    $scope.prolongContract = function () {
        if ($scope.prolongContractForm.$valid) {
            $http({
                url: '/Expertise/Contract/Prolongate',
                method: 'POST',
                data: JSON.stringify($scope.object.contract)
            }).success(function (response) {
                if (!response.IsError) {
                    notify({ message: 'Договор продлен!', classes: 'alert-info', templateUrl: '/Content/templates/notify.html' });
                } else {
                    alert(response.Message);
                }
            });
        }
    }

    $scope.deleteContract = function () {
        $scope.htmlContentReport = '';
        SweetAlert.swal({
            title: "Удалить договор?",
            text: "После удаление договора его не возможно восстановить!",
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
                            SweetAlert.swal("Удаление!", "Договор удален.", "success");
                            $scope.htmlContent = '<p>Договор удален</p>';
                        } else {
                            SweetAlert.swal("Отмена", "Ошибка удаления :)", "error");
                        }
                    });

                } else {
                    SweetAlert.swal("Отмена", "Вы отменили удаление :)", "error");
                }
            });
    }
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


function contractProjectCtrl($scope, $http, DTOptionsBuilder, DTColumnBuilder, SweetAlert, notify, FileUploader) {
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
            url: '/Expertise/Contract/DetailsOrEdit?id=' + data.Id,
            data: 'JSON'
        }).success(function (result) {
            $scope.htmlContent = result;
        });



    }


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
    .controller('createContract', ['$scope', '$http', 'SweetAlert', 'notify', createContract])
    .controller('contractList', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder', contractList])
    .controller('contractProjectCtrl', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder', 'notify', 'FileUploader', contractProjectCtrl])