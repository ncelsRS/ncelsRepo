/*function createMailRemark(modelId) {
    var success = function () {
        $.ajax({
            type: "POST",
            url: "/DrugDeclaration/CreateMailRemark",
            data: { 'id': modelId },
            dataType: 'json',
            cache: false,
            success: function (data) {
                if (data.isSuccess) {
                    $.ajax({
                        url: "/DrugPrimary/PrimaryPageView?Id=" + modelId,
                        success: function (result) {
                            $("#page" + modelId).html(result);
                            $("#loading").hide();
                        }
                    });
                }
            },
            error: function (data) {
                alert("1Error" + data);
            }
        });
    }
    var cancel = function () {
    };
    showConfirmation("Подтверждение", "Вы уверены что хотите сформировать письмо?", success, cancel);
}*/
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
function removeCorespondence(modelId, controller) {
    $.ajax({
        type: "POST",
        url: "/" + controller + "/DeleteCorespondence",
        data: { 'id': modelId },
        dataType: 'json',
        cache: false,
        success: function (data) {
        },
        error: function (data) {
            alert("1Error" + data);
        }
    });
}

function submitCorespondence(controlId, controller) {
    var exampleModal = "#" + controlId + "exampleModal";
    var numberLetter = "#" + controlId + "_NumberLetter";
    var dateSend = "#" + controlId + "_DateSend";
    var subject = "#" + controlId + "_Subject";
    var note = "#" + controlId + "_Note";
    var id = $(exampleModal).attr('currentId');

    $.ajax({
        type: "POST",
        url: "/DrugPrimary/SaveCorespondence",
        data: { 'id': id, 'numberLetter': $(numberLetter).val(), 'subject': $(subject).val(), 'note': $(note).val(), 'dateSend': $(dateSend).val() },
        dataType: 'json',
        cache: false,
        success: function (data) {
            var num = "#" + id + "_Number";
            var dtSend = "#" + id + "_DateSend";
            $(num).val($(numberLetter).val());
            $(dtSend).val($(dateSend).val());
            $(exampleModal).modal('hide');
        },
        error: function (data) {
            alert("1Error" + data);
        }
    });
}

function showDialogCorespondence(id, modelId, readonly, controller) {
    var exampleModal = "#" + modelId + "exampleModal";
    var numberLetter = "#" + modelId + "_NumberLetter";
    var dateSend = "#" + modelId + "_DateSend";
    var subject = "#" + modelId + "_Subject";
    var note = "#" + modelId + "_Note";
    $(numberLetter).prop("readonly", readonly);
    $(subject).prop("readonly", readonly);
    $(note).prop("readonly", readonly);
    $(dateSend).prop("readonly", readonly);
    var submit = "#" + modelId + "_submit";
    if (readonly) {
        $(submit).hide();
    } else {
        $(submit).show();
    }


    $(exampleModal).attr('currentId', id);
    $.ajax({
        type: "POST",
        url: "/" + controller + "/GetCorespondence",
        data: { 'id': id },
        dataType: 'json',
        cache: false,
        success: function (data) {
            $(numberLetter).val(data.NumberLetter);
            $(subject).val(data.Subject);
            $(note).val(data.Note);
            $(dateSend).data('kendoDatePicker').value(data.DateSend);
            $(exampleModal).modal();
        },
        error: function (data) {
            alert("1Error" + data);
        }
    });
}


function cloneDosageFinalDoc(dosageId, controller) {
    var success = function () {
        $.ajax({
            type: "POST",
            url: "/" + controller + "/CloneDosageFinalDoc",
            data: { 'dosageId': dosageId },
            dataType: 'json',
            cache: false,
            success: function (data) {
            },
            error: function (data) {
                alert("1Error" + data);
            }
        });
    }
    var cancel = function () {
    };
    showConfirmation("Подтверждение", "Вы уверены что хотите применить данное заключение и для других заявок?", success, cancel);
}

function closeTree(idtree) {
    $("#" + idtree).jstree('close_all');
}
function openTree(idtree) {
    $("#" + idtree).jstree('open_all');
}
function customMenu(node) {
    // The default set of all items
    var items = {
        renameItem: { // The "rename" menu item
            label: "Комментарии",
            action: function () {
            }
        }
    };

    if ($(node).hasClass("folder")) {
        // Delete the "delete" menu item
        delete items.deleteItem;
    }

    return items;
}
function FillJSTree(id, controlId, controller, withRemarks) {
    var url = "/" + controller + "/GetTreeData?id=" + id + '&withRemarks=' + withRemarks;
    if ($("#showHistory").val() === "true") {
        url += '&disabled=true';
    }
    $("#" + controlId).jstree({
        'core': {
            check_callback: true,
            'data': {
                "themes": {
                    "responsive": true
                },
                "url": url,
                "dataType": "json"
            }
        }, "types": {
            "default": {
                "icon": "glyphicon glyphicon-list-alt"
            },

            "leaf": {
                "icon": "glyphicon glyphicon-cog"
            },
            "remark": {
                "icon": "glyphicon glyphicon-comment"
            }
        },
        "contextmenu": {
            items: function (node) {
                return {
                    renameItem: { 
                        label: "Комментарии",
                        action: function () {
                            $.ajax({
                                type: "POST",
                                url: "/" + controller + "/UpdateOtd",
                                data: { 'stageId': id, 'noteId': data.node.id, 'isChecked': data.node.state.selected },
                                dataType: 'json',
                                cache: false,
                                success: function (data) {
                                },
                                error: function (data) {
                                    alert("1Error" + data);
                                }
                            });

                            $("#" + id + "_modal").modal();
                        }
                    }

                };
            }
        },
        "checkbox": {
            three_state: false,
            cascade: 'down',
            real_checkboxes: true,
            real_checkboxes_names: function (n) {
                return [("check_" + (n[0].id || Math.ceil(Math.random() * 10000))), n[0].id];
            }
        },
        plugins: ["themes", "json_data", "ui", "checkbox", "types", "search", "contextmenu"]
    }).bind("loaded.jstree", function (event, data) {
        $($(event.target).jstree().get_json('#', { flat: true }))
            .each(function (index, node) {
                if (node.type === 'remark') {
                    $('#' + node.id + '_anchor > i.jstree-checkbox').remove();
                    $("#" + controlId).jstree().deselect_node(node, true);
                }
            });

    })
        .bind("hover_node.jstree", function (e, data) {
            $("#" + data.node.id).prop('title', data.node.text);
        })
        .bind("changed.jstree",
        function (e, data) {
            if (data == null || data.node == null || data.node.type === 'remark') {
                return;
            }
            $.ajax({
                type: "POST",
                url: "/" + controller + "/UpdateOtd",
                data: { 'stageId': id, 'noteId': data.node.id, 'isChecked': data.node.state.selected },
                dataType: 'json',
                cache: false,
                success: function (data) {
                },
                error: function (data) {
                    alert("1Error" + data);
                }
            });
        }).bind("select_node.jstree", function (e, data) {
            if (data.node.type === 'remark' && !$("#" + controlId).jstree().get_node(data.node.parent).state.selected) {
                var oldNodeText = data.node.text;
                $("#" + controlId).jstree().edit(data.node, null, function (node, r, s) {
                    $("#" + controlId).jstree().deselect_node(data.node, true);
                    $('#' + data.node.id + '_anchor > i.jstree-checkbox').remove();
                    if (oldNodeText !== node.text) {
                        $.ajax({
                            type: "POST",
                            url: "/" + controller + "/UpdateOtdRemark",
                            data: { 'stageId': id, 'noteId': data.node.id, 'remark': node.text },
                            dataType: 'json',
                            cache: false,
                            success: function (data) {
                            },
                            error: function (data) {
                                alert("1Error" + data);
                            }
                        });
                    }
                });
            }
        });
    $("#" + controlId).jstree('close_all');
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

function changePrimaryRemark(control) {
    changeFieldList(control, "primaryRemark");
}

function createRemark(modelId, controller) {
    var success = function () {
        window.Showbusy(event);
        $.ajax({
            type: "POST",
            url: "/" + controller + "/CreateRemark",
            data: { 'id': modelId },
            dataType: 'json',
            cache: false,
            success: function (data) {
                if (data.isSuccess) {
                    var url;
                    switch (controller) {
                        case "DrugPrimary":
                            url = "/DrugPrimary/PrimaryPageView?Id=";
                            break;
                        case "Pharmaceutical":
                            url = "/Pharmaceutical/PharmaceuticalPageView?Id=";
                            break;
                        case "Pharmacological":
                            url = "/Pharmacological/PharmacologicalPageView?Id=";
                            break;
                        case "Safetyreport":
                            url = "/Safetyreport/SafetyReportPageView?Id=";
                            break;
                    }
                    if (url) {
                        $.ajax({
                            url: url + modelId,
                            success: function (result) {
                                $("#page" + modelId).html(result);
                                $("#loading").hide();
                            }
                        });
                    }
                }
            },
            error: function (data) {
                alert("1Error" + data);
            }
        });
    }
    var cancel = function () {
    };
    showConfirmation("Подтверждение", "Вы уверены что хотите отправить не отмеченные записи в замечания?", success, cancel);
}

function updateFinalDocument(fieldName, fieldValue, objectId, controller) {
    $.ajax({
        type: "POST",
        url: "/" + controller + "/UpdateFinalDocument",
        data: { 'fieldName': fieldName, 'fieldValue': fieldValue, 'objectId': objectId },
        dataType: 'json',
        cache: false,
        success: function (data) {

        },
        error: function (data) {
            alert("1Error" + data);
        }
    });
}

function sendExpertiseLetterOnAgreement(e, letterId, taskType) {
    kendo.ui.progress($("#mainWindowLoader"), true);
    var data = JSON.stringify({
        docId: letterId,
        documentType: '3',
        taskType: taskType
    });
    $.ajax({
        type: 'POST',
        url: '/DrugDeclaration/SendExpertiseDocumentToAgreement',
        data: data,
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            ShowAlert('Сообщение', "Письмо отправленно на согласование", 'Info', 5000);
            $('#' + letterId + '_Status').val(result);
            $(e).hide();
        },
        complete: function () {
            kendo.ui.progress($("#mainWindowLoader"), false);
        }
    });
}