

function panelNomenclatureSelect(e) {
    var selectType = $(e.item).find("> .k-link").attr('ItemType');
    if (selectType != null) {


        var id = $(e.item).find("> .k-link").attr('ModelId');
        var grid = $("#gridListDic" + id).data("kendoGrid");
        $("#typeDic" + id).val(selectType);
        grid.dataSource.read();
       
       
    }
}

function onExportInNomenclature(e) {

    window.open('/Reference/ExportFile?type=Nomenclature');
}

function InitFilterNomenclatureGrid(name) {



    $("#reload" + name).click(function (e) {
        var grid = $("#gridListDic" + name).data("kendoGrid");
        grid.dataSource.read();
    });

    $("#find" + name).click(function (e) {

        var text = $("#findText" + name).val();
        var findType = $("#findType" + name).val();
        var grid = $("#gridListDic" + name).data("kendoGrid");
        if (text != '') {
            $filter = new Array();
            if (findType == 0) {

                $filter.push({ field: "Code", operator: "contains", value: text });
                $filter.push({ field: "Name", operator: "contains", value: text });
                $filter.push({ field: "NameKz", operator: "contains", value: text });

            }
            if (findType == 1) {
                $filter.push({ field: "Code", operator: "contains", value: text });
            }
            if (findType == 2) {
                $filter.push({ field: "Name", operator: "contains", value: text });
            }
            if (findType == 3) {
                $filter.push({ field: "Note", operator: "contains", value: text });
            }
            grid.dataSource.filter({
                logic: "or",
                filters: $filter
            });
        } else {

            grid.dataSource.filter([]);
        }
    });

    $("#findText" + name).change(function (e) {
        if ($("#findText" + name).val() == '') {
            var grid = $("#gridListDic" + name).data("kendoGrid");
            grid.dataSource.read();
        }
    });
}
