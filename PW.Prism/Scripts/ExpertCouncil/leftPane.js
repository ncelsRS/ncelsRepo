$(document).ready(function () {
});

function expertCouncilAddNew(e) {
    $('#expertCouncilAddWindow').kendoWindow({
        width: "600px",
        title: "Добавление заседания экспертного совета",
        visible: false,
        modal: true,
        pinned: false,
        draggable: false,
        actions: ['Close']
    }).data("kendoWindow").center().open();
}

function expertCouncilAddWindowClose() {
    $('#expertCouncilAddName').data("kendoDropDownList").value("");
    $("#expertCouncilAddDate").data('kendoDatePicker').value("");
    $('#expertCouncilAddWindow').data('kendoWindow').close();
}

function expertCouncilSave(e) {
    var council = {
        Name: $("#expertCouncilAddName").data("kendoDropDownList").value(),
        Date: $("#expertCouncilAddDate").data('kendoDatePicker').value()
    };
    if (!council.Date)
        return alert('Внесите дату заседания');
    $.ajax({
        url: "/OBKExpertCouncil/SaveExpertCouncil",
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(council),
        success: function (res) {
            if (res.isSuccess) {
                expertCouncilAddWindowClose(e);
                return updateLeftPane();
            }
            alert('Произошла ошибка');
            expertCouncilAddWindowClose();
        }
    });
}

var ec;

var selectEvent;
function EC_panelEventSelect(e) {
    if (!e) e = selectEvent;
    selectEvent = e;
    var selectType = $(e.item).find("> .k-link").attr('dataType');
    if (selectType) {
        var el = $(e.item).find("> .k-link");
        ec = {};
        ec.Id = el.attr('dataValue');
        ec.IsCompleted = el.attr("dataIsCompleted");
        ec.Number = el.attr("dataNumber");
        ec.ActualDate = el.attr("dataActualDate");
        var gridId = el.attr('ModelId');
        var grid = $("#EC_gridAssessmentDeclaration").data("kendoGrid");
        var filter = new Array();

        if (selectType === "ExpertCouncilId") {
            filter.push({ field: "ExpertCouncilId", operator: "eq", value: ec.Id });
        }
        if (ec.Id === '') {
            grid.dataSource.filter([]);
        } else {
            grid.dataSource.filter({
                logic: "and",
                filters: filter
            });
        }
        updateHtmlVisible();
    }
}