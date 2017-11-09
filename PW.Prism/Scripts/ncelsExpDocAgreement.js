function panelExpDocAgreementSelect(e) {
    var selectType = $(e.item).find("> .k-link").attr('ItemType');
    if (selectType != null) {
        var filterName = $(e.item).find("> .k-link").attr('FilterName');
        var selectValue = $(e.item).find("> .k-link").attr('ItemId');
        var gridId = $(e.item).find("> .k-link").attr('ModelId');
        var grid = $("#gridExpDocAgreement" + gridId).data("kendoGrid");
        var filter = new Array();
        grid.customFilterName = filterName;
        if (selectType === "StatusCode") {
            var filters = selectValue.split(";");
            for (var i = 0; i < filters.length; i++) {
                filter.push({ field: "StatusCode", operator: "eq", value: filters[i] });
            }
        }
        if (selectValue === '') {
            grid.dataSource.filter([]);
        } else {
            grid.dataSource.filter({
                logic: "or",
                filters: filter
            });
        }
    }
}
function openExpAgreementDoc(docId, docNumber, docType) {
    debugger;
    switch (docType) {
       
        case '5':  //итоговый документ
            kendo.ui.progress($("#mainWindowLoader"), true);
            $.ajax({
                type: 'GET',
                url: '/ExpDocumentAgreement/GetDeclarationInfoByFinalDoc?finalDocId=' + docId,
                success: function (result) {
                    debugger;
                    getDeclarationDetails(result.Id, result.Number, result.Controller);
                },
                complete: function () {
                    kendo.ui.progress($("#mainWindowLoader"), false);
                }
            });
            break;
        case '3': //письмо
            {
                window.location.href = '/DrugPrimary/ExportFile/?id=' + docId;
            }
     /*   case '6': //файл с перевода
            {
                window.location.href = '/Upload/FileDownload?id=' + docId;
            }
        case '7': //Макет
            {
                window.location.href = '/Upload/FileDownload?id=' + docId;
            }
            */
    }
}
function InitExpDocAgreementGrid(uiId) {
    hideButtons();
    $("#reload" + uiId).click(function (e) {
        var grid = $("#gridExpDocAgreement" + uiId).data("kendoGrid");
        grid.dataSource.read();
    });
    var grid = $("#gridExpDocAgreement" + uiId).data("kendoGrid");
    grid.bind("dataBound", function () {
        hideButtons();
    });
    grid.bind("change", function () {
        debugger;
        var grid = $("#gridExpDocAgreement" + uiId).data("kendoGrid");
        var selected = this.select();
        if (selected.length > 0 && grid.customFilterName == 'newTasks') {
            var selectedItem = grid.dataItem(grid.select());
            if (selectedItem.TaskTypeCode === '1') {
                $("#agree" + uiId).removeClass('k-state-disabled');
                $("#sign" + uiId).addClass('k-state-disabled');
            }
            if (selectedItem.TaskTypeCode === '2') {
                $("#sign" + uiId).removeClass('k-state-disabled');
                $("#agree" + uiId).addClass('k-state-disabled');
            }
            $("#reject" + uiId).removeClass('k-state-disabled');
        }
    });
    function hideButtons() {
        $("#agree" + uiId).addClass('k-state-disabled');
        $("#sign" + uiId).addClass('k-state-disabled');
        $("#reject" + uiId).addClass('k-state-disabled');
    }

    $("#agree" + uiId).click(function (e) {
        if (!canClickBtn(e.target)) return;
        debugger;
        kendo.ui.progress($("#mainWindowLoader"), true);
        var grid = $("#gridExpDocAgreement" + uiId).data("kendoGrid");
        var selectedItem = grid.dataItem(grid.select());
        var data = {
            documentId: selectedItem.DocumentId,
            activityTypeCode: selectedItem.ActivityTypeCode,
            comment: 'Согласованно'
        };
        var json = JSON.stringify(data);
        $.ajax({
            type: 'POST',
            url: '/Agreement/Approve',
            contentType: 'application/json; charset=utf-8',
            data: json,
            success: function (result) {
                grid.dataSource.read();
            },
            complete: function () {
                kendo.ui.progress($("#mainWindowLoader"), false);
            }
        });
    });
    $("#sign" + uiId).click(function (e) {
        if (!canClickBtn(e.target)) return;
        debugger;        
        var grid = $("#gridExpDocAgreement" + uiId).data("kendoGrid");
        var selectedItem = grid.dataItem(grid.select());
        kendo.ui.progress($("#mainWindowLoader"), true);
        $.ajax({
            type: 'GET',
            url: '/Agreement/GetDataForSign?taskId=' + selectedItem.Id,
            success: function (result) {
                debugger;
                startSign(result, selectedItem.DocumentId, approveDocument);
            },
            complete: function (result) {
                kendo.ui.progress($("#mainWindowLoader"), false);
            }
        });

        function approveDocument(signedData, documentId) {
            var data = {
                documentId: documentId,
                activityTypeCode: selectedItem.ActivityTypeCode,
                comment: 'Утвержденно',
                digSign: signedData
            };
            var json = JSON.stringify(data);
            $.ajax({
                type: 'POST',
                url: '/Agreement/Approve',
                contentType: 'application/json; charset=utf-8',
                data: json,
                success: function (result) {
                    grid.dataSource.read();
                },
                complete: function () {
                    kendo.ui.progress($("#mainWindowLoader"), false);
                }
            });
        }
        
    });
    $("#reject" + uiId).click(function (e) {
        if (!canClickBtn(e.target)) return;
        debugger;
        kendo.ui.progress($("#mainWindowLoader"), true);
        var grid = $("#gridExpDocAgreement" + uiId).data("kendoGrid");
        var selectedItem = grid.dataItem(grid.select());
        var data = {
            documentId: selectedItem.DocumentId,
            activityTypeCode: selectedItem.ActivityTypeCode,
            comment: 'Отклонено'
        };
        var json = JSON.stringify(data);
        $.ajax({
            type: 'POST',
            url: '/Agreement/Reject',
            contentType: 'application/json; charset=utf-8',
            data: json,
            success: function (result) {
                grid.dataSource.read();
            },
            complete: function () {
                kendo.ui.progress($("#mainWindowLoader"), false);
            }
        });
    });
    function canClickBtn(btn) {
        if ($(btn).hasClass('k-state-disabled') ||
            $(btn).hasClass('disabled') ||
            $(btn).is('[disabled]'))
            return false;
        return true;
    }
}