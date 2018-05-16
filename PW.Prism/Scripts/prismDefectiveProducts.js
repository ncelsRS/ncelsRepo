function InitDefectiveProducts(name, id)
{
    var grid = $(name).data("kendoGrid");
    function declarationRowSelect(e)
    {
        var selectedRows = this.select();
        if (selectedRows.length > 0) {            
            $('#formingLetter' + id).removeClass('k-state-disabled');
            $('#SendEDO' + id).removeClass('k-state-disabled');
        }
        $(name + ' tr').find('.checkbox[type=checkbox]').prop('checked', false);
        $(name + ' tr.k-state-selected').find('.checkbox[type=checkbox]').prop('checked', true);
    }
    grid.bind("change", declarationRowSelect);
    grid.bind("dataBound", function ()
    {
        $(name + " .checkbox").bind("change", function (e) {
            var data = grid.dataItem($(e.target).closest("tr"));
            $('#formingLetter' + id).removeClass('k-state-disabled');
            $('#SendEDO' + id).removeClass('k-state-disabled');
            $(e.target).closest("tr").toggleClass("k-state-selected");
        });
        if (!$('#formingLetter' + id).hasClass('k-state-disabled'))
        {
            $('#formingLetter' + id).addClass('k-state-disabled');
            $('#SendEDO' + id).addClass('k-state-disabled');
        }
            
    });

    $("#notSendedReload" + id).click(function (e) {
        grid.dataSource.read();
    });

    $('#SelectAllDefective').click(function ()
    {
        var ds = grid.dataSource.view();
 
        for (var i = 0; i < ds.length; i++)
        {
            var row = grid.table.find("tr[data-uid='" + ds[i].uid + "']");
            var checkbox = $(row).find(".checkbox");
            if ($('#SelectAllDefective').is(":checked"))
            {
                checkbox.prop('checked', true);
            } else
            {
                checkbox.prop('checked', false);     
            }  
        }
    });

    $('#formingLetter' + id).click(function ()
    {
        var idsToSend = [];
        var ds = grid.dataSource.view();

        for (var i = 0; i < ds.length; i++)
        {
            var row = grid.table.find("tr[data-uid='" + ds[i].uid + "']");
            var checkbox = $(row).find(".checkbox");
            if (checkbox.is(":checked"))
            {
                idsToSend.push(ds[i].Id);
            }
        }
        if (idsToSend.length > 0)
        {
            var pdf = "<object data='/OBKDefectiveProducts/ShowPdfLetter?ides=" + idsToSend.toString() + "' type='application/pdf' style='width:100%;height:550px' ></object >"; 

            $("#modal-body-" + id).html(pdf);
            $('#modal'+id).modal('show');

        } 
    });

    $('#SendEDO' + id).click(function ()
    {
        var idsToSend = [];
        var ds = grid.dataSource.view();

        for (var i = 0; i < ds.length; i++)
        {
            var row = grid.table.find("tr[data-uid='" + ds[i].uid + "']");
            var checkbox = $(row).find(".checkbox");
            if (checkbox.is(":checked"))
            {
                idsToSend.push(ds[i].Id);
            }
        }
        if (idsToSend.length > 0)
        {
            debugger;
            $.ajax({
                type: 'POST',
                url: '/OBKDefectiveProducts/SendEDO',
                data: { ides: idsToSend },
                success: function (data)
                {
                    if (data.success == true)
                    {
                        alert("Успешно передано!");
                        grid.dataSource.read();
                    }
                }
            });

        } 
    });
 
}


function InitSendedProducts(name, id)
{
    var grid = $(name).data("kendoGrid");
    function declarationRowSelect(e)
    {
        var selectedRows = this.select();
        if (selectedRows.length > 0)
        {
            $('#addLetterDetails' + id).removeClass('k-state-disabled');
        }
        $(name + ' tr').find('.checkbox[type=checkbox]').prop('checked', false);
        $(name + ' tr.k-state-selected').find('.checkbox[type=checkbox]').prop('checked', true);
    }
    grid.bind("change", declarationRowSelect);
    grid.bind("dataBound", function ()
    {
        $(name + " .checkbox").bind("change", function (e)
        {
            var data = grid.dataItem($(e.target).closest("tr"));
            $('#addLetterDetails' + id).removeClass('k-state-disabled');
            $(e.target).closest("tr").toggleClass("k-state-selected");
        });
        if (!$('#addLetterDetails' + id).hasClass('k-state-disabled'))
        {
            $('#addLetterDetails' + id).addClass('k-state-disabled');
        }
    });

    $("#sendedReload" + id).click(function (e)
    {
        grid.dataSource.read();
    });

    $('#SelectAllSended').click(function ()
    {
        var ds = grid.dataSource.view();

        for (var i = 0; i < ds.length; i++)
        {
            var row = grid.table.find("tr[data-uid='" + ds[i].uid + "']");
            var checkbox = $(row).find(".checkbox");
            if ($('#SelectAllSended').is(":checked"))
            {
                checkbox.prop('checked', true);
            } else
            {
                checkbox.prop('checked', false);
            }
        }
    });

    $('#save' + id).click(function ()
    {
        var idsToSend = getSelectedRows(grid.dataSource.view());
       
        if (checkValidation())
        {
            $.ajax({
                type: 'POST',
                url: '/OBKDefectiveProducts/SaveLetterDetails/',
                data: {
                    number: $('#number' + id).val(),
                    date: $('#date' + id).val(),
                    ides: idsToSend
                },
                success: function (data)
                {
                    if (data.success == true)
                    {
                        $('#letterModal' + id).modal('hide');
                        grid.dataSource.read();
                    }
                }
            });
        }

    });

    $('#addLetterDetails' + id).click(function ()
    {
        var idsToSend = getSelectedRows(grid.dataSource.view());
        $('#number' + id).val("");
        $('#date' + id).val("");
        if (idsToSend.length > 0)
        {
            $('#letterModal' + id).modal('show');
        } else
        {
            alert("Выберите запись из таблицы!")
        }
              
    })

    function getSelectedRows(rows)
    {
        var idsToSend = [];

        for (var i = 0; i < rows.length; i++)
        {
            var row = grid.table.find("tr[data-uid='" + rows[i].uid + "']");
            var checkbox = $(row).find(".checkbox");
            if (checkbox.is(":checked"))
            {
                idsToSend.push(rows[i].Id);
            }
        }

        return idsToSend;
    }

    function checkValidation()
    {
        var valid = true;
        valid = checkAttr("number" + id, valid);
        valid = checkAttr("date" + id, valid);

        if (valid == false)
        {
            alert("Заполните обязательные поля!");
        }
        return valid;
    }

    function checkAttr(attr, valid)
    {
        var val = $("#" + attr + "").val();
        if (val == "")
        {
            $("#" + attr + "-label").css({ 'color': '#a94442' });
            valid = false;
        } else
        {
            $("#" + attr + "-label").css({ 'color': '#555' });
        }
        return valid;
    }

}