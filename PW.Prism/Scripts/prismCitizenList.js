function addCitizenDocClick(e) {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docCitizenAddContent" + guid;
    var nameAjax = '#docCitizenAddContent' + guid;
    tabStrip.append({
        text: 'Новое обращение граждан',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/CitizenDoc/Card?params=" + guidGen(),
        //type: "POST",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}

function addCitizenRepeatDocClick(documentId) {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docCitizenAddContent" + guid;
    var nameAjax = '#docCitizenAddContent' + guid;
    tabStrip.append({
        text: 'Новое обращение граждан',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/CitizenDoc/RepeatCard?id=" + documentId,
        //type: "POST",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}

function panelCitizenSelect(e) {
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
        if (selectType == "ApplicantType") {

            filter.push({ field: "ApplicantType", operator: "eq", value: selectValue });
        }
        if (selectType == "CitizenTypeDictionaryId") {

            filter.push({ field: "CitizenTypeDictionaryId", operator: "eq", value: selectValue });
        }

        if (selectType == "OrganizationId") {
            filter.push({ field: "OrganizationId", operator: "eq", value: selectValue });
        }

        var grid = $("#gridCitizenListDoc" + gridId).data("kendoGrid");
        if (selectValue == '') {
            grid.dataSource.filter([]);
        } else {
            grid.dataSource.filter({
                filters: filter
            });
        }
    }
}

function onExportCitizenDoc(e) {

    window.open('/CitizenDoc/ExportFile');
}

function InitFilterCitizenGrid(name) {
    $("#reload" + name).click(function (e) {
        var grid = $("#gridCitizenListDoc" + name).data("kendoGrid");
        grid.dataSource.read();
    });

    $("#find" + name).click(function (e) {

        var text = $("#findText" + name).val();
        var findType = $("#findType" + name).val();
        var grid = $("#gridCitizenListDoc" + name).data("kendoGrid");
        if (text != '') {
            $filter = new Array();
            if (findType == 0) {

                $filter.push({ field: "Number", operator: "contains", value: text });
                //$filter.push({ field: "SortNumber", operator: "contains", value: text });
                $filter.push({ field: "MonitoringNote", operator: "contains", value: text });
                $filter.push({ field: "CorrespondentsInfo", operator: "contains", value: text });
                $filter.push({ field: "Summary", operator: "contains", value: text });
                $filter.push({ field: "OutgoingNumber", operator: "contains", value: text });
                $filter.push({ field: "ApplicantAddress", operator: "contains", value: text });
                $filter.push({ field: "ResponsibleValue", operator: "contains", value: text });
                $filter.push({ field: "ExecutorsValue", operator: "contains", value: text });
                $filter.push({ field: "AnswersValue", operator: "contains", value: text });
                $filter.push({ field: "AutoAnswersValue", operator: "contains", value: text });
                $filter.push({ field: "AutoAnswersTempValue", operator: "contains", value: text });
                $filter.push({ field: "CitizenTypeDictionaryValue", operator: "contains", value: text });
                $filter.push({ field: "FormDeliveryDictionaryValue", operator: "contains", value: text });
                $filter.push({ field: "LanguageDictionaryValue", operator: "contains", value: text });
                $filter.push({ field: "NomenclatureDictionaryValue", operator: "contains", value: text });
                $filter.push({ field: "QuestionDesignDictionaryValue", operator: "contains", value: text });
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
            var grid = $("#gridCitizenListDoc" + name).data("kendoGrid");
            grid.dataSource.read();
        }
    });
}
function getCitizenDetails(parameters, number) {
    if (docArray.indexOf(parameters.toLowerCase()) != -1)
        docArray.splice(docArray.indexOf(parameters.toLowerCase()), 1);
    var element =document.getElementById(parameters);
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
            url: "/CitizenDoc/Edit?id=" + parameters,
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