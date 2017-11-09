function InitFilterOBKDecalaration(uiId) {
    debugger;
    var grid = $('#gridAssessmentDecalaration' + uiId).data("kendoGrid");
    function declarationRowSelect(e) {
        var selectedRows = this.select();
        if (selectedRows.length > 0) {
            var selectedItem = grid.dataItem(grid.select());
            if (selectedItem.StageStatusCode === 'inQueue' || selectedItem.StageStatusCode === 'inWork')
                $('#toWork' + uiId).removeClass('k-state-disabled');
        }
        $("#gridAssessmentDecalaration" + uiId + ' tr').find('.checkbox[type=checkbox]').prop('checked', false);
        $("#gridAssessmentDecalaration" + uiId + ' tr.k-state-selected').find('.checkbox[type=checkbox]').prop('checked', true);
    }
    grid.bind("change", declarationRowSelect);
    grid.bind("dataBound", function () {
        $("#gridAssessmentDecalaration" + uiId + " .checkbox").bind("change", function (e) {
            var data = grid.dataItem($(e.target).closest("tr"));
            if (e.target.checked && (data.StageStatusCode === 'inQueue' || data.StageStatusCode === 'inWork')) {
                $('#toWork' + uiId).removeClass('k-state-disabled');
            }
            $(e.target).closest("tr").toggleClass("k-state-selected");
        });
        if (!$('#toWork' + uiId).hasClass('k-state-disabled'))
            $('#toWork' + uiId).addClass('k-state-disabled');
        var data = grid.dataSource.data();
        $.each(data,
            function (i, row) {
                if (row.PaymentOverdue) {
                    $('tr[data-uid="' + row.uid + '"] ').css("background-color", "#ff0029"); //red
                } else if (row.CountDosageIsControl > 0) {
                    $('tr[data-uid="' + row.uid + '"] ').css("background-color", "#99cc99"); //green
                }
            });
    });

    $("#reload" + uiId).click(function (e) {
        grid.dataSource.read();
    });

    $('#gridAssessmentDecalaration' + uiId).kendoTooltip({
        filter: "td.need-cell-tooltip",
        position: "left",
        show: function (e) {
            if (this.content.text()) {
                this.content.parent().css("visibility", "visible");
            }
        },
        hide: function (e) {
            this.content.parent().css("visibility", "hidden");
        },
        content: function (e) {
            return $(e.target).text();
        }
    }).data("kendoTooltip");

    var searchButton = $("#find" + uiId).data("kendoButton");
    searchButton.bind("click",
        function (e) {
            debugger;
            var window = $("#TaskCommandWindow");
            window.kendoWindow({
                width: "1200",
                height: "770",
                modal: true,
                actions: ["Close"]
            });
            window.data("kendoWindow").gridId = 'gridAssessmentDecalaration' + uiId;
            if (!searchButton.declarationFilter) {
                searchButton.declarationFilter = {};
            }
            window.data("kendoWindow").windowViewModel = searchButton.declarationFilter;
            window.data("kendoWindow").dialogCallback = function (filter) {
                searchButton.declarationFilter = filter;
            };
            window.data("kendoWindow").title('Поиск');
            window.data("kendoWindow").setOptions({
                width: 1200,
                height: 770
            });

            window.data("kendoWindow").refresh('/DrugDeclaration/DeclarationSearch');
            window.data("kendoWindow").center();
            window.data("kendoWindow").open();
        });

    var clearButton = $('#clearSearch' + uiId).data("kendoButton");
    clearButton.bind("click",
        function (e) {
            var searchButton = $("#find" + uiId).data("kendoButton");
            searchButton.declarationFilter = {};
            grid.dataSource.transport.options.read.data = null;
            grid.dataSource.read();
        });
    $('#toWork' + uiId).click(function () {
        var idsToSend = [];
        var ds = grid.dataSource.view();

        for (var i = 0; i < ds.length; i++) {
            var row = grid.table.find("tr[data-uid='" + ds[i].uid + "']");
            var checkbox = $(row).find(".checkbox");
            if (checkbox.is(":checked") && (ds[i].StageStatusCode === 'inQueue' || ds[i].StageStatusCode === 'inWork')) {
                idsToSend.push(ds[i].StageId);
            }
        }
        if (idsToSend.length > 0) {
            var window = $("#TaskCommandWindow");
            window.kendoWindow({
                width: "550px",
                height: "auto",
                modal: true, resizable: false,
                close: onCloseCommandWindow,
                actions: ["Close"]
            });
            window.data("kendoWindow").dialogCallback = function () {
                grid.dataSource.read();
            };
            window.data("kendoWindow").gridSelectedIds = idsToSend;
            window.data("kendoWindow").title('Отправить в работу');
            window.data("kendoWindow").setOptions({
                width: 550,
                height: 'auto'
            });
            window.data("kendoWindow").refresh('/SafetyAssessment/SetExecuter');
            window.data("kendoWindow").center();
            window.data("kendoWindow").open();
        } else {
            grid.dataSource.read();
        }
    });
}




function panelAssessmentDecalarationSelect(e) {
    var selectType = $(e.item).find("> .k-link").attr('ItemType');
    if (selectType !== null) {
        var selectValue = $(e.item).find("> .k-link").attr('ItemId');
        var gridId = $(e.item).find("> .k-link").attr('ModelId');
        var grid = $("#gridAssessmentDecalaration" + gridId).data("kendoGrid");
        var filter = new Array();

        if (selectType === "StageStatusCode") {

            filter.push({ field: "StageStatusCode", operator: "eq", value: selectValue });
        }
        if (selectValue === '') {
            grid.dataSource.filter([]);
        } else {
            grid.dataSource.filter({
                logic: "and",
                filters: filter
            });
        }
    }
}


function getDeclarationDetails(parameters, number, controllerName) {
    if (docArray.indexOf(parameters.toLowerCase()) !== -1)
        docArray.splice(docArray.indexOf(parameters.toLowerCase()), 1);
    var element = document.getElementById(parameters);
    if (element === null) {
        var tabStrip = $("#tabstrip").data("kendoTabStrip");
        var content = '<div id="' + parameters + '"> </div>';
        var idContent = '#' + parameters;
        tabStrip.append({
            text: 'Заявление: № ' + number,
            content: content

        });

        tabStrip.select(tabStrip.items().length - 1);

        var gridElement = $(idContent);

        gridElement.height($(window).height() - 100);

        $.ajax({
            url: "/" + controllerName + "/Edit?id=" + parameters,
            //type: "POST",
            success: function (result) {
                // refreshes partial view
                $(idContent).html(result);
                $('.mark-check-found').each(function () {
                    var idcontrol = $(this).attr('idCheck');
                    $("#" + idcontrol).prop("checked", true);
                });
            }
        });
    } else {

        var itesm = $('#' + parameters)[0].parentElement.getAttribute('id').split('-')[1];
        if (itesm) {
            $("#tabstrip").data("kendoTabStrip").select(itesm - 1);
        }
    }
    //alert(parameters);
}











function showInformIcon(isShow) {
    debugger;
    if (isShow) {
        $('.input-group-addon').show();
    } else {
        $('.input-group-addon').hide();
    }
}

function InitializeOBKDataDeclaraion(name, repeatId, status, stage, stageId) {
    $("#stop" + stageId).on("click",
        function () {
            debugger;
            stopSafety();
        });
    if (stage !== "1") { 
        return;
    }
    var validator = $("#inDocForm" + name).kendoValidator().data("kendoValidator");

    if (repeatId == '')
        repeatId = 'null';
    $("#reject" + name).on("click",
        function () {
            rejectData();
        });
    $("#review" + name).on("click",
        function () {
            reviewData();
        });
    $("#outputResult" + name).on("click",
        function() {
            outputResultData();
        });
    var viewModel = kendo.observable({
        document: {},
        change: function () {
            validator.validate();
            this.set("hasChanges", true);
        },
        initButton: function () {
            var state = status;
            if (state === "2") {
                this.set("hasReject", true);
                this.set("hasReview", true);
            } else {
                $("#deSignNote").prop("readonly", true);
                this.set("hasReject", false);
                this.set("hasReview", false);
            }
        },
        hasChanges: false,
        hasRegister: false,
        hasReview: false,
        review: function (e) {
            e.preventDefault();
            reviewData();
        },
        reject: function (e) {
            e.preventDefault();
            rejectData();
        }
    });



    function loadDocument(name) {
        kendo.ui.progress($('#loader' + name), true);
        $.ajax({
            type: 'get',
            url: '/DrugDeclaration/DocumentRead?Id=' + name,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                result = JSON.parse(result);
                viewModel.set("document", result);
                viewModel.initButton();
                InitializePropertyIncoming(name, viewModel);
                kendo.bind($("#inDocForm" + name), viewModel);

                kendo.ui.progress($('#loader' + name), false);
            },
            complete: function () {
            }
        });
    }

    function outputResultData() {
        debugger;
        var window = $("#TaskCommandWindow");
        window.kendoWindow({
            width: "550px",
            height: "auto",
            modal: true, resizable: false,
            close: onCloseCommandWindow,
            actions: ["Close"]
        });

        window.data("kendoWindow").title('Выдать результат');
        window.data("kendoWindow").setOptions({
            width: 550,
            height: 'auto'
        });
        window.data("kendoWindow").refresh('/SafetyAssessment/OutputResultView?id=' + name);

        window.data("kendoWindow").center();
        window.data("kendoWindow").open();
    }

    function reviewData() {
        var window = $("#TaskCommandWindow");
        window.kendoWindow({
            width: "550px",
            height: "auto",
            modal: true, resizable: false,
            close: onCloseCommandWindow,
            actions: ["Close"]
        });

        window.data("kendoWindow").title('Отправить на экспертизу документов');
        window.data("kendoWindow").setOptions({
            width: 550,
            height: 'auto'
        });
        window.data("kendoWindow").refresh('/SafetyAssessment/DocumentReview?id=' + stageId);

        window.data("kendoWindow").center();
        window.data("kendoWindow").open();
    };


    function rejectData() {
        debugger;
        var window = $("#TaskCommandWindow");
        var design = $("#deSignNote").val();
        window.kendoWindow({
            width: "550px",
            height: "auto",
            modal: true, resizable: false,
            close: onCloseCommandWindow,
            actions: ["Close"]
        });

        window.data("kendoWindow").title('Вернуть заявтиелю');
        window.data("kendoWindow").setOptions({
            width: 550,
            height: 'auto'
        });
        window.data("kendoWindow").refresh('/SafetyAssessment/Reject?id=' + stageId + '&note=' + design);

        window.data("kendoWindow").center();
        window.data("kendoWindow").open();
    };
    function stopSafety() {
        var window = $("#TaskCommandWindow");
        window.kendoWindow({
            width: "550px",
            height: "auto",
            modal: true, resizable: false,
            close: onCloseCommandWindow,
            actions: ["Close"]
        });

        window.data("kendoWindow").title('Отказ');
        window.data("kendoWindow").setOptions({
            width: 550,
            height: 'auto'
        });
        window.data("kendoWindow").dialogCallback = function () {
            $("#stop" + stageId).attr('disabled', 'disabled');
        }
        window.data("kendoWindow").refresh('/SafetyAssessment/ConfirmeRefuseSafety?stageId=' + stageId);

        window.data("kendoWindow").center();
        window.data("kendoWindow").open();
    };
    loadDocument(name);
};