//function setReadOnlyControl() {
//    $("input").prop("readonly", true);
//    $("textArea").prop("readonly", true);
//    $("#deSignNote").prop("readonly", false);
//    $('#MnnId').select2("enable", false);
//    $('#IsConvention').attr('disabled', 'disabled');
//    $('.is-not-found').attr('disabled', 'disabled');
//    $(".check-wrap").remove();
//    $(".deleteRow").remove();
//    $(".glyphicon-remove").remove();
//    $(".atc-btn").remove();
//    $("#addPriceBtn").remove();
//    $(".add-btn-from4").remove();
//    $('select').attr('disabled', 'disabled');
//}


function showInformIcon(isShow) {
    if (isShow) {
        $('.input-group-addon').show();
    } else {
        $('.input-group-addon').hide();
    }
}

function eritControl(isEdit) {
    if (isEdit) {
        $(".fill-control").prop("readonly", false);
    } else {
        $(".fill-control").prop("readonly", true);
    }
}

function checkAttachFile() {
    var validFile = true;
    $('.file-validation').each(function () {
        var ct = $(this).attr('countFile');
        var attCode = $(this).attr('attCode');
        var count = parseInt(ct, 10) || 0;
        if (count === 0 && (attCode == "dover" || attCode == "letter")) {
            $(this).text("Необходимо вложить файлы");
            validFile = false;
        } else {
            $(this).text("");
        }
    });
    return validFile;
}

function UpdateModel(code, recordId, fieldId, fieldName, fieldValue, type, fieldDisplay) {
    //debugger;
    if (type === "float") {
        if (fieldValue != null && fieldValue.length > 0) {
            fieldValue = replaceAll(' ', '', fieldValue);
            if (fieldValue.indexOf(',') > 0) {
                fieldValue = fieldValue.replace(',', '.');
            }
            if (fieldValue !== '' && !$.isNumeric(fieldValue)) {
                showWarning('@ResourceSetting.sInputNumberRequired');
                $('#' + fieldId).val("");
                return false;
            }
            if (fieldValue.indexOf('-') > -1) {
                showWarning('@ResourceSetting.enterNumberNotMinus');
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
                showWarning('@ResourceSetting.sInputNumberRequired');
                $('#' + fieldId).val("");
                return false;
            }
            if (fieldValue.indexOf('-') > -1) {
                showWarning('@ResourceSetting.enterNumberNotMinus');
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
        url: '/Project/UpdateModel',
        data: { 'code': code, 'modelId': modelId, 'userId': userId, 'recordId': recordId, 'fieldName': fieldName, 'fieldValue': fieldValue, 'fieldDisplay': fieldDisplay },
        dataType: 'json',
        cache: false,
        success: function (data) {
            if (modelId === null) {
                $("#modelId").val(data.modelId);
                $("#loading").hide();
            }
            //if (data.recordId > 0) {
            //    var row = $('#' + fieldName).parent().closest('tr');
            //    row.attr("rowid", data.recordId);
            //    //                    var codeControl = code + '_' + data.recordId + '_' + fieldName;
            //    $('#' + fieldName).attr('id', data.controlId);
            //}
        },
        error: function (data) {
            customWarning("Error" + data);
        }
    });
}

//function setDateFormat(control) {
//    $(control).datepicker({
//        dateFormat: 'dd.mm.yy',
//        language: 'ru',
//        autoclose: true,
//        onSelect: function () {
//            
//        }
//    });
//}

$(document).ready(function () {

    //setDateFormat(".datepicker");

    $(".main-control").change(function () {
        //debugger;
        var controlId = $(this).attr('id');
        var type = "string";
        if ($(this).attr('typeField') != null) {
            type = $(this).attr('typeField');
        }
        var fieldDisplay = $(this).val();
        var fieldValue = $(this).val();

        if ($(this).is("select")) {
            fieldDisplay = $(this).find('option:selected').text();
        }
        if ($(this).hasClass("chzn-select") && fieldValue != null && fieldValue.length > 0) {
            fieldValue = fieldValue.join(',');
        }
        if ($(this).hasClass("select2-offscreen") && $(this).select2('data') != null) {
            fieldDisplay = $(this).select2('data').text;
        }
        UpdateModel("main", null, null, controlId, fieldValue, type, fieldDisplay);
    });

   /* $('.glyphicon-info-sign').each(function () {
        $(this).addClass("def-icon");
    });*/
    $('.rating').each(function () {
        var idcontrol = $(this).attr('idcontrol');
        var control = document.getElementById(idcontrol);
        if (control == null) {
            return;
        }
        var iserror = $(this).attr('iserror');
        if (iserror) {
            control.className += " control-error";
        } else {
            control.className += " control-good";
        }
//        console.log(tableName + '; col:' + columnindex + "; row:" + rowindex);
     

    });
    $('.commentDialog').click(function () {
        //debugger;
        var inputControl = $(this).parent().prev();
        var modelId = $("#modelId").val();
      
        var controlEdit = $(this).parent().parent().find('.edit-control');
        var fieldValue = "";
        var idAttr = controlEdit.attr('id');
        if (idAttr.indexOf('s2id_') === 0) {
            idAttr = idAttr.replace('s2id_','');
        }
        var fieldDisplay = $(controlEdit).val();
        fieldValue = $(controlEdit).val();

        if ($(controlEdit).is("select")) {
            fieldDisplay = $(this).find('option:selected').text();
        }
        if ($(controlEdit).hasClass("select2-offscreen")) {
            fieldDisplay = $(controlEdit).select2('data').text;
        }
       
       // var rowId = $(this).closest('tr').attr('rowid');
        var thisControl = $(this).find("i");
        if (modelId == 0) {
            return;
        }
        var url = "/Project/ShowComment?modelId=" + modelId;
        url += "&idControl=" + idAttr;

        $("<div style=" + '"' + "text-align: center;" + '"' + "><img src=" + '"' + "../../content/img/spinner.gif" + '"' + " style=" + '"' + "display: block; margin: 0 auto;" + '"' + " /></br>" + "....</div>")
            .addClass("dialog")
            .attr("id", $(this)
                .attr("data-dialog-id"))
            .appendTo("body")
            .dialog({
                title: "Описание",
                closeText: "x",
                open: function(event, ui) {
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
                height:400,
                modal: true,
                //   open: function(event, ui) { $(".ui-dialog-titlebar-close").text(''); },
                buttons: {
                    Сохранить: function () {
                        var comment = $("#NoteComment").val();
                        var userId = $("#EditorId").val();
                        var isError;
                        if ($("#IsError").is(":checked")) {
                            isError = true;
                        } else {
                            isError = false;
                        }
                        var params = JSON.stringify({ 'modelId': modelId, 'idControl': idAttr, 'isError': isError, 'comment': comment, 'fieldValue': fieldValue, 'userId': userId, 'fieldDisplay': fieldDisplay });
                        $.ajax({
                            type: "POST",
                            url: '/Project/SaveComment',
                            data: params,
                            dataType: 'json',
                            cache: false,
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                if (!thisControl.hasClass('mark-icon')) {
                                    thisControl.addClass('mark-icon');

                                }
                                if (isError) {
                                    if (!controlEdit.hasClass('control-error')) {
                                        controlEdit.addClass('control-error');
                                    }
                                    if (controlEdit.hasClass('control-good')) {
                                        controlEdit.removeClass('control-good');
                                    }
                                } else {
                                    if (!controlEdit.hasClass('control-good')) {
                                        controlEdit.addClass('control-good');
                                    }
                                    if (controlEdit.hasClass('control-error')) {
                                        controlEdit.removeClass('control-error');
                                    }
                                }

                            },
                            error: function () {
                                alert("Connection Failed. Please Try Again");
                            }
                        });
                        $(this).dialog("close");
                    }
                },
            })
            .load(url);
    });
    $(".close").click(function (e) {
        e.preventDefault();
        $(this).closest(".dialog").dialog("close");
    });
});