function updateLeftPane(cb) {
    $.ajax({
        url: "/OBKExpertCouncil/GetLeftPane",
        success: function (result) {
            $("#expertCouncil_leftPane").html(result);
            if (cb) cb();
        }
    })
}

function updateHtmlVisible() {
    var ecNumber = $("#ECNumber");
    var ecActualDate = $("#expertCouncilActualDate").data("kendoDatePicker");
    var btnEcClose = $("#buttonEcClose");
    ecNumber.val("");
    ecActualDate.value("");
    if (ec.IsCompleted === "True") {
        if (ec.Number)
            ecNumber.val(ec.Number);
        if (ec.ActualDate) {
            var date = new Date(ec.ActualDate);
            ecActualDate.value(date);
        }
        ecNumber.attr("readonly", true);
        ecActualDate.readonly();
        btnEcClose.hide();
    } else {
        ecNumber.attr("readonly", false);
        ecActualDate.readonly(false);
        btnEcClose.show();
    }
}

function ECIsCompeleted() {
    var isCompleted = true;
    var grid = $("#EC_gridAssessmentDeclaration").data("kendoGrid");
    grid.dataSource.view().forEach(declaration => {
        if (!declaration.Result) isCompleted = false;
    });
    return isCompleted;
}

function ecClose() {
    if (!ec) return alert("Выберите экспертный совет!");
    var number = $("#ECNumber").val();
    if (!number) return alert("Введите номер протокола");
    var date = $("#expertCouncilActualDate").data("kendoDatePicker").value();
    if (!date) return alert("Выберите фактическую дату Экспертного Совета");
    if (!ECIsCompeleted()) return alert("Внесите решение во все заявки");
    $.ajax({
        url: "/OBKExpertCouncil/CloseEC",
        contentType: "application/json",
        data: {
            ecId: ec.Id,
            number: number,
            date: date.toISOString()
        },
        success: res => {
            updateLeftPane(function () {
                $("#" + ec.Id).trigger("click");
            });
        },
        error: err => {
            console.error(err);
        }
    })
}