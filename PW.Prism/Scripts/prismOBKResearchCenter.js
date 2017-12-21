function InitFilterOBKResearchCenter(uiId) {
    var grid = $('#gridResearchCenter' + uiId).data("kendoGrid");
    function declarationRowSelect(e) {
        var selectedRows = this.select();
        if (selectedRows.length > 0) {
            var selectedItem = grid.dataItem(grid.select());
            if (selectedItem.StageStatusCode === 'inQueue' || selectedItem.StageStatusCode === 'inWork')
                $('#toWork' + uiId).removeClass('k-state-disabled');
        }
        $("#gridResearchCenter" + uiId + ' tr').find('.checkbox[type=checkbox]').prop('checked', false);
        $("#gridResearchCenter" + uiId + ' tr.k-state-selected').find('.checkbox[type=checkbox]').prop('checked', true);
    }
    grid.bind("change", declarationRowSelect);
    grid.bind("dataBound", function () {
        $("#gridResearchCenter" + uiId + " .checkbox").bind("change", function (e) {
            var data = grid.dataItem($(e.target).closest("tr"));
            if (e.target.checked && (data.StageStatusCode === 'inQueue' || data.StageStatusCode === 'inWork')) {
                $('#toWork' + uiId).removeClass('k-state-disabled');
            }
            $(e.target).closest("tr").toggleClass("k-state-selected");
        });
        if (!$('#toWork' + uiId).hasClass('k-state-disabled'))
            $('#toWork' + uiId).addClass('k-state-disabled');
        var data = grid.dataSource.data();
        $.each(data,
            function (i, row) {
                if (row.PaymentOverdue) {
                    $('tr[data-uid="' + row.uid + '"] ').css("background-color", "#ff0029"); //red
                } else if (row.CountDosageIsControl > 0) {
                    $('tr[data-uid="' + row.uid + '"] ').css("background-color", "#99cc99"); //green
                }
            });
    });

    $("#reload" + uiId).click(function (e) {
        grid.dataSource.read();
    });

    $('#gridResearchCenter' + uiId).kendoTooltip({
        filter: "td.need-cell-tooltip",
        position: "left",
        show: function (e) {
            if (this.content.text()) {
                this.content.parent().css("visibility", "visible");
            }
        },
        hide: function (e) {
            this.content.parent().css("visibility", "hidden");
        },
        content: function (e) {
            return $(e.target).text();
        }
    }).data("kendoTooltip");
    
}


function panelResearchCenter(e) {
    var selectType = $(e.item).find("> .k-link").attr('ItemType');
    if (selectType !== null) {
        var selectValue = $(e.item).find("> .k-link").attr('ItemId');
        var gridId = $(e.item).find("> .k-link").attr('ModelId');
        var grid = $("#gridResearchCenter" + gridId).data("kendoGrid");
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
    }
}