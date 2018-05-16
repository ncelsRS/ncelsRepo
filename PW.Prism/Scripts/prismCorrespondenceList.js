function addInnerDocClick(e) {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docInnerAddContent" + guid;
    var nameAjax = '#docInnerAddContent' + guid;
    tabStrip.append({
        text: 'Новый внутренний документ',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/CorrespondenceDoc/Card?params=" + guidGen(),
        
        //type: "POST",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}

function addInnerInitDocClick(e) {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docInnerAddContent" + guid;
    var nameAjax = '#docInnerAddContent' + guid;
    tabStrip.append({
        text: 'Новый внутренний документ',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/CorrespondenceDoc/CardInit?params=" + guidGen(),

        //type: "POST",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}

function addAdminCorDocClick(e) {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docInnerAddContent" + guid;
    var nameAjax = '#docInnerAddContent' + guid;
    tabStrip.append({
        text: 'Новая служебная записка руководству',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/CorrespondenceDoc/CardAdmin?params=" + guidGen(),

        //type: "POST",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}

function panelInnerSelect(e) {
    var selectType = $(e.item).find("> .k-link").attr('ItemType');
    if (selectType != null) {
        var selectValue = $(e.item).find("> .k-link").attr('ItemId');

        var gridId = $(e.item).find("> .k-link").attr('ModelId');
        var filter = new Array();

        if (selectType == "Status") {

            filter.push({ field: "StateType", operator: "eq", value: selectValue });
        }
        if (selectType == "Monitoring") {

            filter.push({ field: "MonitoringType", operator: "eq", value: selectValue });
        }

        if (selectType == "OrganizationId") {
            filter.push({ field: "OrganizationId", operator: "eq", value: selectValue });
        }

        var grid = $("#gridInnerListDoc" + gridId).data("kendoGrid");
        if (selectValue == '') {
            grid.dataSource.filter([]);
        } else {
            grid.dataSource.filter({
                filters: filter
            });
        }
    }
}

function onExportInnerDoc(e) {

    window.open('/CorrespondenceDoc/ExportFile');
}

function InitFilterInnerGrid(name) {
    $("#reload" + name).click(function (e) {
        var grid = $("#gridInnerListDoc" + name).data("kendoGrid");
        grid.dataSource.read();
    });

    $("#find" + name).click(function (e) {

        var text = $("#findText" + name).val();
        var findType = $("#findType" + name).val();
        var grid = $("#gridInnerListDoc" + name).data("kendoGrid");
        if (text != '') {
            $filter = new Array();
            if (findType == 0) {

                $filter.push({ field: "Number", operator: "contains", value: text });
                $filter.push({ field: "Summary", operator: "contains", value: text });
                $filter.push({ field: "ExecutorsValue", operator: "contains", value: text });
                $filter.push({ field: "ResponsibleValue", operator: "contains", value: text });
                $filter.push({ field: "CorrespondentsValue", operator: "contains", value: text });
                $filter.push({ field: "SignerValue", operator: "contains", value: text });
                $filter.push({ field: "SourceValue", operator: "contains", value: text });
                $filter.push({ field: "RegistratorValue", operator: "contains", value: text });
            }
            if (findType == 1) {
                $filter.push({ field: "Number", operator: "contains", value: text });
            }
            if (findType == 2) {
                $filter.push({ field: "ResponsibleValue", operator: "contains", value: text });
            }
            if (findType == 3) {
                $filter.push({ field: "CorrespondentsInfo", operator: "contains", value: text });
                $filter.push({ field: "CorrespondentsValue", operator: "contains", value: text });
            }
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
            var grid = $("#gridInnerListDoc" + name).data("kendoGrid");
            grid.dataSource.read();
        }
    });
}
function getInnerDetails(parameters, number) {
    if (docArray.indexOf(parameters.toLowerCase()) != -1)
        docArray.splice(docArray.indexOf(parameters.toLowerCase()), 1);
    var element =document.getElementById(parameters);
    if (element == null) {
        var tabStrip = $("#tabstrip").data("kendoTabStrip");
        var content = '<div id="' + parameters + '"> </div>';
        var idContent = '#' + parameters;
        tabStrip.append({
            text: 'Служебка: № ' + number,
            content: content

        });

        tabStrip.select(tabStrip.items().length - 1);

        var gridElement = $(idContent);

        gridElement.height($(window).height() - 100);

        $.ajax({
            url: "/CorrespondenceDoc/Edit?id=" + parameters,
            //type: "POST",
            success: function(result) {
                // refreshes partial view
                $(idContent).html(result);
            }
        });
    } else {

        var itesm = $('#' + parameters)[0].parentElement.getAttribute('id').split('-')[1];
        if (itesm) {
            $("#tabstrip").data("kendoTabStrip").select(itesm - 1);
        }
    }
    //alert(parameters);
}