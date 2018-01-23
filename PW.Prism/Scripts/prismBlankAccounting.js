function InitDecommission()
{
    var grid = $('#OBK_BlankAccounting').data("kendoGrid");
    function declarationRowSelect(e)
    {
        var selectedRows = this.select();
        if (selectedRows.length > 0) {            
            $('#decommission').removeClass('k-state-disabled');
        }
        $("#OBK_BlankAccounting" + ' tr').find('.checkbox[type=checkbox]').prop('checked', false);
        $("#OBK_BlankAccounting" + ' tr.k-state-selected').find('.checkbox[type=checkbox]').prop('checked', true);
    }
    grid.bind("change", declarationRowSelect);
    grid.bind("dataBound", function ()
    {
        $("#OBK_BlankAccounting" + " .checkbox").bind("change", function (e) {
            var data = grid.dataItem($(e.target).closest("tr"));
            $('#decommission').removeClass('k-state-disabled');
            $(e.target).closest("tr").toggleClass("k-state-selected");
        });
        if (!$('#decommission').hasClass('k-state-disabled'))
            $('#decommission').addClass('k-state-disabled');
    });

    $("#reload").click(function (e) {
        grid.dataSource.read();
    });

    $('#SelectPageBlanks').click(function ()
    {
        var ds = grid.dataSource.view();
        debugger;
        for (var i = 0; i < ds.length; i++)
        {
            var row = grid.table.find("tr[data-uid='" + ds[i].uid + "']");
            var checkbox = $(row).find(".checkbox");
            if ($('#SelectPageBlanks').is(":checked"))
            {
                checkbox.prop('checked', true);
            } else
            {
                checkbox.prop('checked', false);     
            }  
        }
    });

    $('#decommission').click(function () {
        var idsToSend = [];
        var ds = grid.dataSource.view();

        for (var i = 0; i < ds.length; i++) {
            var row = grid.table.find("tr[data-uid='" + ds[i].uid + "']");
            var checkbox = $(row).find(".checkbox");
            if (checkbox.is(":checked")) {
                idsToSend.push(ds[i].Id);
            }
        }
        if (idsToSend.length > 0) {
            $.ajax({
                type: 'POST',
                url: '/BlankAccounting/Decommission',
                data: { list: idsToSend},
                success: function (data)
                {
                    grid.dataSource.read();  
                    $('#SelectPageBlanks').prop('checked', false);   
                }
            });
            
        } else {
            grid.dataSource.read();
        }
    });
}

