function gridObject(DTOptionsBuilder) {
    return {
        restrict: 'E',
        template: '<table id="entry-grid" datatable="" dt-options="dtOptions" dt-columns="dtColumns"  class="table table-striped table-bordered table-hover dataTable"></table>',
        compile: function compile(tElement, tAttrs, transclude) {
            return {
                pre: function preLink(scope, iElement, iAttrs, controller) {
                    function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                        $('td', nRow).unbind('click');
                        $('td', nRow).bind('click', function () {

                            var table = $(this.parentNode.parentNode.parentNode).DataTable();
                            table.$('tr.pw-row-selected').removeClass('pw-row-selected');
                            $(this.parentNode).addClass('pw-row-selected');

                            scope.$apply(function () {
                                if (iAttrs.selectfun != undefined) {
                                    scope[iAttrs.selectfun](aData);
                                }
                                scope.row = aData;
                            });
                        });
                        return nRow;
                    }
                    var url = iAttrs.url;
                    var id = iAttrs.objectid;
                    scope.removeRow = function () {
                        $('#entry-grid').DataTable().rows('.selected')
                            .remove()
                            .draw();
                    }

                    var columnOrder = 0;
                    if (iAttrs.columnOrder != "undefined") {
                        columnOrder = iAttrs.columnOrder;
                    }
                    scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax', {
                        dataSrc: 'Data',
                        url: url,
                        type: "POST", data: function (d) {
                            if (id != undefined) {
                                d.id = id;
                            }
                        }
                    })
                        .withOption('processing', true)
                        .withOption('serverSide', true)
                        .withPaginationType('full_numbers')
                        .withOption('rowCallback', rowCallback)
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

function gridLightObject(DTOptionsBuilder) {
    return {
        restrict: 'E',
        template: function(element, attrs) {
            var dtOptions = attrs.dtOptions;
            var dtColumns = attrs.dtColumns;
            var dtId = attrs.dtId;
           
            var htmlText = '<table id="' + dtId + '" datatable="" dt-options="' + dtOptions + '" dt-columns="' + dtColumns + '"  class="table table-striped table-bordered table-hover dataTable"></table>';
     
            return htmlText;
        },
        // element.replaceWith(htmlText);  template: '<table id="entry-grid" datatable="" dt-options="{{dtOptions}}" dt-columns="{{dtColumns}}"  class="table table-striped table-bordered table-hover dataTable"></table>',
        compile: function compile(tElement, tAttrs, transclude) {
         
            return {
                pre: function preLink(scope, iElement, iAttrs, controller) {
                    var dtOptions = iAttrs.dtOptions;
                    var url = iAttrs.url;
                    var id = iAttrs.objectId;
                    var dtId = iAttrs.dtId;
                    var reloadRow = iAttrs.reloadRow;
                    var selectfun = iAttrs.dtSelectfun;
                    scope[reloadRow] = function () {
                        $('#' + dtId).DataTable().ajax.reload();
                    }
                    function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                        $('td', nRow).unbind('click');
                        $('td', nRow).bind('click', function () {

                            var table = $(this.parentNode.parentNode.parentNode).DataTable();
                            table.$('tr.pw-row-selected').removeClass('pw-row-selected');
                            $(this.parentNode).addClass('pw-row-selected');

                            scope.$apply(function () {
                                console.log(selectfun);
                                if (selectfun != undefined) {
                                    scope[selectfun](aData);
                                } 
                            });
                        });
                        return nRow;
                    }
               

                    scope[dtOptions] = DTOptionsBuilder.newOptions().withOption('ajax', {
                        dataSrc: 'Data',
                        url: url,
                        type: "POST", data: function (d) {
                            if (id != undefined) {
                                d.id = id;
                            }
                        }
                    })
                        .withOption('serverSide', true)
                        .withOption('bFilter', false)
                        .withOption('paging', false)
                        .withOption('lengthChange', false)
                        .withOption('rowCallback', rowCallback)
                        .withOption('bInfo', false);
                },
                post: function postLink(scope, iElement, iAttrs, controller) {

                }
            }

        }
    }
};


function gridIntegrationObject(DTOptionsBuilder) {
    return {
        restrict: 'E',
        template: function (element, attrs) {
            var dtOptions = attrs.dtOptions;
            var dtColumns = attrs.dtColumns;
            var dtId = attrs.dtId;

            var htmlText = '<table id="' + dtId + '" datatable="" dt-options="' + dtOptions + '" dt-columns="' + dtColumns + '"  class="table table-striped table-bordered table-hover dataTable"></table>';

            return htmlText;
        },
        // element.replaceWith(htmlText);  template: '<table id="entry-grid" datatable="" dt-options="{{dtOptions}}" dt-columns="{{dtColumns}}"  class="table table-striped table-bordered table-hover dataTable"></table>',
        compile: function compile(tElement, tAttrs, transclude) {

            return {
                pre: function preLink(scope, iElement, iAttrs, controller) {
                    var dtOptions = iAttrs.dtOptions;
                    var url = iAttrs.url;
                    var id = iAttrs.objectId;
                    var dtId = iAttrs.dtId;
                    var reloadRow = iAttrs.reloadRow;
                    var selectfun = iAttrs.dtSelectfun;
                    scope[reloadRow] = function () {
                        $('#' + dtId).DataTable().ajax.reload();
                    }
                    function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                        $('td', nRow).unbind('click');
                        $('td', nRow).bind('click', function () {

                            var table = $(this.parentNode.parentNode.parentNode).DataTable();
                            table.$('tr.pw-row-selected').removeClass('pw-row-selected');
                            $(this.parentNode).addClass('pw-row-selected');

                            scope.$apply(function () {
                                console.log(selectfun);
                                if (selectfun != undefined) {
                                    scope[selectfun](aData);
                                }
                            });
                        });
                        return nRow;
                    }


                    scope[dtOptions] = DTOptionsBuilder.newOptions().withOption('ajax', {
                        dataSrc: 'Data',
                        url: url,
                        type: "POST", data: function (d) {
                            if (id != undefined) {
                                d.id = id;
                            }
                        }
                    })
                        .withOption('processing', true)
                        .withOption('serverSide', true)
                        .withPaginationType('full_numbers')
                        .withOption('rowCallback', rowCallback)
                        .withDisplayLength(5)
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

function gridCorObject(DTOptionsBuilder) {
    return {
        restrict: 'E',
        template: '<table id="cor-grid" datatable="" dt-options="dtOptions" dt-columns="dtColumnsCor"  class="table table-striped table-bordered table-hover dataTable"></table>',
        compile: function compile(tElement, tAttrs, transclude) {
            return {
                pre: function preLink(scope, iElement, iAttrs, controller) {
                    function rowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                        $('td', nRow).unbind('click');
                        $('td', nRow).bind('click', function () {

                            var table = $(this.parentNode.parentNode.parentNode).DataTable();
                            table.$('tr.pw-row-selected').removeClass('pw-row-selected');
                            $(this.parentNode).addClass('pw-row-selected');

                            scope.$apply(function () {
                                if (iAttrs.selectfun != undefined) {
                                    scope[iAttrs.selectfun](aData);
                                }
                                scope.row = aData;
                            });
                        });
                        return nRow;
                    }
                    var url = iAttrs.url;
                    var id = iAttrs.objectid;
                    scope.removeRow = function () {
                        $('#cor-grid').DataTable().rows('.selected')
                            .remove()
                            .draw();
                    }

                    var columnOrder = 0;
                    if (iAttrs.columnOrder != "undefined") {
                        columnOrder = iAttrs.columnOrder;
                    }
                    scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax', {
                        dataSrc: 'Data',
                        url: url,
                        type: "POST", data: function (d) {
                            if (id != undefined) {
                                d.id = id;
                            }
                        }
                    })
                        .withOption('processing', true)
                        .withOption('serverSide', true)
                        .withPaginationType('full_numbers')
                        .withOption('rowCallback', rowCallback)
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
    .directive('gridIntegrationObject', gridIntegrationObject)
    .directive('gridObject', gridObject)
    .directive('gridCorObject', gridCorObject)
    .directive('gridLightObject', gridLightObject);
