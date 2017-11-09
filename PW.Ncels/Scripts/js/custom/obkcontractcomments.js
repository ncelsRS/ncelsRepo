$(document).ready(function () {
    $('.rating').each(function () {
        var idcontrol = $(this).attr('idcontrol');
        var control = document.getElementById(idcontrol);
        if (control == null) {
            return;
        }
        var iserror = $(this).attr('iserror');
        if (iserror) {
            control.className += " control-error";
            if ($(control).is("input")) {
                $(control).css("border-color", "red");
            }
            else if ($(control).is("span")) {
                // ui-select
                $(control).prev().find(".ui-select-toggle").css("border-color", "red");
            }
        } else {
            control.className += " control-good";
            if ($(control).is("input")) {
                $(control).css("border-color", "green");
            }
            else if ($(control).is("span")) {
                // ui-select
                $(control).prev().find(".ui-select-toggle").css("border-color", "green");
            }
        }
    });

    $('.obkcontractdialog').click(function () {
        var inputControl = $(this).parent().prev();
        var modelId = $("#projectId").val();
        var controlEdit = $(this).parent().parent().find('.edit-control');
        var fieldValue = "";
        var idAttr = controlEdit.attr('id');
        if (idAttr.indexOf('s2id_') === 0) {
            idAttr = idAttr.replace('s2id_', '');
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
        if (!modelId) {
            return;
        }
        var url = "/OBKContract/ShowComment?modelId=" + modelId;
        url += "&idControl=" + idAttr;

        $("<div style=" + '"' + "text-align: center;" + '"' + "><img src=" + '"' + "../../content/img/spinner.gif" + '"' + " style=" + '"' + "display: block; margin: 0 auto;" + '"' + " /></br>" + "....</div>")
            .addClass("dialog")
            .attr("id", $(this)
                .attr("data-dialog-id"))
            .appendTo("body")
            .dialog({
                title: "Описание",
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
                    //Сохранить: function () {
                    //    var comment = $("#NoteComment").val();
                    //    var userId = $("#EditorId").val();
                    //    var isError;
                    //    if ($("#IsError").is(":checked")) {
                    //        isError = true;
                    //    } else {
                    //        isError = false;
                    //    }
                    //    var params = JSON.stringify({ 'modelId': modelId, 'idControl': idAttr, 'isError': isError, 'comment': comment, 'fieldValue': fieldValue, 'userId': userId, 'fieldDisplay': fieldDisplay });
                    //    $.ajax({
                    //        type: "POST",
                    //        url: '/OBKContract/SaveComment',
                    //        data: params,
                    //        dataType: 'json',
                    //        cache: false,
                    //        contentType: "application/json; charset=utf-8",
                    //        success: function (data) {
                    //            if (!thisControl.hasClass('mark-icon')) {
                    //                thisControl.addClass('mark-icon');

                    //            }
                    //            if (isError) {
                    //                if (!controlEdit.hasClass('control-error')) {
                    //                    controlEdit.addClass('control-error');
                    //                }
                    //                if (controlEdit.hasClass('control-good')) {
                    //                    controlEdit.removeClass('control-good');
                    //                }
                    //            } else {
                    //                if (!controlEdit.hasClass('control-good')) {
                    //                    controlEdit.addClass('control-good');
                    //                }
                    //                if (controlEdit.hasClass('control-error')) {
                    //                    controlEdit.removeClass('control-error');
                    //                }
                    //            }

                    //        },
                    //        error: function () {
                    //            alert("Connection Failed. Please Try Again");
                    //        }
                    //    });
                    //    $(this).dialog("close");
                    //}
                },
            })
            .load(url);
    });

    $('body').on('click', '.obkpricedialog', function () {
        var guid = $(this).attr("valval");
        if (guid) {
            var url = "/OBKContract/ShowCommentPrice?contractPriceId=" + guid;
            var fieldValue = "";
            var fieldDisplay = "";
            $("<div style=" + '"' + "text-align: center;" + '"' + "><img src=" + '"' + "../../content/img/spinner.gif" + '"' + " style=" + '"' + "display: block; margin: 0 auto;" + '"' + " /></br>" + "....</div>")
            .addClass("dialog")
            .attr("id", $(this)
                .attr("data-dialog-id"))
            .appendTo("body")
            .dialog({
                title: "Описание",
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
                    //Сохранить: function () {
                    //    var comment = $("#NoteComment").val();
                    //    var userId = $("#EditorId").val();
                    //    var isError;
                    //    if ($("#IsError").is(":checked")) {
                    //        isError = true;
                    //    } else {
                    //        isError = false;
                    //    }
                    //    var params = JSON.stringify({ 'contractPriceId': contractPriceId, 'isError': isError, 'comment': comment, 'fieldValue': fieldValue, 'userId': userId, 'fieldDisplay': fieldDisplay });
                    //    $.ajax({
                    //        type: "POST",
                    //        url: '/OBKContract/SaveCommentPrice',
                    //        data: params,
                    //        dataType: 'json',
                    //        cache: false,
                    //        contentType: "application/json; charset=utf-8",
                    //        success: function (data) {
                    //            if (!thisControl.hasClass('mark-icon')) {
                    //                thisControl.addClass('mark-icon');
                    //            }
                    //            if (isError) {
                    //                if (!controlEdit.hasClass('control-error')) {
                    //                    controlEdit.addClass('control-error');
                    //                }
                    //                if (controlEdit.hasClass('control-good')) {
                    //                    controlEdit.removeClass('control-good');
                    //                }
                    //            } else {
                    //                if (!controlEdit.hasClass('control-good')) {
                    //                    controlEdit.addClass('control-good');
                    //                }
                    //                if (controlEdit.hasClass('control-error')) {
                    //                    controlEdit.removeClass('control-error');
                    //                }
                    //            }
                    //        },
                    //        error: function () {
                    //            alert("Connection Failed. Please Try Again");
                    //        }
                    //    });
                    //    $(this).dialog("close");
                    //}
                },
            })
            .load(url);
        }
    });

    $('body').on('click', '.obkproductdialog', function () {
        var productId = $(this).attr("valval");

        if (productId) {
            var url = "/OBKContract/ShowCommentProduct?productId=" + productId;
            var fieldValue = "";
            var fieldDisplay = "";
            $("<div style=" + '"' + "text-align: center;" + '"' + "><img src=" + '"' + "../../content/img/spinner.gif" + '"' + " style=" + '"' + "display: block; margin: 0 auto;" + '"' + " /></br>" + "....</div>")
            .addClass("dialog")
            .attr("id", $(this)
                .attr("data-dialog-id"))
            .appendTo("body")
            .dialog({
                title: "Описание",
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
                    //Сохранить: function () {
                    //    var comment = $("#NoteComment").val();
                    //    var userId = $("#EditorId").val();
                    //    var isError;
                    //    if ($("#IsError").is(":checked")) {
                    //        isError = true;
                    //    } else {
                    //        isError = false;
                    //    }
                    //    var params = JSON.stringify({ 'productId': productId, 'isError': isError, 'comment': comment, 'fieldValue': fieldValue, 'userId': userId, 'fieldDisplay': fieldDisplay });
                    //    $.ajax({
                    //        type: "POST",
                    //        url: '/OBKContract/SaveCommentProduct',
                    //        data: params,
                    //        dataType: 'json',
                    //        cache: false,
                    //        contentType: "application/json; charset=utf-8",
                    //        success: function (data) {
                    //            if (!thisControl.hasClass('mark-icon')) {
                    //                thisControl.addClass('mark-icon');
                    //            }
                    //            if (isError) {
                    //                if (!controlEdit.hasClass('control-error')) {
                    //                    controlEdit.addClass('control-error');
                    //                }
                    //                if (controlEdit.hasClass('control-good')) {
                    //                    controlEdit.removeClass('control-good');
                    //                }
                    //            } else {
                    //                if (!controlEdit.hasClass('control-good')) {
                    //                    controlEdit.addClass('control-good');
                    //                }
                    //                if (controlEdit.hasClass('control-error')) {
                    //                    controlEdit.removeClass('control-error');
                    //                }
                    //            }
                    //        },
                    //        error: function () {
                    //            alert("Connection Failed. Please Try Again");
                    //        }
                    //    });
                    //    $(this).dialog("close");
                    //}
                },
            })
            .load(url);
        }

        
    });

    $('body').on('click', '.obkproductseriedialog', function () {
        var productSerieId = $(this).attr("valval");
        if (productSerieId) {
            var url = "/OBKContract/ShowCommentProductsSerie?productSerieId=" + productSerieId;
            var fieldValue = "";
            var fieldDisplay = "";
            $("<div style=" + '"' + "text-align: center;" + '"' + "><img src=" + '"' + "../../content/img/spinner.gif" + '"' + " style=" + '"' + "display: block; margin: 0 auto;" + '"' + " /></br>" + "....</div>")
            .addClass("dialog")
            .attr("id", $(this)
                .attr("data-dialog-id"))
            .appendTo("body")
            .dialog({
                title: "Описание",
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
                    //Сохранить: function () {
                    //    var comment = $("#NoteComment").val();
                    //    var userId = $("#EditorId").val();
                    //    var isError;
                    //    if ($("#IsError").is(":checked")) {
                    //        isError = true;
                    //    } else {
                    //        isError = false;
                    //    }
                    //    var params = JSON.stringify({ 'productSerieId': productSerieId, 'isError': isError, 'comment': comment, 'fieldValue': fieldValue, 'userId': userId, 'fieldDisplay': fieldDisplay });
                    //    $.ajax({
                    //        type: "POST",
                    //        url: '/OBKContract/SaveCommentProductsSerie',
                    //        data: params,
                    //        dataType: 'json',
                    //        cache: false,
                    //        contentType: "application/json; charset=utf-8",
                    //        success: function (data) {
                    //            if (!thisControl.hasClass('mark-icon')) {
                    //                thisControl.addClass('mark-icon');
                    //            }
                    //            if (isError) {
                    //                if (!controlEdit.hasClass('control-error')) {
                    //                    controlEdit.addClass('control-error');
                    //                }
                    //                if (controlEdit.hasClass('control-good')) {
                    //                    controlEdit.removeClass('control-good');
                    //                }
                    //            } else {
                    //                if (!controlEdit.hasClass('control-good')) {
                    //                    controlEdit.addClass('control-good');
                    //                }
                    //                if (controlEdit.hasClass('control-error')) {
                    //                    controlEdit.removeClass('control-error');
                    //                }
                    //            }
                    //        },
                    //        error: function () {
                    //            alert("Connection Failed. Please Try Again");
                    //        }
                    //    });
                    //    $(this).dialog("close");
                    //}
                },
            })
            .load(url);
        }
    });
});