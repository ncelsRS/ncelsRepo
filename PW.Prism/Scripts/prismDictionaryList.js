

function panelDictionarySelect(e) {
    var selectType = $(e.item).find("> .k-link").attr('ItemType');
    if (selectType != null) {


        var id = $(e.item).find("> .k-link").attr('ModelId');
        var grid = $("#gridListDic" + id).data("kendoGrid");
        $("#typeDic" + id).val(selectType);
        grid.dataSource.read();
       
       
    }
}

function onExportInDic(e) {

    window.open('/Reference/ExportFile');
}

function InitFilterArchivGrid(name) {



    $("#reload" + name).click(function (e) {
        var grid = $("#gridListDic" + name).data("kendoGrid");
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
        var grid = $("#gridListDic" + name).data("kendoGrid");
        if (text != '') {
            $filter = new Array();
            if (findType == 0) {

                $filter.push({ field: "Code", operator: "contains", value: text });
                $filter.push({ field: "DepartmentsValue", operator: "contains", value: text });
                $filter.push({ field: "Summary", operator: "contains", value: text });

            }
            if (findType == 1) {
                $filter.push({ field: "Code", operator: "contains", value: text });
            }
            if (findType == 2) {
                $filter.push({ field: "Summary", operator: "contains", value: text });
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

function InitFilterDictionaryGrid(name) {

    $("#reload" + name).click(function (e) {
        var grid = $("#gridListDic" + name).data("kendoGrid");
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

function InitFilterEmployeGrid(name) {



    $("#reload" + name).click(function (e) {
        var grid = $("#gridListDic" + name).data("kendoGrid");
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
        var grid = $("#gridListDic" + name).data("kendoGrid");
        if (text != '') {
            $filter = new Array();
            if (findType == 0) {

                $filter.push({ field: "DisplayName", operator: "contains", value: text });


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
function InitFilterDictionaryWindowGrid(name) {

    $("#find" + name).click(function (e) {

        var text = $("#findText" + name).val();
        var findType = $("#findType" + name).val();
        var grid = $("#gridDic" + name).data("kendoGrid");
        if (text != '') {
            $filter = new Array();


            $filter.push({ field: "Code", operator: "contains", value: text });
            $filter.push({ field: "Name", operator: "contains", value: text });
            $filter.push({ field: "Email", operator: "contains", value: text });


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
            var grid = $("#gridDic" + name).data("kendoGrid");
            grid.dataSource.read();
        }
    });
}





function DictionaryView(id, type,model) {
    var window = $("#DictionaryCommandWindow");
    window.kendoWindow({
        width: "550px",
        height: "600px",
        content:'/Reference/CorrespondentList?Id=' + id + "&type=" + type,
        modal: true, resizable: false,
        title: 'Корреспонденты',
        actions: ["Close"]
    });
    window.data("kendoWindow").title('Корреспонденты');
    window.data("kendoWindow").modelDocument = model;
    window.data("kendoWindow").setOptions({
        width: 750,
        height: 600
    });
    //window.data("kendoWindow").refresh('/Reference/CorrespondentList?Id=' + id + "&type=" + type);
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}