function SetVersion(version) {
    $('#pwMemuVerItem').children().text("Версия: 3.11.911("+version+")");
}

function resizeGrid(name) {
    var gridElement = $(name);
    var dataArea = gridElement.find(".k-grid-content");

    var newGridHeight = $(document).height() - 250;
    var newDataAreaHeight = newGridHeight;

    dataArea.height(newDataAreaHeight);
    gridElement.height(newGridHeight);
}

function resizeGrid2(name) {
    var gridElement = $(name);
    var dataArea = gridElement.find(".k-grid-content");

    var newGridHeight = $(document).height() - 370;
    var newDataAreaHeight = newGridHeight;

    dataArea.height(newDataAreaHeight);
    gridElement.height(newGridHeight);
}

function resizeGrid3(name, value) {
    var gridElement = $(name);
    var dataArea = gridElement.find(".k-grid-content");

    var newGridHeight = $(document).height() - value;
    var newDataAreaHeight = newGridHeight;

    dataArea.height(newDataAreaHeight);
    gridElement.height(newGridHeight);
}

function resizeUploader(name) {
    var gridElement = $(name);
    var dataArea = gridElement.find(".k-grid-content");

    var newGridHeight = $(document).height() - 370;
    var newDataAreaHeight = newGridHeight;

    dataArea.height(newDataAreaHeight);
    gridElement.height(newGridHeight);
}
var DateDiff = {
    inDays: function(d1, d2) {
        var t2 = d2.getTime();
        var t1 = d1.getTime();

        return parseInt((t2 - t1) / (24 * 3600 * 1000));
    },

    inWeeks: function(d1, d2) {
        var t2 = d2.getTime();
        var t1 = d1.getTime();

        return parseInt((t2 - t1) / (24 * 3600 * 1000 * 7));
    },

    inMonths: function(d1, d2) {
        var d1Y = d1.getFullYear();
        var d2Y = d2.getFullYear();
        var d1M = d1.getMonth();
        var d2M = d2.getMonth();

        return (d2M + 12 * d2Y) - (d1M + 12 * d1Y);
    },

    inYears: function(d1, d2) {
        return d2.getFullYear() - d1.getFullYear();
    }
};
function isoDateReviver(value) {
    if (typeof value === 'string') {
        var a = /^(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2}(?:\.\d*)?)(?:([\+-])(\d{2})\:(\d{2}))?Z?$/.exec(value);
        if (a) {
            var utcMilliseconds = Date.UTC(+a[1], +a[2] - 1, +a[3], +a[4], +a[5], +a[6]);
            return new Date(utcMilliseconds);
        }
    }
    return value;
}

function guidGen() {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000)
                   .toString(16)
                   .substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
           s4() + '-' + s4() + s4() + s4();
}
function StateFilterDoc(element) {

    var dataStateType = [
 // { text: "В работу", value: "0" },
  { text: "Новый", value: 0 },
  { text: "Зарегестрирован", value: 1 },
  { text: "В работе", value: 2 },
  { text: "На исполнении", value: 3 },
  { text: "Исполненно", value: 9 }
    ];
    element.kendoDropDownList({
        dataTextField: "text",
        dataValueField: "value",

        dataSource: dataStateType,
        optionLabel: "--Выберите значения--"
    });

}
function StateFilterOutDoc(element) {

    var dataStateType = [
 // { text: "В работу", value: "0" },
  { text: "Новый", value: 0 },
  { text: "Зарегестрирован", value: 1 }
    ];
    element.kendoDropDownList({
        dataTextField: "text",
        dataValueField: "value",

        dataSource: dataStateType,
        optionLabel: "--Выберите значения--"
    });

}
function StateFilterPrjDoc(element) {

    var dataStateType = [
 // { text: "В работу", value: "0" },
  { text: "Новый", value: 0 },
  { text: "Зарегестрирован", value: 1 },
    { text: "На согласовании", value: 4 },
    { text: "На регистрации", value: 5 },
    { text: "Отозванный", value: 6 },
    { text: "Исполненный отрицательно", value: 7 },
    { text: "Исполненный", value: 9 }
    ];
    element.kendoDropDownList({
        dataTextField: "text",
        dataValueField: "value",

        dataSource: dataStateType,
        optionLabel: "--Выберите значения--"
    });

}
function MonitoringFilterDoc(element) {

    var dataMonitoringType = [
 // { text: "В работу", value: "0" },
  { text: "Не контрольный", value: "1" },
  { text: "Контроль", value: "2" },
  { text: "Особый контроль", value: "3" },
  { text: "До контроль", value: "4" }
    ];
    element.kendoDropDownList({
        dataTextField: "text",
        dataValueField: "value",
        dataSource: dataMonitoringType,
        optionLabel: "--Выберите значения--"
    });
}
function treereload(idModel,parentId) {
    var treeview = $("#treeview" + idModel).data("kendoTreeView");
 
    var getitem = treeview.dataSource.get(parentId);


    var selectitem = treeview.findByUid(getitem.uid);
    treeview.select(selectitem);
    var selected = treeview.select();
    console.log('selectitem', selectitem);
    console.log('selected', selected);
    if (selected != null) {
        selected.haschildren = true;
        getitem.load();
        selected.load();
   
    }
}
function addExtensionClass(extension) {
    switch (extension) {
        case '.jpg':
        case '.jpeg':
        case '.bmp':
        case '.png':
        case '.gif':
            return "img-file";
        case '.doc':
        case '.docx':
            return "doc-file";
        case '.xls':
        case '.xlsx':
            return "xls-file";
        case '.pdf':
            return "pdf-file";
        case '.ppt':
        case '.pptx':
            return "ppt-file";
        case '.zip':
        case '.rar':
            return "zip-file";
        case '.avi':
        case '.mov':
        case '.mp4':
        case '.mkv':
        case '.wmv':
            return "avi-file";
        case '.mp3':
        case '.wav':
            return "mp3-file";
        default:
            return "default-file";
    }
}

getPane = function (index, splitterElement) {
    index = Number(index);

    var panes = splitterElement.children(".k-pane");

    if (!isNaN(index) && index < panes.length) {
        return panes[index];
    }
};

function onActivate(e) {
    var items = $("div[id*='splitter']");
    $.each(items, function (i, item) {

        var id = item.getAttribute('id');
        var splitterElement = $("#" + id);
        var splitter = $("#" + id).data('kendoSplitter');

        var pane = getPane(0, splitterElement);
        if (!pane) return;
        if (splitter != null) {
            splitter.toggle(pane, $(pane).width() <= 0);
            splitter.toggle(pane, $(pane).width() <= 0);
        }
    });


   
}

function onSelect(e) {
    //if ($(e.item)[0].getAttribute('aria-controls').split('-')[1] > 1) {
    if ($(e.item)[0].id != 'main') {
        var id = guidGen();
        $(e.item)[0].id = id;
        $(e.item)[0].innerHTML = "<span class=\"k-loading k-complete\"></span>" +
            "<span class=\"k-link\"style='font-size: 12px !important; padding-right:5px;'>" +
            $(e.item).find("> .k-link").text() +
            "<input class='pwButtonClose' type='button' onClick='closeTab(\"" + id + "\");'></span>";
        //} else {
        //    var id = guidGen();
        //    $(e.item)[0].id = id;
        //    $(e.item)[0].innerHTML = "<span class=\"k-loading k-complete\"></span>" +
        //        "<span class=\"k-link\"style='font-size: 12px !important; padding:0 !important;'>" +
        //        "<div class='pwHomeInfoLogo'/>";
        //}
    }
}

function closeTab(id) {
    var el = document.getElementById(id).getAttribute('aria-controls').split('-')[1] - 1;

    var tabStrip = $("#tabstrip").data("kendoTabStrip");

    var tabDel = tabStrip.tabGroup.children("li").eq(el);

    tabStrip.remove(tabDel);

    var tab = tabStrip.select();
    if (tab.length == 0 && tabStrip.items().length > 0) {
        tab = tabStrip.tabGroup.children("li").eq(tabStrip.items().length - 1);
        tabStrip.select(tab);
    }
}

function fileEdit(id, fileName) {
   // alert(id + ' edit ' + fileName);
    $.ajax({
        type: 'POST',
        url: '/Upload/DeletePreview?documentId=' + id + '&fileName=' + fileName,
        success: function (result) {
          
        }
    });
    try {

        new ActiveXObject("SharePoint.OpenDocuments.4").EditDocument("http://192.168.0.162:8080/Attachments/" + id + "/" + fileName);
        return false;
    }
    catch (e) {
        try {
            document.getElementById("winFirefoxPlugin").EditDocument("http://192.168.0.162:8080/Attachments/" + id + "/" + fileName);
            return false;
        }
        catch (e2) {
            return true;
        }
    }
}
function fileDownload(id, fileName) {
    window.open('/Upload/Download?id=' + id + '&name=' + fileName);
    //alert(id + ' edit ' + fileName);

}
function fileView(id, fileName) {
    var window = $("#windowFile");
    window.kendoWindow({
        width: "800px",
        height: "500px",
        modal: true, resizable: false,
        title: fileName,
        actions: ["Pin", "Refresh", "Maximize", "Close"],
        content: "/Upload/FileView?id=" + id + '&name=' + encodeURIComponent(fileName)
    });
    window.data("kendoWindow").title(fileName);
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();

    // alert(id +' ' + fileName);
}


function printDocumentNew(code, name, mrtName, titleText) {
    var window = $("#windowFile");
    window.kendoWindow({
        width: "800px",
        height: "500px",
        modal: true, resizable: false,
        title: titleText,
        actions: ["Pin", "Refresh", "Maximize", "Close"],
        content: "/Upload/ReportFileView?code=" + code + '&name=' + name + '&report=' + mrtName
    });
    window.data("kendoWindow").title(titleText);
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}




function printDocument(code) {
    var window = $("#windowFile");
    window.kendoWindow({
        width: "800px",
        height: "500px",
        modal: true, resizable: false,
        title: 'Опись дел',
        actions: ["Pin", "Refresh", "Maximize", "Close"],
        content: "/Upload/ReportFileView?code=" + code + '&name=Опись&report=ArchivViewListNomenclature.mrt'
    });
    window.data("kendoWindow").title('Опись дел');
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}
function printDocumentTMC(code) {
    var window = $("#windowFile");
    window.kendoWindow({
        width: "800px",
        height: "500px",
        modal: true, resizable: false,
        title: 'Опись дел',
        actions: ["Pin", "Refresh", "Maximize", "Close"],
        content: "/Upload/ReportFileView?code=" + code + '&name=Форма&report=ApplicationFullDelivery.mrt'
    });
    window.data("kendoWindow").title('Форма');
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}
function printDocumentTMC2(code) {
    var window = $("#windowFile");
    window.kendoWindow({
        width: "800px",
        height: "500px",
        modal: true, resizable: false,
        title: 'Опись дел',
        actions: ["Pin", "Refresh", "Maximize", "Close"],
        content: "/Upload/ReportFileView?code=" + code + '&name=Форма&report=ApplicationPartDelivery.mrt'
    });
    window.data("kendoWindow").title('Форма');
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}

function printTmcReport(code, name, report, dateStart, dateEnd, departmentId) {
    var window = $("#windowFile");
    window.kendoWindow({
        width: "800px",
        height: "500px",
        modal: true, resizable: false,
        title: 'Отчет',
        actions: ["Pin", "Refresh", "Maximize", "Close"],
        content: "/Upload/ReportTmcView?code=" + code
            + "&name=" + name
            + "&report=" + report
            + "&dateStart=" + dateStart.toUTCString()
            + "&dateEnd=" + dateEnd.toUTCString()
            + "&departmentId=" + departmentId
    });
    window.data("kendoWindow").title('Форма');
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}



function substring(str) {
    var exts = str.split('.');
    var ext = exts[exts.length - 1];
    var str2 = '';
    for (var i = 0; i < exts.length - 1; i++) {
        str2 += exts[i];
    }
    if (str2.length > 60)
        return str2.substr(0, 60) + '... .' + ext;
    return str;
}

function bytesToSize(bytes) {
    var k = 1024;
    var sizes = ['Байт', 'КБ', 'МБ', 'ГБ', 'ТБ'];
    if (bytes === 0) return '0 Байт';
    var i = parseInt(Math.floor(Math.log(bytes) / Math.log(k)), 10);
    return (bytes / Math.pow(k, i)).toPrecision(3) + ' ' + sizes[i];
}
function PrintDocumetnt(id) {
    window.open('/ReportForm.aspx?Id=' + id + '&isPrint=1');
}

function DeleteDocumetnt(id) {
    var window = $("#TaskCommandWindow");
    window.kendoWindow({
        width: "450px",
        height: "auto",
        close: onCloseCommandWindow,
        modal: true, resizable: false,
        title: 'Удаление',
        actions: ["Close"]
    });
    window.data("kendoWindow").title('Удаление');
    window.data("kendoWindow").refresh('/Task/DeleteDoc?id=' + id);
    window.data("kendoWindow").setOptions({
        width: 450,
        height: 'auto'
    });
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}
function CardCommandWindowClosed() {
    $("#CardCommandWindow").data("kendoWindow").close();
}

function CardSaveSuccess() {
    var window = $("#CardCommandWindow");
    window.kendoWindow({
        width: "350px",
        height: "auto",
        modal: true, resizable: false,
        title: 'Сохранение',
        actions: ["Close"]
    });
    window.data("kendoWindow").title('Сохранение');
    window.data("kendoWindow").refresh('/Home/SaveCardSuccess');
    window.data("kendoWindow").setOptions({
        width: 350,
        height: 'auto'
    });
    
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}

function CardSaveError() {
    var window = $("#CardCommandWindow");
    window.kendoWindow({
        width: "350px",
        height: "auto",
        modal: true, resizable: false,
        title: 'Сохранение',
        actions: ["Close"]
    });
    window.data("kendoWindow").title('Ошибка');
    window.data("kendoWindow").refresh('/Home/SaveCardError');
    window.data("kendoWindow").setOptions({
        width: 350,
        height: 'auto'
    });

    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}

function CardReviewSuccess() {
    var window = $("#CardCommandWindow");
    window.kendoWindow({
        width: "350px",
        height: "auto",
        modal: true, resizable: false,
        title: 'Рассмотрение',
        actions: ["Close"]
    });
    window.data("kendoWindow").title('Рассмотрение');
    window.data("kendoWindow").setOptions({
        width: 350,
        height: 'auto'
    });
    window.data("kendoWindow").refresh('/Home/ReviewCardSuccess');
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}

function CardReview2Success() {
     var window = $("#CardCommandWindow");
    window.kendoWindow({
        width: "350px",
        height: "auto",
        modal: true, resizable: false,
        title: 'Согласование',
        actions: ["Close"]
    });
    window.data("kendoWindow").title('Согласование');
    window.data("kendoWindow").setOptions({
        width: 350,
        height: 'auto'
    });
    window.data("kendoWindow").refresh('/Home/Review2CardSuccess');
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}

function CardRejectSuccess() {
      var window = $("#CardCommandWindow");
    window.kendoWindow({
        width: "350px",
        height: "auto",
        modal: true, resizable: false,
        title: 'Отзыв',
        actions: ["Close"]
    });
    window.data("kendoWindow").title('Отзыв');
    window.data("kendoWindow").setOptions({
        width: 350,
        height: 'auto'
    });
    window.data("kendoWindow").refresh('/Home/RejectCardSuccess');
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}

function CardBuildSuccess() {
     var window = $("#CardCommandWindow");
    window.kendoWindow({
        width: "450px",
        height: "auto",
        modal: true, resizable: false,
        title: 'Генерация файла',
        actions: ["Close"]
    });
    window.data("kendoWindow").title('Генерация файла');
    window.data("kendoWindow").setOptions({
        width: 450,
        height: 'auto'
    });
    window.data("kendoWindow").refresh('/Home/BuildCardSuccess');
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}

function CardExcludeSuccess() {
    var window = $("#CardCommandWindow");
    window.kendoWindow({
        width: "350px",
        height: "auto",
        modal: true, resizable: false,
        title: 'Исполнение',
        actions: ["Close"]
    });
    window.data("kendoWindow").title('Исполнение');
    window.data("kendoWindow").setOptions({
        width: 350,
        height: 'auto',
    });
    window.data("kendoWindow").refresh('/Home/ExcludeCardSuccess');
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}

function CardRegisterSuccess(number, documentDate) {
     var window = $("#CardCommandWindow");
    window.kendoWindow({
        width: "450px",
        height: "auto",
        modal: true, resizable: false,
        title: 'Регистрация',
        actions: ["Close"]
    });
    window.data("kendoWindow").title('Регистрация');
    window.data("kendoWindow").setOptions({
        width: 450,
        height: 'auto'
    });
    window.data("kendoWindow").refresh('/Home/RegisterCardSuccess?number=' + number + '&documentDate=' + documentDate);
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}

function taskCountClick(type, header) {
    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var url = '';

    if (type == 'New') {
        url = "/Task/Index?type=New";
    }

    if (type == 'Done') {
        url = "/Task/Index?type=Done";
    }

    if (type == 'Expired') {
        url = "/Task/Index?type=Expired";
    }
    if (type == 'InWork') {
        url = "/Task/Index?type=InWork";
    }
    if (type == 'InQueueContracts') {
        url = "/Contract/Contract?filterId=inQueueContracts";
    }
    if (type == 'InQueueContractAdditions') {
        url = "/Contract/ContractAddition?filterId=inQueueContracts";
    }
    if (type == 'NewContracts') {
        url = "/Contract/Contract?filterId=inWorkContracts";
    }
    if (type == 'NewContractAdditions') {
        url = "/Contract/ContractAddition?filterId=inWorkContracts";
    }
    if (type == 'InWorkContracts') {
        url = "/Contract/Contract?filterId=correctedContracts";
    }
    if (type == 'InWorkeContractAdditions') {
        url = "/Contract/ContractAddition?filterId=correctedContracts";
    }
    if (type == 'OnAgreementContracts') {
        url = "/Contract/Contract?filterId=onAgreementContracts";
    }
    if (type == 'OnAgreementContractAdditions') {
        url = "/Contract/ContractAddition?filterId=onAgreementContracts";
    }   
    if (type == 'ApprovedContracts') {
        url = "/Contract/Contract?filterId=approvedContracts";
    }
    if (type == 'ApprovedContractAdditions') {
        url = "/Contract/ContractAddition?filterId=approvedContracts";
    }
    if (type == 'OnRegistrationContracts') {
        url = "/Contract/Contract?filterId=onRegistrationContracts";
    }
    if (type == 'OnRegistrationContractAdditions') {
        url = "/Contract/ContractAddition?filterId=onRegistrationContracts";
    }
    
    var name = guidGen();
    tabStrip.append({
        text: header,
        content: '<div id="' + name + '"> </div>'

    });

    tabStrip.select(tabStrip.items().length - 1);

    var gridElement = $("#" + name);

    gridElement.height($(window).height() - 100);

    $.ajax({
        url: url,
        //type: "POST",
        success: function (result) {
            // refreshes partial view
            $('#' + name).html(result);
        }
    });
}

function tagAnswerClick(id, header) {
    docArray.push(id.toLowerCase());
    var element = document.getElementById(id);
    if (element == null) {
        var tabStrip = $("#tabstrip").data("kendoTabStrip");
        tabStrip.append({
            text: header,
            content: '<div id="' + id + '"> </div>'

        });

        tabStrip.select(tabStrip.items().length - 1);

        var gridElement = $("#" + id);

        gridElement.height($(window).height() - 100);

        $.ajax({
            url: 'Reference/OpenDocument?id=' + id,
            //type: "POST",
            success: function(result) {
                // refreshes partial view
                $('#' + id).html(result);
            }
        });
    } else {

        var itesm = $('#' + id)[0].parentElement.getAttribute('id').split('-')[1];
        if (itesm) {
            $("#tabstrip").data("kendoTabStrip").select(itesm - 1);
        }
    }
}

function ProfileView() {
    var window = $("#ProfileCommandWindow");
    window.kendoWindow({
        width: "500px",
        height: "263px",
        modal: true, resizable: false,
        title: 'Профиль',
        actions: ["Close"],
        content: "/Account/Profile"
    });
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}

function SetReadonlyDoc(id, isEditDoc) {
    if (isEditDoc === 'True') {
        var attachReadonly = document.getElementById('attachReadonly' + id);
        if (attachReadonly != null)
            attachReadonly.remove();
        return;
    }

    //if (docArray.indexOf(id.toLowerCase()) == -1) {
    //    var attachReadonly2 = document.getElementById('attachReadonly' + id);
    //    if (attachReadonly2 != null)
    //        attachReadonly2.remove();
    //    return;
    //}

    if (isEditDoc === 'False') {
        var docToolbar = document.getElementById('docToolbar' + id);
        if (docToolbar != null)
            docToolbar.remove();

        var taskToolbar = document.getElementById('taskToolbar' + id);
        if (taskToolbar != null)
            taskToolbar.remove();

        var attachEdit = document.getElementById('attachEdit' + id);
        if (attachEdit != null)
            attachEdit.remove();
    }
}

var docArray = new Array();

function InitializeStatusBar(id, viewModel) {
    document.getElementById("pwStatusbarState" + id).innerHTML = viewModel.get('document.StateTypeValue');
}

function GenerationStampOK(id, fileName) {
    $.ajax({
        type: 'get',
        url: '/Upload/GenerationStamp?id=' + id + '&name=' + fileName,
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            var window = $("#StampCommandWindow");
            window.data("kendoWindow").close();
        },
        complete: function () {
        }
    });
}

function GenerationStampClosed() {
    var window = $("#StampCommandWindow");
    window.data("kendoWindow").close();
}
function GenerationStamp(id, fileName) {
    var window = $("#StampCommandWindow");
    window.kendoWindow({
        width: "500px",
        height: "140",
        modal: true, resizable: false,
        title: 'Поставить печать',
        actions: ["Close"],
        content: "/Home/GenerationStamp?id=" + id + '&fileName=' + fileName
    });
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
      
}


function Valid(data) {

    if (data.IsValid) {
        ValidTrue(data.Text);
    } else {
        ValidFalse(data.Text);
    }
}

function CloseValidWindow() {
    $('#validWindow').data('kendoWindow').close();
}

function ValidTrue(text) {
    var window = $("#validWindow");
    window.kendoWindow({
        title: "Уведомление",
        width: "350px",
        height: "120px",
        modal: true, resizable: false,
        actions: ["Close"],
    });

    text = "<div id='pwValidWindowContent'>" + text + "</div>" +
           "<div id='pwValidWindowButtons'><button class='k-button' onclick='CloseValidWindow()'>OK</button></div>";

    window.data("kendoWindow").content(text);
    window.data("kendoWindow").setOptions({
        width: "350px",
        height: "120px",
    });
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}
function ValidFalse(text) {
    var window = $("#validWindow");
    window.kendoWindow({
        title: "Ошибка",
        width: "550px",
        height: "370px",
        modal: true, resizable: false,
        actions: ["Close"],
    });

    text = "<div id='pwInvalidWindowContent'>Сведения об ошибках:</div>" +
           "<div id='pwInvalidWindowText'>" + text + "</div>" +
           "<div id='pwInvalidWindowButtons'><button class='k-button' onclick='CloseValidWindow()'>OK</button></div>";

    window.data("kendoWindow").content(text);
    window.data("kendoWindow").setOptions({
        width: "550px",
        height: "370px",
    });
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}

function dataBoundPriceMainList() {
    var dataView = this.dataSource.view();
    for (var i = 0; i < dataView.length; i++) {
        if (dataView[i].CountryName == 'Венгрия' || dataView[i].CountryName == 'Беларусь' || dataView[i].CountryName == 'Латвия' || dataView[i].CountryName == 'Чехия') {
            var uid = dataView[i].uid;
            $("#" + this.element[0].getAttribute("Id") + " tbody").find("tr[data-uid=" + uid + "]").addClass("customClass2");
        }
    }

}

function initCustomColumnMenu(gridId, menuHolderId, gridName, employeeId) {
    var gridColumnSettings = {
        gridId: gridId,
        gridName: gridName,
        employeeId: employeeId
    };
    var columns = null;
    $("#" + gridId).data("kendoGrid").gridColumnCustomSettings = gridColumnSettings;
    $.ajax({
        type: 'GET',
        url: '/Grid/Settings?gridName=' + gridName + "&employeeId=" + employeeId,
        success: function (result) {
            if (result)
                columns = result.Columns;
        },
        complete: function () {
            buildColumnMenu(gridId, menuHolderId, columns);
        }
    });
    function buildColumnMenu(gridId, menuHolderId, columns) {
        
        var grid = $("#" + gridId).data("kendoGrid");
        var ds = [];
        if (columns) {
            for (var i = 0, max = columns.length; i < max; i++) {
                var column = columns[i];
                var gridColumn = grid.columns.find(function (e) {
                    return e.field == column.Field;
                });
                if (gridColumn) {
                    if (column.Hidden) {
                        grid.hideColumn(column.Field);
                    } else {
                        grid.showColumn(column.Field);
                    }
                }
            }
        }
        for (var i = 0, max = grid.columns.length; i < max; i++) {
            if ($("#" + gridId + ' .k-header[data-field="' + grid.columns[i].field + '"]').hasClass('not-visible-custom-column')) continue;
            ds.push({
                encoded: false,
                text: "<label><input type='checkbox' checked='checked' " +
                    " class='check' data-field='" + grid.columns[i].field + "' data-title='" + grid.columns[i].title +
                    "'/>" + grid.columns[i].title + "</label>"
            });
        }
        $("#" + menuHolderId).kendoMenu({
            dataSource: [{
                text: "Столбцы",
                items: ds
            }],
            openOnClick: true,
            closeOnClick: false,
            open: function () {
                
                var selector;
                $.each(grid.columns, function () {
                    if (this.hidden) {
                        selector = "input[data-field='" + this.field + "']";
                        $(selector).prop("checked", false);
                    }
                });
            },
            select: function (e) {
                if ($(e.item).parent().filter("div").length) return;
                
                var input = $(e.item).find("input.check");
                var field = $(input).data("field");
                if ($(input).is(":checked")) {
                    grid.showColumn(field);
                } else {
                    grid.hideColumn(field);
                }
                saveSettings(gridId);
            }
        });
    }
    function saveSettings(gridId) {
        var grid = $("#" + gridId).data("kendoGrid");
        var settings = grid.gridColumnCustomSettings;
        settings.columnModel = {
            Columns: []
        };
        for (var i = 0, max = grid.columns.length; i < max; i++) {
            settings.columnModel.Columns.push({
                Field: grid.columns[i].field,
                Title: grid.columns[i].title,
                Hidden: grid.columns[i].hidden ? grid.columns[i].hidden : false
            });
        }
        var json = JSON.stringify(settings);
        $.ajax({
            type: 'POST',
            url: '/Grid/Settings',
            contentType: 'application/json; charset=utf-8',
            data: json,
            success: function (result) {
                
            },
            complete: function () {
                
            }
        });
    }

}