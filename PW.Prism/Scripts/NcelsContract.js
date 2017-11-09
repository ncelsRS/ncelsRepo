
function InitFilterContractAllGrid(name) {
    $("#reload" + name).click(function (e) {
        var grid = $("#gridContractAll" + name).data("kendoGrid");
        grid.dataSource.read();
    });


    $("#findText" + name).keypress(function (d) {
        if (d.keyCode == 13) {
            $("#find" + name).click();
        }
    });
    $("#find" + name).click(function (e) {

        var text = $("#findText" + name).val();
        var findType = $("#findType" + name).val();
        var grid = $("#gridContractAll" + name).data("kendoGrid");
        if (text != '') {
            $filter = new Array();
            if (findType == 0) {

                $filter.push({ field: "Number", operator: "contains", value: text });
                $filter.push({ field: "ManufacturerName", operator: "contains", value: text });
                $filter.push({ field: "ManufacturerCountry", operator: "contains", value: text });
                $filter.push({ field: "ApplicantName", operator: "contains", value: text });
                $filter.push({ field: "ApplicantCountry", operator: "contains", value: text });
                $filter.push({ field: "Login", operator: "contains", value: text });
                $filter.push({ field: "ApplicantCurrency", operator: "contains", value: text });
                $filter.push({ field: "DocumentTypeName", operator: "contains", value: text });
                $filter.push({ field: "DoverennostNumber", operator: "contains", value: text });
            }
            //if (findType == 1) {
            //    $filter.push({ field: "Number", operator: "contains", value: text });
            //}
            //if (findType == 2) {
            //    $filter.push({ field: "ResponsibleValue", operator: "contains", value: text });
            //}
            //if (findType == 3) {
            //    $filter.push({ field: "CorrespondentsInfo", operator: "contains", value: text });
            //    $filter.push({ field: "CorrespondentsValue", operator: "contains", value: text });
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
            var grid = $("#gridContractAll" + name).data("kendoGrid");
            grid.dataSource.read();
        }
    });


}

function contractGridDataBounded(e) {
    debugger;
    var grid = $("#" + e.sender.table.context.id).data("kendoGrid");
    var modelId = $("#" + e.sender.table.context.id).data('ModelId');
    var panelBar = $("#panelbar" + modelId).data("kendoPanelBar");
    var selectedItem = panelBar.select();
    if (selectedItem) {
        var selectValue = $(selectedItem).find("> .k-link").attr('ItemId');
        if (selectValue === "7") {
            grid.showColumn(0);
        } else {
            grid.hideColumn(0);
        }
    }
}

function panelContractSelect(e) {
    debugger;
    var selectType = $(e.item).find("> .k-link").attr('ItemType');
    if (selectType != null) {
        var selectValue = $(e.item).find("> .k-link").attr('ItemId');
        var gridId = $(e.item).find("> .k-link").attr('ModelId');
        var grid = $("#gridContractAll" + gridId).data("kendoGrid");
        var filter = new Array();
        if (selectType === "StatusCode") {

            filter.push({ field: "StatusCode", operator: "eq", value: selectValue });
        }
        if (selectValue === '') {
            grid.dataSource.filter([]);
        } else {
            grid.dataSource.filter({
                logic: "and",
                filters: filter
            });
        }
    }
}

function getContractDetails(parameters, number, type) {
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
        var url;
        if (type == 4) {
            url = "/Contract/Card1";
        } else if (type == 5) {
            url = "/Contract/Card2";
        }
        if (url != null) {
            $.ajax({
                url: url + "?id=" + parameters,
                //type: "POST",
                success: function (result) {
                    // refreshes partial view
                    $(idContent).html(result);
                }
            });
        }

    } else {

        var itesm = $('#' + parameters)[0].parentElement.getAttribute('id').split('-')[1];
        if (itesm) {
            $("#tabstrip").data("kendoTabStrip").select(itesm - 1);
        }
    }
    //alert(parameters);
}


function InitializeNcelsContract(name, type) {
    var validator = $("#prjDocForm" + name).kendoValidator().data("kendoValidator");


    var viewModel = kendo.observable({
        document: {},
        change: function () {
            validator.validate();
            this.set("hasChanges", true);
        },
        initButton: function () {

            var state = this.get("document.StateType");
            if (state == 0) {
                this.set("hasRegister", true);
            } else {
                this.set("hasRegister", false);
            }
            if (state == 1 || state == 6) {
                this.set("hasSend", true);
            } else {
                this.set("hasSend", false);
            }
            if (state == 1) {
                this.set("hasBuild", true);
            } else {
                this.set("hasBuild", false);
            }
            if (state == 4 || state == 5) {
                this.set("hasReject", true);
            } else {
                this.set("hasReject", false);
            }
            if (state > 0) {
                this.set("hasPrint", true);
            } else {
                this.set("hasPrint", false);
            }
        },
        hasChanges: false,
        hasRegister: false,
        register: function (e) {
            e.preventDefault();
            if (validator.validate()) {
                this.set("hasChanges", false);
                registerData();
            };

        },

        hasBuild: true,
        sendNote: function (e) {
            e.preventDefault();
            addProjectOutClick();
        },

        hasPrint: true,
        print: function (e) {
            e.preventDefault();
            PrintDocumetnt(name);
        },
    });
    kendo.bind($("#docToolbar" + name), viewModel);


    function addProjectOutClick(e) {

        var tabStrip = $("#tabstrip").data("kendoTabStrip");
        var guid = guidGen();
        var res = "docProjectAddContent" + guid;
        var nameAjax = '#docProjectAddContent' + guid;
        tabStrip.append({
            text: 'Новый проект',
            content: '<div id="' + res + '"> </div>'

        });

        tabStrip.select(tabStrip.items().length - 1);

        var gridElement = $(nameAjax);

        gridElement.height($(window).height() - 100);

        $.ajax({
            url: "/Contract/CardOut?id=" + name + "&guid=" + guid,
            //type: "POST",
            success: function (result) {
                // refreshes partial view
                $(nameAjax).html(result);
            }
        });
    }
}

function InitializeDataProjectContract(name, answerId) {
    var validator = $("#prjDocForm" + name).kendoValidator().data("kendoValidator");


    var viewModel = kendo.observable({
        document: {},
        change: function () {
            validator.validate();
            this.set("hasChanges", true);
        },
        initButton: function () {

            var state = this.get("document.StateType");
            if (state == 0) {
                this.set("hasRegister", true);
            } else {
                this.set("hasRegister", false);
            }
            if (state == 1 || state == 6) {
                this.set("hasSend", true);
            } else {
                this.set("hasSend", false);
            }
            if (state == 1) {
                this.set("hasBuild", true);
            } else {
                this.set("hasBuild", false);
            }
            if (state == 4 || state == 5) {
                this.set("hasReject", true);
            } else {
                this.set("hasReject", false);
            }
            if (state > 0) {
                this.set("hasPrint", true);
            } else {
                this.set("hasPrint", false);
            }
        },
        hasChanges: false,
        hasRegister: false,
        register: function (e) {
            e.preventDefault();
            if (validator.validate()) {
                this.set("hasChanges", false);
                registerData();
            };

        },
        hasSend: false,
        send: function (e) {
            e.preventDefault();
            reviewData();
        },
        hasReject: false,
        reject: function (e) {
            e.preventDefault();
            rejectData();
        },
        hasBuild: false,
        build: function (e) {
            e.preventDefault();
            buildData();
        },
        hasExecute: false,
        execute: function (e) {
            e.preventDefault();
            excludeData();
        },
        hasPrint: false,
        print: function (e) {
            e.preventDefault();
            PrintDocumetnt(this.get("document.Id"));
        },
        hasSave: false,
        save: function (e) {
            e.preventDefault();
            this.set("hasChanges", false);
            sendData();
        },
        deleteDoc: function (e) {
            e.preventDefault();
            DeleteDocumetnt(this.get("document.Id"));
        },
        dictionaryView: function (e) {
            e.preventDefault();
            DictionaryView(name, 'True', this);
        },
    });
    function loadDocument() {
        kendo.ui.progress($('#loader' + name), true);
        $.ajax({
            type: 'get',
            url: '/Contract/DocumentRead?Id=' + name + '&documentId=' + answerId,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                result = JSON.parse(result);
                // alert(JSON.stringify(result));
                viewModel.set("document", result);
                viewModel.initButton();
                //viewModel.person = JSON.stringify(result);
                InitializePropertyProject(name, viewModel, 'Outgoing');
                kendo.bind($("#prjDocForm" + name), viewModel);
                kendo.ui.progress($('#loader' + name), false);
            },
            complete: function () {
                //  alert('Success! User Loaded!');
                InitializeStatusBar(name, viewModel);
            }
        });
    }
    function excludeData() {
        kendo.ui.progress($('#loader' + name), true);

        var json = JSON.stringify(viewModel.get('document'));


        $.ajax({
            type: 'POST',
            url: '/ProjectDoc/DocumentExclude',
            data: json,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                result = JSON.parse(result);
                if (result.State == true) {
                    viewModel.set("document", result.document);
                    viewModel.initButton();
                    CardExcludeSuccess();
                };
            },
            complete: function () {
                kendo.ui.progress($('#loader' + name), false);
            }
        });
    };
    function registerData() {
        kendo.ui.progress($('#loader' + name), true);

        var json = JSON.stringify(viewModel.get('document'));


        $.ajax({
            type: 'POST',
            url: '/ProjectDoc/DocumentRegister',
            data: json,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                result = JSON.parse(result);
                if (result.State == true) {
                    viewModel.set("document", result.document);
                    viewModel.initButton();
                    CardRegisterSuccess(result.document.Number, result.document.DocumentDate);
                };
            },
            complete: function () {
                kendo.ui.progress($('#loader' + name), false);
            }
        });
    };

    function reviewData() {
        kendo.ui.progress($('#loader' + name), true);

        var json = JSON.stringify(viewModel.get('document'));


        $.ajax({
            type: 'POST',
            url: '/ProjectDoc/DocumentReview',
            data: json,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                result = JSON.parse(result);
                if (result.State == true) {
                    viewModel.set("document", result.document);
                    viewModel.initButton();
                    CardReview2Success();
                };
            },
            complete: function () {
                kendo.ui.progress($('#loader' + name), false);
            }
        });
    };
    function buildData() {
        kendo.ui.progress($('#loader' + name), true);

        var json = JSON.stringify(viewModel.get('document'));


        $.ajax({
            type: 'POST',
            url: '/ProjectDoc/DocumentBuild',
            data: json,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                result = JSON.parse(result);
                if (result.State == true) {
                    viewModel.set("document", result.document);
                    viewModel.initButton();
                    $.ajax({
                        type: 'get',
                        url: '/Contract/DocumentAttachFiles?attachPath=' + viewModel.get('document.AttachPath') + '&isArchive=' + viewModel.get('document.IsArchive'),
                        contentType: 'application/json; charset=utf-8',
                        success: function (result) {
                            InitializeDocumentAttachList(name, viewModel, result);
                        },
                        complete: function () {
                        }
                    });
                };
            },
            complete: function () {
                kendo.ui.progress($('#loader' + name), false);
            }
        });
    };
    function rejectData() {
        kendo.ui.progress($('#loader' + name), true);

        var json = JSON.stringify(viewModel.get('document'));


        $.ajax({
            type: 'POST',
            url: '/ProjectDoc/DocumentReject',
            data: json,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                result = JSON.parse(result);
                if (result.State == true) {
                    viewModel.set("document", result.document);
                    viewModel.initButton();
                    CardRejectSuccess();
                };
            },
            complete: function () {
                kendo.ui.progress($('#loader' + name), false);
            }
        });
    };
    function sendData() {
        kendo.ui.progress($('#loader' + name), true);

        var json = JSON.stringify(viewModel.get('document'));


        $.ajax({
            type: 'POST',
            url: '/ProjectDoc/DocumentUpdate',
            data: json,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                if (result == 'True') {
                    CardSaveSuccess();

                };
            },
            complete: function () {
                kendo.ui.progress($('#loader' + name), false);
            }
        });
    };

    loadDocument();

}

function takeToWorkContract(e) {
    debugger;
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    var window = $("#TaskCommandWindow");

    window.kendoWindow({
        width: "650",
        height: "auto",
        modal: true,
        title: 'Отправить в работу',
        actions: ["Close"]
    });
    $("#TaskCommandWindow").data("kendoWindow").gridId = e.delegateTarget.id;
    window.data("kendoWindow").refresh('/Contract/TakeToWork?id=' + dataItem.Id);
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}

function openContractCard(parameters, number) {
    if (docArray.indexOf(parameters.toLowerCase()) !== -1)
        docArray.splice(docArray.indexOf(parameters.toLowerCase()), 1);
    var element = document.getElementById(parameters);
    if (element === null) {
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
            url: "/Contract/ContractCard?contractId=" + parameters,
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

function InitContractCard(uiId) {
    debugger;
    function canClickBtn(btn) {
        if ($(btn).hasClass('k-state-disabled') ||
            $(btn).hasClass('disabled') ||
            $(btn).is('[disabled]'))
            return false;
        return true;
    }

    function commentRowSelect(e) {
        debugger;
        var selectedRows = this.select();
        if (selectedRows.length > 0) {
            $('#editCommentBtn' + uiId).removeClass('k-state-disabled');
            $('#sendCommentsBtn' + uiId).removeClass('k-state-disabled');
        }
        $("#contractCommentsGrid" + uiId+' tr').find('[type=checkbox]').prop('checked', false);
        $("#contractCommentsGrid" + uiId+' tr.k-state-selected').find('[type=checkbox]').prop('checked', true);
    }
    var commentsGrid = $("#contractCommentsGrid" + uiId).data("kendoGrid");
    commentsGrid.bind("change", commentRowSelect);
    commentsGrid.bind("dataBound",
        function (e) {
            debugger;
            $("#contractCommentsGrid" + uiId + " .checkbox").bind("change", function (e) {
                if (e.target.checked) {
                    $('#editCommentBtn' + uiId).removeClass('k-state-disabled');
                    $('#sendCommentsBtn' + uiId).removeClass('k-state-disabled');
                }
                $(e.target).closest("tr").toggleClass("k-state-selected");
            });
            if (!$('#editCommentBtn' + uiId).hasClass('k-state-disabled'))
                $('#editCommentBtn' + uiId).addClass('k-state-disabled');
            if (!$('#sendCommentsBtn' + uiId).hasClass('k-state-disabled'))
                $('#sendCommentsBtn' + uiId).addClass('k-state-disabled');
        });
    $('#sign' + uiId).attr("data-bind", "click: showSignBtns");
    $('#sign' + uiId).html("<span class='glyphicon glyphicon-edit'></span> " + $('#sign' + uiId).text());
    $('#signWithoutDigCert' + uiId).attr("data-bind", "click: sendToSignWithoutDc");
    $('#signWithDigCert' + uiId).attr("data-bind", "click: sendToSignWithtDc");
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
        },
        addComment: function (e) {
            if (!canClickBtn(e.target)) return;
            addContractComment($('#contractId' + uiId).val());
        },
        editComment: function (e) {
            debugger;
            if (!canClickBtn(e.target)) return;
            var selectedItem = commentsGrid.dataItem(commentsGrid.select());
            if (selectedItem && !selectedItem.Sended) {
                addContractComment($('#contractId' + uiId).val(), selectedItem.Id);
            }
        },
        sendComments: function(e) {
            var idsToSend = [];            
            var ds = commentsGrid.dataSource.view();

            for (var i = 0; i < ds.length; i++) {
                var row = commentsGrid.table.find("tr[data-uid='" + ds[i].uid + "']");
                var checkbox = $(row).find(".checkbox");
                if (checkbox.is(":checked") && !ds[i].Sended) {
                    idsToSend.push(ds[i].Id);
                }
            }
            if (idsToSend.length > 0) {
                var json = JSON.stringify(idsToSend);
                kendo.ui.progress($("#contractCardLoader" + uiId), true);
                $.ajax({
                    type: 'POST',
                    url: '/Contract/SendComments',
                    contentType: 'application/json; charset=utf-8',
                    data: json,
                    success: function (result) {
                        debugger;
                        alert("Замечания отправлены");
                        commentsGrid.dataSource.read();
                    },
                    complete: function () {
                        kendo.ui.progress($("#contractCardLoader" + uiId), false);
                    }
                });
            }
        },
        addNewFileVersion: function (e) {
            debugger;
            if (!canClickBtn(e.target)) return;
            var window = $("#TaskCommandWindow");
            window.kendoWindow({
                width: "650",
                height: "auto",
                modal: true,
                title: 'Добавление новой версии договора'
            });
            $("#TaskCommandWindow").data("kendoWindow").contractFilesGrid = 'contractAttachesGrid' + uiId;
            window.data("kendoWindow").refresh('/Contract/AddNewContractVersion?contractId=' + $('#contractId' + uiId).val());
            window.data("kendoWindow").title('Добавление новой версии договора');
            window.data("kendoWindow").setOptions({
                width: "650",
                height: "auto"
            });
            window.data("kendoWindow").center();
            window.data("kendoWindow").open();
        },
        sendToHeadAgreement: function (e) {
            debugger;
            if (!canClickBtn(e.target)) return;
            expAgreementProc.sendToAgreement({
                documentId: $('#contractId' + uiId).val(),
                documentTypeCode: '1',
                activityTypeCode: '1',
                loaderId: 'contractCardLoader' + uiId,
                callBack: function (r) {
                    debugger;
                    alert('отправлено на согласование');
                    $('#addCommentBtn' + uiId).addClass('k-state-disabled');
                    $('#sendToHeadAgreement' + uiId).addClass('k-state-disabled');
                    $('#addCommentBtn1' + uiId).addClass('k-state-disabled');
                    $('#addNewFile' + uiId).addClass('k-state-disabled');
                }

            });
        },
        approve: function (e) {
            debugger;
            if (!canClickBtn(e.target)) return;
            var window = $("#TaskCommandWindow");
            window.kendoWindow({
                width: "650",
                height: "auto",
                modal: true,
                title: 'Согласовать',
                actions: ["Close"]
            });
            $("#TaskCommandWindow").data("kendoWindow").dialogCallback = function () {
                alert('Согласованно');
                $('#approveContract' + uiId).addClass('k-state-disabled');
            };
            window.data("kendoWindow").refresh('/Contract/Approve?contractId=' + $('#contractId' + uiId).val());
            window.data("kendoWindow").title('Согласовать');
            window.data("kendoWindow").setOptions({
                width: "650",
                height: "auto"
            });
            window.data("kendoWindow").center();
            window.data("kendoWindow").open();
        },
        showSignBtns: function (e) {
            if (!canClickBtn(e.target)) return;
            $('#signWithoutDigCert' + uiId).closest('.k-popup').data("kendoPopup").open();
        },
        sendToSignWithoutDc: function (e) {
            if (!canClickBtn(e.target)) return;
            sendToSign(false);
        },
        sendToSignWithtDc: function (e) {
            if (!canClickBtn(e.target)) return;
            sendToSign(true);
        },
        registerWithoutDc: function (e) {
            if (!canClickBtn(e.target)) return;
            debugger;
            registerContract(false);
        },
        registerWithDc: function (e) {
            if (!canClickBtn(e.target)) return;
            debugger;
            registerContract(true);
        },
        signContract: function (e) {
            if (!canClickBtn(e.target)) return;
            signContract();
        }
    });
    kendo.bind($("#splitter" + uiId), viewModel);
    kendo.bind($('#signWithoutDigCert' + uiId), viewModel);
    kendo.bind($('#signWithDigCert' + uiId), viewModel);
    function sendToSign(withDc) {
        debugger;
        kendo.ui.progress($("#contractCardLoader" + uiId), true);
        $.ajax({
            type: 'POST',
            url: '/Contract/SendToSign?contractId=' + $('#contractId' + uiId).val() + "&withDc=" + withDc,
            success: function (result) {
                debugger;
                $('#signWithoutDigCert' + uiId).addClass('k-state-disabled');
                $('#signWithDigCert' + uiId).addClass('k-state-disabled');
                alert("Отправлен на подписание");
            },
            complete: function () {
                kendo.ui.progress($("#contractCardLoader" + uiId), false);
            }
        });
    }
    function registerContract(withDc) {
        if (withDc) {
            kendo.ui.progress($("#contractCardLoader" + uiId), true);
            $.ajax({
                type: 'POST',
                url: '/Contract/RegisterContract?contractId=' + $('#contractId' + uiId).val(),
                success: function (result) {
                    debugger;
                    $('#registerWithDc' + uiId).addClass('k-state-disabled');
                    alert("Зарегистрирован");
                },
                complete: function () {
                    kendo.ui.progress($("#contractCardLoader" + uiId), false);
                }
            });
        } else {
            var window = $("#TaskCommandWindow");
            window.kendoWindow({
                width: "650",
                height: "auto",
                modal: true,
                title: 'Регистрация договора'
            });
            $("#TaskCommandWindow").data("kendoWindow").dialogCallback = function () {
                $('#registerWithoutDc' + uiId).addClass('k-state-disabled');
            };
            window.data("kendoWindow").refresh('/Contract/AddNewContractVersion?contractId=' +
                $('#contractId' + uiId).val());
            window.data("kendoWindow").title('Регистрация договора');
            window.data("kendoWindow").setOptions({
                width: "650",
                height: "auto"
            });
            window.data("kendoWindow").center();
            window.data("kendoWindow").open();
        }
    }
    function signContract() {
        debugger;
        kendo.ui.progress($("#mainWindowLoader"), true);
        $.ajax({
            type: 'GET',
            url: '/Contract/SignData?contractId=' + $('#contractId' + uiId).val(),
            success: function (result) {
                debugger;
                startSign(result.data, $('#contractId' + uiId).val(), saveSignedContract);
            },
            complete: function () {
                kendo.ui.progress($("#mainWindowLoader"), false);
            }
        });
        function saveSignedContract(signedData, contractId) {
            kendo.ui.progress($("#contractCardLoader" + uiId), true);
            var data = {
                contractId: $('#contractId' + uiId).val(),
                signedData: signedData
            };
            var json = JSON.stringify(data);
            $.ajax({
                type: 'POST',
                url: '/Contract/SaveSignedContract',
                contentType: 'application/json; charset=utf-8',
                data: json,
                success: function (result) {
                    debugger;
                    $('#signContract' + uiId).addClass('k-state-disabled');
                    alert("Договор подписан");
                },
                complete: function () {
                    kendo.ui.progress($("#contractCardLoader" + uiId), false);
                }
            });
        }        
    }

    function addContractComment(contractId, commentId) {
        debugger;
        var windowTitle = commentId ? "Редактирование замечания" : "Добавление замечания";
        if (!commentId)
            commentId = "00000000-0000-0000-0000-000000000000";
        var window = $("#TaskCommandWindow");
        window.kendoWindow({
            width: "650",
            height: "auto",
            modal: true,
            title: windowTitle,
            actions: ["Close"]
        });
        $("#TaskCommandWindow").data("kendoWindow").contractCommentsGridId = 'contractCommentsGrid' + uiId;
        $("#TaskCommandWindow").data("kendoWindow").dialogCallback = function() {
            $('#sendToHeadAgreement' + uiId).addClass('k-state-disabled');
        };
        window.data("kendoWindow").refresh('/Contract/AddComment?contractId=' + contractId +"&commentId="+commentId);
        window.data("kendoWindow").title(windowTitle);
        window.data("kendoWindow").setOptions({
            width: "650",
            height: "auto"
        });
        window.data("kendoWindow").center();
        window.data("kendoWindow").open();
    }
}
