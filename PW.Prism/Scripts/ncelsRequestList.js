function InitFilterListRequestGrid(name) {
    $("#reload" + name).click(function (e) {
        var grid = $("#gridContractAll" + name).data("kendoGrid");
        grid.dataSource.read();
    });


    $("#findText" + name).keypress(function (d) {
        if (d.keyCode == 13) {
            $("#find" + name).click();
        }
    });
    $("#find" + name).click(function (e) {

        var text = $("#findText" + name).val();
        var findType = $("#findType" + name).val();
        var grid = $("#gridRequestList" + name).data("kendoGrid");
        if (text != '') {
            $filter = new Array();
          
                //$filter.push({ field: "MnnName", operator: "contains", value: text });
                //$filter.push({ field: "TradeName", operator: "contains", value: text });
                //$filter.push({ field: "DrugForm", operator: "contains", value: text });
                //$filter.push({ field: "Concentration", operator: "contains", value: text });
                //$filter.push({ field: "RegNumber", operator: "contains", value: text });
                //$filter.push({ field: "Manufacturer", operator: "contains", value: text });
                //$filter.push({ field: "Country", operator: "contains", value: text });
            //$filter.push({ field: "AtxCode", operator: "contains", value: text });

            $filter.push({ field: "C_int_name", operator: "contains", value: text });
            $filter.push({ field: "name", operator: "contains", value: text });
            $filter.push({ field: "C_dosage_form_name", operator: "contains", value: text });
            $filter.push({ field: "concentration", operator: "contains", value: text });
            $filter.push({ field: "reg_number", operator: "contains", value: text });
            $filter.push({ field: "C_producer_name", operator: "contains", value: text });
            $filter.push({ field: "C_country_name", operator: "contains", value: text });
            $filter.push({ field: "C_atc_code", operator: "contains", value: text });

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
            var grid = $("#gridRequestList" + name).data("kendoGrid");
            grid.dataSource.read();
        }
    });


}