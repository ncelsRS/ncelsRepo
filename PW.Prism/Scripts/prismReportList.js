

function panelReportSelect(e) {
    var selectType = $(e.item).find("> .k-link").attr('ItemType');
    if (selectType != null && selectType != '') {


        var id = $(e.item).find("> .k-link").attr('ModelId');
      //  $("#typeReport" + id).val(selectType);
        document.getElementById("typeReport" + id).value = selectType;

        $.ajax({
            url: '/Report/Details?name=' + selectType,
            //type: "POST",
            success: function (result) {
                // refreshes partial view
                $('#contentReport' + id).html(result);
                var gridElement = $("#contentReport" + id);

                gridElement.height($(window).height() - 100);
            }
        });
    } else {
     
    }
}

function buttonReportGenSelect(e) {
    var id = e.sender.element[0].getAttribute('ModelId');
    var selectType = $("#typeReport" + id).val();
    if (selectType != null && selectType != '') {
        var dateStart = $("#dateStart" + id).val();
        var dateEnd = $("#dateEnd" + id).val();
        var stage;
        var stageName;
        var stageList = $("#selectStage" + id).data("kendoDropDownList");
        if (stageList && stageList.select() !== -1) {
            stage = stageList.select();
            stageName = stageList.text();
        }

        $.ajax({
            url: '/Report/Details?name=' + selectType + '&dateStart=' + dateStart + '&dateEnd=' + dateEnd + '&stage=' + stage + '&stageName=' + stageName,
            //type: "POST",
            success: function(result) {
                // refreshes partial view
                $('#contentReport' + id).html(result);
                var gridElement = $("#contentReport" + id);

                gridElement.height($(window).height() - 100);
            }
        });
    } else {
        alert('Не выбран отчет');
    }
}

function buttonReportDepGenSelect(e) {
    var id = e.sender.element[0].getAttribute('ModelId');
    var selectType = $("#typeReport" + id).val();
    if (selectType != null && selectType != '') {
        var dateStart = $("#dateStart" + id).val();
        var dateEnd = $("#dateEnd" + id).val();
        var lsPriceComparisonItem1 = $('#lsPriceComparisonItem1' + id).val();
        var lsPriceComparisonItem2 = $('#lsPriceComparisonItem2' + id).val();
        var urlLocal = '/Report/DepartamentDetails?name=' +
            selectType +
            '&dateStart=' +
            dateStart +
            '&dateEnd=' +
            dateEnd +
            '&lsPriceComparisonItem1=' +
            lsPriceComparisonItem1 +
            '&lsPriceComparisonItem2=' +
            lsPriceComparisonItem2;

        if ($("#ddl" + id)) {
            var depId = $("#ddl" + id).val();
            urlLocal += '&departmentId=' + depId;
        }

        $.ajax({
            url: urlLocal,
            //type: "POST",
            success: function (result) {
                // refreshes partial view
                $('#contentReport' + id).html(result);
                var gridElement = $("#contentReport" + id);

                gridElement.height($(window).height() - 100);
            }
        });
    } else {
        alert('Не выбран отчет');
    }
}

function buttonReportDepSendSelect(e) {

    var id = e.sender.element[0].getAttribute('ModelId');
    var selectType = $("#typeReport" + id).val();
    if (selectType != null && selectType != '') {
        var dateStart = $("#dateStart" + id).val();
        var dateEnd = $("#dateEnd" + id).val();
        var urlLocal = '/Report/DepartamentDetailsSend?name=' + selectType + '&dateStart=' + dateStart + '&dateEnd=' + dateEnd;
        if ($("#ddl" + id)) {
            var depId = $("#ddl" + id).val();
            urlLocal += '&departmentId=' + depId;
        }

        $.ajax({
            url: urlLocal,
            //type: "POST",
            success: function (result) {
                if (result.IsSuccess) {
                    $("#hiddenSendId" + id).val(result.Id);
                    alert("Отчет отправлен");
                } else {
                    // $("#hiddenSendId" + id).val(result.Id);
                    alert("Ошибка отправки: " + result.Reason);
                }
               

                // refreshes partial view
                //$('#contentReport' + id).html(result);
                //var gridElement = $("#contentReport" + id);

                //gridElement.height($(window).height() - 100);
            }
        });
    } else {
        alert('Не выбран отчет');
    }
}

function panelReportDepSelect(e) {
    var selectType = $(e.item).find("> .k-link").attr('ItemType');
    var id = $(e.item).find("> .k-link").attr('ModelId');

    if (selectType != null && selectType != '') {
        $("#typeReport" + id).val(selectType);
        if (selectType === 'PriceComparison.mrt') {
            initLsPriceComparisonReportParams();
            return;
        }

        // config 
        if (selectType == 'applicationD_1.mrt') {
            $('#divDepartmentContainer_' + id).show();
            $('#divSendButtonContainer_' + id).show();
        } else if (selectType == 'applicationD_5.mrt') {
            $('#divDepartmentContainer_' + id).hide();
            $('#divSendButtonContainer_' + id).show();
        } else {
            $('#divDepartmentContainer_' + id).hide();
            $('#divSendButtonContainer_' + id).hide();
        }
        $('#dateStart' + id).data("kendoDatePicker").enable(true);
        $('#dateEnd' + id).data("kendoDatePicker").enable(true);
        $('#export' + id).data("kendoButton").enable(true);
        $('#buttonGen' + id).data("kendoButton").enable(true);

        if (selectType === 'PpArchiveCountsReport.mrt') {
            return;
        }

        var dateStart = $("#dateStart" + id).val();
        var dateEnd = $("#dateEnd" + id).val();
        var lsPriceComparisonItem1 = $('#lsPriceComparisonItem1' + id).val();
        var lsPriceComparisonItem2 = $('#lsPriceComparisonItem2' + id).val();
        var urlLocal = '/Report/DepartamentDetails?name=' + selectType +
            '&dateStart=' +
            dateStart +
            '&dateEnd=' +
            dateEnd +
            '&lsPriceComparisonItem1=' +
            lsPriceComparisonItem1 +
            '&lsPriceComparisonItem2=' +
            lsPriceComparisonItem2;


        $.ajax({
            url: urlLocal,
            //type: "POST",
            success: function(result) {
                // refreshes partial view
                $('#contentReport' + id).html(result);
                var gridElement = $("#contentReport" + id);

                gridElement.height($(window).height() - 100);
            }
        });
    } else {
        $('#dateStart' + id).data("kendoDatePicker").enable(false);
        $('#dateEnd' + id).data("kendoDatePicker").enable(false);
        $('#export' + id).data("kendoButton").enable(false);
        $('#buttonGen' + id).data("kendoButton").enable(false);
    }
}

function onExportReport(e) {
    debugger;
    var name = e.sender.element.attr('ModelId');
    var type = $("#typeReport" + name).val();
    if (type == '' || type == null) {
        alert('Не выбран отчет');
    } else {
        var dateStart = $("#dateStart" + name).val();
        var dateEnd = $("#dateEnd" + name).val();
        var lsPriceComparisonItem1 = $('#lsPriceComparisonItem1' + name).val();
        var lsPriceComparisonItem2 = $('#lsPriceComparisonItem2' + name).val();
        window.open('/Report/ExportFile?name=' + type + '&dateStart=' + dateStart + '&dateEnd=' + dateEnd+ '&lsPriceComparisonItem1=' + lsPriceComparisonItem1 + '&lsPriceComparisonItem2='+lsPriceComparisonItem2);
    }
}

function initLsPriceComparisonReportParams() {
    var window = $("#TaskCommandWindow");
    window.kendoWindow({
        width: "550px",
        height: "auto",
        modal: true,
        close: function() {
            var window = $("#TaskCommandWindow");
            window.data("kendoWindow").content('');
        },
        title: 'Параметры отчета',
        actions: ["Close"]
    });

    window.data("kendoWindow").setOptions({
        width: 550,
        height: 'auto'
    });
    window.data("kendoWindow").refresh('/Report/LsPriceComparisonReportParams');

    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}

function closeReportParamWindow() {
    debugger;
    $("#TaskCommandWindow").data("kendoWindow").close();
}
function generateLsPriceComparisonReport() {
    debugger;
    var id = $('#reportIndexDepartmentModelId').val();
    var firstLsCombobox = $('#firstLs' + lsPriceComparisonReportParamsModel).data("kendoComboBox");
    var secondLsCombobox = $('#secondLs' + lsPriceComparisonReportParamsModel).data("kendoComboBox");
    if (firstLsCombobox.value() && secondLsCombobox.value()) {
        $('#lsPriceComparisonItem1' + id).val(firstLsCombobox.value());
        $('#lsPriceComparisonItem2' + id).val(secondLsCombobox.value());
        closeReportParamWindow();
        buttonReportDepGenSelect();
    }
}

function InitFilterReportGrid(name) {



   
    var gridElement = $("#contentReport" + name);

    gridElement.height($(window).height() - 100);



}
