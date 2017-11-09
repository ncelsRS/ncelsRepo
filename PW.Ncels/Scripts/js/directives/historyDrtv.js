
function historyObject(DTOptionsBuilder, DTColumnBuilder) {
    return {
        restrict: 'E',
        template: '<table id="entry-grid" datatable="" dt-options="dtOptionsHistory" dt-columns="dtColumnsHistory"  class="table table-striped table-bordered table-hover dataTable ng-isolate-scope no-footer"></table>',
        compile: function compile(tElement, tAttrs, transclude) {
            return {
                pre: function preLink(scope, iElement, iAttrs, controller) {
                    var url = iAttrs.url;
                    var tablename = iAttrs.tablename;
                    var objectId = iAttrs.objectid;

                    scope.dtColumnsHistory = [
                        DTColumnBuilder.newColumn("Record", "Запись").withOption('name', 'Record'),
                        DTColumnBuilder.newColumn("Time", "Время").withOption('name', 'Time'),
                        DTColumnBuilder.newColumn("User", "Пользователь").withOption('name', 'User'),
                        DTColumnBuilder.newColumn("OperationValue", "Операция").withOption('name', 'OperationValue'),
                        DTColumnBuilder.newColumn("OldValue", "Старое значение").withOption('name', 'OldValue'),
                        DTColumnBuilder.newColumn("NewValue", "Новое значение").withOption('name', 'NewValue')
                    ];
                    var columnOrder;

                    if (iAttrs.columnOrder != "undefined") {
                        columnOrder = iAttrs.columnOrder;
                    } else {
                        columnOrder = 0;
                    }
                    scope.dtOptionsHistory = DTOptionsBuilder.newOptions().withOption('ajax', {
                        dataSrc: 'Data',
                        url: url,
                        type: "POST",
                        data: function (d) {
                            d.id = objectId;
                        }
                    })
                        .withOption('processing', true) //for show progress bar
                        //.withOption('oLanguage', {
                        //    "sUrl": "/Content/json/Russian.json",
                            
                        //}) //for show progress bar
                        .withOption('serverSide', true) // for server side processing
                        .withPaginationType('full_numbers') // for get full pagination options // first / last / prev / next and page numbers
                        .withDisplayLength(10) // Page size
                      .withOption('aaSorting', [[columnOrder, 'desc']])
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
                },
                post: function postLink(scope, iElement, iAttrs, controller) {
                    
                }
            }
        
        }
    }
};




angular
    .module('app')
    .directive('historyObject', historyObject);