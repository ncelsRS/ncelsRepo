function addProjectOutClick(e) {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docProjectAddContent" + guid;
    var nameAjax = '#docProjectAddContent' + guid;
    tabStrip.append({
        text: 'Новый проект',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/ProjectDoc/CardOut?params=" + guidGen(),
        //type: "POST",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}
function addProjectAdmClick(e) {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docProjectAddContent" + guid;
    var nameAjax = '#docProjectAddContent' + guid;
    tabStrip.append({
        text: 'Новый проект',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/ProjectDoc/CardAdm?params=" + guidGen(),
        //type: "POST",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}

function addProjectAdmMainClick(e) {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docProjectAddContent" + guid;
    var nameAjax = '#docProjectAddContent' + guid;
    tabStrip.append({
        text: 'Новый проект',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/ProjectDoc/CardAdmMain?params=" + guidGen(),
        //type: "POST",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}

function addProjectPrtClick(e) {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docProjectAddContent" + guid;
    var nameAjax = '#docProjectAddContent' + guid;
    tabStrip.append({
        text: 'Новый проект',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/ProjectDoc/CardPrt?params=" + guidGen(),
        //type: "POST",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}
function addProjectOutClickByTask(id) {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docProjectAddContent" + guid;
    var nameAjax = '#docProjectAddContent' + guid;
    tabStrip.append({
        text: 'Новый проект',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/ProjectDoc/CardOutByTask?params=" + guidGen() + '&taskId=' + id,
        //type: "POST",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}
function addProjectCorClick() {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docProjectAddContent" + guid;
    var nameAjax = '#docProjectAddContent' + guid;
    tabStrip.append({
        text: 'Новый проект',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/ProjectDoc/CardCorInit",
        //type: "POST",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}

function addProjectZklClick() {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docProjectAddContent" + guid;
    var nameAjax = '#docProjectAddContent' + guid;
    tabStrip.append({
        text: 'Новый проект',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/ProjectDoc/CardZkl",
        //type: "POST",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}

function addProjectAktClick() {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docProjectAddContent" + guid;
    var nameAjax = '#docProjectAddContent' + guid;
    tabStrip.append({
        text: 'Новый проект',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/ProjectDoc/CardAkt",
        //type: "POST",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}

function addProjectExpClick() {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docProjectAddContent" + guid;
    var nameAjax = '#docProjectAddContent' + guid;
    tabStrip.append({
        text: 'Новый проект',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/ProjectDoc/CardExp",
        //type: "POST",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}

function addProjectFklClick() {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docProjectAddContent" + guid;
    var nameAjax = '#docProjectAddContent' + guid;
    tabStrip.append({
        text: 'Новый проект',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/ProjectDoc/CardFkl",
        //type: "POST",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}

function addProjectFctClick() {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docProjectAddContent" + guid;
    var nameAjax = '#docProjectAddContent' + guid;
    tabStrip.append({
        text: 'Новый проект',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/ProjectDoc/CardFct",
        //type: "POST",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}

function addProjectIspClick() {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docProjectAddContent" + guid;
    var nameAjax = '#docProjectAddContent' + guid;
    tabStrip.append({
        text: 'Новый проект',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/ProjectDoc/CardIsp",
        //type: "POST",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}

function addProjectPerClick() {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docProjectAddContent" + guid;
    var nameAjax = '#docProjectAddContent' + guid;
    tabStrip.append({
        text: 'Новый проект',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/ProjectDoc/CardPer",
        //type: "POST",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}

function addProjectZfkClick() {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docProjectAddContent" + guid;
    var nameAjax = '#docProjectAddContent' + guid;
    tabStrip.append({
        text: 'Новый проект',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/ProjectDoc/CardZfk",
        //type: "POST",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}

function addProjectZfcClick() {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docProjectAddContent" + guid;
    var nameAjax = '#docProjectAddContent' + guid;
    tabStrip.append({
        text: 'Новый проект',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/ProjectDoc/CardZfc",
        //type: "POST",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}

function addProjectPfkClick() {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docProjectAddContent" + guid;
    var nameAjax = '#docProjectAddContent' + guid;
    tabStrip.append({
        text: 'Новый проект',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/ProjectDoc/CardPfk",
        //type: "POST",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}

function addProjectPfcClick() {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docProjectAddContent" + guid;
    var nameAjax = '#docProjectAddContent' + guid;
    tabStrip.append({
        text: 'Новый проект',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/ProjectDoc/CardPfc",
        //type: "POST",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}

function addProjectNapClick() {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docProjectAddContent" + guid;
    var nameAjax = '#docProjectAddContent' + guid;
    tabStrip.append({
        text: 'Новый проект',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/ProjectDoc/CardNap",
        //type: "POST",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}

function addProjectPesClick() {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docProjectAddContent" + guid;
    var nameAjax = '#docProjectAddContent' + guid;
    tabStrip.append({
        text: 'Новый проект',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/ProjectDoc/CardPes",
        //type: "POST",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}

function addProjectCorClickByTask(id) {

    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var guid = guidGen();
    var name = "docProjectAddContent" + guid;
    var nameAjax = '#docProjectAddContent' + guid;
    tabStrip.append({
        text: 'Новый проект',
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $(nameAjax);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: "/ProjectDoc/CardCorByTask?taskId=" + id,
        //type: "POST",
        success: function (result) {
            // refreshes partial view
            $(nameAjax).html(result);
        }
    });
}

function panelProjectSelect(e) {
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


        var grid = $("#gridProjectListDoc" + gridId).data("kendoGrid");
        if (selectValue == '') {
            grid.dataSource.filter([]);
        } else {
            grid.dataSource.filter({
                filters: filter
            });
        }
    }
}

function onExportProjectDoc(e) {
    window.open('/ProjectDoc/ExportFile');
}

function InitFilterProjectGrid(name) {
    $("#reload" + name).click(function (e) {
        var grid = $("#gridProjectListDoc" + name).data("kendoGrid");
        grid.dataSource.read();
    });

    $("#find" + name).click(function (e) {

        var text = $("#findText" + name).val();
        var findType = $("#findType" + name).val();
        var grid = $("#gridProjectListDoc" + name).data("kendoGrid");
        if (text != '') {
            $filter = new Array();
            if (findType == 0) {

                $filter.push({ field: "Number", operator: "contains", value: text });
                $filter.push({ field: "Summary", operator: "contains", value: text });
                $filter.push({ field: "ExecutorsValue", operator: "contains", value: text });
                $filter.push({ field: "ResponsibleValue", operator: "contains", value: text });
                $filter.push({ field: "CorrespondentsValue", operator: "contains", value: text });
                $filter.push({ field: "SignerValue", operator: "contains", value: text });
                $filter.push({ field: "DestinationValue", operator: "contains", value: text });
                $filter.push({ field: "RegistratorValue", operator: "contains", value: text });
                $filter.push({ field: "DocumentDictionaryTypeValue", operator: "contains", value: text });
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
            var grid = $("#gridProjectListDoc" + name).data("kendoGrid");
            grid.dataSource.read();
        }
    });
}
function getProjectDetails(parameters, number) {
    var element =document.getElementById(parameters);
    if (element == null) {
        var tabStrip = $("#tabstrip").data("kendoTabStrip");
        var content = '<div id="' + parameters + '"> </div>';
        var idContent = '#' + parameters;
        tabStrip.append({
            text: number,
            content: content

        });

        tabStrip.select(tabStrip.items().length - 1);

        var gridElement = $(idContent);

        gridElement.height($(window).height() - 100);

        $.ajax({
            url: "/ProjectDoc/Edit?id=" + parameters,
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

function openPpHistoryDetails(parameters, number) {
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
            url: "/Project/PpHistoryDetails?id=" + parameters,
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


function InitPpHistoryCard(uiId) {
    //var commentsGrid = $("#contractCommentsGrid" + uiId).data("kendoGrid");
}