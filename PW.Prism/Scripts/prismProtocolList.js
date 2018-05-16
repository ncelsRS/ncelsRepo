function addProtocolDocClick(e) {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docAdminAddContent" + guid;
    var nameAjax = '#docAdminAddContent' + guid;
    tabStrip.append({
        text: 'Новый протокол',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/ProtocolDoc/Card?params=" + guidGen(),
        //type: "POST",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}

function addProtocolRepeatDocClick(documentId) {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docAdminAddContent" + guid;
    var nameAjax = '#docAdminAddContent' + guid;
    tabStrip.append({
        text: 'Новый протокол',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/ProtocolDoc/RepeatCard?id=" + documentId,
        //type: "POST",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}

function panelProtocolSelect(e) {
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


        var grid = $("#gridAdminListDoc" + gridId).data("kendoGrid");
        if (selectValue == '') {
            grid.dataSource.filter([]);
        } else {
            grid.dataSource.filter({
                filters: filter
            });
        }
    }
}

function onExportProtocolDoc(e) {

    window.open('/ProtocolDoc/ExportFile');
}

function InitFilterProtocolGrid(name) {
    $("#reload" + name).click(function (e) {
        var grid = $("#gridAdminListDoc" + name).data("kendoGrid");
        grid.dataSource.read();
    });

    $("#find" + name).click(function (e) {

        var text = $("#findText" + name).val();
        var findType = $("#findType" + name).val();
        var grid = $("#gridAdminListDoc" + name).data("kendoGrid");
        if (text != '') {
            $filter = new Array();
            if (findType == 0) {

                $filter.push({ field: "Number", operator: "contains", value: text });
                $filter.push({ field: "MonitoringNote", operator: "contains", value: text });
                $filter.push({ field: "SignerValue", operator: "contains", value: text });
                $filter.push({ field: "SigningFormDictionaryValue", operator: "contains", value: text });
                $filter.push({ field: "Summary", operator: "contains", value: text });
                $filter.push({ field: "ReadersValue", operator: "contains", value: text });
                $filter.push({ field: "AgreementsValue", operator: "contains", value: text });
                $filter.push({ field: "ExecutorsValue", operator: "contains", value: text });
                $filter.push({ field: "ResponsibleValue", operator: "contains", value: text });
                $filter.push({ field: "AdministrativeTypeDictionaryValue", operator: "contains", value: text });
                $filter.push({ field: "NomenclatureDictionaryValue", operator: "contains", value: text });
                $filter.push({ field: "CompleteDocumentsValue", operator: "contains", value: text });
                $filter.push({ field: "EditDocumentsValue", operator: "contains", value: text });
                $filter.push({ field: "RepealDocumentsValue", operator: "contains", value: text });
                $filter.push({ field: "AutoCompleteDocumentsValue", operator: "contains", value: text });
                $filter.push({ field: "AutoEditDocumentsValue", operator: "contains", value: text });
                $filter.push({ field: "AutoRepealDocumentsValue", operator: "contains", value: text });
                $filter.push({ field: "RegistratorValue", operator: "contains", value: text });
                $filter.push({ field: "Note", operator: "contains", value: text });
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
            var grid = $("#gridAdminListDoc" + name).data("kendoGrid");
            grid.dataSource.read();
        }
    });
}

function getProtocolDetails(parameters, number) {
    if (docArray.indexOf(parameters.toLowerCase()) != -1)
        docArray.splice(docArray.indexOf(parameters.toLowerCase()), 1);
    var element =document.getElementById(parameters);
    if (element == null) {
        var tabStrip = $("#tabstrip").data("kendoTabStrip");
        var content = '<div id="' + parameters + '"> </div>';
        var idContent = '#' + parameters;
        tabStrip.append({
            text: 'Протокол: № ' + number,
            content: content

        });

        tabStrip.select(tabStrip.items().length - 1);

        var gridElement = $(idContent);

        gridElement.height($(window).height() - 100);

        $.ajax({
            url: "/ProtocolDoc/Edit?id=" + parameters,
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