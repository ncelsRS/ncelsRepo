function addInDocClick(e) {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docInAddContent" + guid;
    var nameAjax = '#docInAddContent' + guid;
    tabStrip.append({
        text: 'Новый входящий',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/IncomingDoc/Card?params=" + guidGen(),
        type: "get",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}

function addInRepeatDocClick(documentId) {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docInAddContent" + guid;
    var nameAjax = '#docInAddContent' + guid;
    tabStrip.append({
        text: 'Новый входящий',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/IncomingDoc/RepeatCard?id=" + documentId,
        type: "get",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}

function panelIncomingSelect(e) {
    var selectType = $(e.item).find("> .k-link").attr('ItemType');
    if (selectType != null) {
        var selectValue = $(e.item).find("> .k-link").attr('ItemId');
        var gridId = $(e.item).find("> .k-link").attr('ModelId');
        var grid = $("#gridInListDoc" + gridId).data("kendoGrid");
        var filter = new Array();

        if (selectType == "Status") {

            filter.push({ field: "StateType", operator: "eq", value: selectValue });
        }
        if (selectType == "Monitoring") {

            filter.push({ field: "MonitoringType", operator: "eq", value: selectValue });
        }
        if (selectType == "DocumentKindDictionaryId") {


            if (selectValue == '1') {
                filter.push({ field: "DocumentKindDictionaryId", operator: "neq", value: '95E21020-089C-470F-B20E-CE8F0023D66B' });
                filter.push({ field: "DocumentKindDictionaryId", operator: "neq", value: 'ABF72347-C50C-46DD-B86C-61D170A5A417' });
                filter.push({ field: "AnswersValue", operator: "eq", value: '' });
            }
            if (selectValue == '2') {
                filter.push({ field: "DocumentKindDictionaryId", operator: "eq", value: '95E21020-089C-470F-B20E-CE8F0023D66B' });
                filter.push({ field: "AnswersValue", operator: "eq", value: '' });
            }
            if (selectValue == '3') {
                filter.push({ field: "DocumentKindDictionaryId", operator: "eq", value: 'ABF72347-C50C-46DD-B86C-61D170A5A417' });
                filter.push({ field: "AnswersValue", operator: "eq", value: '' });
            }
            if (selectValue == '4') {
                filter.push({ field: "AnswersValue", operator: "neq", value: '' });
            }
        }

        if (selectType == "OrganizationId") {
            filter.push({ field: "OrganizationId", operator: "eq", value: selectValue });
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
function panelIncomingRegionSelect(e) {
    var selectType = $(e.item).find("> .k-link").attr('ItemType');
    if (selectType != null) {


        var gridId = $(e.item).find("> .k-link").attr('ModelId');
        var filter = new Array();

        $("#OrganizationId" + gridId).val(selectType);
        filter.push({ field: "OrganizationId", operator: "eq", value: selectType });


        var grid = $("#gridInListDoc" + gridId).data("kendoGrid");
        if (selectType == '') {
            grid.dataSource.filter([]);
        } else {
            grid.dataSource.filter({
                filters: filter
            });
        }
    }
}
function onExportInDoc(e) {

    window.open('/IncomingDoc/ExportFile');
}

function onExportEdList(e) {
    window.open('/IncomingDoc/ExportEdList');
}

function onExportAloList(e) {
    window.open('/IncomingDoc/ExportAloList');
}

function onExportKnfList(e) {
    window.open('/IncomingDoc/ExportKnfList');
}

function onExportOthList(e) {
    window.open('/IncomingDoc/ExportOthList');
}

function InitFilterIncomingGrid(name) {
    $("#reload" + name).click(function (e) {
        var grid = $("#gridInListDoc" + name).data("kendoGrid");
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
        var grid = $("#gridInListDoc" + name).data("kendoGrid");
        if (text != '') {
            $filter = new Array();
            if (findType == 0) {

                $filter.push({ field: "Number", operator: "contains", value: text });
                $filter.push({ field: "MonitoringNote", operator: "contains", value: text });
                $filter.push({ field: "CorrespondentsValue", operator: "contains", value: text });
                $filter.push({ field: "CorrespondentsInfo", operator: "contains", value: text });
                $filter.push({ field: "OutgoingNumber", operator: "contains", value: text });
                $filter.push({ field: "Summary", operator: "contains", value: text });
                $filter.push({ field: "ResponsibleValue", operator: "contains", value: text });
                $filter.push({ field: "ExecutorsValue", operator: "contains", value: text });
                $filter.push({ field: "AnswersValue", operator: "contains", value: text });
                $filter.push({ field: "AutoAnswersValue", operator: "contains", value: text });
                $filter.push({ field: "AutoAnswersTempValue", operator: "contains", value: text });
                $filter.push({ field: "LanguageDictionaryValue", operator: "contains", value: text });
                $filter.push({ field: "QuestionDesignDictionaryValue", operator: "contains", value: text });
                $filter.push({ field: "DocumentKindDictionaryValue", operator: "contains", value: text });
                $filter.push({ field: "FormDeliveryDictionaryValue", operator: "contains", value: text });
                $filter.push({ field: "NomenclatureDictionaryValue", operator: "contains", value: text });
                $filter.push({ field: "CompleteDocumentsValue", operator: "contains", value: text });
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
            var grid = $("#gridInListDoc" + name).data("kendoGrid");
            grid.dataSource.read();
        }
    });


}
function InitFilterIncomingRegionGrid(name) {
    $("#reload" + name).click(function (e) {
        var grid = $("#gridInListDoc" + name).data("kendoGrid");
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
        var grid = $("#gridInListDoc" + name).data("kendoGrid");
        if (text != '') {
            $filter = new Array();
            if (findType == 0) {

                $filter.push({ field: "Number", operator: "contains", value: text });
                $filter.push({ field: "MonitoringNote", operator: "contains", value: text });
                $filter.push({ field: "CorrespondentsValue", operator: "contains", value: text });
                $filter.push({ field: "CorrespondentsInfo", operator: "contains", value: text });
                $filter.push({ field: "OutgoingNumber", operator: "contains", value: text });
                $filter.push({ field: "Summary", operator: "contains", value: text });
                $filter.push({ field: "ResponsibleValue", operator: "contains", value: text });
                $filter.push({ field: "ExecutorsValue", operator: "contains", value: text });
                $filter.push({ field: "AnswersValue", operator: "contains", value: text });
                $filter.push({ field: "AutoAnswersValue", operator: "contains", value: text });
                $filter.push({ field: "AutoAnswersTempValue", operator: "contains", value: text });
                $filter.push({ field: "LanguageDictionaryValue", operator: "contains", value: text });
                $filter.push({ field: "QuestionDesignDictionaryValue", operator: "contains", value: text });
                $filter.push({ field: "DocumentKindDictionaryValue", operator: "contains", value: text });
                $filter.push({ field: "FormDeliveryDictionaryValue", operator: "contains", value: text });
                $filter.push({ field: "NomenclatureDictionaryValue", operator: "contains", value: text });
                $filter.push({ field: "CompleteDocumentsValue", operator: "contains", value: text });
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

            if ($("#OrganizationId" + name).val() != null && $("#OrganizationId" + name).val() != '') {
                grid.dataSource.filter({
                    logic: "and",
                    filters: [{ logic: "or", filters: $filter }, { field: "OrganizationId", operator: "eq", value: $("#OrganizationId" + name).val() }]
                });
            } else {
                grid.dataSource.filter({
                    logic: "or",
                    filters: $filter
                });
            }
        } else {
            if ($("#OrganizationId" + name).val() != null && $("#OrganizationId" + name).val() != '') {
                grid.dataSource.filter({
                    logic: "and",
                    filters: [{ field: "OrganizationId", operator: "eq", value: $("#OrganizationId" + name).val() }]
                });
            } else {
                grid.dataSource.filter([]);
            }
        }
    });
    $("#findText" + name).keypress(function (d) {
        if (d.keyCode == 13) {
            $("#find" + name).click();
        }
    });
    $("#findText" + name).change(function (e) {
        if ($("#findText" + name).val() == '') {
            var grid = $("#gridInListDoc" + name).data("kendoGrid");
            grid.dataSource.read();
        }
    });


}

function getIncomingDetails(parameters, number) {
    if (docArray.indexOf(parameters.toLowerCase()) != -1)
        docArray.splice(docArray.indexOf(parameters.toLowerCase()), 1);
    var element = document.getElementById(parameters);
    if (element == null) {
        var tabStrip = $("#tabstrip").data("kendoTabStrip");
        var content = '<div id="' + parameters + '"> </div>';
        var idContent = '#' + parameters;
        tabStrip.append({
            text: 'Заявка: № ' + number,
            content: content

        });

        tabStrip.select(tabStrip.items().length - 1);

        var gridElement = $(idContent);

        gridElement.height($(window).height() - 100);

        $.ajax({
            url: "/IncomingDoc/Edit?id=" + parameters,
            //type: "POST",
            success: function (result) {
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