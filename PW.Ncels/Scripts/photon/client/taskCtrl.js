function taskNewCtrl($scope, DTColumnBuilder) {
    $scope.dtColumns = [
        DTColumnBuilder.newColumn("Id", "Номер").withOption('name', 'Id'),
        DTColumnBuilder.newColumn("ExecutionDate", "Дата исполнения").withOption('name', 'ExecutionDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("ProjectNumber", "Номер проекта").withOption('name', 'ProjectNumber').renderWith(actionsHtml),
        DTColumnBuilder.newColumn("TaskTypeName", "Тип").withOption('name', 'TaskTypeName'),
        DTColumnBuilder.newColumn("AuthorName", "Автор").withOption('name', 'AuthorName'),
        DTColumnBuilder.newColumn("Id", "Действия").withOption('name', 'Id').renderWith(actionsTask)
    ];
}

function taskWorkCtrl($scope,   DTColumnBuilder) {
    $scope.dtColumns = [
       DTColumnBuilder.newColumn("Id", "Номер").withOption('name', 'Id'),
       DTColumnBuilder.newColumn("ExecutionDate", "Дата исполнения").withOption('name', 'ExecutionDate').renderWith(dateformatHtml),
       DTColumnBuilder.newColumn("ProjectNumber", "Номер проекта").withOption('name', 'ProjectNumber').renderWith(actionsHtml),
       DTColumnBuilder.newColumn("TaskTypeName", "Тип").withOption('name', 'TaskTypeName'),
       DTColumnBuilder.newColumn("AuthorName", "Автор").withOption('name', 'AuthorName'),
       DTColumnBuilder.newColumn("Id", "Действия").withOption('name', 'Id').renderWith(actionsTask)
    ];
}

function taskSuccessCtrl($scope, DTColumnBuilder) {
    $scope.dtColumns = [
        DTColumnBuilder.newColumn("Id", "Номер").withOption('name', 'Id'),
        DTColumnBuilder.newColumn("ExecutionDate", "Дата исполнения").withOption('name', 'ExecutionDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("ProjectNumber", "Номер проекта").withOption('name', 'ProjectNumber').renderWith(actionsHtml),
        DTColumnBuilder.newColumn("TaskTypeName", "Тип").withOption('name', 'TaskTypeName'),
        DTColumnBuilder.newColumn("AuthorName", "Автор").withOption('name', 'AuthorName'),
        DTColumnBuilder.newColumn("Id", "Действия").withOption('name', 'Id').renderWith(actionsTask)
    ];
}

function taskNewClientCtrl($scope,  DTColumnBuilder) {
    $scope.dtColumns = [
        DTColumnBuilder.newColumn("ProjectNumber", "Номер проекта").withOption('name', 'ProjectNumber').renderWith(actionsTask),
        DTColumnBuilder.newColumn("ApplicationDate", "Дата проекта").withOption('name', 'ApplicationDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("ExecutorOrgName", "Исполнитель").withOption('name', 'ExecutorOrgName'),
        DTColumnBuilder.newColumn("ProjectName", "Наименование объекта").withOption('name', 'ProjectName'),
        DTColumnBuilder.newColumn("TaskTypeName", "Тип поручения").withOption('name', 'TaskTypeName'),
        DTColumnBuilder.newColumn("AuthorName", "Автор поручения").withOption('name', 'AuthorName'),
        DTColumnBuilder.newColumn("ExecutionDate", "Срок исполнения").withOption('name', 'ExecutionDate').renderWith(dateformatHtml)
    ];
}

function taskWorkClientCtrl($scope,  DTColumnBuilder) {
    $scope.dtColumns = [
        DTColumnBuilder.newColumn("ProjectNumber", "Номер проекта").withOption('name', 'ProjectNumber').renderWith(actionsTask),
        DTColumnBuilder.newColumn("ApplicationDate", "Дата проекта").withOption('name', 'ApplicationDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("ExecutorOrgName", "Исполнитель").withOption('name', 'ExecutorOrgName'),
        DTColumnBuilder.newColumn("ProjectName", "Наименование объекта").withOption('name', 'ProjectName'),
        DTColumnBuilder.newColumn("TaskTypeName", "Тип поручения").withOption('name', 'TaskTypeName'),
        DTColumnBuilder.newColumn("AuthorName", "Автор поручения").withOption('name', 'AuthorName'),
        DTColumnBuilder.newColumn("ExecutionDate", "Срок исполнения").withOption('name', 'ExecutionDate').renderWith(dateformatHtml)
    ];
}

function taskSuccessClientCtrl($scope,  DTColumnBuilder) {
    $scope.dtColumns = [
        DTColumnBuilder.newColumn("ProjectNumber", "Номер проекта").withOption('name', 'ProjectNumber').renderWith(actionsTask),
        DTColumnBuilder.newColumn("ApplicationDate", "Дата проекта").withOption('name', 'ApplicationDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("ExecutorOrgName", "Исполнитель").withOption('name', 'ExecutorOrgName'),
        DTColumnBuilder.newColumn("ProjectName", "Наименование объекта").withOption('name', 'ProjectName'),
        DTColumnBuilder.newColumn("TaskTypeName", "Тип поручения").withOption('name', 'TaskTypeName'),
        DTColumnBuilder.newColumn("AuthorName", "Автор поручения").withOption('name', 'AuthorName'),
        DTColumnBuilder.newColumn("ExecutionDate", "Срок исполнения").withOption('name', 'ExecutionDate').renderWith(dateformatHtml)
    ];
}

//function taskTreeCtrl($scope, $http) {

//    $scope.selectFun = function(data) {
//        $http({
//            method: 'GET',
//            url: '/Expertise/Task/ReadTask?id=' + data,
//            data: 'JSON'
//        }).success(function (result) {

//            $scope.htmlContent = result;
//        });
//    }

//}

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

function actionsTask(data, type, full, meta) {
    return '<a  class="pw-task-link" href="/Client/Project/DetailTasks?id=' + full.ProjectId + '&taskId=' + full.Id + '" >' + data + '</a>';
}
function actionsHtml(data, type, full, meta) {
    return '<a   href="/Client/Project/Detail?id=' + full.ProjectId + '" >' +
   full.ProjectNumber +
   '</a>';
}

function updateTask($scope, $http) {
    $scope.object = {};
    loadEnums($scope, 'TaskType', $http);

    var id = $("#taskId").val();
    $http({
        method: 'GET',
        url: '/Client/Task/GetData?id=' + id,
        data: 'JSON'
    }).success(function (result) {
        $scope.object = result;
        $scope.object.ExecutionDate = new Date(parseInt(result.ExecutionDate.slice(6, -2)));
        $scope.object.ExecutorOrgId = $scope.getExecutorOrganizationId(result.ExecutorId);
    });


    $scope.update = function () {
        $http({
            url: '/Client/Task/Update',
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

    $scope.getExecutorOrganizationId = function (id) {
        $http({
            method: 'GET',
            url: '/Dictionaries/GetExecutorId',
            data: 'JSON',
            params: { id: id }
        }).success(function (result) {
            $scope.object.ExecutorOrgId = result;
            $scope.GetExecutorEmployee();
        });
    }

    $http({
        method: 'GET',
        url: '/Dictionaries/GetExecutor',
        data: 'JSON'
    }).success(function (result) {
        $scope.ExecutorOrgs = result;
    });

    $scope.GetExecutorEmployee = function () {
        $http({
            method: 'GET',
            url: '/Dictionaries/GetExecutor',
            data: 'JSON',
            params: { id: $scope.object.ExecutorOrgId }
        }).success(function (result) {
            $scope.Executors = result;
        });
    }
}
function taskTreeCtrl($scope, $http, SweetAlert) {

    var id = $('#taskId').val();
    var projectId = $('#projectId').val();
    $scope.object = {
        ExecutionDate: new Date(),
        ParentId: id,
        ProjectId: projectId,
        Text: '',
        Executors: []
    };
    $scope.expert = {
        Date: new Date(),
        TaskId: id,
        ProjectId: projectId
    };
    $scope.contract = {
        Date: new Date(),
        TaskId: id,
        ProjectId: projectId
    };
    $scope.conclusion = {
        Date: new Date(),
        TaskId: id,
        ProjectId: projectId
    };
    $scope.completen = {
        Date: new Date(),
        TaskId: id,
        ProjectId: projectId
    };
    $scope.calculation = {
        Date: new Date(),
        TaskId: id,
        ProjectId: projectId
    };

    $scope.detail = function () {
        $scope.htmlContent = $scope.detailContent;
    }

    $scope.selectFun = function (data) {
        id = data[0];
        $scope.expert.TaskId = id;
        $scope.contract.TaskId = id;
        $scope.conclusion.TaskId = id;
        $scope.completen.TaskId = id;
        $scope.calculation.TaskId = id;
        $scope.object.ParentId = data[0];
        $scope.object.Text = '';
        $scope.object.Executors = [];
        if (id != undefined) {
            $http({
                method: 'GET',
                url: '/Client/Task/ReadTask?id=' + data,
                data: 'JSON'
            }).success(function (result) {
                $scope.detailContent = result;
                $scope.htmlContent = result;
            });
        }
        $http({
            method: 'GET',
            url: '/Client/Task/GetData?id=' + id,
            data: 'JSON'
        }).success(function (result) {
            $scope.task = result;
            //if (result.TaskStateCode == "success") {
            //    $scope.getReport(result.TaskTypeCode);
            //} else {
            //    $scope.htmlContentReport = null;
            //}
        });
    }
    $scope.detailTask = function () {
        $http({
            method: 'GET',
            url: '/Client/Task/ReadTask?id=' + id,
            data: 'JSON'
        }).success(function (result) {

            $scope.htmlContent = result;
        });
    }
    $scope.reassignmentTask = function () {
        $scope.object.Text = '';
        $scope.object.Executors = [];
        $http({
            method: 'GET',
            url: '/Expertise/Task/Create',
            data: 'JSON'
        }).success(function (result) {

            $scope.htmlContent = result;
        });
    }

    $scope.deciseAgreeement = function () {
        if ($scope.executeAgreementForm.$valid) {
            $http({
                url: '/Client/Task/DecisionAgree',
                method: 'POST',
                data: JSON.stringify($scope.agreement)
            }).success(function (response) {
                if (!response.IsError) {
                    $scope.htmlContent = '<p>Все хорошо</p>';
                    //$scope.reloadTree(response.Result);
                } else {
                    alert(response.Message);
                }
            });
        }
    }

    $scope.deciseRemark = function (code) {
        if ($scope.executeRemarkForm.$valid) {
            $http({
                url: '/Client/Task/DecisionRemark',
                method: 'POST',
                data: JSON.stringify($scope.remark),
                params: { code: $scope.task.TaskTypeCode }
            }).success(function (response) {
                if (!response.IsError) {
                    $scope.htmlContent = '<p>Все хорошо</p>';
                    //$scope.reloadTree(response.Result);
                } else {
                    alert(response.Message);
                }
            });
        }
    }

    $scope.executeTask = function () {
        $scope.contract.Number = '';
        $scope.contract.Date = new Date();
        $scope.contract.CalculationId = '';
        $scope.contract.ContractTypeId = '';
        $scope.contract.PrepayPercent = '';
        $scope.contract.ExecutionPeriod = '';

        $scope.expert.ExpertiseTypeId = '';
        $scope.expert.Date = new Date();
        $scope.expert.Text = '';

        $scope.conclusion.Number = '';
        $scope.conclusion.ConclusionTypeId = '';

        $scope.completen.Date = new Date();
        $scope.completen.Text = '';
        $scope.completen.CompletenessTypeId = '';

        $scope.calculation.Date = new Date();
        $scope.calculation.CalculationTypeId = '';
        $scope.calculation.Pir = '';
        $scope.calculation.Year = '';
        $scope.calculation.MrpPriv = '';
        $scope.calculation.MrpCalc = '';
        $scope.calculation.PirCalc = '';
        $scope.calculation.Norm = '';
        $scope.calculation.Cost = '';
        $scope.calculation.NdsPercent = '';
        $scope.calculation.CostNds = '';
        $scope.calculation.Nds = '';


        $scope.agreement = { Id: id };

        $scope.remark = { Id: id };

        var url = "";
        switch ($scope.task.TaskTypeCode) {
            case "remark":
            case "correspondence":
                url = '/Client/Task/Remark';
                break;
            case "agreement":
                url = '/Client/Task/Agreement';
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
                $scope.htmlContent = result;
            });
        }


        $http({
            method: 'GET',
            url: '/Dictionaries/GetAgreementState',
            data: 'JSON'
        }).success(function (result) {
            $scope.AgreementType = result;
        });
        //$http({
        //    method: 'GET',
        //    url: '/Client/Task/Exclude?id=' + id,
        //    data: 'JSON'
        //}).success(function (result) {
        //    $scope.htmlContent = result;
        //});
    }
    $scope.editTask = function () {
        $http({
            method: 'GET',
            url: '/Client/Task/Edit?id=' + id,
            data: 'JSON'
        }).success(function (resultData) {
            $http({
                method: 'GET',
                url: '/Client/Task/GetData?id=' + id,
                data: 'JSON'
            }).success(function (result) {
                result.ExecutionDate = new Date(parseInt(result.ExecutionDate.slice(6, -2)));
                $scope.object = result;
                $scope.htmlContent = resultData;
            });
        });
    }
    $scope.create = function () {
        if ($scope.taskForm.$valid) {
            $http({
                url: '/Expertise/Task/Create',
                method: 'POST',
                data: JSON.stringify($scope.object)
            }).success(function (response) {
                if (!response.IsError) {
                    $scope.htmlContent = '<p>Все хорошо</p>';
                    $scope.reloadTree(response.Result);
                } else {
                    alert(response.Message);
                }
            });
        }
    }

    $http({
        method: 'GET',
        url: '/Dictionaries/GetExecutorByProject?id=' + projectId,
        data: 'JSON'
    }).success(function (result) {
        $scope.Executors = result;
    });

    $scope.update = function () {
        if ($scope.editTaskForm.$valid) {
            $http({
                url: '/Expertise/Task/Update',
                method: 'POST',
                data: JSON.stringify($scope.object)
            }).success(function (response) {
                $scope.htmlContent = '<p>Все хорошо</p>';
            });
        }
    }
    $scope.createExpert = function () {
        if ($scope.expertiseForm.$valid) {
            $http({
                url: '/Expertise/Expertise/Create',
                method: 'POST',
                data: JSON.stringify($scope.expert)
            }).success(function (response) {
                if (!response.IsError) {

                    $scope.htmlContent = '<p>Все хорошо</p>';
                } else {
                    alert(response.Message);
                }
            });
        }
    }
    $scope.createContract = function () {
        if ($scope.contractForm.$valid) {
            $http({
                url: '/Expertise/Contract/Create',
                method: 'POST',
                data: JSON.stringify($scope.contract)
            }).success(function (response) {
                if (!response.IsError) {

                    $scope.htmlContent = '<p>Все хорошо</p>';
                } else {
                    alert(response.Message);
                }
            });
        }
    }
    $scope.createConclusion = function () {
        if ($scope.conclusionForm.$valid) {
            $http({
                url: '/Expertise/Conclusion/Create',
                method: 'POST',
                data: JSON.stringify($scope.conclusion)
            }).success(function (response) {
                if (!response.IsError) {

                    $scope.htmlContent = '<p>Все хорошо</p>';
                } else {
                    alert(response.Message);
                }
            });
        }
    }


    $scope.createCompleten = function () {

        if ($scope.completenForm.$valid) {
            $http({
                url: '/Expertise/Completeness/Create',
                method: 'POST',
                data: JSON.stringify($scope.completen)
            }).success(function (response) {
                if (!response.IsError) {
                    $scope.htmlContent = '<p>Все хорошо</p>';
                } else {
                    alert(response.Message);
                }
            });
        }
    }




    // авто заполнения расчетов не забудь перенести вместе с контроллероом
    $scope.setPirCalc = function () {
        if ($scope.calculation.Pir != '' && $scope.calculation.MrpPriv != '' && $scope.calculation.MrpCalc != '' && $scope.calculation.MrpCalc != 0) {
            var pir = $scope.calculation.Pir;
            var mrpPriv = $scope.calculation.MrpPriv;
            var mrpCalc = $scope.calculation.MrpCalc;
            var norm;
            $scope.calculation.PirCalc = pir * mrpPriv / mrpCalc;
            var pirCalc = $scope.calculation.PirCalc;
            if (pirCalc <= 3682.08) {
                $scope.calculation.Norm = 30.28 + ((pirCalc - 3682.08) / ((0 - 3682.08) * (0 - 30.28)));
                norm = $scope.calculation.Norm;
            } else {
                $scope.calculation.Norm = 1.1431 * (Math.pow(pirCalc, 0.39906));
                norm = $scope.calculation.Norm;
            }
            var psod = 23982;
            $scope.calculation.Cost = psod * norm;
            var cost = $scope.calculation.Cost;
            $scope.calculation.CostNds = cost * 1.12;
            $scope.calculation.Nds = cost * 0.12;
        }
    }

    $scope.setCode = function (calcType) {
        $scope.calcType = calcType;
    }

    $scope.createCalculation = function () {
        if ($scope.calculationForm.$valid) {
            //if ($scope.calculation.Cost == "") {
            //    $scope.calculation.Cost = $scope.calculation.ContractCost;
            //}
            $http({
                url: '/Expertise/Calculation/Create',
                method: 'POST',
                data: JSON.stringify($scope.calculation)
            }).success(function (response) {
                if (!response.IsError) {

                    $scope.htmlContent = '<p>Все хорошо</p>';
                } else {
                    alert(response.Message);
                }
            });
        }
    }
    //$http({
    //    method: 'GET',
    //    url: '/Expertise/Contract/GetCalculation',
    //    data: 'JSON',
    //    params: { id: projectId }
    //}).success(function (result) {
    //    $scope.Calculations = result;
    //});
    $scope.deleteTask = function () {
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
                        url: '/Client/Task/Delete',
                        data: { id: id }
                    }).success(function (result) {
                        if (!result.IsError) {
                            $scope.deleteNode(id);
                            SweetAlert.swal("Удаление!", "Задание удалено.", "success");
                            $scope.htmlContent = '<p>Все хорошо</p>';
                        } else {
                            SweetAlert.swal("Отмена", "Ошибка удаления :)", "error");
                        }
                    });

                } else {
                    SweetAlert.swal("Отмена", "Вы отменили удаление :)", "error");
                }
            });
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

    $scope.spinOptionM = {
        postfix: 'тенге',
        verticalbuttons: true,
        min: 1982,
        max: 2121,
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

    loadEnums($scope, 'CalculationType', $http);
    loadEnums($scope, 'CompletenessType', $http);
    loadEnums($scope, 'ConclusionType', $http);
    loadEnums($scope, 'ContractType', $http);
    loadEnums($scope, 'ExpertiseType', $http);
    loadEnums($scope, 'TaskType', $http);
}
angular
    .module('app')
    .controller('taskTreeCtrl', ['$scope', '$http', 'SweetAlert',taskTreeCtrl])
    .controller('taskNewCtrl', ['$scope', 'DTColumnBuilder',taskNewCtrl]) 
    .controller('taskSuccessCtrl', ['$scope', 'DTColumnBuilder',taskSuccessCtrl])
    .controller('taskWorkCtrl', ['$scope', 'DTColumnBuilder',taskWorkCtrl])
    .controller('taskNewClientCtrl', ['$scope', 'DTColumnBuilder',taskNewClientCtrl])
    .controller('taskSuccessClientCtrl', ['$scope', 'DTColumnBuilder',taskSuccessClientCtrl])
    .controller('taskWorkClientCtrl',['$scope', 'DTColumnBuilder',taskWorkClientCtrl])
    .controller('updateTask', ['$scope', '$http',updateTask])
    //.controller('taskTreeCtrl', taskTreeCtrl)
