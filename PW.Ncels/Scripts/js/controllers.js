/**
 * INSPINIA - Responsive Admin Theme
 *
 * Main controller.js file
 * Define controllers with data used in Inspinia theme
 *
 *
 * Functions (controllers)
 *  - MainCtrl
 *  - dashboardFlotOne
 *  - dashboardFlotTwo
 *  - dashboardFlotFive
 *  - dashboardMap
 *  - flotChartCtrl
 *  - rickshawChartCtrl
 *  - sparklineChartCtrl
 *  - widgetFlotChart
 *  - modalDemoCtrl
 *  - ionSlider
 *  - wizardCtrl
 *  - CalendarCtrl
 *  - chartJsCtrl
 *  - GoogleMaps
 *  - ngGridCtrl
 *  - codeEditorCtrl
 *  - nestableCtrl
 *  - notifyCtrl
 *  - translateCtrl
 *  - imageCrop
 *  - diff
 *  - idleTimer
 *  - liveFavicon
 *  - formValidation
 *  - agileBoard
 *  - draggablePanels
 *  - chartistCtrl
 *  - metricsCtrl
 *  - sweetAlertCtrl
 *  - selectCtrl
 *  - toastrCtrl
 *  - loadingCtrl
 *  - datatablesCtrl
 *  - truncateCtrl
 *  - touchspinCtrl
 *  - tourCtrl
 *  - jstreeCtrl
 *
 *
 */

/**
 * MainCtrl - controller
 * Contains several global data used in different view
 *
 */
function MainCtrl($scope, $http) {
};


//function createExpetiseObject($scope, $http,FileUploader) {
//    $scope.object = {
//        OutgoingDate: new Date(),
//        ApplicationDate: new Date()
//    };
//    $scope.BuildingBranchDict = [];
//    $scope.ObjectTypeDict = [];
//    $scope.TerritoryKatoDict = [];
//    $scope.BuildingTypeDict = [];
//    $scope.DesignStageDict = [];
//    $scope.DangerClassDict = [];
//    $scope.CategoryEccologyDict = [];
//    $scope.DeclaredCostDict = [];
//    $scope.PrimaryReviewDict = [];
//    $scope.FloorCountDict = [];
//    $scope.LevelResponsibilityDict = [];
//    $scope.CustomerConstructionDict = [];
//    $scope.DeveloperDict = [];
//    $scope.SourceFinancingDict = [];
//    $scope.ExecutorExpertUnit = [];

//       $http({
//        method: 'GET',
//        url: '/Dictionaries/GetReference',
//        data:'JSON',
//        params: { type: 'BuildingTypeDict' }
//    }).success(function (result) {
//        $scope.BuildingTypeDict = result;
//    }); 
//       $http({
//        method: 'GET',
//        url: '/Dictionaries/GetReference',
//        data:'JSON',
//        params: { type: 'ObjectTypeDict' }
//    }).success(function (result) {
//        $scope.ObjectTypeDict = result;
//    }); 
//       $http({
//        method: 'GET',
//        url: '/Dictionaries/GetReference',
//        data:'JSON',
//        params: { type: 'KatoDict' }
//    }).success(function (result) {
//        $scope.TerritoryKatoDict = result;
//    }); 
//       $http({
//        method: 'GET',
//        url: '/Dictionaries/GetReference',
//        data:'JSON',
//        params: { type: 'BuildingBranchDict' }
//    }).success(function (result) {
//        $scope.BuildingBranchDict = result;
//    }); 
//       $http({
//        method: 'GET',
//        url: '/Dictionaries/GetReference',
//        data:'JSON',
//        params: { type: 'DesignStageDict' }
//    }).success(function (result) {
//        $scope.DesignStageDict = result;
//    }); 
//       $http({
//        method: 'GET',
//        url: '/Dictionaries/GetReference',
//        data:'JSON',
//        params: { type: 'DangerClassDict' }
//    }).success(function (result) {
//        $scope.DangerClassDict = result;
//    }); 
//       $http({
//        method: 'GET',
//        url: '/Dictionaries/GetReference',
//        data:'JSON',
//        params: { type: 'CategoryEccologyDict' }
//    }).success(function (result) {
//        $scope.CategoryEccologyDict = result;
//    }); 
//       $http({
//        method: 'GET',
//        url: '/Dictionaries/GetReference',
//        data:'JSON',
//        params: { type: 'DeclaredCostDict' }
//    }).success(function (result) {
//        $scope.DeclaredCostDict = result;
//    });    
    
//    $http({
//        method: 'GET',
//        url: '/Dictionaries/GetReference',
//        data:'JSON',
//        params: { type: 'PrimaryReviewDict' }
//    }).success(function (result) {
//        $scope.PrimaryReviewDict = result;
//    });     
//    $http({
//        method: 'GET',
//        url: '/Dictionaries/GetReference',
//        data:'JSON',
//        params: { type: 'FloorCountDict' }
//    }).success(function (result) {
//        $scope.FloorCountDict = result;
//    });

//    $http({
//        method: 'GET',
//        url: '/Dictionaries/GetReference',
//        data: 'JSON',
//        params: { type: 'LevelResponsibilityDict' }
//    }).success(function (result) {
//        $scope.LevelResponsibilityDict = result;
//    });

//     $http({
//        method: 'GET',
//        url: '/Dictionaries/GetReference',
//        data:'JSON',
//        params: { type: 'CustomerConstructionDict' }
//    }).success(function (result) {
//        $scope.CustomerConstructionDict = result;
//    }); 

//     $http({
//        method: 'GET',
//        url: '/Dictionaries/GetReference',
//        data:'JSON',
//        params: { type: 'DeveloperDict' }
//    }).success(function (result) {
//        $scope.DeveloperDict = result;
//    }); 

//     $http({
//        method: 'GET',
//        url: '/Dictionaries/GetReference',
//        data:'JSON',
//        params: { type: 'SourceFinancingDict' }
//    }).success(function (result) {
//        $scope.SourceFinancingDict = result;
//    });

//    $http({
//        method: 'GET',
//        url: '/Dictionaries/GetExpertUnit',
//        data:'JSON'
//    }).success(function (result) {
//        $scope.ExecutorExpertUnit = result;
//    });


//     $scope.postRequest = function () {
//         $scope.object.DesignStageDictId = $scope.DesignStageDict.selected.Id;
//         $scope.object.BuildingTypeDictId = $scope.BuildingTypeDict.selected.Id;
//         $scope.object.ObjectTypeDictId = $scope.ObjectTypeDict.selected.Id;
//         $scope.object.TerritoryKatoDictId = $scope.TerritoryKatoDict.selected.Id;
//         $scope.object.PrimaryReviewDictId = $scope.PrimaryReviewDict.selected.Id;
//         $scope.object.BuildingBranchDictId = $scope.BuildingBranchDict.selected.Id;
//         $scope.object.LevelResponsibilityDictId = $scope.LevelResponsibilityDict.selected.Id;
//         $scope.object.CustomerConstructionDictId = $scope.CustomerConstructionDict.selected.Id;
//         $scope.object.DeveloperDictId = $scope.DeveloperDict.selected.Id;
//         $scope.object.DangerClassDictId = $scope.DangerClassDict.selected.Id;
//         $scope.object.CategoryEccologyDictId = $scope.CategoryEccologyDict.selected.Id;
//         $scope.object.DeclaredCostDictId = $scope.DeclaredCostDict.selected.Id;
//         $scope.object.FloorCountDictId = $scope.FloorCountDict.selected.Id;
//         $scope.object.SourceFinancingDictId = $scope.SourceFinancingDict.selected.Id;
//         $scope.object.ExecutorExpertId = $scope.ExecutorExpertUnit.selected.Id;
//         $http({
//             url: '/Project/PostNew',
//             method: 'POST',
//             data: JSON.stringify($scope.object)
//         }).success(function (response) {
//             if (!response.IsError) {

//                 window.location = '/Home/Success';
//             } else {
//                 alert(response.Message);
//             }
//         });
//     }
     

//     $scope.uploaders = {};

   
 
//   $http({
//       method: 'GET',
//       url: '/Upload/GetPathAttach',
//       data: 'JSON'
//   }).success(function (path) {

//       $scope.object.AttachPath = path;

//       $http({
//           method: 'GET',
//           url: '/Dictionaries/GetReference',
//           data: 'JSON',
//           params: { type: 'sysAttachMaterialsDict' }
//       }).success(function (result) {
//           result.forEach(function (item, i, arr) {
//               $scope.uploaders[item.Id] = new FileUploader({
//                   url: '/Upload/FilePost?code=' + item.Id + '&path=' + path
//               });
//               $scope.uploaders[item.Id].onSuccessItem = function (fileItem, response, status, headers) {
//                   fileItem.Id = response.Id;
                   
//               };
           
//           });
//           $scope.attachMaterials = result;
//       });
//       $http({
//           method: 'GET',
//           url: '/Dictionaries/GetReference',
//           data: 'JSON',
//           params: { type: 'sysAttachExpertDic' }
//       }).success(function (result) {
//           result.forEach(function (item, i, arr) {
//               $scope.uploaders[item.Id] = new FileUploader({
//                   url: '/Upload/FilePost?code=' + item.Id + '&path=' + path
//               });
//               $scope.uploaders[item.Id].onSuccessItem = function (fileItem, response, status, headers) {
//                   fileItem.Id = response.Id;

//               };

//           });
//           $scope.attachExp = result;
//       });
//       $http({
//           method: 'GET',
//           url: '/Dictionaries/GetReference',
//           data: 'JSON',
//           params: { type: 'sysAttachProjectDocumentationDict' }
//       }).success(function (result) {
//           result.forEach(function (item, i, arr) {
//               $scope.uploaders[item.Id] = new FileUploader({
//                   url: '/Upload/FilePost?code=' + item.Id + '&path=' + path
//               });
//               $scope.uploaders[item.Id].onSuccessItem = function (fileItem, response, status, headers) {
//                   fileItem.Id = response.Id;
                  
//               };
//           });
//           $scope.attachProjectDocumentation = result;
//       });

//   });

//   $scope.AttachRemove = function (item) {
//       $http({
//           url: '/Upload/FileDelete',
//           method: 'POST',
//           data: JSON.stringify({ Id: item.Id })
//       }).success(function (response) {
//           if (!response.IsError) {
//               item.remove();
//           } else {

//           }
//       });
//   }
//}

function datatablesCtrl($scope, $http, DTOptionsBuilder, DTColumnBuilder) {
    $scope.dtOptions = DTOptionsBuilder.newOptions()
        .withDOM('<"html5buttons"B>lTfgitp')
         .withOption('oLanguage', {
             "sUrl": "/Content/json/Russian.json"
         })
        .withButtons([
            { extend: 'copy' },
            { extend: 'csv' },
            { extend: 'excel', title: 'ExampleFile' },
            { extend: 'pdf', title: 'ExampleFile' },

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
    $scope.persons = [
        {
            id: '1',
            date: '12.04.2016',
            NumberDoc: 'П-5001',
            Exp: 'Дом жилой',
            File: 'договор.pdf',
            FileEx: 'заключение.pdf',
            Person: 'Иванов Иван Иванович'
        },
         {
             id: '2',
             date: '13.04.2016',
             NumberDoc: 'П-5002',
             Exp: 'Жилой комплекс',
             File: 'договор.pdf',
             FileEx: 'заключение.pdf',
             Person: 'Петрович Петр Сергеевич'
         },
         {
             id: '3',
             date: '14.04.2016',
             NumberDoc: 'П-5003',
             Exp: 'Стадион',
             File: 'договор.pdf',
             FileEx: 'заключение.pdf',
             Person: 'Коканов Бауржан'
         },
         {
             id: '4',
             date: '15.04.2016',
             NumberDoc: 'П-5004',
             Exp: 'Гараж',
             File: 'договор.pdf',
             FileEx: 'заключение.pdf',
             Person: 'Иванников Игорь'
         },
         {
             id: '5',
             date: '16.04.2016',
             NumberDoc: 'П-5005',
             Exp: 'Школа',
             File: 'договор.pdf',
             FileEx: 'заключение.pdf',
             Person: 'Мусин Ардак'
         },
         {
             id: '6',
             date: '17.04.2016',
             NumberDoc: 'П-5006',
             Exp: 'Мост',
             File: 'договор.pdf',
             FileEx: 'заключение.pdf',
             Person: 'Третьяков Игорь'
         },
         {
             id: '7',
             date: '18.04.2016',
             NumberDoc: 'П-5007',
             Exp: 'Концертный зал',
             File: 'договор.pdf',
             FileEx: 'заключение.pdf',
             Person: 'Усанин Сергей'
         }
    ];
    function dateformatHtml(data, type, full, meta) {
        var date = new Date(parseInt(data.slice(6, -2)));
        var month = date.getMonth() + 1;
        return date.getDate() + "." + (month.length > 1 ? month : "0" + month) + "." + date.getFullYear();
        //return '<span>' + dat+ '</span>'
    }
    $scope.dtColumnsIndex = [
        DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("ApplicationDate", "Дата").withOption('name', 'ApplicationDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("NameRu", "Наименование").withOption('name', 'NameRu'),
        DTColumnBuilder.newColumn("StateName", "Текущий этап").withOption('name', 'StateName'),
        DTColumnBuilder.newColumn("CountMessage", "Замечания").withOption('name', 'CountMessage'),
        DTColumnBuilder.newColumn("Id", "Действия").withOption('name', 'Id').renderWith(actionsHtml)
    ];

    $scope.dtColumnsDraft = [
        DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("ApplicationDate", "Дата").withOption('name', 'ApplicationDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("NameRu", "Наименование").withOption('name', 'NameRu'),
        DTColumnBuilder.newColumn("StateName", "Текущий этап").withOption('name', 'StateName'),
        DTColumnBuilder.newColumn("CountMessage", "Замечания").withOption('name', 'CountMessage'),
        DTColumnBuilder.newColumn("Id", "Действия").withOption('name', 'Id').notSortable().renderWith(actionsHtml)
    ];
    $scope.dtColumnsWork = [
        DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("ApplicationDate", "Дата").withOption('name', 'ApplicationDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("NameRu", "Наименование").withOption('name', 'NameRu'),
        DTColumnBuilder.newColumn("StateName", "Текущий этап").withOption('name', 'StateName'),
        DTColumnBuilder.newColumn("CountMessage", "Замечания").withOption('name', 'CountMessage'),
        DTColumnBuilder.newColumn("Id", "Действия").withOption('name', 'Id').notSortable().renderWith(actionsHtml)
    ];
    $scope.dtColumnsReview = [
        DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("ApplicationDate", "Дата").withOption('name', 'ApplicationDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("NameRu", "Наименование").withOption('name', 'NameRu'),
        DTColumnBuilder.newColumn("StateName", "Текущий этап").withOption('name', 'StateName'),
        DTColumnBuilder.newColumn("CountMessage", "Замечания").withOption('name', 'CountMessage'),
        DTColumnBuilder.newColumn("Id", "Действия").withOption('name', 'Id').notSortable().renderWith(actionsHtml)
    ];
    $scope.dtColumnsSuccess = [
        DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("ApplicationDate", "Дата").withOption('name', 'ApplicationDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("NameRu", "Наименование").withOption('name', 'NameRu'),
        DTColumnBuilder.newColumn("StateName", "Текущий этап").withOption('name', 'StateName'),
        DTColumnBuilder.newColumn("CountMessage", "Замечания").withOption('name', 'CountMessage'),
        DTColumnBuilder.newColumn("Id", "Действия").withOption('name', 'Id').notSortable().renderWith(actionsHtml)
    ];
    $scope.dtColumnsFailure = [
        DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("ApplicationDate", "Дата").withOption('name', 'ApplicationDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("NameRu", "Наименование").withOption('name', 'NameRu'),
        DTColumnBuilder.newColumn("StateName", "Текущий этап").withOption('name', 'StateName'),
        DTColumnBuilder.newColumn("CountMessage", "Замечания").withOption('name', 'CountMessage'),
        DTColumnBuilder.newColumn("Id", "Действия").withOption('name', 'Id').notSortable().renderWith(actionsHtml)
    ];


      function actionsHtml(data, type, full, meta) {
             return '<a  class="btn btn-warning" href="/Client/Project/Edit?id=' + data + '">' +
            '   <i class="fa fa-edit"></i>' +
            '</a>&nbsp;' +
            '<a  class="btn btn-success" href="/Client/Project/Detail?id=' + data + '" >' +
            '   <i class="fa fa-search"></i>' +
            '</a>';
    }



    $scope.dtOptionsIndex = DTOptionsBuilder.newOptions().withOption('ajax', {
            dataSrc: 'Data',
            url: "/Project/ReadIndex",
            type: "POST"
        })
        .withOption('processing', true) //for show progress bar
        .withOption('oLanguage', {
            "sUrl": "/Content/json/Russian.json"
        }) //for show progress bar
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

    $scope.dtOptionsDraft = DTOptionsBuilder.newOptions().withOption('ajax', {
            dataSrc: 'Data',
            url: "/Project/ReadDraft",
            type: "POST"
    })
         .withOption('oLanguage', {
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

    $scope.dtOptionsWork = DTOptionsBuilder.newOptions().withOption('ajax', {
            dataSrc: 'Data',
            url: "/Project/ReadWork",
            type: "POST"
    })
         .withOption('oLanguage', {
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
    $scope.dtOptionsReview = DTOptionsBuilder.newOptions().withOption('ajax', {
            dataSrc: 'Data',
            url: "/Project/ReadReview",
            type: "POST"
    })
         .withOption('oLanguage', {
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
    $scope.dtOptionsSuccess = DTOptionsBuilder.newOptions().withOption('ajax', {
            dataSrc: 'Data',
            url: "/Project/ReadSuccess",
            type: "POST"
    })
         .withOption('oLanguage', {
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
    $scope.dtOptionsFailure = DTOptionsBuilder.newOptions().withOption('ajax', {
            dataSrc: 'Data',
            url: "/Project/ReadFailure",
            type: "POST"
    })
         .withOption('oLanguage', {
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

function mailCtrl($scope, $http, $window) {

    $scope.mail = {
        Body: '',
        Title:''
    };
    $scope.search = '';
    $scope.skip = 0;
    $scope.changeSearch = function () {
        $http({
            method: 'GET',
            url: '/Mail/ReadIndex?skip=' + 0 + '&search=' +  $scope.search,
            data: 'JSON'
        }).success(function (result) {
            $scope.mails = result;
        });
    };

    $scope.openDetail = function() {
        $window.location.href = '/Mail/Detail';
    };

    $scope.leftMessage = function () {
        $scope.skip = $scope.skip - 10;
        if ($scope.skip < 0) {
            $scope.skip = 0;
        }
        $http({
            method: 'GET',
            url: '/Mail/ReadIndex?skip=' +  $scope.skip,
            data: 'JSON'
        }).success(function (result) {
            $scope.mails = result;
        });
    };
    $scope.rightMessage = function () {
        $scope.skip = $scope.skip + 10;
        $http({
            method: 'GET',
            url: '/Mail/ReadIndex?skip=' + $scope.skip,
            data: 'JSON'
        }).success(function (result) {
            $scope.mails = result;
        });
    };

    $http({
        method: 'GET',
        url: '/Mail/ReadIndex',
        data: 'JSON'
    }).success(function (result) {
        $scope.mails = result;
    });
    $scope.sendPost = function () {
        $http({
            url: 'SendNew',
            method: 'POST',
            data: JSON.stringify($scope.mail)
        }).success(function (response) {
            if (!response.IsError) {
                $window.location.href = '/Home/Success';
            } else {
                alert(response.Message);
            }
        });
    }

    $scope.saveDraft = function () {

        $http({
            url: '/Mail/SaveDraft',
            method: 'POST',
            data: JSON.stringify($scope.mail)
        }).success(function (response) {
            if (!response.IsError) {

            } else {
                alert(response.Message);
            }
        });
    }
}


function detailsProjectCtrl($scope, $http, DTOptionsBuilder, DTColumnBuilder) {

    $scope.mail = {
        Body: '',
        Title: ''
    };
    $scope.search = '';
    $scope.skip = 0;

    $scope.changeSearch = function () {
        $http({
            method: 'GET',
            url: '/Mail/ReadIndex?skip=' + 0 + '&search=' + $scope.search,
            data: 'JSON'
        }).success(function (result) {
            $scope.mails = result;
        });
    };
    $scope.leftMessage = function () {
        $scope.skip = $scope.skip - 10;
        if ($scope.skip < 0) {
            $scope.skip = 0;
        }
        $http({
            method: 'GET',
            url: '/Mail/ReadIndex?skip=' + $scope.skip,
            data: 'JSON'
        }).success(function (result) {
            $scope.mails = result;
        });
    };
    $scope.rightMessage = function () {
        $scope.skip = $scope.skip + 10;
        $http({
            method: 'GET',
            url: '/Mail/ReadIndex?skip=' + $scope.skip,
            data: 'JSON'
        }).success(function (result) {
            $scope.mails = result;
        });
    };

    $http({
        method: 'GET',
        url: '/Mail/ReadIndex',
        data: 'JSON'
    }).success(function (result) {
        $scope.mails = result;
    });

    $scope.dtOptions = DTOptionsBuilder.newOptions()
        .withDOM('<"html5buttons"B>lTfgitp')
         .withOption('oLanguage', {
             "sUrl": "/Content/json/Russian.json"
         })
        .withButtons([
            { extend: 'copy' },
            { extend: 'csv' },
            { extend: 'excel', title: 'ExampleFile' },
            { extend: 'pdf', title: 'ExampleFile' },

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
    $scope.persons = [
        {
            id: '1',
            date: '12.04.2016',
            NumberDoc: 'П-5001',
            Exp: 'Дом жилой',
            File: 'договор.pdf',
            FileEx: 'заключение.pdf',
            Person: 'Иванов Иван Иванович'
        },
         {
             id: '2',
             date: '13.04.2016',
             NumberDoc: 'П-5002',
             Exp: 'Жилой комплекс',
             File: 'договор.pdf',
             FileEx: 'заключение.pdf',
             Person: 'Петрович Петр Сергеевич'
         },
         {
             id: '3',
             date: '14.04.2016',
             NumberDoc: 'П-5003',
             Exp: 'Стадион',
             File: 'договор.pdf',
             FileEx: 'заключение.pdf',
             Person: 'Коканов Бауржан'
         },
         {
             id: '4',
             date: '15.04.2016',
             NumberDoc: 'П-5004',
             Exp: 'Гараж',
             File: 'договор.pdf',
             FileEx: 'заключение.pdf',
             Person: 'Иванников Игорь'
         },
         {
             id: '5',
             date: '16.04.2016',
             NumberDoc: 'П-5005',
             Exp: 'Школа',
             File: 'договор.pdf',
             FileEx: 'заключение.pdf',
             Person: 'Мусин Ардак'
         },
         {
             id: '6',
             date: '17.04.2016',
             NumberDoc: 'П-5006',
             Exp: 'Мост',
             File: 'договор.pdf',
             FileEx: 'заключение.pdf',
             Person: 'Третьяков Игорь'
         },
         {
             id: '7',
             date: '18.04.2016',
             NumberDoc: 'П-5007',
             Exp: 'Концертный зал',
             File: 'договор.pdf',
             FileEx: 'заключение.pdf',
             Person: 'Усанин Сергей'
         }
    ];

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
    }).success(function(result) {
        $scope.attachProjectDocumentation = result;
    });
    $scope.dtColumnsHistory = [

       DTColumnBuilder.newColumn("Record", "Запись").withOption('name', 'Record'),

       DTColumnBuilder.newColumn("Time", "Время").withOption('name', 'Time'),
       DTColumnBuilder.newColumn("User", "Пользователь").withOption('name', 'User'),
       DTColumnBuilder.newColumn("OperationValue", "Операция").withOption('name', 'OperationValue'),
              DTColumnBuilder.newColumn("OldValue", "Старое значение").withOption('name', 'OldValue'),
       DTColumnBuilder.newColumn("NewValue", "Новое значение").withOption('name', 'NewValue')
    ];

  
   



    $scope.dtOptionsHistory = DTOptionsBuilder.newOptions().withOption('ajax', {
        dataSrc: 'Data',
        url: "/History/Index" ,
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
    .controller('MainCtrl', MainCtrl)
    .controller('createExpetiseObject', createExpetiseObject)
    .controller('datatablesCtrl', datatablesCtrl)
    .controller('detailsProjectCtrl', detailsProjectCtrl)
    .controller('MailCtrl', mailCtrl);

