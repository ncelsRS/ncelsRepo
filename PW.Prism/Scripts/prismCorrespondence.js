
function InitializeSizeCorrespondence(name) {
    var gridElement = $("#pwContentRightId" + name);
    gridElement.height($(window).height() - 120);
}

function InitializeDataCorrespondence(name) {
    var validator = $("#corDocForm" + name).kendoValidator().data("kendoValidator");


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
            if (state == 1) {
                this.set("hasReview", true);
            } else {
                this.set("hasReview", false);
            }
            if (state > 1 && state < 9) {
                this.set("hasExecute", true);
            } else {
                this.set("hasExecute", false);
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
        hasReview: false,
        review: function (e) {
            e.preventDefault();
            reviewData();
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
        }
    });

    function loadDocument() {
        kendo.ui.progress($('#loader' + name), true);
        $.ajax({
            type: 'get',
            url: '/CorrespondenceDoc/DocumentRead?Id=' + name,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                result = JSON.parse(result);
                // alert(JSON.stringify(result));
                viewModel.set("document", result);
                viewModel.initButton();
                //viewModel.person = JSON.stringify(result);
                InitializePropertyCorrespondence(name, viewModel);
                kendo.bind($("#corDocForm" + name), viewModel);
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
            url: '/CorrespondenceDoc/DocumentExclude',
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
				InitializeStatusBar(name, viewModel);
            }
        });
    };
    function registerData() {
        kendo.ui.progress($('#loader' + name), true);

        var json = JSON.stringify(viewModel.get('document'));


        $.ajax({
            type: 'POST',
            url: '/CorrespondenceDoc/DocumentRegister',
            data: json,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                result = JSON.parse(result);
                if (result.State == true) {
                    viewModel.set("document", result.document);
                    viewModel.initButton();
                    CardRegisterSuccess(result.document.Number, result.document.DocumentDate);
                } else {
                    CardSaveError();
                }
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
            url: '/CorrespondenceDoc/DocumentReview',
            data: json,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                result = JSON.parse(result);
                if (result.State == true) {
                    viewModel.set("document", result.document);
                    viewModel.initButton();
                    CardReviewSuccess();
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
            url: '/CorrespondenceDoc/DocumentUpdate',
            data: json,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                if (result == 'True') {
                    CardSaveSuccess();
                } else {
                    CardSaveError();
                }
            },
            complete: function () {
                kendo.ui.progress($('#loader' + name), false);
            }
        });
    };

    loadDocument();

}

function InitializePropertyCorrespondence(name, viewModel) {

    var initialFiles = viewModel.get('document.AttachFiles');

    $("#files" + name).kendoUpload({
        localization: {
            select: 'Выбрать файл...',
            remove: '',
            cancel: '',
            headerStatusUploading: "Загрузка...",
            headerStatusUploaded: "Загружено!"
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
            $.each(files, function (i, file) {
                file.onclick = function () {
                    fileView(viewModel.get('document.AttachPath'), file.getAttribute('fileName'));
                };
            });
            var filesButton = $("#files" + name).closest(".k-upload").find("button");

            $.each(filesButton, function (i, file) {
                if (file.className == 'file-edit') {
                    file.onclick = function () {

                        fileEdit(viewModel.get('document.AttachPath'), file.getAttribute('fileName'));

                    };
                };
            });
            var filesDownload = $("#files" + name).closest(".k-upload").find("button");
            $.each(filesDownload, function (i, file) {
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

    //$("#save" + name).kendoButton({imageUrl: "/Content/images/add.png"});
    var dataMonitoringType = [
// { text: "В работу", value: "0" },
{ text: "Не контрольный", value: "1" },
{ text: "Контроль", value: "2" },
{ text: "Особый контроль", value: "3" },
{ text: "До контроль", value: "4" }
    ];
    $("#MonitoringType" + name).kendoDropDownList({
        dataTextField: "text",
        dataValueField: "value",
        index: 0,
        select: function (e) {
            var dropdownlist = $("#MonitoringType" + name).data("kendoDropDownList")
            var dataItem = this.dataItem(e.item.index()); console.log('dddd', e);
            if (dataItem.value != 1) {

                document.getElementById('ExecutionDate' + name).setAttribute("required", "required");
                //  $("#ExecutionDate" + name).setAttribute("required", "required");               
            } else {
                document.getElementById('ExecutionDate' + name).removeAttribute("required");
            }
        },
        dataSource: dataMonitoringType
    });
    $("#ExecutionDate" + name).kendoDatePicker();

    $("#Number" + name).kendoMaskedTextBox({ mask: "" }).attr("readonly", true);

    $("#DocumentDate" + name).kendoDateTimePicker();

    $("#AutoFirstExecutionDate" + name).kendoDatePicker();

    $("#AutoFactExecutionDate" + name).kendoDatePicker();

    $("#CorrespondentsId" + name).kendoMultiSelect({
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

    $("#ExecutorsId" + name).kendoMultiSelect({
        dataTextField: "Name",
        dataValueField: "Id",
      //  maxSelectedItems: 1,
        filter: "startswith",
        autoBind: false,

        dataSource: {
            transport: {
                read: {
                    url: '/Reference/AllListEmploye'

                }
            }
        }
    });

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
        //if (multiSelectCreatedUserId != null) {
        //    multiSelectCreatedUserId.readonly();
        //}
    }

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
                    url: '/Reference/ListDocumentOut',
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
    
    $("#Summary" + name).kendoAutoComplete({
        filter: "contains",
    //    dataTextField: "Name",
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

    $("#SourceId" + name).kendoMultiSelect({
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
                    url: '/Reference/ListDocumentPrj',
                    data: function () {
                        return {
                            text: $("#SourceId" + name).data('kendoMultiSelect').input.val(),
                            data: JSON.stringify(viewModel.get('document.SourceId')),
                        };
                    }
                }
            }
        }
    });
    //$("#SourceId" + name).data('kendoMultiSelect').readonly();

    $("#Book" + name).kendoMaskedTextBox({ mask: "" });
    $("#Deed" + name).kendoMaskedTextBox({ mask: "" });
    $("#Akt" + name).kendoMaskedTextBox({ mask: "" });
}