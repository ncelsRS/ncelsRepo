
function InitializeSizeIncoming(name) {
    var gridElement = $("#pwContentRightId" + name);
    gridElement.height($(window).height() - 120);
}

function InitializeDataIncomingPrice(name) {
    var viewModel = kendo.observable({
        Id: name,
       
        save: function (e) {
            buildPriceData(name);
        },
        calcMark: function (e) {
            calcMarkMethod(name);
        }
    });

   
    kendo.bind($("#docToolbarPrice" + name), viewModel);
    // validator.validate();
}


function buildPriceData(name) {
    kendo.ui.progress($('#loader' + name), true);
    $.fn.serializeObject = function() {
        var o = {};
        var a = this.serializeArray();
        $.each(a,
            function() {
                if (o[this.name]) {
                    if (!o[this.name].push) {
                        o[this.name] = [o[this.name]];
                    }
                    o[this.name].push(this.value || '');
                } else {
                    o[this.name] = this.value || '';
                }
            });
        return o;
    };

    $.ajax({
        type: 'POST',
        url: '/Application/DocumentBuild',
        data: JSON.stringify($("#priceFormToSave" + name).serializeObject()),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            $('#listView_FileData' + name).data("kendoListView").dataSource.read();
            $('#listView_FileData' + name).data("kendoListView").refresh();
            //CardBuildSuccess();
        },
        complete: function() {
            kendo.ui.progress($('#loader' + name), false);
        }
    });
};

function calcMarkMethod(name) {
    kendo.ui.progress($('#loader' + name), true);
    $.fn.serializeObject = function () {
        var o = {};
        var a = this.serializeArray();
        $.each(a,
            function () {
                if (o[this.name]) {
                    if (!o[this.name].push) {
                        o[this.name] = [o[this.name]];
                    }
                    o[this.name].push(this.value || '');
                } else {
                    o[this.name] = this.value || '';
                }
            });
        return o;
    };

    $.ajax({
        type: 'POST',
        url: '/Application/CalcMark',
        data: JSON.stringify($("#priceFormToSave" + name).serializeObject()),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            alert("Наценка рассичтана, обновите страницу!");
        },
        complete: function () {
            kendo.ui.progress($('#loader' + name), false);
        }
    });
};

function InitializeDataIncoming(name, repeatId) {
    var validator = $("#inDocForm" + name).kendoValidator().data("kendoValidator");

    if (repeatId == '')
        repeatId = 'null';
    
    var viewModel = kendo.observable({
        document: {},
        change: function () {
            validator.validate();
            this.set("hasChanges", true);
        },
        initButton: function () {
            var outNumber = this.get("document.OutgoingNumber");
            var state = this.get("document.StateType");
            if (state == 0) {
                this.set("hasRegister", true);
            } else {
                this.set("hasRegister", false);
            }
            
            if (state == 1 && outNumber != null) {
                this.set("hasReview", true);
            } else {
                this.set("hasReview", false);
            }   
            if (state == 1) {
                this.set("hasReview2", true);
            } else {
                this.set("hasReview2", true);
            }
            if (state == 1) {
                this.set("hasChangeStatus", true);
            } else {
                this.set("hasChangeStatus", true);
            }          
            if (state == 1) {
                this.set("hasChangeStatusPrice", true);
            } else {
                this.set("hasChangeStatusPrice", true);
            }
            if (state == 1) {
                this.set("hasSendArchive", true);
            } else {
                this.set("hasSendArchive", true);
            }
            if (state >= 1) {
                this.set("hasReport", true);
            } else {
                this.set("hasReport", false);
            }
            if (state > 1 && state < 9) {
                this.set("hasExecute", true);
            } else {
                this.set("hasExecute", false);
            }
            if (state > 0) {
                this.set("hasPrint", true);
            }else {
                this.set("hasPrint", false);
            }
            if (state == 1) {
                this.set("hasBuild", true);
            } else {
                this.set("hasBuild", false);
            }
            if (state > 0 && state < 9) {

                this.set("hasExtensionExecution", true);
            } else {
                this.set("hasExtensionExecution", false);
            }

            if (outNumber == null) {
                this.set("hasRegisterOutgoingNumber", true);
            } else {
                this.set("hasRegisterOutgoingNumber", false);
            }

        },
        hasChanges: false,
        hasRegister:false,
        register: function(e) {
            e.preventDefault();
            if (validator.validate()) {
                this.set("hasChanges", false);
                registerData();
            } else {

            };
        },
        hasRegisterOutgoingNumber: false,
        registerOutgoingNumber: function (e) {
            registerOutgoingNumber();
        },
        hasReview: false,
        review: function (e) {
            e.preventDefault();
            reviewData();
        },   
        hasReview2: true,
        review2: function (e) {
            e.preventDefault();
            reviewData2();
        },
        hasReviewRef: true,
        reviewRef: function (e) {
            e.preventDefault();
            reviewDataRef();
        },
        hasChangeStatus: true,
        changeStatus: function (e) {
            e.preventDefault();
            changeStatusData();
        }, 
        hasChangeStatusPrice: true,
        changeStatusPrice: function (e) {
            e.preventDefault();
            changeStatusPriceData();
        },
        report: function (e) {
            e.preventDefault();
            reportData();
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
        hasBuild: true,
        sendNote: function (e) {
            e.preventDefault();
            addProjectOutClick();
        },
        hasSendArchive: true,
        sendArchive: function (e) {
            e.preventDefault();
            sendArchiveData();
        },
        deleteDoc: function (e) {
            e.preventDefault();
            DeleteDocumetnt(this.get("document.Id"));
        },
        dictionaryView: function(e) {
            e.preventDefault();
            DictionaryView(name, 'False', this);
        },
        hasExtensionExecution: false,
        extensionExecution:function(e) {
            e.preventDefault();
            extensionExecution();
        },
        save: function (e) {
            e.preventDefault();
            this.set("hasChanges", false);
            sendData();
        }
    });

    

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

    function loadDocument() {
        kendo.ui.progress($('#loader' + name), true);
        $.ajax({
            type: 'get',
            url: '/IncomingDoc/DocumentRead?Id=' + name + '&repeatId=' + repeatId,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                result = JSON.parse(result);
                // alert(JSON.stringify(result));
                viewModel.set("document", result);
                viewModel.initButton();
                //viewModel.person = JSON.stringify(result);
                InitializePropertyIncoming(name, viewModel);
                kendo.bind($("#inDocForm" + name), viewModel);
               // validator.validate();
           
                kendo.ui.progress($('#loader' + name), false);
            },
            complete: function () {
                //validator.validate();
                //  alert('Success! User Loaded!');
                InitializeStatusBar(name, viewModel);
            }
        });
    }

    function excludeData() {
        var window = $("#TaskCommandWindow");
        window.kendoWindow({
            width: "550px",
            height: "auto",
            modal: true, resizable: false,
            close: onCloseCommandWindow,
            actions: ["Close"]
        });

        window.data("kendoWindow").title('Исполнение заявки');
        window.data("kendoWindow").setOptions({
            width: 550,
            height: 'auto'
        });
        window.data("kendoWindow").refresh('/IncomingDoc/Execution?id=' + viewModel.get("document.Id"));
        window.data("kendoWindow").viewModel = viewModel;
        window.data("kendoWindow").center();
        window.data("kendoWindow").open();
    };
    function registerData() {
        kendo.ui.progress($('#loader' + name), true);

        var json = JSON.stringify(viewModel.get('document'));


        $.ajax({
            type: 'POST',
            url: '/IncomingDoc/DocumentRegister',
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

    function registerOutgoingNumber() {
        var window = $("#TaskCommandWindow");
        window.kendoWindow({
            width: "550px",
            height: "auto",
            modal: true, resizable: false,
            close: onCloseCommandWindow,
            actions: ["Close"]
        });

        window.data("kendoWindow").title('Входщий номер');
        window.data("kendoWindow").setOptions({
            width: 550,
            height: 'auto'
        });
        window.data("kendoWindow").refresh('/IncomingDoc/RegisterOutgoingNumber?documentId=' + viewModel.get("document.Id"));

        window.data("kendoWindow").center();
        window.data("kendoWindow").open();
    };

    function extensionExecution() {
        var window = $("#TaskCommandWindow");
        window.kendoWindow({
            width: "550px",
            height: "auto",
            modal: true, resizable: false,
            close: onCloseCommandWindow,
            actions: ["Close"]
        });

        window.data("kendoWindow").title('Продлить исполнение');
        window.data("kendoWindow").setOptions({
            width: 550,
            height: 'auto'
        });
        window.data("kendoWindow").refresh('/IncomingDoc/ExtensionExecution?id=' + viewModel.get("document.Id"));

        window.data("kendoWindow").center();
        window.data("kendoWindow").open();
    }
    function reviewData() {
        var window = $("#TaskCommandWindow");
        window.kendoWindow({
            width: "550px",
            height: "auto",
            modal: true, resizable: false,
            close: onCloseCommandWindow,
            actions: ["Close"]
        });

        window.data("kendoWindow").title('Отправить на первичную экспертизу');
        window.data("kendoWindow").setOptions({
            width: 550,
            height: 'auto'
        });
        window.data("kendoWindow").refresh('/IncomingDoc/DocumentReview?id=' + viewModel.get("document.Id"));

        window.data("kendoWindow").center();
        window.data("kendoWindow").open();
    };

    function reviewData2() {
        var window = $("#TaskCommandWindow");
        window.kendoWindow({
            width: "550px",
            height: "auto",
            modal: true, resizable: false,
            close: onCloseCommandWindow,
            actions: ["Close"]
        });

        window.data("kendoWindow").title('Отправить в бухгалтерию');
        window.data("kendoWindow").setOptions({
            width: 550,
            height: 'auto'
        });
        window.data("kendoWindow").refresh('/IncomingDoc/DocumentReview2?id=' + viewModel.get("document.Id"));

        window.data("kendoWindow").center();
        window.data("kendoWindow").open();
    };

    function reviewDataRef() {
        var window = $("#TaskCommandWindow");
        window.kendoWindow({
            width: "550px",
            height: "auto",
            modal: true, resizable: false,
            close: onCloseCommandWindow,
            actions: ["Close"]
        });

        window.data("kendoWindow").title('Отправить заявку');
        window.data("kendoWindow").setOptions({
            width: 550,
            height: 'auto'
        });
        window.data("kendoWindow").refresh('/IncomingDoc/DocumentReviewRef?id=' + viewModel.get("document.Id"));

        window.data("kendoWindow").center();
        window.data("kendoWindow").open();
    };

    function changeStatusData() {
        var window = $("#TaskCommandWindow");
        window.kendoWindow({
            width: "550px",
            height: "auto",
            modal: true, resizable: false,
            close: onCloseCommandWindow,
            actions: ["Close"]
        });

        window.data("kendoWindow").title('Изменить статус');
        window.data("kendoWindow").setOptions({
            width: 550,
            height: 'auto'
        });
        window.data("kendoWindow").refresh('/IncomingDoc/ChangeStatus?id=' + viewModel.get("document.Id"));

        window.data("kendoWindow").center();
        window.data("kendoWindow").open();
    };

    function changeStatusPriceData() {
        var window = $("#TaskCommandWindow");
        window.kendoWindow({
            width: "550px",
            height: "auto",
            modal: true, resizable: false,
            close: onCloseCommandWindow,
            actions: ["Close"]
        });

        window.data("kendoWindow").title('Изменить статус');
        window.data("kendoWindow").setOptions({
            width: 550,
            height: 'auto'
        });
        window.data("kendoWindow").refresh('/IncomingDoc/ChangeStatusPrice?id=' + viewModel.get("document.Id"));

        window.data("kendoWindow").center();
        window.data("kendoWindow").open();
    };

    function sendArchiveData() {
        var window = $("#TaskCommandWindow");
        window.kendoWindow({
            width: "550px",
            height: "auto",
            modal: true, resizable: false,
            close: onCloseCommandWindow,
            actions: ["Close"]
        });

        window.data("kendoWindow").title('Отправить в архив');
        window.data("kendoWindow").setOptions({
            width: 550,
            height: 'auto'
        });
        window.data("kendoWindow").refresh('/IncomingDoc/SendArchive?id=' + viewModel.get("document.Id"));

        window.data("kendoWindow").center();
        window.data("kendoWindow").open();
    };

    function reportData() {
        var window = $("#TaskCommandWindow");
        window.kendoWindow({
            width: "550px",
            height: "auto",
            modal: true, resizable: false,
            close: onCloseCommandWindow,
            actions: ["Close"]
        });

        window.data("kendoWindow").title('Добавить замечание');
        window.data("kendoWindow").setOptions({
            width: 550,
            height: 'auto'
        });
        window.data("kendoWindow").refresh('/IncomingDoc/ExtensionReview?id=' + viewModel.get("document.Id"));

        window.data("kendoWindow").center();
        window.data("kendoWindow").open();
    };
    function sendData() {
        kendo.ui.progress($('#loader' + name), true);

        var json = JSON.stringify(viewModel.get('document'));

        $.ajax({
            type: 'POST',
            url: '/IncomingDoc/DocumentUpdate',
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
};

function InitializePropertyIncoming(name, viewModel) {
    var dataMonitoringType = [
      // { text: "В работу", value: "0" },
       { text: "Не контрольный", value: "1" },
       { text: "Контроль", value: "2" },
       { text: "Особый контроль", value: "3" },
       { text: "До контроль", value: "4" }
    ];

    var initialFiles = viewModel.get('document.AttachFiles');
    
    $("#files" + name).kendoUpload({
        // multiple: true,
        localization: {
            select: 'Выбрать файл...',
            remove: '',
            cancel: '',
            headerStatusUploading: "Загрузка...",
            headerStatusUploaded: "Загружено!"
        },
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
            var fileStamp = $("#files" + name).closest(".k-upload").find("button");
            $.each(fileStamp, function (i, file) {
                if (file.className == 'file-stamp') {
                    file.onclick = function () {
                        GenerationStamp(viewModel.get('document.AttachPath'), file.getAttribute('fileName'));
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
        template: kendo.template($('#fileStampTemplate').html()),
        files: initialFiles
    });

    //$("#save" + name).kendoButton({ imageUrl: "/Content/images/commands/doc_save.png" });
    //$("#register" + name).kendoButton({ imageUrl: "/Content/images/commands/doc_register.png" });
    //$("#review" + name).kendoButton({ imageUrl: "/Content/images/commands/doc_review.png" });
    //$("#execute" + name).kendoButton({ imageUrl: "/Content/images/commands/doc_execute.png" });
    //$("#reject" + name).kendoButton({ imageUrl: "/Content/images/commands/doc_reject.png" });
    //$("#print" + name).kendoButton({ imageUrl: "/Content/images/commands/doc_print.png" });


    $("#MonitoringType" + name).kendoDropDownList({
        dataTextField: "text",
        dataValueField: "value",
        index: 0,
        select:function (e) {
            var dropdownlist = $("#MonitoringType" + name).data("kendoDropDownList");
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
    //$("#ExecutionDate" + name).kendoValidator({
    //    rules   : {
    //        //implement your custom date validation
    //        required: function (e) {
    //            console.log("e", e);
    //            //var currentDate = Date.parse($(e).val());
    //            ////Check if Date parse is successful
    //            //if (!currentDate) {
    //            //    return false;
    //            //}
    //            return true;
    //        }
    //    },
    //    messages: {
    //        //Define your custom validation massages
    //        required      : "Date is required message",
    //        dateValidation: "Invalid date message"
    //    }
    //});
    //if (viewModel.get('document.StateType') > 0) {

    //    $("#ExecutionDate" + name).data("kendoDatePicker").readonly();
    //}
    $("#MonitoringNote" + name).kendoMaskedTextBox({ mask: "" });

    $("#Number" + name).kendoMaskedTextBox({ mask: "" }).attr("readonly", true);

    $("#DocumentDate" + name).kendoDateTimePicker();

    $("#AutoFirstExecutionDate" + name).kendoDatePicker();

    $("#AutoFactExecutionDate" + name).kendoDatePicker();

    $("#CorrespondentsInfo" + name).kendoMaskedTextBox({ mask: "" });

    $("#ApplicantName" + name).kendoMaskedTextBox({ mask: "" });

    $("#ApplicantEmail" + name).kendoMaskedTextBox({ mask: "" });
   
    $("#OutgoingNumber" + name).kendoAutoComplete({
        filter: "contains",
     //   dataTextField: "Name",
        placeholder: "Введите текст...",
        //template: '#: Name #'+' от ' + '#: Date #',
        autoBind: false,
        minLength: 3,
        dataSource: {
            serverFiltering: true,
            transport: {
                read: {
                    url: '/Reference/OutgoingNumbersAll',
                    data: function() {
                        return {
                            correspondent: $("#CorrespondentsId" + name).data("kendoMultiSelect").value(),
                            text: $("#OutgoingNumber" + name).val()
                        };
                    },
                }
            }
        }
    });

  

    $("#OutgoingDate" + name).kendoDatePicker();

    $("#ExecutorsId" + name).kendoMultiSelect({
        dataTextField: "Name",
        dataValueField: "Id",
        maxSelectedItems: 1,
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
    $("#ResponsibleValue" + name).kendoMaskedTextBox({ mask: "" }).attr("readonly", true);

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
        autoBind: false,
        maxSelectedItems: 1,
        filter: "contains",
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
        filter: "contains",
        maxSelectedItems: 1,
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

    $("#FormDeliveryDictionaryId" + name).kendoMultiSelect({
        dataTextField: "Name",
        dataValueField: "Id",
        filter: "contains",
        maxSelectedItems: 1,
        autoBind: false,
        dataSource: {

            transport: {
                read: {
                    url: '/Reference/List',
                    data: { text: "FormDeliveryDictionary" }
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

    $("#AnswersId" + name).kendoMultiSelect({

        filter: "contains",
        dataValueField: "Id",
        dataTextField: "Name",
        placeholder: "Введите текст...",
        autoBind: false,
        tagTemplate: "<a style='text-decoration: underline' onClick='tagAnswerClick(\"#=data.Id#\",\"#=data.Name#\")'>#: data.Name # </a>",
        minLength: 2,
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


    $("#CompleteDocumentsId" + name).kendoMultiSelect({

        filter: "contains",
        dataValueField: "Id",
        dataTextField: "Name",
        tagTemplate: "<a style='text-decoration: underline' onClick='tagAnswerClick(\"#=data.Id#\",\"#=data.Name#\")'>#: data.Name # </a>",
        placeholder: "Введите текст...",
        autoBind: false,
        minLength: 2,
        dataSource: {
            serverFiltering: true,
            transport: {
                read: {
                    url: '/Reference/ListDocumentIn',
                    data: function () {
                        return {
                            text: $("#CompleteDocumentsId" + name).data('kendoMultiSelect').input.val(),
                            data: JSON.stringify(viewModel.get('document.CompleteDocumentsId')),
                        };
                    }
                }
            }
        }
    });

    $("#AutoAnswersId" + name).kendoMultiSelect({

        dataValueField: "Id",
        dataTextField: "Name",
        tagTemplate: "<a style='text-decoration: underline' onClick='tagAnswerClick(\"#=data.Id#\",\"#=data.Name#\")'>#: data.Name # </a>",
        autoBind: false,
        minLength: 3,
        dataSource: {
            serverFiltering: true,
            transport: {
                read: {
                    url: '/Reference/ListDocumentOut',
                    data: function () {
                        return {
                            text: $("#AutoAnswersId" + name).data('kendoMultiSelect').input.val(),
                            data: JSON.stringify(viewModel.get('document.AutoAnswersId')),
                        };
                    }
                }
            }
        }
    });
    //$("#AutoAnswersId" + name).data('kendoMultiSelect').readonly();
    $("#AutoAnswersTempId" + name).kendoMultiSelect({

        dataValueField: "Id",
        dataTextField: "Name",
        tagTemplate: "<a style='text-decoration: underline' onClick='tagAnswerClick(\"#=data.Id#\",\"#=data.Name#\")'>#: data.Name # </a>",
        autoBind: false,
        minLength: 3,
        dataSource: {
            serverFiltering: true,
            transport: {
                read: {
                    url: '/Reference/ListDocumentOut',
                    data: function () {
                        return {
                            text: $("#AutoAnswersTempId" + name).data('kendoMultiSelect').input.val(),
                            data: JSON.stringify(viewModel.get('document.AutoAnswersTempId')),
                        };
                    }
                }
            }
        }
    });
   // $("#AutoAnswersTempId" + name).data('kendoMultiSelect').readonly();
   // $("#Note" + name).kendoMaskedTextBox({ mask: "" });

    $("#CorrespondentsId" + name).kendoMultiSelect({
        dataTextField: "Name",
        dataValueField: "Id",
        maxSelectedItems: 1,
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
    $("#Book" + name).kendoMaskedTextBox({ mask: "" });
    $("#Deed" + name).kendoMaskedTextBox({ mask: "" });
    $("#Akt" + name).kendoMaskedTextBox({ mask: "" });

    $("#Summary" + name).kendoAutoComplete({
        filter: "contains",
   //     dataTextField: "Name",
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

    

}
