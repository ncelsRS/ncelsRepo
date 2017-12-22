
function InitializeZBKCopyFile(name) {
    var viewModel = kendo.observable({
        document: {},
        change: function () {
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
        }
    });

    function loadDocument() {
        kendo.ui.progress($('#loader' + name), true);
debugger;
        var Path = $("#AttachPath").val();
        $.ajax({
            type: 'get',
            url: '/ZBKCopy/DocumentRead?AttachPath=' + Path,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                result = JSON.parse(result);

                viewModel.set("document", result);
                viewModel.initButton();
                InitializePropertyCertificate(name, viewModel);
                kendo.ui.progress($('#loader' + name), false);
                
            },
            complete: function () {
                //InitializeStatusBar(name, viewModel);
            }
        });
    }
    loadDocument();
}


function InitializePropertyCertificate(name, viewModel) {
    debugger
    var initialFiles = viewModel.get('document.AttachFiles');
    
    if ($("#AttachPath").val() == null || $("#AttachPath").val() == ""){
        $("#AttachPath").val(viewModel.get('document.AttachPath')).change();
    }

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
            saveUrl: "/Upload/SaveFile",
            removeUrl: "/Upload/removeFile",
            autoUpload: true
        }
        , upload: function (e) {
            $('.k-grid-update').hide();
            $("#simulationUpdate").show();
            e.data = { certificateId: viewModel.get('document.AttachPath') };
        },
        remove: function (e) {
            e.data = { certificateId: viewModel.get('document.AttachPath') };
        },
        complete: function (e) {

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
            $('.k-grid-update').show();
            $("#simulationUpdate").hide();

        },
        template: kendo.template($('#ZBKCopyfileTemplateRead').html()),
        files: initialFiles
    });
}

