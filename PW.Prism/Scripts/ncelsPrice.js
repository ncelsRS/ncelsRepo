
function InitFilterPriceGrid(name) {
    var grid = $("#gridPrice" + name).data("kendoGrid");
    grid.bind("dataBound",
        function() {
            var data = grid.dataSource.data();
            $.each(data,
                function(i, row) {
                    if (!row.IsSigned) {

                        $('tr[data-uid="' + row.uid + '"] ').css("background-color", "pink"); //red
                    }
                });
        });
    $("#reload" + name).click(function (e) {
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
        var grid = $("#gridPrice" + name).data("kendoGrid");
        if (text != '') {
            $filter = new Array();
            if (findType == 0) {

                $filter.push({ field: "Number", operator: "contains", value: text });
                $filter.push({ field: "MnnRu", operator: "contains", value: text });
                $filter.push({ field: "FormNameRu", operator: "contains", value: text });
                $filter.push({ field: "Concentration", operator: "contains", value: text });
                $filter.push({ field: "ManufacturerOrgName", operator: "contains", value: text });
                $filter.push({ field: "CountryName", operator: "contains", value: text });
                $filter.push({ field: "ApplicantOrgName", operator: "contains", value: text });
                $filter.push({ field: "RePriceName", operator: "contains", value: text });
            }
            //if (findType == 1) {
            //    $filter.push({ field: "Number", operator: "contains", value: text });
            //}
            //if (findType == 2) {
            //    $filter.push({ field: "ResponsibleValue", operator: "contains", value: text });
            //}
            //if (findType == 3) {
            //    $filter.push({ field: "CorrespondentsInfo", operator: "contains", value: text });
            //    $filter.push({ field: "CorrespondentsValue", operator: "contains", value: text });
            //}


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
            var grid = $("#gridPrice" + name).data("kendoGrid");
            grid.dataSource.read();
        }
    });


}


function panelPriceSelect(e) {
    var selectType = $(e.item).find("> .k-link").attr('ItemType');
    if (selectType != null) {
        var selectValue = $(e.item).find("> .k-link").attr('ItemId');
        var gridId = $(e.item).find("> .k-link").attr('ModelId');
        var grid = $("#gridPrice" + gridId).data("kendoGrid");
        var filter = new Array();

        if (selectType == "Status") {

            filter.push({ field: "Status", operator: "eq", value: selectValue });
        }


        if (selectValue == '') {
            grid.dataSource.filter([]);
        } else {
            grid.dataSource.filter({
                logic: "and",
                filters: filter
            });
        }
    }
}