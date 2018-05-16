
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
        var Path = $("#AttachPath").val()
        $.ajax({
            type: 'get',
            url: '/ZBKCopy/DocumentRead?AttachPath=' + Path,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                result = JSON.parse(result);
                viewModel.set("document", result);
                viewModel.initButton();

                InitializePropertyCertificate(name, viewModel);
                kendo.bind($("#outDocForm" + name), viewModel);
                kendo.ui.progress($('#loader' + name), false);

                debugger;
                if (notEditable == true)
                {
                    $(".k-delete").hide();                          
                    $(".k-upload-button").hide();
                }
                
            },
            complete: function () {
                //InitializeStatusBar(name, viewModel);
            }
        });
    }
    loadDocument();
}


function InitializePropertyCertificate(name, viewModel) {
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
                    fileViewKendo(viewModel.get('document.AttachPath'), file.getAttribute('fileName'));
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
                if (file.className == 'glyphicon glyphicon-download') {
                    file.onclick = function () {
                        fileDownload(viewModel.get('document.AttachPath'), file.getAttribute('fileName'));
                    };
                };
            });
            $('.k-grid-update').show();
            $("#simulationUpdate").hide();
      
        },
        template: kendo.template($('#fileTemplate').html()),
        files: initialFiles
    });
}

 function addExtensionClass(extension) {
            switch (extension) {
                case '.jpg':
                case '.jpeg':
                case '.bmp':
                case '.png':
                case '.gif':
                    return "img-file";
                case '.doc':
                case '.docx':
                    return "doc-file";
                case '.xls':
                case '.xlsx':
                    return "xls-file";
                case '.pdf':
                    return "pdf-file";
                case '.ppt':
                case '.pptx':
                    return "ppt-file";
                case '.zip':
                case '.rar':
                    return "zip-file";
                case '.avi':
                case '.mov':
                case '.mp4':
                case '.mkv':
                case '.wmv':
                    return "avi-file";
                case '.mp3':
                case '.wav':
                    return "mp3-file";
                default:
                    return "default-file";
            }
        }

        function substring(str) {
            var exts = str.split('.');
            var ext = exts[exts.length - 1];
            var str2 = '';
            for (var i = 0; i < exts.length - 1; i++) {
                str2 += exts[i];
            }
            if (str2.length > 60)
                return str2.substr(0, 60) + '... .' + ext;
            return str;
        }

        function bytesToSize(bytes) {
            var k = 1024;
            var sizes = ['Байт', 'КБ', 'МБ', 'ГБ', 'ТБ'];
            if (bytes === 0) return '0 Байт';
            var i = parseInt(Math.floor(Math.log(bytes) / Math.log(k)), 10);
            return (bytes / Math.pow(k, i)).toPrecision(3) + ' ' + sizes[i];
        }

      
function fileViewKendo(id, fileName) {
    var window = $("#windowFile");
    window.kendoWindow({
        width: "800px",
        height: "500px",
        modal: true, resizable: false,
        title: fileName,
        actions: ["Pin", "Refresh", "Maximize", "Close"],
        content: "/Upload/FileViewKendo?id=" + id + '&name=' + encodeURIComponent(fileName)
    });
    window.data("kendoWindow").title(fileName);
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}

function fileDownload(id, fileName) {
    window.open('/Upload/Download?id=' + id + '&name=' + fileName);
}
