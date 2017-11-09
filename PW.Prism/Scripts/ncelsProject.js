function InitFilterProtocols(uiId) {
    $("#delete" + uiId).addClass('k-state-disabled');
    var grid = $('#gridPpProtocols' + uiId).data("kendoGrid");
    grid.bind("change", function () {
        var selected = this.select();

        if (selected.length > 0) {
            $("#delete" + uiId).removeClass('k-state-disabled');
        } else {
            $("#delete" + uiId).addClass('k-state-disabled');
        }
    });

    $("#reload" + uiId).click(function (e) {
        grid.dataSource.read();
    });

    var addButton = $("#add" + uiId).data("kendoButton");
    addButton.bind("click",
        function (e) {
            //debugger;
            var window = $("#TaskCommandWindow");
            window.kendoWindow({
                width: "900",
                height: "500",
                modal: true,
                actions: ["Close"]
            });
            window.data("kendoWindow").title('Протокол');
            window.data("kendoWindow").setOptions({
                width: 900,
                height: 500
            });

            window.data("kendoWindow").gridId = 'gridPpProtocols' + uiId;
            window.data("kendoWindow").content("<div id='window'><img style=\"position:absolute;left: 50%; top: 30%;\" src=\"/Content/Default/loading-image.gif\"/></div>");
            window.data("kendoWindow").refresh('/Protocol/ProtocolForm');
            window.data("kendoWindow").center();
            window.data("kendoWindow").open();
        });

    var deleteButton = $("#delete" + uiId).data("kendoButton");
    deleteButton.bind("click",
        function (e) {
            var r = confirm("Вы уверены что хотите удалить протокол?");
            if (r != true)
                return;

            var grid = $('#gridPpProtocols' + uiId).data("kendoGrid");
            var selected = grid.select();
            if (selected.length > 0) {
                var selectedItem = grid.dataItem(grid.select());
                var id = selectedItem.Id;

                $.ajax({
                    type: 'POST',
                    url: '/Protocol/DeleteProtocol',
                    data: '{rowId:\'' + id + '\'}',
                    contentType: 'application/json; charset=utf-8',
                    success: function (result) {
                        if (result == 'True') {
                            ShowAlert('Сообщение', 'Запись успешно удалена', 'Success');
                            grid.dataSource.read();
                        };
                    },
                    complete: function () {
                    }
                });
            }
        });
}

function openProtocol(parameters, number, uiId) {
    var window = $("#TaskCommandWindow");
    window.kendoWindow({
        width: "900",
        height: "500",
        modal: true,
        actions: ["Close"]
    });
    window.data("kendoWindow").title('Протокол ' + number);
    window.data("kendoWindow").setOptions({
        width: 900,
        height: 500
    });

    window.data("kendoWindow").gridId = 'gridPpProtocols' + uiId;
    window.data("kendoWindow").content("<div id='window'><img style=\"position:absolute;left: 50%; top: 30%;\" src=\"/Content/Default/loading-image.gif\"/></div>");
    window.data("kendoWindow").refresh('/Protocol/ProtocolForm?id=' + parameters);
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();

}

function openProtocolDocument(pId, number, uiId) {
    window.open('/Protocol/GetOutputDocument?id=' + pId + "&name=" + number);
}

function panelProtocolsSelect(e) {
    var selectType = $(e.item).find("> .k-link").attr('ItemType');
    if (selectType !== null) {
        var selectValue = $(e.item).find("> .k-link").attr('ItemId');
        var gridId = $(e.item).find("> .k-link").attr('ModelId');
        var grid = $("#gridPpProtocols" + gridId).data("kendoGrid");
        var filter = new Array();

        if (selectType === "Status") {
            filter.push({ field: "Status", operator: "eq", value: selectValue });
        }
        if (selectValue === '') {
            grid.dataSource.filter([]);
        } else {
            grid.dataSource.filter({
                logic: "and",
                filters: filter
            });
        }
    }
}


function InitFilterRegisterProjectGrid(name) {
    $("#reload" + name).click(function (e) {
        var grid = $("#gridRegisterProjec" + name).data("kendoGrid");
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
        var grid = $("#gridRegisterProjec" + name).data("kendoGrid");
        if (text != '') {
            $filter = new Array();
            if (findType == 0) {

                $filter.push({ field: "Number", operator: "contains", value: text });
                //$filter.push({ field: "ManufaturerName", operator: "contains", value: text });
                $filter.push({ field: "CountryName", operator: "contains", value: text });
                $filter.push({ field: "ApplicantName", operator: "contains", value: text });
                $filter.push({ field: "Classification", operator: "contains", value: text });
                //$filter.push({ field: "Login", operator: "contains", value: text });
                //$filter.push({ field: "Mnn", operator: "contains", value: text });
                //$filter.push({ field: "LsFormNameRu", operator: "contains", value: text });
                //$filter.push({ field: "Dosage", operator: "contains", value: text });
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
            var grid = $("#gridRegisterProjec" + name).data("kendoGrid");
            grid.dataSource.read();
        }
    });


}

function panelRegisterProjecSelect(e) {
    var selectType = $(e.item).find("> .k-link").attr('ItemType');
    if (selectType != null) {
        var selectValue = $(e.item).find("> .k-link").attr('ItemId');
        var gridId = $(e.item).find("> .k-link").attr('ModelId');
        var grid = $("#gridRegisterProjec" + gridId).data("kendoGrid");
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