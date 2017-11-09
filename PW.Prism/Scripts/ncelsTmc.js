var GeneralTmc = {
    expandedItemIDs: [],
    subgridChange: function (gridId) {
        var grid = $('#' + gridId).data("kendoGrid");
        for (var i = 0; i < GeneralTmc.expandedItemIDs.length; i++) {
            var row = $(grid.tbody).find("tr.k-master-row:eq(" + GeneralTmc.expandedItemIDs[i] + ")");
            grid.expandRow(row);
            //grid.expandRow("tr[data-uid='" + GeneralTmc.expandedItemIDs[i] + "']");
        }
    },
    contains: function(a, obj) {
        for (var i = 0; i < a.length; i++)
            if (a[i] === obj) return true;
        return false;
    },
    save: function (e) {
        debugger;
        kendo.ui.progress(e.container, true);
        e.container.find(".k-grid-update").addClass("k-state-disabled");
        e.container.find(".k-grid-update").click(GeneralTmc.stopEvent);
    },
    stopEvent: function (e) {
        e.preventDefault();
        e.stopPropagation();
    }
}