function InitFilterSrReestrAllGrid(name) {
    $("#reload" + name).click(function (e) {
        var grid = $("#gridReestrList" + name).data("kendoGrid");
        grid.dataSource.read();
    });

    $("#find" + name).click(function (e) {

        var text = $("#findText" + name).val();
        var findType = $("#findType" + name).val();
        var grid = $("#gridReestrList" + name).data("kendoGrid");
        if (text != '') {
            $filter = new Array();
            if (findType == 0) {
                $filter.push({ field: "name", operator: "contains", value: text });
                $filter.push({ field: "name_kz", operator: "contains", value: text });
                $filter.push({ field: "reg_number", operator: "contains", value: text });
                $filter.push({ field: "C_producer_name", operator: "contains", value: text });
                $filter.push({ field: "C_producer_name_en", operator: "contains", value: text });
                $filter.push({ field: "C_producer_name_kz", operator: "contains", value: text });
                $filter.push({ field: "C_country_name", operator: "contains", value: text });
            }
            if (findType == 1) {
                $filter.push({ field: "C_producer_name", operator: "contains", value: text });
            }
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
            var grid = $("#gridReestrList" + name).data("kendoGrid");
            grid.dataSource.read();
        }
    });
}