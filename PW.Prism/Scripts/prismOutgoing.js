
function InitializeSizeOutgoing(name) {
    var gridElement = $("#pwContentRightId" + name);
    gridElement.height($(window).height() - 120);
}

function InitializeDataOutgoing(name) {
    var validator = $("#outDocForm" + name).kendoValidator().data("kendoValidator");


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
                this.set("hasOnjob", true);
            } else {
                this.set("hasOnjob", false);
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
        hasOnjob: false,
        onjob: function (e) {
            e.preventDefault();
            sendDoc();
        },
        hasPrint: false,
        print: function (e) {
            e.preventDefault();
            PrintDocumetnt(this.get("document.Id"));
        },
        deleteDoc: function (e) {
            e.preventDefault();
            DeleteDocumetnt(this.get("document.Id"));
        },
        dictionaryView: function (e) {
            e.preventDefault();
            DictionaryView(name, 'True', this);
        },
        save: function (e) {
            e.preventDefault();
            this.set("hasChanges", false);
            sendData();
        }
    });

    function loadDocument() {
        kendo.ui.progress($('#loader' + name), true);
        $.ajax({
            type: 'get',
            url: '/OutgoingDoc/DocumentRead?Id=' + name,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                result = JSON.parse(result);
                // alert(JSON.stringify(result));
                viewModel.set("document", result);
                viewModel.initButton();
                //viewModel.person = JSON.stringify(result);
                InitializePropertyOutgoing(name, viewModel);
                kendo.bind($("#outDocForm" + name), viewModel);
                kendo.ui.progress($('#loader' + name), false);
            },
            complete: function () {
                //  alert('Success! User Loaded!');
				InitializeStatusBar(name, viewModel);
            }
        });
    }
    function sendDoc() {
        var window = $("#TaskCommandWindow");
        window.kendoWindow({
            width: "550px",
            height: "auto",
            modal: true, resizable: false,
            close: onCloseCommandWindow,
            title: 'Резолюция',
            actions: ["Close"]
        });

        window.data("kendoWindow").title('Отправка документа');
        window.data("kendoWindow").setOptions({
            width: 550,
            height: 'auto'
        });
        window.data("kendoWindow").refresh('/OutgoingDoc/DocumentSend?id=' + viewModel.get("document.Id"));

        window.data("kendoWindow").center();
        window.data("kendoWindow").open();

        $("#TaskCommandWindow").closest(".k-window").css({
            top: 55,
        
        });
    }
    function registerData() {
        kendo.ui.progress($('#loader' + name), true);

        var json = JSON.stringify(viewModel.get('document'));


        $.ajax({
            type: 'POST',
            url: '/OutgoingDoc/DocumentRegister',
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
            url: '/OutgoingDoc/DocumentSend',
            data: json,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
              //  result = JSON.parse(result);
                if (result == 'True') {

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
            url: '/OutgoingDoc/DocumentUpdate',
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

function InitializePropertyOutgoing(name, viewModel) {
    var dataOutgoingType = [
       { text: "Инициативный", value: "0" },
       { text: "Ответный", value: "1" },
       { text: "Промежуточный", value: "2" }
    ];

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
        }
        , upload: function (e) {

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


    $("#OutgoingType" + name).kendoDropDownList({
        dataTextField: "text",
        dataValueField: "value",
        index: 0,
        dataSource: dataOutgoingType
    });

    $("#Number" + name).kendoMaskedTextBox({ mask: "" }).attr("readonly", true);

    $("#DocumentDate" + name).kendoDateTimePicker();

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

    $("#ApplicantEmail" + name).kendoMaskedTextBox({ mask: "" });

    $("#CorrespondentsInfo" + name).kendoMaskedTextBox({ mask: "" });

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

    $("#ExecutorsId" + name).kendoMultiSelect({
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

    $("#Summary" + name).kendoAutoComplete({
        filter: "contains",
      //  dataTextField: "Name",
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

    //$("#Counters" + name).kendoMaskedTextBox({ mask: "листов: 0, приложений: 0" });  

        $("#PageCount" + name).kendoNumericTextBox({format: "#"});
        $("#CopiesCount" + name).kendoNumericTextBox({format: "#"});
    $("#Counters" + name).kendoMaskedTextBox();

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

    $("#NomenclatureDictionaryId" + name).kendoMultiSelect({
        dataTextField: "Name",
        dataValueField: "Id",
        maxSelectedItems: 1,
        filter: "contains",
        autoBind: false,
        dataSource: {

            transport: {
                read: {
                    url: '/Reference/List',
                    data: { text: "Nomenclature" }
                }
            }
        }
    });

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
                    url: '/Reference/ListDocumentInAndCit',
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

    $("#AutoAnswersId" + name).kendoMultiSelect({
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
                    url: '/Reference/ListDocumentInById',
                    data: function () {
                        return {
                            id: viewModel.get('document.Id'),
                            propertyName: 'AutoAnswersId',
                        };
                    }
                }
            }
        }
    });
    
    $("#AutoAnswersId" + name).data('kendoMultiSelect').readonly();
    $("#Book" + name).kendoMaskedTextBox({ mask: "" });
    $("#Deed" + name).kendoMaskedTextBox({ mask: "" });
    $("#Akt" + name).kendoMaskedTextBox({ mask: "" });
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
}