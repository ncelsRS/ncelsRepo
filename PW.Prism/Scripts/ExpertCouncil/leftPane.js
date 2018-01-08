$(document).ready(function () {
    $('#expertCouncilAddNew').on('click', function (e) {
        $('#expertCouncilAddWindow').kendoWindow({
            width: "600px",
            title: "Добавление заседания экспертного совета",
            visible: false,
            modal: true,
            pinned: false,
            draggable: false,
            actions: ['Close']
        }).data("kendoWindow").center().open();
    });
})

function expertCouncilAddWindowClose() {
    $('#expertCouncilAddName').val("");
    $("#expertCouncilAddDate").data('kendoDatePicker').value("");
    $('#expertCouncilAddWindow').data('kendoWindow').close();
}

function expertCouncilSave(e) {
    var council = {
        Name: $("#expertCouncilAddName").val(),
        Date: $("#expertCouncilAddDate").data('kendoDatePicker').value()
    };
    alert(JSON.stringify(council));
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