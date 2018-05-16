
function InitFilterPriceProcessingGrid(name) {
    $("#reload" + name).click(function (e) {
        var grid = $("#gridPriceProcessing" + name).data("kendoGrid");
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
        var grid = $("#gridPriceProcessing" + name).data("kendoGrid");
        if (text != '') {
            $filter = new Array();
            if (findType == 0) {

                $filter.push({ field: "Number", operator: "contains", value: text });
                $filter.push({ field: "MnnRu", operator: "contains", value: text });
                //$filter.push({ field: "FormNameRu", operator: "contains", value: text });
                //$filter.push({ field: "Concentration", operator: "contains", value: text });
                $filter.push({ field: "ManufacturerOrgName", operator: "contains", value: text });
                //$filter.push({ field: "CountryName", operator: "contains", value: text });
                //$filter.push({ field: "ApplicantOrgName", operator: "contains", value: text });
                //$filter.push({ field: "RePriceName", operator: "contains", value: text });
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
            var grid = $("#gridPriceProcessing" + name).data("kendoGrid");
            grid.dataSource.read();
        }
    });
}

function getPpDetails(id, number) {
    var element = document.getElementById(id);
    if (element === null) {
        var tabStrip = $("#tabstrip").data("kendoTabStrip");
        var content = '<div id="' + id + '"> </div>';
        var idContent = '#' + id;
        tabStrip.append({
            text: 'Заявление: № ' + number,
            content: content

        });

        tabStrip.select(tabStrip.items().length - 1);

        var gridElement = $(idContent);

        gridElement.height($(window).height() - 100);

        $.ajax({
            url: "/PriceProcessing/Edit?id=" + id,
            //type: "POST",
            success: function (result) {
                // refreshes partial view
                $(idContent).html(result);
                $('.mark-check-found').each(function () {
                    var idcontrol = $(this).attr('idCheck');
                    $("#" + idcontrol).prop("checked", true);
                });
            }
        });
    } else {
        var itesm = $('#' + id)[0].parentElement.getAttribute('id').split('-')[1];
        if (itesm) {
            $("#tabstrip").data("kendoTabStrip").select(itesm - 1);
        }
    }
}

function InitializeSizeEditForm(name) {
    var gridElement = $("#pwContentRightId" + name);
    gridElement.height($(window).height() - 120);
}

function panelPriceProcessingSelectType(e) {
    debugger;
    var selectType = $(e.item).find("> .k-link").attr('ItemType');
    if (selectType != null) {
        var selectValue = $(e.item).find("> .k-link").attr('ItemId');
        var gridId = $(e.item).find("> .k-link").attr('ModelId');
        var grid = $("#gridPriceProcessing" + gridId).data("kendoGrid");
        var filter = new Array();

        if (selectType == "Type") {
            filter.push({ field: "Type", operator: "eq", value: selectValue });
        }

        if (selectValue == '') {
            grid.dataSource.filter([]);
        } else {
            var curr_filters = grid.dataSource.filter().filters;
            curr_filters.push(filter);
            grid.dataSource.filter(curr_filters);
        }
    }
}

function panelPriceProcessingSelect(e) {
    var modelId = $(e.item).find("> .k-link").attr('ModelId');
    if (modelId != null) {
        var filter = new Array();
        var grid = $("#gridPriceProcessing" + modelId).data("kendoGrid");
        var selectType = $("#panelbarType" + modelId).find(".k-state-selected");
        var selectTypeType = $(selectType).attr('ItemType');
        var selectTypeValue = $(selectType).attr('ItemId');
        if (selectTypeType == "Type") {
            if (selectTypeValue != '') {
                filter.push({ field: "Type", operator: "eq", value: selectTypeValue });
            }
        }

        var selectStatus = $("#panelbarStatus" + modelId).find(".k-state-selected");
        var selectStatusType = $(selectStatus).attr('ItemType');
        var selectStatusValue = $(selectStatus).attr('ItemId');
        if (selectStatusType == "SelectStatus") {
            if (selectStatusValue != '') {
                filter.push({ field: "SelectStatus", operator: "eq", value: selectStatusValue });
            }
        }

        grid.dataSource.filter(filter);

    }
}