
function attachFile(id) {
    var url = "/DrugDeclaration/ShowAttach?recordId=" + id;
    $("<div style=" + '"' + "text-align: center;" + '"' + "><img src=" + '"' + "../../content/img/spinner.gif" + '"' + " style=" + '"' + "display: block; margin: 0 auto;" + '"' + " /></br>" + "....</div>")
      .addClass("dialog")
        .attr("id", $(this)
            .attr("data-dialog-id"))
        .appendTo("body")
       .dialog({
           title: "Файлы",
           closeText: "x",
           open: function (event, ui) {
               $(event.target).parent().css('position', 'fixed');
               $(event.target).parent().css('top', '150px');
               $(event.target).parent().css('center');
               $(this).closest(".ui-dialog")
 .find(".ui-dialog-titlebar-close")
//      .removeClass("ui-dialog-titlebar-close")
 .html("<span class='ui-button-icon-primary ui-icon ui-icon-closethick'></span>");


           },
           close: function () { $(this).remove(); },
           width: 800,
           height: 400,
           modal: true,
           //   open: function(event, ui) { $(".ui-dialog-titlebar-close").text(''); },
           buttons: {
           }
       })
      .load(url);
}
function createMailRemark(modelId) {
    var success = function () {
        window.Showbusy(event);
        $.ajax({
            type: "POST",
            url: "/DrugDeclaration/CreateMailRemark",
            data: { 'id': modelId },
            dataType: 'json',
            cache: false,
            success: function (data) {
                if (data.stageType === 1) //Первичная
                {
                    $.ajax({
                        url: "/DrugPrimary/PrimaryPageView?Id=" + modelId,
                        success: function(result) {
                            $("#page" + modelId).html(result);
                            $("#loading").hide();
                        }
                    });
                }
                if (data.stageType === 2) //ФМЦ
                {
                    $.ajax({
                        url: "/Pharmaceutical/PharmaceuticalPageView?Id=" + modelId,
                        success: function (result) {
                            $("#page" + modelId).html(result);
                            $("#loading").hide();
                        }
                    });
                }
                if (data.stageType === 3) //ФМК
                {
                    $.ajax({
                        url: "/Pharmacological/PharmacologicalPageView?Id=" + modelId,
                        success: function (result) {
                            $("#page" + modelId).html(result);
                            $("#loading").hide();
                        }
                    });
                }
                if (data.stageType === 4) //Аналитическая экспертиза
                {
                    $.ajax({
                        url: "/DrugAnalitic/AnaliticPageView?Id=" + modelId,
                        success: function (result) {
                            $("#page" + modelId).html(result);
                            $("#loading").hide();
                        }
                    });
                }
                if (data.stageType === 5) //перевод
                {
                    $.ajax({
                        url: "/Translate/TranslatePageView?Id=" + modelId,
                        success: function (result) {
                            $("#page" + modelId).html(result);
                            $("#loading").hide();
                        }
                    });
                }
                if (data.stageType === 8) //Заключение о безопастности
                {
                    $.ajax({
                        url: "/Safetyreport/SafetyReportPageView?Id=" + modelId,
                        success: function (result) {
                            $("#page" + modelId).html(result);
                            $("#loading").hide();
                        }
                    });
                }
                //readOnlyRemark();
            },
            error: function (data) {
                alert("1Error" + data);
            }
        });
    }
    var cancel = function () {
    };
    showConfirmation("Подтверждение", "Вы уверены что хотите сформировать письмо?", success, cancel);
}
function readOnlyRemark() {
    $(".remark").each(function () {
        if ($(this).is("select")) {
            $(this).attr('disabled', 'disabled');
        } else
            if ($(this).is(":checkbox")) {
                $(this).prop("readonly", true);
            } else {
                $(this).prop("readonly", true);
            }
    });
}

function changeRemark(control) {
    changeFieldList(control, "remark");
}

var currnetGrowControl;
function growthControl(control) {
    $(control).focusout(function () {
        $(control).height(20);
    });
    $(control).focus(function () {
        $(this).animate({ height: "200px" }, 500);
    });

}

function DeleteRecord(code, recordId) {
    $.ajax({
        type: "POST",
        url: "/DrugDeclaration/DeleteRecord",
        data: { 'code': code, 'recordId': recordId },
        dataType: 'json',
        cache: false,
        success: function (data) {
        },
        error: function (data) {
            alert("1Error" + data);
        }
    });
}

function checkValidateion(type, fieldValue, fieldId) {
    if (type === "float") {
        if (fieldValue != null && fieldValue.length > 0) {
            fieldValue = replaceAll(' ', '', fieldValue);
            if (fieldValue.indexOf(',') > 0) {
                fieldValue = fieldValue.replace(',', '.');
            }
            if (fieldValue !== '' && !$.isNumeric(fieldValue)) {
                showWarning('Поле должно содержат числовое значение');
                $('#' + fieldId).val("");
                return false;
            }
            if (fieldValue.indexOf('-') > -1) {
                showWarning('Введите число без знака минус');
                $('#' + fieldId).val("");
                return false;
            }
        }
    }
    if (type === "long") {
        if (fieldValue != null && fieldValue.length > 0) {
            if (fieldValue.indexOf(',') > 0 || fieldValue.indexOf('.') > 0) {
                showWarning("Введите целое число");
                $('#' + fieldId).val("");
                return false;
            }
            if (fieldValue !== '' && !$.isNumeric(fieldValue)) {
                showWarning('Поле должно содержат числовое значение');
                $('#' + fieldId).val("");
                return false;
            }
            if (fieldValue.indexOf('-') > -1) {
                showWarning('Введите число без знака минус');
                $('#' + fieldId).val("");
                return false;
            }
        }
    }
    return true;
}

function changeDosageCollection(control, code) {
    $(control).change(function () {
       
        var fieldId = $(this).attr('id');
        var type = "string";

        if ($(this).attr('typeField') != null) {
            type = $(this).attr('typeField');
        }
        var fieldDisplay = $(this).val();
        var fieldValue = $(this).val();

        if ($(this).is("select")) {
            fieldDisplay = $(this).find('option:selected').text();
        }
        if ($(this).is(":checkbox")) {
            fieldValue = $(this).prop('checked');
            if ($(this).prop('checked')) {
                fieldDisplay = "Да";
            } else {
                fieldDisplay = "Нет";
            }
        }

        if ($(this).hasClass("select2-offscreen") && $(this).select2('data') != null) {
            fieldDisplay = $(this).select2('data').text;
        }

        if (!checkValidateion(type, fieldValue, fieldId)) {
            return;
        }
        var modelId = $("#modelId").val();
        var userId = $("#EditorId").val();

        if (modelId === null || modelId.length === 0) {
            modelId = null;
            window.Showbusy(event);
        }

        var row = $(this).closest('tr');
        var entityId=0;
        if (row != null) {
            entityId = row.attr('rowid');
        }
        var dosagePanel = $(this).closest('.dosage-id');
        var dosageId = 0;
        if (dosagePanel != null) {
            dosageId = dosagePanel.attr('rowid');
        }
        $.ajax({
            type: "POST",
            url: "/DrugDeclaration/UpdateSubModel",
            data: { 'code': code, 'modelId': modelId, 'subModelId': dosageId, 'userId': userId, 'recordId': entityId, 'fieldName': fieldId, 'fieldValue': fieldValue, 'fieldDisplay': fieldDisplay },
            dataType: 'json',
            cache: false,
            success: function (data) {
                if (modelId === null) {
                    $("#modelId").val(data.modelId);
                    $("#loading").hide();
                }
                if (data.recordId > 0) {
                    row.attr("rowid", data.recordId);
                    $('#' + fieldId).attr('id', data.controlId);
                }
            },
            error: function (data) {
                alert("1Error" + data);
            }
        });
    });
}
function setDateFormat(control) {
    $(control).datepicker({
        dateFormat: 'dd/mm/yy',
        language: 'ru',
        autoclose: true
    });
}
function UpdateModel(code, recordId, fieldId, fieldName, fieldValue, type, fieldDisplay) {
    if (type === "float") {
        if (fieldValue != null && fieldValue.length > 0) {
            fieldValue = replaceAll(' ', '', fieldValue);
            if (fieldValue.indexOf(',') > 0) {
                fieldValue = fieldValue.replace(',', '.');
            }
            if (fieldValue !== '' && !$.isNumeric(fieldValue)) {
                showWarning('Поле должно содержать числовое значение');
                $('#' + fieldId).val("");
                return false;
            }
            if (fieldValue.indexOf('-') > -1) {
                showWarning('Введите число без знака минус');
                $('#' + fieldId).val("");
                return false;
            }
        }
    }
    if (type === "long") {
        if (fieldValue != null && fieldValue.length > 0) {
            if (fieldValue.indexOf(',') > 0 || fieldValue.indexOf('.') > 0) {
                showWarning("Введите целое число");
                $('#' + fieldId).val("");
                return false;
            }
            if (fieldValue !== '' && !$.isNumeric(fieldValue)) {
                showWarning('Поле должно содержат числовое значение');
                $('#' + fieldId).val("");
                return false;
            }
            if (fieldValue.indexOf('-') > -1) {
                showWarning('Введите число без знака минус');
                $('#' + fieldId).val("");
                return false;
            }
        }
    }

    var modelId = $("#modelId").val();
    var userId = $("#EditorId").val();
    if (modelId === null || modelId.length === 0) {
        modelId = null;
        window.Showbusy(event);
    }
    $.ajax({
        type: "POST",
        url: "/DrugDeclaration/UpdateModel",
        data: { 'code': code, 'modelId': modelId, 'userId': userId, 'recordId': recordId, 'fieldName': fieldName, 'fieldValue': fieldValue, 'fieldDisplay': fieldDisplay },
        dataType: 'json',
        cache: false,
        success: function (data) {
            if (data.recordId > 0) {
                var row = $('#' + fieldName).parent().closest('tr');
                row.attr("rowid", data.recordId);
                $('#' + fieldName).attr('id', data.controlId);
            }
        },
        error: function (data) {
            alert("1Error" + data);
        }
    });
}



