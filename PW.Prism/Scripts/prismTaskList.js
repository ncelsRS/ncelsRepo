

function panelTaskSelect(e) {
    var selectType = $(e.item).find("> .k-link").attr('ItemType');

    if (selectType != null) {
        var selectValue = $(e.item).find("> .k-link").attr('ItemId');
        var gridId = $(e.item).find("> .k-link").attr('ModelId');
        var filter = new Array();
        var grid = $("#gridTaskList" + gridId).data("kendoGrid");
        if (selectValue == '') {
            grid.dataSource.filter([]);
        } else {


            if (selectType == "Status") {
                filter.push({ field: "State", operator: "eq", value: selectValue });
                if (selectValue == 0) {
                    filter.push({ field: "State", operator: "eq", value: 4 });
                }
                grid.dataSource.filter({
                    logic: "or",
                    filters: filter
                });
            }
            if (selectType == "TypeEx") {
                filter.push({ field: "TypeEx", operator: "eq", value: selectValue });
                grid.dataSource.filter({
                    logic: "or",
                    filters: filter
                });
            }
            if (selectType == "Monitoring") {
                filter.push({ field: "Type", operator: "eq", value: selectValue });
                filter.push({ field: "TypeEx", operator: "eq", value: 0 });
                grid.dataSource.filter({
                    logic: "and",
                    filters: filter
                });
            }

        }

        //if (selectValue == '') {
        //    grid.dataSource.filter([]);
        //} else {

        //}
    }
}

function onExportTaskDoc(e) {

    window.open('/Task/ExportFile');
}

function InitFilterTaskGrid(name) {
    $("#reload" + name).click(function (e) {
        var grid = $("#gridTaskList" + name).data("kendoGrid");
        grid.dataSource.read();
    });

    $("#find" + name).click(function (e) {

        var text = $("#findText" + name).val();
        var findType = $("#findType" + name).val();
        var grid = $("#gridTaskList" + name).data("kendoGrid");
        if (text != '') {
            $filter = new Array();
            if (findType == 0) {

                $filter.push({ field: "Author", operator: "contains", value: text });
                $filter.push({ field: "DocumentNumber", operator: "contains", value: text });
                $filter.push({ field: "ResponsibleValue", operator: "contains", value: text });
                $filter.push({ field: "CorrespondentsValue", operator: "contains", value: text });
                $filter.push({ field: "Executor", operator: "contains", value: text });
                $filter.push({ field: "Number", operator: "contains", value: text });
                $filter.push({ field: "Text", operator: "contains", value: text });

                $filter.push({ field: "Summary", operator: "contains", value: text });
                $filter.push({ field: "OutgoingNumber", operator: "contains", value: text });

            }
            if (findType == 1) {
                $filter.push({ field: "Number", operator: "contains", value: text });
            }
            if (findType == 2) {
                $filter.push({ field: "DocumentNumber", operator: "contains", value: text });
            }
            if (findType == 3) {
                $filter.push({ field: "Author", operator: "contains", value: text });
            }
            if (findType == 4) {
                $filter.push({ field: "Summary", operator: "contains", value: text });
            }
            if (findType == 5) {
                $filter.push({ field: "OutgoingNumber", operator: "contains", value: text });
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
            var grid = $("#gridTaskList" + name).data("kendoGrid");
            grid.dataSource.read();
        }
    });
}

function corsubstring(data) {
    return data.substring(0, 15);
}
function onCloseCommandWindow(e) {
    var window = $("#TaskCommandWindow");
    window.data("kendoWindow").content('');
}

function MonitoringFilterTask(element) {

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
function TypeFilterTask(element) {

    var dataType = [
 // { text: "В работу", value: "0" },
  { text: "Рассмотрение", value: "0" },
  { text: "Резолюция", value: "1" },
  { text: "Перепоручение", value: "2" },
  { text: "Согласование", value: "3" },
  { text: "На регистрацию", value: "4" }
    ];
    element.kendoDropDownList({
        dataTextField: "text",
        dataValueField: "value",
        dataSource: dataType,
        optionLabel: "--Выберите значения--"
    });
}
function StateFilterTask(element) {

    var dataType = [
 // { text: "В работу", value: "0" },
  { text: "Новый", value: "0" },
  { text: "В работе", value: "1" },
  { text: "Исполненный положительно", value: "2" },
  { text: "Исполненный отрицательно", value: "3" },
  { text: "Принять на исполнение", value: "4" }
    ];
    element.kendoDropDownList({
        dataTextField: "text",
        dataValueField: "value",
        dataSource: dataType,
        optionLabel: "--Выберите значения--"
    });
}
function taskOnJob(e) {
    var window = $("#TaskCommandWindow");
    window.kendoWindow({
        width: "450px",
        height: "auto",
        modal: true, resizable: false,
        close: onCloseCommandWindow,
        title: 'Принять в работу',
        actions: ["Close"],
    });


    window.data("kendoWindow").title('Принять в работу');
    window.data("kendoWindow").setOptions({
        width: 450,
        height: 'auto'
    });
    window.data("kendoWindow").refresh('/Task/Job?taskId=' + e);
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
   
}
function taskOnRejectEdit(e) {
    var window = $("#TaskCommandWindow");
    window.kendoWindow({
        width: "450px",
        height: "auto",
        modal: true, resizable: false,
        close: onCloseCommandWindow,
        title: 'Отредактировать',
        actions: ["Close"],
    });


    window.data("kendoWindow").title('Отредактировать');
    window.data("kendoWindow").setOptions({
        width: 450,
        height: 'auto'
    });
    window.data("kendoWindow").refresh('/Task/JobEdit?taskId=' + e);
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
   
}
function taskOnResolution(e) {
    var window = $("#TaskCommandWindow");
    window.kendoWindow({
        width: "550px",
        height: "auto",
        modal: true, resizable: false,
        close: onCloseCommandWindow,
        title: 'Распределение',
        actions: ["Close"],
        content: '/Task/Resolution?taskId=' + e
    });

    window.data("kendoWindow").title('Распределение');
    window.data("kendoWindow").setOptions({
        width: 550,
        height: 'auto'
    });
    window.data("kendoWindow").refresh('/Task/Resolution?taskId=' + e);

    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}
function taskOnResolutionDoc(id) {
    var window = $("#TaskCommandWindow");
    window.kendoWindow({
        width: "550px",
        height: "auto",
        modal: true, resizable: false,
        close: onCloseCommandWindow,
        title: 'Резолюция',
        actions: ["Close"]
    });

    window.data("kendoWindow").title('Резолюция');
    window.data("kendoWindow").setOptions({
        width: 550,
        height: 'auto'
    });
    window.data("kendoWindow").refresh('/Task/ResolutionDoc?taskId=' + id);

    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}
function taskOnAgreementDoc(id) {
    var window = $("#TaskCommandWindow");
    window.kendoWindow({
        width: "550px",
        height: "auto",
        modal: true,
        close: onCloseCommandWindow,
        title: 'Согласование',
        actions: ["Close"]
    });

    window.data("kendoWindow").title('Согласование');
    window.data("kendoWindow").setOptions({
        width: 550,
        height: 'auto'
    });
    window.data("kendoWindow").refresh('/Task/AgreementDoc?documentId=' + id);

    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}
function taskOnSingDoc(id) {
    var window = $("#TaskCommandWindow");
    window.kendoWindow({
        width: "550px",
        height: "auto",
        modal: true,
        close: onCloseCommandWindow,
        title: 'Подписание',
        actions: ["Close"]
    });

    window.data("kendoWindow").title('Подписание');
    window.data("kendoWindow").setOptions({
        width: 550,
        height: 'auto'
    });
    window.data("kendoWindow").refresh('/Task/SigningDoc?documentId=' + id);

    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}
function taskOnReassignment(e) {
    var window = $("#TaskCommandWindow");
    window.kendoWindow({
        width: "550px",
        height: "auto",
        close: onCloseCommandWindow,
        modal: true, resizable: false,
        title: 'Поручение',
        actions: ["Close"],
        content: '/Task/Reassignment?taskId=' + e
    });
    window.data("kendoWindow").title('Поручение');
    window.data("kendoWindow").refresh('/Task/Reassignment?taskId=' + e);
    window.data("kendoWindow").setOptions({
        width: 550,
        height: 'auto'
    });
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}
function taskOnReassignmentRef(e) {
    var window = $("#TaskCommandWindow");
    window.kendoWindow({
        width: "550px",
        height: "auto",
        close: onCloseCommandWindow,
        modal: true, resizable: false,
        title: 'Поручение',
        actions: ["Close"],
        content: '/Task/ReassignmentRef?taskId=' + e
    });
    window.data("kendoWindow").title('Поручение');
    window.data("kendoWindow").refresh('/Task/ReassignmentRef?taskId=' + e);
    window.data("kendoWindow").setOptions({
        width: 550,
        height: 'auto'
    });
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}
function taskOnReassignmentDoc(id) {
    var window = $("#TaskCommandWindow");
    window.kendoWindow({
        width: "550px",
        height: "auto",
        close: onCloseCommandWindow,
        modal: true, resizable: false,
        title: 'Поручение',
        actions: ["Close"]
    });
    window.data("kendoWindow").title('Поручение');
    window.data("kendoWindow").refresh('/Task/ReassignmentDoc?taskId=' + id);
    window.data("kendoWindow").setOptions({
        width: 550,
        height: 'auto'
    });
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}
function taskOnAgreement(e) {
    var window = $("#TaskCommandWindow");
    window.kendoWindow({
        width: "550px",
        height: "auto",
        close: onCloseCommandWindow,
        modal: true, resizable: false,
        title: 'Согласовать',
        actions: ["Close"],
        content: '/Task/Agreement?taskId=' + e
    });
    window.data("kendoWindow").title('Согласовать');
    window.data("kendoWindow").refresh('/Task/Agreement?taskId=' + e);
    window.data("kendoWindow").setOptions({
        width: 550,
        height: 'auto'
    });
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}
function taskOnAddAgreement(e) {
    var window = $("#TaskCommandWindow");
    window.kendoWindow({
        width: "550px",
        height: "auto",
        close: onCloseCommandWindow,
        modal: true, resizable: false,
        title: 'Добавить согласование',
        actions: ["Close"],
        content: '/Task/AddAgreement?taskId=' + e
    });
    window.data("kendoWindow").title('Добавить согласование');
    window.data("kendoWindow").refresh('/Task/AddAgreement?taskId=' + e);
    window.data("kendoWindow").setOptions({
        width: 550,
        height: 'auto'
    });
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}
function taskOnAgreement1(e) {
    var window = $("#TaskCommandWindow");
    window.kendoWindow({
        width: "550px",
        height: "auto",
        close: onCloseCommandWindow,
        modal: true, resizable: false,
        title: 'Переревести',
        actions: ["Close"],
        content: '/Task/Agreement?taskId=' + e
    });
    window.data("kendoWindow").title('Переревести');
    window.data("kendoWindow").refresh('/Task/Agreement?taskId=' + e);
    window.data("kendoWindow").setOptions({
        width: 550,
        height: 'auto'
    });
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}
function taskOnSing(e) {
    var window = $("#TaskCommandWindow");
    window.kendoWindow({
        width: "550px",
        height: "auto",
        close: onCloseCommandWindow,
        modal: true, resizable: false,
        title: 'Отправить',
        actions: ["Close"],
        content: '/Task/Signing?taskId=' + e
    });
    window.data("kendoWindow").title('Отправить');
    window.data("kendoWindow").refresh('/Task/Signing?taskId=' + e);
    window.data("kendoWindow").setOptions({
        width: 550,
        height: 'auto'
    });
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}
function taskOnTranslate(e) {
    var window = $("#TaskCommandWindow");
    window.kendoWindow({
        width: "550px",
        height: "auto",
        close: onCloseCommandWindow,
        modal: true, resizable: false,
        title: 'Отправить на перевод',
        actions: ["Close"],
        content: '/Task/Translate?taskId=' + e
    });
    window.data("kendoWindow").title('Отправить на перевод');
    window.data("kendoWindow").refresh('/Task/Translate?taskId=' + e);
    window.data("kendoWindow").setOptions({
        width: 550,
        height: 'auto'
    });
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}

function taskOnExclude(e) {
    var window = $("#TaskCommandWindow");
    window.kendoWindow({
        width: "550px",
        height: "auto",
        close: onCloseCommandWindow,
        modal: true, resizable: false,
        title: 'Исполнение',
        actions: ["Close"],
        content: '/Task/Exclude?taskId=' + e
    });
    window.data("kendoWindow").title('Исполнение');
    window.data("kendoWindow").refresh('/Task/Exclude?taskId=' + e);
    window.data("kendoWindow").setOptions({
        width: 550,
        height: 'auto'
    });
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}

function taskOnExcludeTask(e) {
    var window = $("#TaskCommandWindow");
    window.kendoWindow({
        width: "550px",
        height: "auto",
        close: onCloseCommandWindow,
        modal: true, resizable: false,
        title: 'Исполнение',
        actions: ["Close"],
        content: '/Task/ExcludeTask?taskId=' + e
    });
    window.data("kendoWindow").title('Исполнение');
    window.data("kendoWindow").refresh('/Task/ExcludeTask?taskId=' + e);
    window.data("kendoWindow").setOptions({
        width: 550,
        height: 'auto'
    });
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}
function taskOnExcludeCommentTask(e) {
    var window = $("#TaskCommandWindow");
    window.kendoWindow({
        width: "550px",
        height: "auto",
        close: onCloseCommandWindow,
        modal: true, resizable: false,
        title: 'Исполнение',
        actions: ["Close"],
        content: '/Task/ExcludeTaskComment?taskId=' + e
    });
    window.data("kendoWindow").title('Исполнение');
    window.data("kendoWindow").refresh('/Task/ExcludeTaskComment?taskId=' + e);
    window.data("kendoWindow").setOptions({
        width: 550,
        height: 'auto'
    });
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}
function taskOnExcludeDoc(id) {
    var window = $("#TaskCommandWindow");
    window.kendoWindow({
        width: "550px",
        height: "auto",
        close: onCloseCommandWindow,
        modal: true, resizable: false,
        title: 'Исполнение',
        actions: ["Close"]
    });
    window.data("kendoWindow").title('Исполнение');
    window.data("kendoWindow").refresh('/Task/ExcludeDoc?taskId=' + id);
    window.data("kendoWindow").setOptions({
        width: 550,
        height: 'auto'
    });
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}
function taskEditDoc(id,taskId) {
    var window = $("#TaskCommandWindow");
    window.kendoWindow({
        width: "550px",
        height: "auto",
        close: onCloseCommandWindow,
        modal: true, resizable: false,
        title: 'Редактировние',
        actions: ["Close"]
    });
    window.data("kendoWindow").title('Редактировние');
    window.data("kendoWindow").refresh('/Task/ReassignmentEditDoc?actionId=' + id);
    window.data("kendoWindow").setOptions({
        width: 550,
        height: 'auto'
    });
    window.data("kendoWindow").taskId = taskId;
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}
function taskAgreementEditDoc(id) {
    var window = $("#TaskCommandWindow");
    window.kendoWindow({
        width: "550px",
        height: "auto",
        close: onCloseCommandWindow,
        modal: true, resizable: false,
        title: 'Редактировние',
        actions: ["Close"]
    });
    window.data("kendoWindow").title('Редактировние');
    window.data("kendoWindow").refresh('/Task/AgreementEditDoc?actionId=' + id);
    window.data("kendoWindow").setOptions({
        width: 550,
        height: 'auto'
    });
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}
function taskDeleteDoc(id,taskId) {
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
    window.data("kendoWindow").refresh('/Task/Delete?actionId=' + id);
    window.data("kendoWindow").setOptions({
        width: 450,
        height: 'auto'
    });
    window.data("kendoWindow").taskId = taskId;
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}
function taskOnReject(e) {
    var window = $("#TaskCommandWindow");
    window.kendoWindow({
        width: "550px",
        height: "auto",
        close: onCloseCommandWindow,
        modal: true, resizable: false,
        title: 'Отказать',
        actions: ["Close"],
        content: '/Task/Reject?taskId=' + e
    });
    window.data("kendoWindow").title('Отказать');
    window.data("kendoWindow").refresh('/Task/Reject?taskId=' + e);
    window.data("kendoWindow").setOptions({
        width: 550,
        height: 'auto'
    });
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}

function taskOnProject(e) {
    var window = $("#TaskCommandWindow");
    window.kendoWindow({
        width: "300px",
        height: "auto",
        close: onCloseCommandWindow,
        modal: true, resizable: false,
        title: 'Проект',
        actions: ["Close"],
        content: '<div>' + e.sender.element.attr('taskId') + '</div>'
    });
    window.data("kendoWindow").title('Проект');
    window.data("kendoWindow").setOptions({
        width: 300,
        height: 'auto'
    });
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}

function taskActionError() {
    $("#TaskCommandWindow").data("kendoWindow").close();
    var window = $("#TaskCommandWindowError");
    window.kendoWindow({
        width: "450px",
        height: "auto",
        modal: true, resizable: false,
        actions: ["Close"],
    });
    window.data("kendoWindow").title('Предупреждение');
    window.data("kendoWindow").refresh('/Home/TaskActionError');
    window.data("kendoWindow").setOptions({
        width: 450,
        height: 'auto'
    });
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}

function WindowWarningClose() {
    $("#TaskCommandWindowError").data("kendoWindow").close();
}
function dataBoundTaskList() {
    var dataView = this.dataSource.view();
    for(var i = 0; i < dataView.length; i++) {
        if(dataView[i].State == 0) {
            var uid = dataView[i].uid;

            $("#"+ this.element[0].getAttribute("Id")  +" tbody").find("tr[data-uid=" + uid + "]").addClass("customClass");
        }
    }
 
}



function DownloadAllFile(name) {
    window.open('/Task/DownloadAllFile?id=' + name);
}
function DetailExpandTaskList(e) {
    //this._rowHeight = 0;
    //this.virtualScrollable.refresh();
    e.masterRow.removeClass("customClass");
}
function DetailCollapseExpandTaskList(e) {
    this.virtualScrollable.refresh();
}
function ActionTaskEnable(stateTask, stateDocument, typeTask, monitoringDocument, taskId) {

    if (typeTask == 0) {
        
        if (stateTask == 4) {
            $("#ButtonResolution_" + taskId).prop('disabled', false);
        } else {
            $("#ButtonResolution_" + taskId).prop('disabled', true);
        }
    }

    if (typeTask == 1 || typeTask == 2) {
        if (stateTask == 1 || stateTask == 4 || stateTask == 3) {
            $("#ButtonReasigment_" + taskId).prop('disabled', false);
        } else {
            $("#ButtonReasigment_" + taskId).prop('disabled', true);
        }
    }

    if (typeTask == 3) {
        if ( stateTask == 4) {
            $("#ButtonAgreement_" + taskId).prop('disabled', false);
        } else {
            $("#ButtonAgreement_" + taskId).prop('disabled', true);
        }
    }

    if (typeTask == 4) {
        if ( stateTask == 4) {
            $("#ButtonSigning_" + taskId).prop('disabled', false);
        } else {
            $("#ButtonSigning_" + taskId).prop('disabled', true);
        }
    }

    if (typeTask == 0 || typeTask == 1 || typeTask == 2) {
        if ((stateTask == 1 || stateTask == 4 || stateTask == 3) && (monitoringDocument != 2 && monitoringDocument != 3 && monitoringDocument!=4) ) {
            $("#ButtonExecuteTrue_" + taskId).prop('disabled', false);
        } else {
            $("#ButtonExecuteTrue_" + taskId).prop('disabled', true);
        }
    }

    if (typeTask == 3 || typeTask == 4) {
        if (stateTask == 1 || stateTask == 4 ) {
            $("#ButtonExecuteFalse_" + taskId).prop('disabled', false);
        } else {
            $("#ButtonExecuteFalse_" + taskId).prop('disabled', true);
        }
    }
 
    if (typeTask == 0 || typeTask == 1 || typeTask == 2) {
        if (stateTask == 1 || stateTask == 4 || stateTask == 3) {
            $("#ButtonCreateProject_" + taskId).prop('disabled', false);
        } else {
            $("#ButtonCreateProject_" + taskId).prop('disabled', true);
        }
    }
}