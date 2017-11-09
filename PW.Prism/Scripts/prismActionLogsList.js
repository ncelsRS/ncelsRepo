var permissionRolesFindButton;
function InitFilterActionLogsListGrid(name) {
    $("#reload" + name).click(function (e) {
        var grid = $("#actionLogsList" + name).data("kendoGrid");
        grid.dataSource.read();
    });
    permissionRolesFindButton = "#find" + name;
    $(permissionRolesFindButton).click(function (e) {
        var text = $("#findText" + name).val();
        //var findType = $("#findType" + name).val();
        var grid = $("#actionLogsList" + name).data("kendoGrid");
        if (text != '') {
            $filter = new Array();
           // if (findType == 0) {
            $filter.push({ field: "Text", operator: "contains", value: text });
            $filter.push({ field: "EmployeeLogin", operator: "contains", value: text });
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
            var grid = $("#actionLogsList" + name).data("kendoGrid");
            grid.dataSource.read();
        }
    });
}