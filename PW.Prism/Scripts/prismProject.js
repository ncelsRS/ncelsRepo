
function InitializeSizeProject(name) {
    var gridElement = $("#pwContentRightId" + name);
    gridElement.height($(window).height() - 120);
}

function InitializeDataProject(name, taskId,type) {
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
        buildPer1: function (e) {
            e.preventDefault();
            buildData2('Per1');
        },
        buildPer2: function (e) {
            e.preventDefault();
            buildData2('Per2');
        },
        buildPer3: function (e) {
            e.preventDefault();
            buildData2('Per3');
        },
        buildZfk1: function (e) {
            e.preventDefault();
            buildData2('Zfk1');
        },
        buildZfc1: function (e) {
            e.preventDefault();
            buildData2('Zfc1');
        },
        buildZkl1: function (e) {
            e.preventDefault();
            buildData2('Zkl1');
        },
        buildZkl2: function (e) {
            e.preventDefault();
            buildData2('Zkl2');
        },
        buildZkl3: function (e) {
            e.preventDefault();
            buildData2('Zkl3');
        },
        buildZkl4: function (e) {
            e.preventDefault();
            buildData2('Zkl4');
        },
        buildIsp1: function (e) {
            e.preventDefault();
            buildData2('Isp1');
        },
        buildNap1: function (e) {
            e.preventDefault();
            buildData2('Nap1');
        },
        buildNap2: function (e) {
            e.preventDefault();
            buildData2('Nap2');
        },
        buildNap3: function (e) {
            e.preventDefault();
            buildData2('Nap3');
        },
        buildNap4: function (e) {
            e.preventDefault();
            buildData2('Nap4');
        },
        buildAkt1: function (e) {
            e.preventDefault();
            buildData2('Akt1');
        },
        buildPfk1: function (e) {
            e.preventDefault();
            buildData2('Pfk1');
        },
        buildFkt1: function (e) {
            e.preventDefault();
            buildData2('Fkt1');
        },
        buildFkl1: function (e) {
            e.preventDefault();
            buildData2('Fkl1');
        },
        buildPfc1: function (e) {
            e.preventDefault();
            buildData2('Pfc1');
        },
        buildFct1: function (e) {
            e.preventDefault();
            buildData2('Fct1');
        },
        buildPes1: function (e) {
            e.preventDefault();
            buildData2('Pes1');
        },
        buildExp1: function (e) {
            e.preventDefault();
            buildData2('Exp1');
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
            url: '/ProjectDoc/DocumentRead?Id=' + name + '&taskId=' + taskId + '&type=' + type,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                result = JSON.parse(result);
                // alert(JSON.stringify(result));
                viewModel.set("document", result);
                viewModel.initButton();
                //viewModel.person = JSON.stringify(result);
                InitializePropertyProject(name, viewModel, type);
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
                    CardBuildSuccess();
                };
            },
            complete: function () {
                kendo.ui.progress($('#loader' + name), false);
            }
        });
    };

    function buildData2(type) {
        kendo.ui.progress($('#loader' + name), true);

        var json = JSON.stringify(viewModel.get('document'));


        $.ajax({
            type: 'POST',
            url: '/ProjectDoc/DocumentBuildWithParam?type=' + type,
            data: json,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                result = JSON.parse(result);
                if (result.State == true) {
                    viewModel.set("document", result.document);
                    viewModel.initButton();
                    CardBuildSuccess();
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

function InitializeDocumentAttachList(name, viewModel, initialFiles) {
    $("#files" + name)
        .kendoUpload({
            localization: {
                select: 'Выбрать файл...'
            },
            // multiple: true,
            async: {
                saveUrl: "/Upload/save",
                removeUrl: "/Upload/remove",
                autoUpload: true
            },
            upload: function (e) {

                e.data = { documentId: viewModel.get('document.AttachPath') };
            },
            remove: function (e) {
                e.data = { documentId: viewModel.get('document.AttachPath') };
            },
            complete: function (e) {
                //console.log('.k-upload', $("#files" + name).closest(".k-upload").find("a"));
                //var data =
                //data[0].onclick = function() {
                //    fileView(viewModel.get('document.Id'), data[0].getAttribute('fileName'));
                //};
                var files = $("#files" + name).closest(".k-upload").find("a");
                $.each(files,
                    function (i, file) {
                        file.onclick = function () {
                            fileView(viewModel.get('document.AttachPath'), file.getAttribute('fileName'));
                        };
                    });
                var filesButton = $("#files" + name).closest(".k-upload").find("button");

                $.each(filesButton,
                    function (i, file) {
                        if (file.className == 'file-edit') {
                            file.onclick = function () {

                                fileEdit(viewModel.get('document.AttachPath'), file.getAttribute('fileName'));

                            };
                        };
                    });
                var filesDownload = $("#files" + name).closest(".k-upload").find("button");
                $.each(filesDownload,
                    function (i, file) {
                        if (file.className == 'file-download') {
                            file.onclick = function () {
                                fileDownload(viewModel.get('document.AttachPath'), file.getAttribute('fileName'));
                            };
                        };
                    });
                // e.files[0]["documentId"] = viewModel.get('document.Id');
            },
            select: function (e) {
                // console.log('.k-upload', $("#files" + name).closest(".k-upload").find("a"));

                //var files = e.files;
                //$.each(files, function (i, file) {
                //    file["documentId"] = viewModel.get('document.Id');
                //    file.rawFile["documentId"] = viewModel.get('document.Id');
                //    //$('span.k-filename[title="' + file.name + '"]').parent().remove();
                //});
                // e.files[0]["documentId"] = viewModel.get('document.Id');
            },
            template: kendo.template($('#fileTemplate').html()),
            files: initialFiles
        });
}

function InitializePropertyProject(name, viewModel, type) {
    var dataCitizenType = [
        { text: "Переписка", value: "0" },
        { text: "Руководству", value: "1" }
    ];
    var dataOutgoingType = [
        { text: "Инициативный", value: "0" },
        { text: "Ответный", value: "1" },
        { text: "Промежуточный", value: "2" }
    ];
    var dataMonitoringType = [
        // { text: "В работу", value: "0" },
        { text: "Не контрольный", value: "1" },
        { text: "Контроль", value: "2" },
        { text: "Особый контроль", value: "3" },
        { text: "До контроль", value: "4" }
    ];
    var initialFiles = viewModel.get('document.AttachFiles');

    InitializeDocumentAttachList(name, viewModel, initialFiles);

    //$("#save" + name).kendoButton({imageUrl: "/Content/images/add.png"});

    if ($("#MonitoringType" + name) != null)
        $("#MonitoringType" + name)
            .kendoDropDownList({
                dataTextField: "text",
                dataValueField: "value",
                select: function(e) {
                    var dropdownlist = $("#MonitoringType" + name).data("kendoDropDownList")
                    var dataItem = this.dataItem(e.item.index());
                    console.log('dddd', e);
                    if (dataItem.value != 1) {

                        document.getElementById('ExecutionDate' + name).setAttribute("required", "required");
                        //  $("#ExecutionDate" + name).setAttribute("required", "required");               
                    } else {
                        document.getElementById('ExecutionDate' + name).removeAttribute("required");
                    }
                },
                index: 0,
                dataSource: dataMonitoringType
            });

    if ($("#ReadersId" + name) != null)
        $("#ReadersId" + name)
            .kendoMultiSelect({
                dataTextField: "Name",
                dataValueField: "Id",
                filter: "contains",
                autoBind: false,

                dataSource: {
                    transport: {
                        read: {
                            url: '/Reference/AllListEmploye'

                        }
                    }
                }
            });

    if ($("#AdministrativeTypeDictionaryId" + name) != null && type == 'AdmMain')
        $("#AdministrativeTypeDictionaryId" + name)
            .kendoMultiSelect({
                dataTextField: "Name",
                dataValueField: "Id",
                maxSelectedItems: 1,
                filter: "contains",
                autoBind: false,
                dataSource: {
                    transport: {
                        read: {
                            url: '/Reference/List',
                            data: { text: "AdministrativeMainTypeDictionary" }
                        }
                    }
                }
            });

    if ($("#AdministrativeTypeDictionaryId" + name) != null && type == 'Adm')
        $("#AdministrativeTypeDictionaryId" + name)
            .kendoMultiSelect({
                dataTextField: "Name",
                dataValueField: "Id",
                maxSelectedItems: 1,
                filter: "contains",
                autoBind: false,
                dataSource: {
                    transport: {
                        read: {
                            url: '/Reference/List',
                            data: { text: "AdministrativeTypeDictionary" }
                        }
                    }
                }
            });

    if ($("#AdministrativeTypeDictionaryId" + name) != null && (type == 'Protocol' || type == 'Zkl' || type == 'Akt' || type == 'Exp' || type == 'Fct' || type == 'Fkl' || type == 'Isp' || type == 'Nap' || type == 'Per' || type == 'Pes' || type == 'Pfc' || type == 'Pfk' || type == 'Prt' || type == 'Zfc' || type == 'Zfk'))
        $("#AdministrativeTypeDictionaryId" + name).kendoMultiSelect({
            dataTextField: "Name",
            dataValueField: "Id",
            maxSelectedItems: 1,
            filter: "contains",
            autoBind: false,
            dataSource: {
                transport: {
                    read: {
                        url: '/Reference/List',
                        data: { text: "ProtocolTypeDictionary" }
                    }
                }
            }
        });

    if ($("#BlankNumber" + name) != null)
        $("#BlankNumber" + name).kendoMaskedTextBox({ mask: "" });

    if ($("#SigningFormDictionaryId" + name) != null)
        $("#SigningFormDictionaryId" + name).kendoMultiSelect({
            dataTextField: "Name",
            dataValueField: "Id",
            maxSelectedItems: 1,
            filter: "contains",
            autoBind: false,
            dataSource: {
                transport: {
                    read: {
                        url: '/Reference/List',
                        data: { text: "SigningFormDictionary" }
                    }
                }
            }
        });



    if ($("#ExecutionDate" + name) != null)
        $("#ExecutionDate" + name).kendoDatePicker();
    
    if ($("#Number" + name) != null)
        $("#Number" + name).kendoMaskedTextBox({ mask: "" }).attr("readonly", true);

    if ($("#RemarkText1" + name) != null)
        $("#RemarkText1" + name).kendoMaskedTextBox({ mask: "" });

    if ($("#RemarkText2" + name) != null)
        $("#RemarkText2" + name).kendoMaskedTextBox({ mask: "" });

    if ($("#RemarkText3" + name) != null)
        $("#RemarkText3" + name).kendoMaskedTextBox({ mask: "" });

    if ($("#OutgoingNumber" + name) != null)
        $("#OutgoingNumber" + name).kendoMaskedTextBox({ mask: "" });

    if ($("#DocumentDate" + name) != null)
        $("#DocumentDate" + name).kendoDateTimePicker();

    if ($("#ParleyStartDate" + name) != null)
        $("#ParleyStartDate" + name).kendoDateTimePicker();

    if ($("#ParleyEndDate" + name) != null)
        $("#ParleyEndDate" + name).kendoDateTimePicker();

    if ($("#CorrespondentsId" + name) != null)
        $("#CorrespondentsId" + name).kendoMultiSelect({
            dataTextField: "Name",
            dataValueField: "Id",
            filter: "contains",
            autoBind: false,
            dataSource: {
                transport: {
                    read: {
                        url: '/Reference/AllMultiListUnits'

                    }
                }
            }
        });

    if ($("#CorrespondentsInfo" + name) != null)
        $("#CorrespondentsInfo" + name).kendoMaskedTextBox({ mask: "" });

    //if ($("#CorrespondentsId" + name) != null)
    //    $("#CorrespondentsId" + name).kendoMultiSelect({
    //        dataTextField: "Name",
    //        dataValueField: "Id",
    //        maxSelectedItems: 1,
    //        filter: "contains",
    //        autoBind: false,

    //        dataSource: {
    //            transport: {
    //                read: {
    //                    url: '/Reference/ListEmploye'

    //                }
    //            }
    //        }
    //    });

    if ($("#ExecutorsId" + name) != null)
        $("#ExecutorsId" + name).kendoMultiSelect({
            dataTextField: "Name",
            dataValueField: "Id",
            filter: "contains",
            autoBind: false,

            dataSource: {
                transport: {
                    read: {
                        url: '/Reference/AllListEmploye'

                    }
                }
            }
        });
    if ($("#CreatedUserId" + name) != null) {
        $("#CreatedUserId" + name).kendoMultiSelect({
            dataTextField: "Name",
            dataValueField: "Id",
            maxSelectedItems: 1,
            filter: "contains",
            autoBind: false,

            dataSource: {
                transport: {
                    read: {
                        url: '/Reference/AllListEmploye'

                    }
                }
            }
        });
        var multiSelectCreatedUserId = $("#CreatedUserId" + name).data("kendoMultiSelect");
        if (multiSelectCreatedUserId != null) {
            multiSelectCreatedUserId.readonly();
        }
    }

    if ($("#SignerId" + name) != null)
        $("#SignerId" + name).kendoMultiSelect({
            dataTextField: "Name",
            dataValueField: "Id",
            maxSelectedItems: 1,
            filter: "contains",
            autoBind: false,

            dataSource: {
                transport: {
                    read: {
                        url: '/Reference/AllListEmploye'

                    }
                }
            }
        });

    if ($("#NomenclatureDictionaryId" + name) != null)
        $("#NomenclatureDictionaryId" + name).kendoMultiSelect({
            dataTextField: "Name",
            dataValueField: "Id",
            filter: "contains",
            autoBind: false,
            maxSelectedItems: 1,
            dataSource: {

                transport: {
                    read: {
                        url: '/Reference/List',
                        data: { text: "Nomenclature" }
                    }
                }
            }
        });

    if ($("#AnswersId" + name) != null)
        $("#AnswersId" + name).kendoMultiSelect({
            filter: "contains",
            dataValueField: "Id",
            dataTextField: "Name",
            placeholder: "Введите текст...",
            autoBind: false,
            tagTemplate: "<a style='text-decoration: underline' onClick='tagAnswerClick(\"#=data.Id#\",\"#=data.Name#\")'>#: data.Name # </a>",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: '/Reference/ListDocumentIn',
                        data: function () {
                            return {
                                text: $("#AnswersId" + name).data('kendoMultiSelect').input.val(),
                                data: JSON.stringify(viewModel.get('document.AnswersId')),
                            };
                        }
                    }
                }
            }
        });


    if ($("#OutgoingType" + name) != null) {
        var multiSelect = $("#AnswersId" + name).data("kendoMultiSelect");
        $("#OutgoingType" + name).kendoDropDownList({
            dataTextField: "text",
            dataValueField: "value",
            index: 0,
            select: function(e) {

                //var dataItem = this.dataItem(e.item.index());

                //if (dataItem.value != 0) {

                //    multiSelect.enable();
                //    document.getElementById('AnswersId' + name).setAttribute("required", "required");
                     
                //} else {
                //    document.getElementById('AnswersId' + name).removeAttribute("required");
                //    var validator = $("#prjDocForm" + name).kendoValidator().data("kendoValidator");
                //    validator.validate();
                //    multiSelect.readonly();
                //}
            },
            dataSource: dataOutgoingType
        });
        if (multiSelect != null) {
           // multiSelect.readonly();
        }
    }

    if ($("#Summary" + name) != null)
        $("#Summary" + name).kendoAutoComplete({
            filter: "contains",
           // dataTextField: "Name",
            placeholder: "Введите текст...",

            autoBind: false,
            dataSource: {

                transport: {
                    read: {
                        url: '/Reference/ListAutoComplete',
                        data: { text: "NoteTypeDictionary" }
                    }
                }
            }
        });

    if ($("#DestinationId" + name) != null) {
        $("#DestinationId" + name).kendoMultiSelect({
            filter: "contains",
            dataValueField: "Id",
            dataTextField: "Name",
            autoBind: false,
            tagTemplate: "<a style='text-decoration: underline' onClick='tagAnswerClick(\"#=data.Id#\",\"#=data.Name#\")'>#: data.Name # </a>",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: '/Reference/ListDocumentDestination',
                        data: function() {
                            return {
                                text: $("#DestinationId" + name).data('kendoMultiSelect').input.val(),
                                data: JSON.stringify(viewModel.get('document.DestinationId')),
                            };
                        }
                    }
                }
            }
        });
       // $("#DestinationId" + name).data('kendoMultiSelect').readonly();
    }

    if ($("#DocumentKindDictionaryId" + name) != null)
        $("#DocumentKindDictionaryId" + name).kendoMultiSelect({
            dataTextField: "Name",
            dataValueField: "Id",
            maxSelectedItems: 1,
            filter: "contains",
            autoBind: false,
            dataSource: {

                transport: {
                    read: {
                        url: '/Reference/List',
                        data: { text: "DocumentKindDictionary" }
                    }
                }
            }
        });

    if ($("#QuestionDesignDictionaryId" + name) != null)
        $("#QuestionDesignDictionaryId" + name).kendoMultiSelect({
            dataTextField: "Name",
            dataValueField: "Id",
            maxSelectedItems: 1,
            filter: "contains",
            autoBind: false,
            dataSource: {

                transport: {
                    read: {
                        url: '/Reference/List',
                        data: { text: "QuestionDesignDictionary" }
                    }
                }
            }
        });

    if ($("#Counters" + name) != null)
        $("#Counters" + name).kendoMaskedTextBox();

    if ($("#PageCount" + name) != null)
            $("#PageCount" + name).kendoNumericTextBox({format: "#"});
    if ($("#CopiesCount" + name) != null)
            $("#CopiesCount" + name).kendoNumericTextBox({format: "#"});
    //if ($("#AppendixCount" + name) != null)
    //    $("#AppendixCount" + name).kendoMaskedTextBox();

    if ($("#ApplicantEmail" + name) != null)
        $("#ApplicantEmail" + name).kendoMaskedTextBox({ mask: "" });

    if ($("#LanguageDictionaryId" + name) != null)
        $("#LanguageDictionaryId" + name).kendoMultiSelect({
            dataTextField: "Name",
            dataValueField: "Id",
            maxSelectedItems: 1,
            filter: "contains",
            autoBind: false,
            dataSource: {

                transport: {
                    read: {
                        url: '/Reference/List',
                        data: { text: "LanguageDictionary" }
                    }
                }
            }
        });

    if ($("#FormSendingDictionaryId" + name) != null)
        $("#FormSendingDictionaryId" + name).kendoMultiSelect({
            dataTextField: "Name",
            dataValueField: "Id",
            maxSelectedItems: 1,
            filter: "contains",
            autoBind: false,
            dataSource: {

                transport: {
                    read: {
                        url: '/Reference/List',
                        data: { text: "FormSendingDictionary" }
                    }
                }
            }
        });

    $("#Book" + name).kendoMaskedTextBox({ mask: "" });
    $("#Deed" + name).kendoMaskedTextBox({ mask: "" });
    $("#Akt" + name).kendoMaskedTextBox({ mask: "" });

    $("#ApplicantType" + name).kendoDropDownList({
        dataTextField: "text",
        dataValueField: "value",
        index: 0,
        select: function (e) {
            var dropdownlist = $("#ApplicantType" + name).data("kendoDropDownList");
            var dataItem = this.dataItem(e.item.index()); console.log('dddd', e);
            if (dataItem.value == 0) {
                document.getElementById('NomenclatureDictionaryId' + name).setAttribute("required", "required");
            } else {
                document.getElementById('NomenclatureDictionaryId' + name).removeAttribute("required");
            }
        },
        dataSource: dataCitizenType
    });
}