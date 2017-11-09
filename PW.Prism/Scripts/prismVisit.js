var VisitsFindButton;

function submitVisit(visitGridId) {
    var exampleModal = "#" + visitGridId + "calendarModal";
    var dateBegin = $("#"+visitGridId + "_DateBegin").val();
    var dateEnd = $("#" + visitGridId + "_DateEnd").val();
    var issaturday = false;
    if ($("#" + visitGridId + "_Saturday").prop('checked')) {
        issaturday = true;
    } else {
        issaturday = false;
    }
    var issunday = false;
    if ($("#" + visitGridId + "_Sunday").prop('checked')) {
        issunday = true;
    } else {
        issunday = false;
    }
    var from = $("#" +visitGridId + "_from").val();
    var from1 = $("#" +visitGridId + "_from1").val();
    var to = $("#" +visitGridId + "_to").val();
    var to1 = $("#" + visitGridId + "_to1").val();

    if (!$("#" + visitGridId + "_checkbox").prop('checked')) {
        from1 = null;
        to1 = null;
    } 

    $.ajax({
        type: 'POST',
        url: "/Visit/CreatePeriodWorkingDayHours",
        data: { dateBegin: dateBegin, dateEnd: dateEnd, issaturday: issaturday, issunday: issunday, from: from, to: to, from1: from1, to1: to1 },
        success: function (result) {
            if (result.success == false) {
                ShowAlert('Внимание!', result.message, window.AlertType.Warning, 3000);
            }
            else {
                $(exampleModal).modal('hide');
                getWorkingTime();
            }
        },
        error: function () {
            ShowAlert('Ошибка!', window.commonErrorMessage, window.AlertType.Error, 3000);
        }
    });

}

function showDialogVisitDatePeriod(modelId) {
    var exampleModal = "#" + modelId + "calendarModal";
    $(exampleModal).modal();
}

function InitFilterVisitAllGrid(name) {
    $("#reload" + name).click(function (e) {
        var grid = $("#gridVisitList" + name).data("kendoGrid");
        grid.dataSource.read();
    });
    VisitsFindButton = "#find" + name;
    $(VisitsFindButton).click(function (e) {
        var text = $("#findText" + name).val();
        //var findType = $("#findType" + name).val();
        var grid = $("#gridVisitList" + name).data("kendoGrid");
        if (text != '') {
            $filter = new Array();
           // if (findType == 0) {
                $filter.push({ field: "Name", operator: "contains", value: text });
          //  }
            grid.dataSource.filter({
                logic: "or",
                filters: $filter
            });
        } else {

            grid.dataSource.filter([]);
        }
    });
    $("#findText" + name).keypress(function (d) {
        if (d.keyCode == 13) {
            $("#find" + name).click();
        }
    });
    $("#findText" + name).change(function (e) {
        if ($("#findText" + name).val() == '') {
            var grid = $("#gridVisitList" + name).data("kendoGrid");
            grid.dataSource.read();
        }
    });
}