function setReadOnlyControl() {
    $("input").prop("readonly", true);
    $("textArea").prop("readonly", true);
    $("#deSignNote").prop("readonly", false);
    $('#MnnId').select2("enable", false);
    $('#IsConvention').attr('disabled', 'disabled');
    $('.is-not-found').attr('disabled', 'disabled');
    $(".check-wrap").remove();
    $(".deleteRow").remove();
    $(".glyphicon-remove").remove();
    $(".atc-btn").remove();
    $("#addPriceBtn").remove();
    $(".add-btn-from4").remove();
    $('select').attr('disabled', 'disabled');
}


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

$(document).ready(function () {

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

            var tab = $("#" + idcontrol).parent().closest(".comment-page");
            var tabId = tab.attr("id");
            var tabStr = tab.attr("control-collection") + ";" + idcontrol;
            tab.attr("control-collection", tabStr);
            if (!$("#" + tabId + "-title").hasClass("label-tab-danger")) {
                $("#" + tabId + "-title").addClass("label-tab-danger");
            }
            $("#" + tabId + "-span").show();

            var tabDosage = $("#" + idcontrol).parent().closest(".dosage-page");
            var tabDosageId = tabDosage.attr("id");
            if (tabDosageId != null) {
                var tabDosageStr = tab.attr("control-collection") + ";" + idcontrol;
                tabDosage.attr("control-collection", tabDosageStr);
                if (!$("#" + tabDosageId + "-title").hasClass("label-tab-danger")) {
                    $("#" + tabDosageId + "-title").addClass("label-tab-danger");
                }
                $("#" + tabDosageId + "-span").show();
            }


        } else {
            control.className += " control-good";
        }
//        console.log(tableName + '; col:' + columnindex + "; row:" + rowindex);
     

    });
    $('.commentDialog').click(function () {
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
        var url = "/DrugDeclaration/ShowComment?modelId=" + modelId;
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
                            url: '/DrugDeclaration/SaveComment',
                            data: params,
                            dataType: 'json',
                            cache: false,
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                var tabDosage = controlEdit.parent().closest(".dosage-page");
                                var tabDosageId = tabDosage.attr("id");
                                var tabDosageCollection =null;
                                var tabDosageStr =null;
                                if (tabDosageId != null) {
                                    tabDosageStr = tabDosage.attr("control-collection");
                                    tabDosageCollection = tabDosageStr.split(";");
                                }
                                var tab = controlEdit.parent().closest(".comment-page");
                                var tabId = tab.attr("id");
                                var tabStr = tab.attr("control-collection");
                                var tabCollection = tabStr.split(";");
                                
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

                                    if (tabCollection.indexOf(idAttr) < 0) {
                                        tab.attr("control-collection", tabStr + ";" + idAttr);
                                        $("#" + tabId + "-title").addClass("label-tab-danger");
                                        $("#" + tabId + "-span").show();
                                    }

                                    if (tabDosageCollection != null && tabDosageCollection.indexOf(idAttr) < 0) {
                                        tabDosage.attr("control-collection", tabDosageStr + ";" + idAttr);
                                        $("#" + tabDosageId + "-title").addClass("label-tab-danger");
                                        $("#" + tabDosageId + "-span").show();
                                    }
                                } else {
                                    if (!controlEdit.hasClass('control-good')) {
                                        controlEdit.addClass('control-good');
                                    }
                                    if (controlEdit.hasClass('control-error')) {
                                        controlEdit.removeClass('control-error');
                                    }
                                    if (tabDosageCollection != null) {
                                        if (tabDosageCollection.indexOf(idAttr) > -1) {
                                            $.each(tabDosageCollection,
                                                function (i) {
                                                    if (tabDosageCollection[i] === idAttr) {
                                                        tabDosageCollection.splice(i, 1);
                                                    }
                                                });
                                            var tstr1 = tabDosageCollection.join(";");
                                            tabDosage.attr("control-collection", tstr1);
                                            if (tstr1.length === 0) {
                                                $("#" + tabDosageId + "-title").removeClass("label-tab-danger");
                                                $("#" + tabDosageId + "-span").hide();
                                            }
                                        }
                                    }
                                    if (tabCollection.indexOf(idAttr) > -1) {
                                        $.each(tabCollection,
                                            function(i) {
                                                if (tabCollection[i] === idAttr) {
                                                    tabCollection.splice(i, 1);
                                                }
                                            });
                                        var tstr = tabCollection.join(";");
                                        tab.attr("control-collection", tstr);
                                       if (tstr.length === 0) {
                                            $("#" + tabId + "-title").removeClass("label-tab-danger");
                                            $("#" + tabId + "-span").hide();
                                        }
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