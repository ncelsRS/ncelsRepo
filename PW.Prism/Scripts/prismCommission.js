var CommissionsFindButton;
function InitFilterCommissionAllGrid(name) {
    $("#reload" + name).click(function (e) {
        var grid = $("#gridCommissionList" + name).data("kendoGrid");
        grid.dataSource.read();
    });
    CommissionsFindButton = "#find" + name;
    $(CommissionsFindButton).click(function (e) {
       // var text = $("#findText" + name).val();
        var findType = $("#findType" + name).val();
        var grid = $("#gridCommissionList" + name).data("kendoGrid");
        if (findType != '') {
            $filter = new Array();
            //if (findType == 0) {
            $filter.push({ field: "TypeId", operator: "equal", value: findType });
            //}
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
            var grid = $("#gridCommissionList" + name).data("kendoGrid");
            grid.dataSource.read();
        }
    });
}

var CommissionQuestionsFindButton;
function InitFilterCommissionQuestionAllGrid(name) {
    $("#reload" + name).click(function (e) {
        var grid = $("#gridCommissionsQuestionsList" + name).data("kendoGrid");
        grid.dataSource.read();
    });
    CommissionQuestionsFindButton = "#find" + name;
    $(CommissionQuestionsFindButton).click(function (e) {
        var text = $("#findText" + name).val();
        //var findType = $("#findType" + name).val();
        var grid = $("#gridCommissionsQuestionsList" + name).data("kendoGrid");
        if (text != '') {
            $filter = new Array();
            // if (findType == 0) {
            $filter.push({ field: "Number", operator: "contains", value: text });
            //  }
            grid.dataSource.filter({
                //logic: "or",
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
            var grid = $("#gridCommissionsQuestionList" + name).data("kendoGrid");
            grid.dataSource.read();
        }
    });
}


var CommissionQuestionsForAddFindButton;
function InitFilterCommissionQuestionForAddAllGrid(name) {
    $("#reload" + name).click(function (e) {
        var grid = $("#gridCommissionQuestionsForAddList" + name).data("kendoGrid");
        grid.dataSource.read();
    });
    CommissionQuestionsForAddFindButton = "#find" + name;
    $(CommissionQuestionsForAddFindButton).click(function (e) {
        var text = $("#findText" + name).val();
        //var findType = $("#findType" + name).val();
        var grid = $("#gridCommissionQuestionsForAddList" + name).data("kendoGrid");
        if (text != '') {
            $filter = new Array();
            // if (findType == 0) {
            $filter.push({ field: "Number", operator: "contains", value: text });
            //  }
            grid.dataSource.filter({
                //logic: "or",
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
            var grid = $("#gridCommissionQuestionsForAddList" + name).data("kendoGrid");
            grid.dataSource.read();
        }
    });
}

function commissionTypeChange(tabName) {
    var prefix = '#' + tabName + ' ';
    var id = $(prefix + '#commissionId').val();
    if (id != undefined && id != '') {
        return;
    }
    var typeId = $(prefix + '#commissionType').val();
    $.ajax({
        type: 'POST',
        url: "/Commission/GetCommissionNumber",
        data: { type: typeId },
        success: function (result) {
            if (result.success == false) {
                ShowAlert('Внимание!', result.message, window.AlertType.Warning, 3000);
            }
            else {
                $(prefix + '#commissionNumber').val(result.number);
            }
        },
        error: function () {
            ShowAlert('Ошибка!', window.commonErrorMessage, window.AlertType.Error, 3000);
        }
    });
}

function changeEditMode(tabName) {
    var prefix = '#' + tabName + ' ';
    if ($(prefix+'#UnitsTable').hasClass('edit-mode')) {
        $(prefix+'#UnitsTable').removeClass('edit-mode');
        $(prefix+'#UnitsTable').addClass('show-mode');
        $(prefix+'#UnitsTable .dep-row').addClass('hidden');
        $(prefix+'#UnitsTable .unit-row.not-checked').addClass('hidden');
        $(prefix+'#UnitsTable .unit-type-cell').prop("disabled", true);
    } else {
        $(prefix+'#UnitsTable').addClass('edit-mode');
        $(prefix+'#UnitsTable').removeClass('show-mode');
        $(prefix+'#UnitsTable .dep-row').removeClass('hidden');
        $(prefix+'#UnitsTable .unit-row').removeClass('hidden');
        $(prefix+'#UnitsTable .unit-type-cell').prop("disabled", false);
    }
}

function changeUnitType(elem, tabName) {
    var prefix = '#' + tabName + ' ';
    var empId = $(elem).data('unit-id');
    var type = $(elem).data('unit-type');
    var unitCells = $(prefix+'.unit-type-cell:checked[data-unit-id="' + empId + '"]');
    var isOneChecked = false;
    for (var i = 0; i < unitCells.length; i++) {
        var t = $(unitCells[i]).data('unit-type');
        if (t != type) {
            $(unitCells[i]).prop("checked", false);
        } else {
            isOneChecked = true;
        }
    }
    if (isOneChecked) {
        $('.unit-row[data-unit-id="' + empId + '"]').removeClass('not-checked');
    } else {
        $('.unit-row[data-unit-id="' + empId + '"]').addClass('not-checked');
    }
}

function searchEmployeeChange(elem, tabName) {
    var prefix = '#' + tabName + ' ';
    var search = $(elem).val().toLowerCase();
    if (search == '') {
        $(prefix + '#UnitsTable .unit-row').removeClass('unit-not-found');
    } else {
        $(prefix + '#UnitsTable .unit-row').not('[data-unit-search*="' + search + '"]').addClass('unit-not-found');
        $(prefix + '#UnitsTable .unit-row[data-unit-search*="' + search + '"]').removeClass('unit-not-found');
    }
    $(prefix + '#UnitsTable .dep-row').removeClass('unit-not-found');
    var depIds = [];
    var i;
    var depId;
    var all;
    var notfound;
    for (i = 0; i < $(prefix + '#UnitsTable .unit-row.unit-not-found').length; i++) {
        depId = $($(prefix + '#UnitsTable .unit-row.unit-not-found')[i]).data('dep');
        depIds.push(depId);
    }
    var uniqueDepIds = depIds.filter(function (itm, i, a) {
        return i == depIds.indexOf(itm);
    });
    for (i = 0; i < uniqueDepIds.length; i++) {
        depId = uniqueDepIds[i];
        all = $(prefix + '#UnitsTable .unit-row[data-dep="' + depId + '"]').length;
        notfound = $(prefix + '#UnitsTable .unit-row.unit-not-found[data-dep="' + depId + '"]').length;
        if (notfound == all) {
            $(prefix + '#UnitsTable .dep-row[data-dep="' + depId + '"]').addClass('unit-not-found');
        }
    }

    var depIds2 = [];
    for (i = 0; i < $(prefix + '#UnitsTable .dep-row.unit-not-found').length; i++) {
        depId = $($(prefix + '#UnitsTable .dep-row.unit-not-found')[i]).data('parent-dep');
        depIds2.push(depId);
    }
    var uniqueDepIds2 = depIds.filter(function (itm, i, a) {
        return i == depIds.indexOf(itm);
    });
    for (i = 0; i < uniqueDepIds2.length; i++) {
        depId = uniqueDepIds2[i];
        all = $(prefix + '#UnitsTable .dep-row[data-dep="' + depId + '"]').length;
        notfound = $(prefix + '#UnitsTable .dep-row.unit-not-found[data-dep="' + depId + '"]').length;
        if (notfound == all) {
            $(prefix + '#UnitsTable .dep-row[data-dep="' + depId + '"]').addClass('unit-not-found');
        }
    }
}


function showAddQuestionList(tabName) {
    var prefix = '#' + tabName + ' ';
    if ($(prefix+'#addQuestionsMain').hasClass('hidden')) {
        $(prefix+'#addQuestionsMain').removeClass('hidden');
    } else {
        $(prefix+'#addQuestionsMain').addClass('hidden');
    }
}

function AddQuestion(commissionId, tabName) {
    var prefix = '#' + tabName + ' ';
    $.ajax({
        type: 'POST',
        url: "/Commission/GetCreateCommissionQuestionView",
        data: { commissionId: commissionId },
        success: function (result) {
            if (result.success == false) {
                ShowAlert('Внимание!', result.message, window.AlertType.Warning, 3000);
            }
            else {
                $(prefix + '#commissionCreateQuestionModal').html(result);
                $(prefix + '#commissionCreateQuestionModal').modal();
            }
        },
        error: function () {
            ShowAlert('Ошибка!', window.commonErrorMessage, window.AlertType.Error, 3000);
        }
    });
}

function CreateCommissionQuestionConfirm(commissionId) {
    var tabName = 'tabstripCommission' + commissionId;
    var prefix = '#' + tabName + ' ';
    var data = {
        Comment: $(prefix+'#createCommissionQuestionComment').val(),
        Type: $(prefix + '#createCommissionQuestionType').val(),
        Commissionid: commissionId,
    }
    $.ajax({
        type: 'POST',
        url: "/Commission/AddCommissionQuestion",
        data: { data: data },
        success: function (result) {
            if (result.success == false) {
                ShowAlert('Внимание!', result.message, window.AlertType.Warning, 3000);
            }
            else {
                $('#commissionCreateQuestionModal').modal("hide");
                $('#CommissionQuestionsMain').html(result);
            }
        },
        error: function () {
            ShowAlert('Ошибка!', window.commonErrorMessage, window.AlertType.Error, 3000);
        }
    });
}


function ChangeCommissionQuestionComment(elem, tabName) {
    var prefix = '#' + tabName + ' ';
    $(prefix + '#' + elem.id + 'SaveBtn').removeClass('hidden');
}

function SaveCommissionQuestionComment(id, commissionId, typeId, tabName) {
    var prefix = '#' + tabName + ' ';
    var val = $(prefix+'#' + id).val();
    $.ajax({
        type: 'POST',
        url: "/Commission/SaveCommissionQuestionComment",
        data: { comment: val, commissionId: commissionId, typeId: typeId },
        success: function (result) {
            if (result.success == false) {
                ShowAlert('Внимание!', result.message, window.AlertType.Warning, 3000);
            }
            else {
                ShowAlert('Успех!', 'Операция успешно выполнена', window.AlertType.Success, 3000);
                $(prefix + '#' + id + 'SaveBtn').addClass('hidden');
            }
        },
        error: function () {
            ShowAlert('Ошибка!', window.commonErrorMessage, window.AlertType.Error, 3000);
        }
    });
}


function ConclusionCommissionDrugDeclaration(id, commissionId) {
    var tabName = 'tabstripCommission' + commissionId;
    var prefix = '#' + tabName + ' ';
    $.ajax({
        type: 'POST',
        url: "/Commission/GetConclusionCommissionDrugDeclarationView",
        data: { id: id },
        success: function (result) {
            if (result.success == false) {
                ShowAlert('Внимание!', result.message, window.AlertType.Warning, 3000);
            }
            else {
                $(prefix + '#commissionConclusionDrugDeclarationModal').html(result);
                $(prefix + '#commissionConclusionDrugDeclarationModal').modal();
            }
        },
        error: function () {
            ShowAlert('Ошибка!', window.commonErrorMessage, window.AlertType.Error, 3000);
        }
    });
}

function ConclusionCommissionDrugDeclarationConfirm(id, commissionId) {
    var tabName = 'tabstripCommission' + commissionId;
    var prefix = '#' + tabName + ' ';
    var data = {
        Comment: $(prefix+'#conclusionCommissionDrugDeclarationComment').val(),
        Type: $(prefix + '#conclusionCommissionDrugDeclarationType').val(),
        Id: id,
    }
    $.ajax({
        type: 'POST',
        url: "/Commission/CreateConclusionCommissionDrugDeclaration",
        data: { data: data },
        success: function (result) {
            if (result.success == false) {
                ShowAlert('Внимание!', result.message, window.AlertType.Warning, 3000);
            }
            else {
                ShowAlert('Успех!', 'Операция успешно выполнена', window.AlertType.Success, 3000);
                $(prefix + '#commissionConclusionDrugDeclarationModal').modal("hide");
                //for (var i = 0; i < $("[id*='gridCommissionQuestionsList']").length ;i++) {
                //    $($("[id*='gridCommissionQuestionsList']")[i]).data("kendoGrid").dataSource.read();
                //}
                $(prefix+"[id*='gridCommissionQuestionsList']").data("kendoGrid").dataSource.read();

            }
        },
        error: function () {
            ShowAlert('Ошибка!', window.commonErrorMessage, window.AlertType.Error, 3000);
        }
    });
}

function AddCommissionDrugDeclaration(dosageId, dosageStageId, commissionId) {
    var tabName = 'tabstripCommission' + commissionId;
    var prefix = '#' + tabName + ' ';
    $.ajax({
        type: 'POST',
        url: "/Commission/AddCommissionDrugDeclaration",
        data: { dosageId: dosageId, dosageStageId: dosageStageId, commissionId: commissionId },
        success: function (result) {
            if (result.success == false) {
                ShowAlert('Внимание!', result.message, window.AlertType.Warning, 3000);
            }
            else {
                ShowAlert('Успех!', 'Операция успешно выполнена', window.AlertType.Success, 3000);
                $(prefix + "[id*='gridCommissionQuestionsList']").data("kendoGrid").dataSource.read();
                $(prefix + "[id*='gridCommissionQuestionsForAddList']").data("kendoGrid").dataSource.read();
            }
        },
        error: function () {
            ShowAlert('Ошибка!', window.commonErrorMessage, window.AlertType.Error, 3000);
        }
    });
}

function RemoveCommissionDrugDeclaration(id, commissionId) {
    var tabName = 'tabstripCommission' + commissionId;
    var prefix = '#' + tabName + ' ';
    $.ajax({
        type: 'POST',
        url: "/Commission/RemoveCommissionDrugDeclaration",
        data: { id: id },
        success: function (result) {
            if (result.success == false) {
                ShowAlert('Внимание!', result.message, window.AlertType.Warning, 3000);
            }
            else {
                ShowAlert('Успех!', 'Операция успешно выполнена', window.AlertType.Success, 3000);
                $(prefix + "[id*='gridCommissionQuestionsList']").data("kendoGrid").dataSource.read();
                $(prefix + "[id*='gridCommissionQuestionsForAddList']").data("kendoGrid").dataSource.read();
            }
        },
        error: function () {
            ShowAlert('Ошибка!', window.commonErrorMessage, window.AlertType.Error, 3000);
        }
    });
}

function SaveCommission(tabName) {
    var prefix = '#' + tabName + ' ';
    var data = {
        Id: $(prefix + '#commissionId').val(),
        Type: $(prefix + '#commissionType').val(),
        Date: $(prefix + '#calendarContainer .k-input').val(),
        Kind: $(prefix + '#commissionKind').val(),
        Comment: $(prefix + '#commissionComment').val(),
        Complete: $(prefix + '#commissionComplete').val(),
    }
    var units = [];
    var unitCells = $(prefix + '.unit-type-cell:checked');
    for (var i = 0; i < unitCells.length; i++) {
        var empId = $(unitCells[i]).data('unit-id');
        var type = $(unitCells[i]).data('unit-type');
        var unit = {
            Id: empId,
            Type: type
        }
        units.push(unit);
    }
    data.Units = units;
    $.ajax({
        type: 'POST',
        url: "/Commission/SaveCommission",
        data: { data: data },
        success: function (result) {
            if (result.success == false) {
                ShowAlert('Внимание!', result.message, window.AlertType.Warning, 3000);
            }
            else {
                ShowAlert('Успех!', 'Операция успешно выполнена', window.AlertType.Success, 3000);
                $(".commission-reload-list-btn").click();

                if (data.Id != result.id) {
                    var name = $(prefix + '#commissionNumber').val();
                    var activeTabId = $('.k-widget.k-tabstrip.k-header.pwTab').attr('aria-activedescendant'); //я тот ещё извращенец
                    closeTab(activeTabId);
                    commissionOpen2(result.id, name);
                }else {
                    $(prefix + "[id*='gridCommissionQuestionsList']").data("kendoGrid").dataSource.read();
                    $(prefix + "[id*='gridCommissionQuestionsForAddList']").data("kendoGrid").dataSource.read();
                }
            }
        },
        error: function () {
            ShowAlert('Ошибка!', window.commonErrorMessage, window.AlertType.Error, 3000);
        }
    });
}