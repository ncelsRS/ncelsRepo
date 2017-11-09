function detailsProjectCtrl($scope, $http, DTOptionsBuilder, DTColumnBuilder) {

 
    $scope.changeHash = function(e) {
        window.location = e;
    }

    
}


function detailsAttachCtrl($scope, $http, DTOptionsBuilder, DTColumnBuilder) {


    $http({
        method: 'GET',
        url: '/Upload/GetAttachList',
        data: 'JSON',
        params: { type: 'sysAttachMaterialsDict', id: $("#projectId").val() }
    }).success(function (result) {
        $scope.attachMaterials = result;
    });
    $http({
        method: 'GET',
        url: '/Upload/GetAttachList',
        data: 'JSON',
        params: { type: 'sysAttachProjectDocumentationDict', id: $("#projectId").val() }
    }).success(function (result) {
        $scope.attachProjectDocumentation = result;
    });
    $http({
        method: 'GET',
        url: '/Upload/GetAttachList',
        data: 'JSON',
        params: { type: 'sysAttachExpertDic', id: $("#projectId").val() }
    }).success(function (result) {
        $scope.sysAttachExpertDic = result;
    });



  


    $scope.changeHash = function (e) {
        window.location = e;
    }


   
}

function detailsHistoryCtrl($scope, $http, DTOptionsBuilder, DTColumnBuilder) {


  



    //$scope.dtColumnsHistory = [

    //   DTColumnBuilder.newColumn("Record", "Запись").withOption('name', 'Record'),

    //   DTColumnBuilder.newColumn("Time", "Время").withOption('name', 'Time'),
    //   DTColumnBuilder.newColumn("User", "Пользователь").withOption('name', 'User'),
    //   DTColumnBuilder.newColumn("OperationValue", "Операция").withOption('name', 'OperationValue'),
    //          DTColumnBuilder.newColumn("OldValue", "Старое значение").withOption('name', 'OldValue'),
    //   DTColumnBuilder.newColumn("NewValue", "Новое значение").withOption('name', 'NewValue')
    //];



    $scope.changeHash = function (e) {
        window.location = e;
    }


    //$scope.dtOptionsHistory = DTOptionsBuilder.newOptions().withOption('ajax', {
    //    dataSrc: 'Data',
    //    url: "/History/Index",
    //    type: "POST"
    //    , data: function (d) {
    //        d.id = $("#projectId").val();
    //    }
    //}).withOption('oLanguage', {
    //    "sUrl": "/Content/json/Russian.json"
    //})
    //    .withOption('processing', true) //for show progress bar
    //    .withOption('serverSide', true) // for server side processing
    //    .withPaginationType('full_numbers') // for get full pagination options // first / last / prev / next and page numbers
    //    .withDisplayLength(10) // Page size
    //    .withOption('aaSorting', [0, 'asc'])
    //    .withDOM('<"html5buttons"B>lTfgitp')
    //    .withButtons([

    //        { extend: 'csv' },
    //        { extend: 'excel', title: 'Список' },
    //        { extend: 'pdf', title: 'Список' },
    //        {
    //            extend: 'print',
    //            customize: function (win) {
    //                $(win.document.body).addClass('white-bg');
    //                $(win.document.body).css('font-size', '10px');

    //                $(win.document.body).find('table')
    //                    .addClass('compact')
    //                    .css('font-size', 'inherit');
    //            }
    //        }
    //    ]);
}

function detailsTaskCtrl($scope, $http, DTOptionsBuilder, DTColumnBuilder) {
    function dateformatHtml(data, type, full, meta) {
        var date = new Date(parseInt(data.slice(6, -2)));
        var month = date.getMonth() + 1;
        return date.getDate() + "." + (month.length > 1 ? month : "0" + month) + "." + date.getFullYear();
    }
    
    $scope.dtColumnsHistory = [
       DTColumnBuilder.newColumn("Id", "Номер").withOption('name', 'Id'),
       DTColumnBuilder.newColumn("TaskStateName", "Статус").withOption('name', 'TaskStateName'),
       DTColumnBuilder.newColumn("ExecutionDate", "Срок исполнения").withOption('name', 'ExecutionDate').renderWith(dateformatHtml),
       DTColumnBuilder.newColumn("TaskTypeName", "Тип").withOption('name', 'TaskTypeName'),
       DTColumnBuilder.newColumn("Text", "Текст").withOption('name', 'Text')
    ];



    $scope.changeHash = function (e) {
        window.location = e;
    }


    $scope.dtOptionsHistory = DTOptionsBuilder.newOptions().withOption('ajax', {
        dataSrc: 'Data',
        url: "/Client/Task/Read",
        type: "POST"
        , data: function (d) {
            d.id = $("#projectId").val();
        }
    }).withOption('oLanguage', {
        "sUrl": "/Content/json/Russian.json"
    })
        .withOption('processing', true) //for show progress bar
        .withOption('serverSide', true) // for server side processing
        .withPaginationType('full_numbers') // for get full pagination options // first / last / prev / next and page numbers
        .withDisplayLength(10) // Page size
        .withOption('aaSorting', [0, 'asc'])
        .withDOM('<"html5buttons"B>lTfgitp')
        .withButtons([

            { extend: 'csv' },
            { extend: 'excel', title: 'Список' },
            { extend: 'pdf', title: 'Список' },
            {
                extend: 'print',
                customize: function (win) {
                    $(win.document.body).addClass('white-bg');
                    $(win.document.body).css('font-size', '10px');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ]);
}

function detailsContractCtrl($scope, $http, DTOptionsBuilder, DTColumnBuilder) {


    function dateformatHtml(data, type, full, meta) {
        var date = new Date(parseInt(data.slice(6, -2)));
        var month = date.getMonth() + 1;
        return date.getDate() + "." + (month.length > 1 ? month : "0" + month) + "." + date.getFullYear();
    }

    $scope.dtColumnsHistory = [
        DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("Date", "Дата").withOption('name', 'Date').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("PrepayPercent", "PrepayPercent").withOption('name', 'PrepayPercent'),
        DTColumnBuilder.newColumn("ContractStateName", "Статус").withOption('name', 'ContractStateName'),
        DTColumnBuilder.newColumn("ContractTypeName", "Тип").withOption('name', 'ContractTypeName'),
        DTColumnBuilder.newColumn("SignEmployeeName", "Подписывающий").withOption('name', 'SignEmployeeName'),
        DTColumnBuilder.newColumn("ExecutorName", "Исполнитель").withOption('name', 'ExecutorName')
    ];


    $scope.changeHash = function(e) {
        window.location = e;
    }


    $scope.dtOptionsHistory = DTOptionsBuilder.newOptions().withOption('ajax', {
            dataSrc: 'Data',
            url: "/Client/Contract/Read",
            type: "POST",
            data: function(d) {
                d.id = $("#projectId").val();
            }
        }).withOption('oLanguage', {
            "sUrl": "/Content/json/Russian.json"
        })
        .withOption('processing', true) //for show progress bar
        .withOption('serverSide', true) // for server side processing
        .withPaginationType('full_numbers') // for get full pagination options // first / last / prev / next and page numbers
        .withDisplayLength(10) // Page size
        .withOption('aaSorting', [0, 'asc'])
        .withDOM('<"html5buttons"B>lTfgitp')
        .withButtons([
            { extend: 'csv' },
            { extend: 'excel', title: 'Список' },
            { extend: 'pdf', title: 'Список' },
            {
                extend: 'print',
                customize: function(win) {
                    $(win.document.body).addClass('white-bg');
                    $(win.document.body).css('font-size', '10px');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ]);
}

function detailsConclusionCtrl($scope, $http, DTOptionsBuilder, DTColumnBuilder) {


    function dateformatHtml(data, type, full, meta) {
        var date = new Date(parseInt(data.slice(6, -2)));
        var month = date.getMonth() + 1;
        return date.getDate() + "." + (month.length > 1 ? month : "0" + month) + "." + date.getFullYear();
    }


    $scope.dtColumnsHistory = [

       DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
       DTColumnBuilder.newColumn("Date", "Дата").withOption('name', 'Date').renderWith(dateformatHtml),

       DTColumnBuilder.newColumn("ConclusionStateName", "Статус").withOption('name', 'ConclusionStateName'),
       DTColumnBuilder.newColumn("ConclusionTypeName", "Тип").withOption('name', 'ConclusionTypeName'),
       DTColumnBuilder.newColumn("SignEmployeeName", "Подписывающий").withOption('name', 'SignEmployeeName')
    ];



    $scope.changeHash = function (e) {
        window.location = e;
    }


    $scope.dtOptionsHistory = DTOptionsBuilder.newOptions().withOption('ajax', {
        dataSrc: 'Data',
        url: "/Client/Conclusion/Read",
        type: "POST"
        , data: function (d) {
            d.id = $("#projectId").val();
        }
    }).withOption('oLanguage', {
        "sUrl": "/Content/json/Russian.json"
    })
        .withOption('processing', true) //for show progress bar
        .withOption('serverSide', true) // for server side processing
        .withPaginationType('full_numbers') // for get full pagination options // first / last / prev / next and page numbers
        .withDisplayLength(10) // Page size
        .withOption('aaSorting', [0, 'asc'])
        .withDOM('<"html5buttons"B>lTfgitp')
        .withButtons([

            { extend: 'csv' },
            { extend: 'excel', title: 'Список' },
            { extend: 'pdf', title: 'Список' },
            {
                extend: 'print',
                customize: function (win) {
                    $(win.document.body).addClass('white-bg');
                    $(win.document.body).css('font-size', '10px');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ]);
}

angular
    .module('app')
    .controller('detailsProjectCtrl', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder',detailsProjectCtrl])
    .controller('detailsAttachCtrl', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder',detailsAttachCtrl])
    .controller('detailsHistoryCtrl', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder',detailsHistoryCtrl])
    .controller('detailsTaskCtrl', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder',detailsTaskCtrl])
    .controller('detailsContractCtrl', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder',detailsContractCtrl])
    .controller('detailsConclusionCtrl', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder',detailsConclusionCtrl]);

