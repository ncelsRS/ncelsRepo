function getEmpContractDetails(parameters, number) {
    if (docArray.indexOf(parameters.toLowerCase()) != -1)
        docArray.splice(docArray.indexOf(parameters.toLowerCase()), 1);
    var element = document.getElementById(parameters);
    if (element == null) {
        var tabStrip = $("#tabstrip").data("kendoTabStrip");
        var content = '<div id="' + parameters + '"> </div>';
        var idContent = '#' + parameters;
        tabStrip.append({
            text: 'Договор: № ' + number,
            content: content

        });

        tabStrip.select(tabStrip.items().length - 1);

        var gridElement = $(idContent);

        gridElement.height($(window).height() - 100);

        $.ajax({
            url: "/EMPContract/Edit?id=" + parameters,
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

function initFilterEmpContract(uiId) {
    $("#reload" + uiId).click(function () {
        var grid = $("#grid" + uiId).data("kendoGrid");
        grid.dataSource.read();
    });
}

function panelEmpContractSelect(e) {
    debugger;
    var selectType = $(e.item).find("> .k-link").attr('ItemType');
    if (selectType !== null) {
        var selectValue = $(e.item).find("> .k-link").attr('ItemId');
        var gridId = $(e.item).find("> .k-link").attr('ModelId');
        var grid = $("#grid" + gridId).data("kendoGrid");
        var filter = new Array();
        if (selectType === "StageStatusCode") {
            filter.push({ field: "StageStatusCode", operator: "eq", value: selectValue });
        }
        if (selectValue === '') {
            grid.dataSource.filter([]);
        } else {
            grid.dataSource.filter({
                logic: "and",
                filters: filter
            });
        }

        //var panelBar = $(e.item).closest(".k-panelbar");
        //var panelId = panelBar.attr("id");
        //var uiId = panelId.replace(/\panelbar/g, '');
        //setToWorkBtnVisibility(uiId);
        //setFindAreaVisibility(uiId);
    }
}