function panelInstallationSelect(e) {

    var selectType = $(e.item).find("> .k-link").attr('ItemType');
    if (selectType != null) {
        var selectValue = $(e.item).find("> .k-link").attr('ItemId');

        var gridId = $(e.item).find("> .k-link").attr('ModelId');
        var filter = new Array();

        if (selectType == "Type") {

            filter.push({ field: "Type", operator: "eq", value: selectValue });
        }


        var grid = $("#gridInstallationList" + gridId).data("kendoGrid");
        if (selectValue == '') {
            grid.dataSource.filter([]);
        } else {
            grid.dataSource.filter({
                filters: filter
            });
        }
    }
}