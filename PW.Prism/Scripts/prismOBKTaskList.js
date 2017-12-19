function InitFilterOBKTaskGrid(uiId) {
    debugger;
    var grid = $('#gridTaskList' + uiId).data("kendoGrid");

    $("#reload" + uiId).click(function (e) {
        grid.dataSource.read();
    });
}

function getTaskDetails(parameters, number) {
    debugger;
    var element = document.getElementById(parameters);
    if (element === null) {
        var tabStrip = $("#tabstrip").data("kendoTabStrip");
        var content = '<div id="' + parameters + '"> </div>';
        var idContent = '#' + parameters;
        tabStrip.append({
            text: 'Задание: № ' + number,
            content: content
        });
        tabStrip.select(tabStrip.items().length - 1);
        var gridElement = $(idContent);
        gridElement.height($(window).height() - 100);
        $.ajax({
            url: "/OBKTask/EditTask?taskId=" + parameters,
            success: function (result) {
                $(idContent).html(result);
            }
        });
    } else {
        var itesm = $('#' + parameters)[0].parentElement.getAttribute('id').split('-')[1];
        if (itesm) {
            $("#tabstrip").data("kendoTabStrip").select(itesm - 1);
        }
    }
}