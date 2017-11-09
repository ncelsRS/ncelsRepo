function getObkContractDetails(parameters, number) {
    if (docArray.indexOf(parameters.toLowerCase()) != -1)
        docArray.splice(docArray.indexOf(parameters.toLowerCase()), 1);
    var element = document.getElementById(parameters);
    if (element == null) {
        var tabStrip = $("#tabstrip").data("kendoTabStrip");
        var content = '<div id="' + parameters + '"> </div>';
        var idContent = '#' + parameters;
        tabStrip.append({
            text: 'Договор: № ' + number,
            content: content

        });

        tabStrip.select(tabStrip.items().length - 1);

        var gridElement = $(idContent);

        gridElement.height($(window).height() - 100);

        $.ajax({
            url: "/OBKContract/Edit?id=" + parameters,
            success: function (result) {
                $(idContent).html(result);
            }
        });
    } else {

        var itesm = $('#' + parameters)[0].parentElement.getAttribute('id').split('-')[1];
        if (itesm) {
            $("#tabstrip").data("kendoTabStrip").select(itesm - 1);
        }
    }
}

function InitObkContractCard(uiId) {
    var viewModel = kendo.observable({
        contractCardTabSelect: function (e) {
            debugger;
            var tabid = $(e.item).attr('tabid');
            $('#contractDataTabs' + uiId + ' > .row').each(function (i, el) {
                if (!$(el).hasClass("hidden")) {
                    $(el).addClass("hidden");
                }
            });
            $('#' + tabid).removeClass("hidden");
            
            if (tabid == "contractDataTab4" + uiId) {
                $("#contracHistoryGrid" + uiId).data("kendoGrid").dataSource.read();
            }
        },
        meetsRequirements: function (e) {
            var question = "Вы подтверждаете действие \"Соответствует требованиям\"?";
            if (confirm(question)) {
                var modelId = $("#modelId").val();
                kendo.ui.progress($("#loader" + uiId), true);
                $.ajax({
                    type: 'POST',
                    url: '/OBKContract/MeetsRequirements',
                    data: JSON.stringify({ contractId: modelId }),
                    contentType: 'application/json; charset=utf-8',
                    success: function (result) {
                        $(e.target).hide();
                        $("#doesNotMeetRequirementsBtn" + uiId).hide();
                    },
                    complete: function () {
                        kendo.ui.progress($("#loader" + uiId), false);
                    }
                });
            }
        },
        doesNotMeetRequirements: function (e) {
            var question = "Вы подтверждаете действие \"Несоответствует требованиям\"?";
            if (confirm(question)) {
                var modelId = $("#modelId").val();
                kendo.ui.progress($("#loader" + uiId), true);
                $.ajax({
                    type: 'POST',
                    url: '/OBKContract/DoesNotMeetRequirements',
                    data: JSON.stringify({ contractId: modelId }),
                    contentType: 'application/json; charset=utf-8',
                    success: function (result) {
                        $(e.target).hide();
                        $("#meetsRequirementsBtn" + uiId).hide();
                    },
                    complete: function () {
                        kendo.ui.progress($("#loader" + uiId), false);
                    }
                });
            }
        },
        returnToApplicant: function (e) {
            var question = "Вы подтверждаете действие \"Вернуть на доработку\"?";
            if (confirm(question)) {
                var modelId = $("#modelId").val();
                $.ajax({
                    type: 'POST',
                    url: '/OBKContract/ReturnToApplicant',
                    data: JSON.stringify({ contractId: modelId }),
                    contentType: 'application/json; charset=utf-8',
                    success: function (result) {
                        $(e.target).hide();
                        $("#sendToBossForApprovalBtn" + uiId).hide();
                        $("#sendToBossForApprovalWithWarningBtn" + uiId).hide();
                    },
                    complete: function () {

                    }
                });
            }
        },
        sendToBossForApproval: function (e) {
            var question = "Вы подтверждаете действие \"На согласование руководителю\"?";
            if (confirm(question)) {
                var modelId = $("#modelId").val();
                $.ajax({
                    type: 'POST',
                    url: '/OBKContract/SendToBossForApproval',
                    data: JSON.stringify({ contractId: modelId }),
                    contentType: 'application/json; charset=utf-8',
                    success: function (result) {
                        $(e.target).hide();
                        $("#returnToApplicantBtn" + uiId).hide();
                    },
                    complete: function () {

                    }
                });
            }
        },
        sendToBossForApprovalWithWarning: function (e) {
            var question = $("#question" + uiId).text();
            if (confirm(question)) {
                var modelId = $("#modelId").val();
                $.ajax({
                    type: 'POST',
                    url: '/OBKContract/SendToBossForApproval',
                    data: JSON.stringify({ contractId: modelId }),
                    contentType: 'application/json; charset=utf-8',
                    success: function (result) {
                        $(e.target).hide();
                        $("#returnToApplicantBtn" + uiId).hide();
                    },
                    complete: function () {

                    }
                });
            }
        },
        doApprovement: function (e) {
            var question = "Вы подтверждаете действие \"Согласовать\"?";
            if (confirm(question)) {
                var modelId = $("#modelId").val();
                $.ajax({
                    type: 'POST',
                    url: '/OBKContract/DoApprovement',
                    data: JSON.stringify({ contractId: modelId }),
                    contentType: 'application/json; charset=utf-8',
                    success: function (result) {
                        $(e.target).hide();
                        $("#refuseApprovementBtn" + uiId).hide();
                    },
                    complete: function () {

                    }
                });
            }
        },
        refuseApprovement: function (e) {
            var modelId = $("#modelId").val();
            var window = $("#TaskCommandWindow");
            window.kendoWindow({
                width: "550px",
                height: "auto",
                modal: true,
                resizable: false,
                close: onCloseCommandWindow,
                actions: ["Close"]
            });
            window.data("kendoWindow").contractId = modelId;
            window.data("kendoWindow").dialogCallback = function () {
                $(e.target).hide();
                $("#doApprovementBtn" + uiId).hide();
            };
            window.data("kendoWindow").title('Основание отказа');
            window.data("kendoWindow").setOptions({
                width: 550,
                height: 'auto'
            });
            window.data("kendoWindow").refresh('/OBKContract/RefuseReasonDlg');
            window.data("kendoWindow").center();
            window.data("kendoWindow").open();
        },
        showRefuseReason: function (e) {
            var modelId = $("#modelId").val();
            var window = $("#TaskCommandWindow");
            window.kendoWindow({
                width: "550px",
                height: "auto",
                modal: true,
                resizable: false,
                close: onCloseCommandWindow,
                actions: ["Close"]
            });
            window.data("kendoWindow").title('Основание отказа');
            window.data("kendoWindow").setOptions({
                width: 550,
                height: 'auto'
            });
            window.data("kendoWindow").refresh('/OBKContract/ShowRefuseReasonDlg?contractId=' + modelId);
            window.data("kendoWindow").center();
            window.data("kendoWindow").open();
        },
        signContract: function (e) {
            var question = "Вы подтверждаете действие \"Подписать\"?";
            if (confirm(question)) {
                signContract();
            }
        },
        register: function (e) {
            if (!$(e.target).hasClass("disabled")) {
                var question = "Вы подтверждаете действие \"Зарегистрировать\"?";
                if (confirm(question)) {
                    var modelId = $("#modelId").val();
                    $(e.target).addClass("disabled");
                    $.ajax({
                        type: 'POST',
                        url: '/OBKContract/RegisterContract',
                        data: JSON.stringify({ contractId: modelId }),
                        contentType: 'application/json; charset=utf-8',
                        success: function (result) {
                            $(e.target).hide();
                            $("#attachContractBtn" + uiId).removeClass("disabled");
                            var window = $("#WindowContractRegistered" + uiId);
                            $("#registeredNumber").text(result);
                            window.kendoWindow({
                                width: "400px",
                                height: "auto",
                                modal: true,
                                resizable: false,
                                actions: ["Close"]
                            });
                            window.data("kendoWindow").title('Сообщение');
                            window.data("kendoWindow").setOptions({
                                width: 400,
                                height: 'auto'
                            });
                            window.data("kendoWindow").center();
                            window.data("kendoWindow").open();
                        },
                        complete: function () {
                            $(e.target).removeClass("disabled");
                        }
                    });
                }
            }
        },
        attachContract: function (e) {
            if (!$(e.target).hasClass("disabled")) {

                var modelId = $("#modelId").val();
                var window = $("#TaskCommandWindow");
                window.kendoWindow({
                    width: "550px",
                    height: "auto",
                    modal: true,
                    resizable: false,
                    actions: ["Close"]
                });
                window.data("kendoWindow").dialogCallback = function (completed) {
                    if (completed) {
                        $(e.target).hide();
                    }
                };
                window.data("kendoWindow").title('Прикрепление документа');
                window.data("kendoWindow").setOptions({
                    width: 550,
                    height: 'auto'
                });
                window.data("kendoWindow").refresh('/OBKContract/UploadContract?contractId=' + modelId);
                window.data("kendoWindow").center();
                window.data("kendoWindow").open();
            }
            else {
                alert("Зарегистрируйте договор");
            }
        },
        printForm: function (e) {
            var modelId = $("#modelId").val();
            window.open('/OBKContract/GetContractTemplatePdf?id=' + modelId);
        }
    });
    kendo.bind($("#splitter" + uiId), viewModel);
    $("#WindowContractRegisteredCancel" + uiId).click(function () {
        $("#WindowContractRegistered" + uiId).data("kendoWindow").close();
    });
    //resizeGrid("#contracHistoryGrid" + uiId);

    function signContract() {
        debugger;
        kendo.ui.progress($("#mainWindowLoader"), true);
        $.ajax({
            type: 'GET',
            url: '/OBKContract/SignData?contractId=' + $('#modelId').val(),
            success: function (result) {
                debugger;
                startSign(result.data, $('#modelId').val(), saveSignedContract);
            },
            complete: function () {
                kendo.ui.progress($("#mainWindowLoader"), false);
            }
        });
        function saveSignedContract(signedData, contractId) {
            kendo.ui.progress($("#contractLoader" + uiId), true);
            var data = {
                contractId: $('#modelId').val(),
                signedData: signedData
            };
            var json = JSON.stringify(data);
            $.ajax({
                type: 'POST',
                url: '/OBKContract/SaveSignedContract',
                contentType: 'application/json; charset=utf-8',
                data: json,
                success: function (result) {
                    debugger;
                    $('#signContractBtn' + uiId).hide();
                    alert("Договор подписан");
                },
                complete: function () {
                    kendo.ui.progress($("#contractLoader" + uiId), false);
                }
            });
        }
    }
}

function initFilterOBKContract(uiId) {

    $("#toPay" + uiId).click(function () {
        debugger;
        var grid = $('#gridContractAll' + uiId).data("kendoGrid");
        var selectedItem = grid.dataItem(grid.select());
        $.ajax({
            type: 'POST',
            url: '/OBKContract/SendToPay',
            data: JSON.stringify({ contractId: selectedItem.ContractId }),
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
            },
            complete: function () {

            }
        });
    });


    $('#toWork' + uiId).click(function () {
        var grid = $('#gridContractAll' + uiId).data("kendoGrid");
        var selectedItem = grid.dataItem(grid.select());
        if (selectedItem) {
            var window = $("#TaskCommandWindow");
            window.kendoWindow({
                width: "550px",
                height: "auto",
                modal: true,
                resizable: false,
                close: onCloseCommandWindow,
                actions: ["Close"]
            });
            window.data("kendoWindow").stageId = selectedItem.ContractStageId;
            window.data("kendoWindow").dialogCallback = function () {
                grid.dataSource.read();
            };
            window.data("kendoWindow").title('Согласовать');
            window.data("kendoWindow").setOptions({
                width: 550,
                height: 'auto'
            });
            window.data("kendoWindow").refresh('/OBKContract/SetExecutor');
            window.data("kendoWindow").center();
            window.data("kendoWindow").open();
        }
        else {
            alert("Выберите договор!");
        }
    });

    $("#find" + uiId).click(function () {
        var findType = $("#findType" + uiId).val();
        if (findType != '') {
            $filter = new Array();
            if (findType == 0) {
                $filter.push({ field: "StageStatusCode", operator: "eq", value: "inWork" });
            }
            else if (findType == 1) {
                $filter.push({ field: "StageStatusCode", operator: "eq", value: "inWork" });
                $filter.push({ field: "StageCOZResult", operator: "eq", value: 1 });
                $filter.push({ field: "StageUOBKResult", operator: "eq", value: 1 });
                $filter.push({ field: "StageDEFResult", operator: "eq", value: 1 });
            }
            else if (findType == 2) {
                var correctionFilters = new Array();
                correctionFilters.push({ field: "StageCOZResult", operator: "eq", value: 2 });
                correctionFilters.push({ field: "StageUOBKResult", operator: "eq", value: 2 });
                correctionFilters.push({ field: "StageDEFResult", operator: "eq", value: 2 });
                $filter.push({ field: "StageStatusCode", operator: "eq", value: "inWork" });
                $filter.push({ logic: "or", filters: correctionFilters });
            }
            var grid = $("#gridContractAll" + uiId).data("kendoGrid");
            grid.dataSource.filter({
                logic: "and",
                filters: $filter
            });
        }
    });

    $("#findTypeActiveBtn" + uiId).click(function () {
        var findType = $("#findTypeActiveContract" + uiId).val();
        if (findType != '') {
            $filter = new Array();
            if (findType == 0) {
                $filter.push({ field: "StageStatusCode", operator: "eq", value: "active" });
            }
            else {
                $filter.push({ field: "StageStatusCode", operator: "eq", value: "active" });
                $filter.push({ field: "ContractStatusId", operator: "eq", value: findType });
            }
            var grid = $("#gridContractAll" + uiId).data("kendoGrid");
            grid.dataSource.filter({
                logic: "and",
                filters: $filter
            });
        }
    });

    setToWorkBtnVisibility(uiId);

    setFindAreaVisibility(uiId);
}

function setToWorkBtnVisibility(uiId) {
    var links = $("#panelbar" + uiId).find(".k-state-selected");
    if (links.length > 0) {
        if (links.eq(0).attr("itemid") == "inQueue") {
            $('button[id="toWork' + uiId + '"]').show();
        }
        else {
            $('button[id="toWork' + uiId + '"]').hide();
        }
    }
}

function setFindAreaVisibility(uiId) {
    var links = $("#panelbar" + uiId).find(".k-state-selected");
    if (links.length > 0) {
        if (links.eq(0).attr("itemid") == "inWork") {
            $('div[id="lblStateBlock' + uiId + '"]').show();
            $('button[id="find' + uiId + '"]').show();
            $('button[id="find' + uiId + '"]').parent().show();
            $("#findType" + uiId).data("kendoDropDownList").wrapper.show();
            $("#findType" + uiId).data("kendoDropDownList").wrapper.parent().show();
            $("#findType" + uiId).data("kendoDropDownList").select(0);

            $('button[id="findTypeActiveBtn' + uiId + '"]').hide();
            $("#findTypeActiveContract" + uiId).data("kendoDropDownList").wrapper.hide();
            $("#findTypeActiveContract" + uiId).data("kendoDropDownList").select(0);
        }
        else if ((links.eq(0).attr("itemid") == "active")) {
            $('div[id="lblStateBlock' + uiId + '"]').show();
            $('button[id="findTypeActiveBtn' + uiId + '"]').show();
            $('button[id="findTypeActiveBtn' + uiId + '"]').parent().show();
            $("#findTypeActiveContract" + uiId).data("kendoDropDownList").wrapper.show();
            $("#findTypeActiveContract" + uiId).data("kendoDropDownList").wrapper.parent().show();
            $("#findTypeActiveContract" + uiId).data("kendoDropDownList").select(0);

            $('button[id="find' + uiId + '"]').hide();
            $("#findType" + uiId).data("kendoDropDownList").wrapper.hide();
            $("#findType" + uiId).data("kendoDropDownList").select(0);
        }
        else {
            $('div[id="lblStateBlock' + uiId + '"]').hide();
            $('button[id="find' + uiId + '"]').hide();
            $('button[id="find' + uiId + '"]').parent().hide();
            $("#findType" + uiId).data("kendoDropDownList").wrapper.hide();
            $("#findType" + uiId).data("kendoDropDownList").wrapper.parent().hide();
            $("#findType" + uiId).data("kendoDropDownList").select(0);

            $('button[id="findTypeActiveBtn' + uiId + '"]').hide();
            $("#findTypeActiveContract" + uiId).data("kendoDropDownList").wrapper.hide();
            $("#findTypeActiveContract" + uiId).data("kendoDropDownList").select(0);
        }
    }
}

function panelObkContractSelect(e) {
    var selectType = $(e.item).find("> .k-link").attr('ItemType');
    if (selectType !== null) {
        var selectValue = $(e.item).find("> .k-link").attr('ItemId');
        var gridId = $(e.item).find("> .k-link").attr('ModelId');
        var grid = $("#gridContractAll" + gridId).data("kendoGrid");
        var filter = new Array();
        if (selectType === "StageStatusCode") {
            filter.push({ field: "StageStatusCode", operator: "eq", value: selectValue });
        }
        if (selectValue === '') {
            grid.dataSource.filter([]);
        } else {
            grid.dataSource.filter({
                logic: "and",
                filters: filter
            });
        }

        var panelBar = $(e.item).closest(".k-panelbar");
        var panelId = panelBar.attr("id");
        var uiId = panelId.replace(/\panelbar/g, '');
        setToWorkBtnVisibility(uiId);
        setFindAreaVisibility(uiId);
    }
}