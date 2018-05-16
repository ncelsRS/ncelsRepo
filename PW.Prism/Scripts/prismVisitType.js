var VisitTypesFindButton;
function InitFilterVisitTypeAllGrid(name) {
    $("#reload" + name).click(function (e) {
        var grid = $("#gridVisitTypeList" + name).data("kendoGrid");
        grid.dataSource.read();
    });
    VisitTypesFindButton = "#find" + name;
    $(VisitTypesFindButton).click(function (e) {
        var text = $("#findText" + name).val();
        //var findType = $("#findType" + name).val();
        var grid = $("#gridVisitTypeList" + name).data("kendoGrid");
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
            var grid = $("#gridVisitTypeList" + name).data("kendoGrid");
            grid.dataSource.read();
        }
    });
}