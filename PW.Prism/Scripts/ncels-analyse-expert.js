function exportProtocol(id) {
    window.open('/DrugAnalitic/ExportProtocol?id=' + id);
}

function chekcInProtocol(control) {
    $(control).change(function () {
        var controlId = $(this).attr('id');
        var fieldValue = $(this).prop('checked');
        var row = $(this).closest('tr');
        var tbody = $(row).closest('tbody').attr('id');
        var entityId=null;
        if (row != null) {
            entityId = row.attr('id');
        }
        $.ajax({
            type: "POST",
            url: "/DrugAnalitic/CheckInProtocol",
            data: { 'entityId': entityId, 'fieldValue': fieldValue },
            dataType: 'json',
            cache: false,
            success: function (data) {
                var exist = false;
                $(".protocol-row").each(function () {
                    if ($(this).attr('rowId') === entityId) {
                        exist = true;
                        if (!fieldValue) {
                            $('#' + $(this).attr('Id')).hide();
                        } else {
                            $('#' + $(this).attr('Id')).show();
                        }
                        return;

                    }
                });
                if (exist) {
                    return;
                }
                var rowId = entityId + "rowId";
                var nameRu = entityId + "_NameRu1";
                var temperature = entityId + "_Temperature1";
                var humidity = entityId + "_Humidity1";
                var designation = entityId + "_Designation1";
                var demand = entityId + "_Demand1";
                var actualResult = entityId + "_ActualResult1";
                var tbodyName = tbody + "1";
                var cols = '<tr rowId="' + entityId + '" class="protocol-row"  id="' + rowId + '">';
                cols += '<td><input type="text" readonly="readonly" id="' + nameRu + '"  class="form-control" value="' + $("#" + entityId + "_NameRu").val() + '"/></td>';
                cols += '<td><input type="text" readonly="readonly" id="' + temperature + '"  class="form-control" value="' + $("#" + entityId + "_Temperature").val() + '"/></td>';
                cols += '<td><input type="text" readonly="readonly" id="' + humidity + '"  class="form-control" value="' + $("#" + entityId + "_Humidity").val() + '"/></td>';
                cols += '<td><input type="text" readonly="readonly" id="' + demand + '"  class="form-control growed" value="' + $("#" + entityId + "_Demand").val() + '"/></td>';
                cols += '<td><input type="text" readonly="readonly" id="' + actualResult + '"  class="form-control growed" value="' + $("#" + entityId + "_ActualResult").val() + '"/></td>';
                cols += '</tr>';
                $('#' + tbodyName).append(cols);
            },
            error: function (data) {
                alert("1Error" + data);
            }
        });
    });
}

function cloneAnaliseDosage(dosageId) {
    var success = function () {
        $.ajax({
            type: "POST",
            url: "/DrugAnalitic/CloneAnaliseDosage",
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
function removeAnalyseInidcator(modelId) {
    $.ajax({
        type: "POST",
        url: "/DrugAnalitic/DeleteIndicator",
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
function showAnalyseDialog(id, stageId, readonly) {
    var temperatureTemp = "#" + stageId + "_Temperature";
    var humidityTemp = "#" + stageId + "_Humidity";
    var designationTemp = "#" + stageId + "_Designation";
    var demandTemp = "#" + stageId + "_Demand";
    var actualResultTemp = "#" + stageId + "_ActualResult";
    var analyseIndicator = "#" + "AnalyseIndicatorList" + stageId;
    var isMatches = "#" + "Booleans" + stageId;
    var exampleModal = "#" + stageId + "exampleModal";
    $(exampleModal).attr('currentId', id);
    $(temperatureTemp).prop("readonly", readonly);
    $(humidityTemp).prop("readonly", readonly);
    $(designationTemp).prop("readonly", readonly);
    $(demandTemp).prop("readonly", readonly);
    $(actualResultTemp).prop("readonly", readonly);
  
    var submit = "#" + stageId + "_submit";
    if (readonly) {
        $(analyseIndicator).attr('disabled', 'disabled');
        $(isMatches).attr('disabled', 'disabled');
        $(submit).hide();
    } else {
        $(analyseIndicator).removeAttr('disabled');
        $(isMatches).removeAttr('disabled');
        $(submit).show();
    }
    $.ajax({
        type: "POST",
        url: "/DrugAnalitic/GetIndicator",
        data: { 'id': id },
        dataType: 'json',
        cache: false,
        success: function (data) {
            $(temperatureTemp).val(data.Temperature);
            $(humidityTemp).val(data.Humidity);
            $(designationTemp).val(data.Designation);
            $(demandTemp).val(data.Demand);
            $(actualResultTemp).val(data.ActualResult);
            $(analyseIndicator).val(data.AnalyseIndicator);
            $(isMatches).val(data.IsMatches);
            $(exampleModal).modal();
        },
        error: function (data) {
            alert("1Error" + data);
        }
    }); 
}

function submitAnalyseIndicator(stageId,tbody) {
    var temperatureTemp = "#" + stageId + "_Temperature";
    var humidityTemp = "#" + stageId + "_Humidity";
    var designationTemp = "#" + stageId + "_Designation";
    var demandTemp = "#" + stageId + "_Demand";
    var actualResultTemp = "#" + stageId + "_ActualResult";
    var exampleModal = "#" + stageId + "exampleModal";
    var id = $(exampleModal).attr('currentId');
    var analyseIndicator = "#" + "AnalyseIndicatorList" + stageId;
    var isMatches = "#" + "Booleans" + stageId;

    $.ajax({
        type: "POST",
        url: "/DrugAnalitic/SaveOrUpdateIndicator",
        data: {'stageId':stageId, 'id': id, 'temperature': $(temperatureTemp).val(), 'humidity': $(humidityTemp).val(), 'designation': $(designationTemp).val(), 'demand': $(demandTemp).val(), 'actualResult': $(actualResultTemp).val(), 'analyseIndicator': $(analyseIndicator).val(), 'isMatches': $(isMatches).val() },
        dataType: 'json',
        cache: false,
        success: function (data) {
            $(exampleModal).attr('currentId', data.modelId);
            var nameRu = data.modelId + "_NameRu";
            var temperature = data.modelId + "_Temperature";
            var humidity = data.modelId + "_Humidity";
            var designation = data.modelId + "_Designation";
            var demand = data.modelId + "_Demand";
            var actualResult = data.modelId + "_ActualResult";
            var inProtocol = data.modelId + "_InProtocol";
            var isMatchesStr = data.modelId + "_IsMatchesStr";
            debugger;
            if (data.isNew) {
                var cols = '<tr id="' + data.modelId + '">';
                cols += '<td>'+ data.PositionNumber + '</td>';
                cols += '<td><input type="text" readonly="readonly" id="' + nameRu + '"  class="form-control" value="' + data.AnalyseIndicatorName + '"/></td>';
                cols += '<td><input type="text" readonly="readonly" id="' + temperature + '"  class="form-control" value="' + $(temperatureTemp).val() + '"/></td>';
                cols += '<td><input type="text" readonly="readonly" id="' + humidity + '"  class="form-control" value="' + $(humidityTemp).val() + '"/></td>';
                cols += '<td><input type="text" readonly="readonly" id="' + designation + '"  class="form-control" value="' + $(designationTemp).val() + '"/></td>';
                cols += '<td><input type="text" readonly="readonly" id="' + demand + '"  class="form-control" value="' + $(demandTemp).val() + '"/></td>';
                cols += '<td><input type="text" readonly="readonly" id="' + actualResult + '"  class="form-control" value="' + $(actualResultTemp).val() + '"/></td>';
                cols += '<td><input type="text" readonly="readonly" id="' + isMatchesStr + '"  class="form-control" value="' + data.IsMatchesStr + '"/></td>';
                cols += '<td><input class="form-control" data-val="true" data-val-required="The InProtocol field is required." id="' + data.modelId + '_InProtocol" name="ExpDrugAnaliseIndicators[0].InProtocol" type="checkbox" value="true"></td>';
                cols +=
                    '<td><div class="btn-group" style="float: right; margin-right: 10px; color: white; text-align: left"><button type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Операции <span class="caret"></span></button>' +
                    '<ul class="dropdown-menu btnmenu"><li class="btn-info">' +
                    '<a href="#" class="link-object" onclick="showAnalyseDialog(' +
                    data.modelId +
                    ',' +
                    stageId +
                    ',true)" style="color: white; padding: 5px"><span class="glyphicon glyphicon-search" aria-hidden="true"></span> Просмотр</a></li>' +
                    '<li class="btn-warning edit-li"><a href="#" class="editExpRow" onclick="showAnalyseDialog(' + data.modelId + ',' + stageId +
                    ',false)" class="link-object" style="color: white; padding: 5px"><span class="glyphicon glyphicon-edit" aria-hidden="true"></span> Редактировать</a> </li>' +
                    '<li class="btn-danger edit-li"><a href="#" class="deleteExpRow" style="color: white; padding: 5px"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span> Удалить</a></li></ul></div></td>';
                cols += '</tr>';
                $('#' + tbody).append(cols);

                var rowId1 = data.modelId + "rowId";
                var nameRu1 = data.modelId + "_NameRu1";
                var temperature1 = data.modelId + "_Temperature1";
                var humidity1 = data.modelId + "_Humidity1";
                var designation1 = data.modelId + "_Designation1";
                var demand1 = data.modelId + "_Demand1";
                var actualResult1 = data.modelId + "_ActualResult1";
                var tbodyName1 = tbody + "1";
                var cols1 = '<tr rowId="' + data.modelId + '" class="protocol-row"  id="' + rowId1 + '">';
                cols1 += '<td><input type="text" readonly="readonly" id="' + nameRu1 + '"  class="form-control" value="' + data.AnalyseIndicatorName + '"/></td>';
                cols1 += '<td><input type="text" readonly="readonly" id="' + temperature1 + '"  class="form-control" value="' + $(temperatureTemp).val() + '"/></td>';
                cols1 += '<td><input type="text" readonly="readonly" id="' + humidity1 + '"  class="form-control" value="' + $(humidityTemp).val() + '"/></td>';
                cols1 += '<td><input type="text" readonly="readonly" id="' + demand1 + '"  class="form-control growed" value="' + $(demandTemp).val() + '"/></td>';
                cols1 += '<td><input type="text" readonly="readonly" id="' + actualResult1 + '"  class="form-control growed" value="' + $(actualResultTemp).val() + '"/></td>';
                cols1 += '</tr>';
                $('#' + tbodyName1).append(cols1);

            } else {
                $("#" + nameRu).val(data.AnalyseIndicatorName);
                $("#" + temperature).val($(temperatureTemp).val());
                $("#" + humidity).val($(humidityTemp).val());
                $("#" + designation).val($(designationTemp).val());
                $("#" + demand).val($(demandTemp).val());
                $("#" + actualResult).val($(actualResultTemp).val());
                $("#" + isMatchesStr).val(data.IsMatchesStr);
            }
            $(exampleModal).modal('hide');
            
        },
        error: function (data) {
            alert("1Error" + data);
        }
    });
}