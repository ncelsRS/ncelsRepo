class Program {

    constructor(program) {
        this._html = {
            DateFrom: $("#declarationProgramDateFrom" + modelId).data('kendoDatePicker'),
            DateTo: $("#declarationProgramDateTo" + modelId).data('kendoDatePicker'),
            KendoUpload: $("#assessmentDeclarationOPProgramKendoUpload" + modelId),
            DownloadBtn: $("#assessmentDeclarationOPProgramLink" + modelId),
            CommonTable: $('#dateOPProgram' + modelId)
        };

        this._dateFrom;
        this._dateTo;
        this._attachedFile;
        this.StatusCode;
        this.IsExecutor;
        if (program) {
            this.DateFrom = program.DateFrom;
            this.DateTo = program.DateTo;
            this.AttachedFile = program.Attach;
            this.StatusCode = program.StatusCode;
            this.IsExecutor = program.IsExecutor;
        }
    }

    set DateFrom(date) {
        if (!date) return;
        this._dateFrom = new Date(parseInt(date.substr(6)));
        this._html.DateFrom.value(this._dateFrom);
    }
    get DateFrom() { return this._dateFrom; }

    set DateTo(date) {
        if (!date) return;
        this._dateTo = new Date(parseInt(date.substr(6)));
        this._html.DateTo.value(this._dateTo);
    }
    get DateTo() { return this._dateTo; }

    set AttachedFile(file) {
        this._attachedFile = file.Data[0];
        if (this._attachedFile.Items.length === 0) {
            this._html.KendoUpload.show();
            this._html.DownloadBtn.hide();
        } else {
            this._html.KendoUpload.hide();
            this._html.DownloadBtn.show();
            var programAttachDate = this._attachedFile.Items[0].sysCreatedDate;
            programAttachDate = new Date(parseInt(programAttachDate.substr(6)));
            this._html.CommonTable.html(programAttachDate.toLocaleDateString());
        }
    }
    get AttachedFile() { return this._attachedFile; }

    toVm() {
        this._dateFrom = this._html.DateFrom.value();
        this._dateTo = this._html.DateTo.value();
        return {
            DateFrom: this._dateFrom,
            DateTo: this._dateTo,
            DeclarationId: modelId
        };
    }
}
class Executor {
    constructor(entity) {
        this._html = {
            OrganizationId: $("#OrganizationId" + modelId).data('kendoDropDownList'),
            UnitId: $("#UnitId" + modelId).data('kendoDropDownList'),
            EmployeeId: $("#EmployeeId" + modelId).data('kendoDropDownList'),
            Panel: $("#creatyOrModifyPanel" + modelId)
        };

        if (entity) {
            this.OrganizationId = entity.OrganizationId;
            this.UnitId = entity.UnitId;
            this.EmployeeId = entity.EmployeeId;
            this._html.Panel.show();
        } else {
            this._orgId = null;
            this._unitId = null;
            this._id = null;
        }

        this._html.Panel.show();
    }

    set OrganizationId(value) {
        this._orgId = value;
        this._html.OrganizationId.value(value);
    }
    get OrganizationId() {
        this._orgId = this._html.OrganizationId.value();
        return this._orgId;
    }

    set UnitId(value) {
        this._unitId = value;
        this._html.UnitId.value(value);
    }
    get UnitId() {
        this._unitId = this._html.UnitId.value();
        return this._unitId;
    }

    set EmployeeId(value) {
        this._id = value;
        this._html.EmployeeId.value(value);
    }
    get EmployeeId() {
        this._employeeId = this._html.EmployeeId.value();
        return this._employeeId;
    }

    destroy() {
        this._html.EmployeeId.value("");
        this._html.UnitId.value("");
        this._html.OrganizationId.value("");

        this._html.Panel.hide();

        delete this;
    }
}

var program;
var executor;

var executors;

var statuses = {
    OPProgramNew: "new",
    OPProgramSigned: "signed",
    OPProgramInConfirm: "inconfirm",
    OPProgramConfirmed: "confirmed",
    OPProgramInReWork: "inrework"
};
var statusesArr = [];
Object.keys(statuses).forEach(key => {
    var value = statuses[key];
    statusesArr.push("show-executor-" + value);
    statusesArr.push("show-nonexecutor-" + value);
});
function updateHtmlVisible() {
    var status = program.IsExecutor
        ? "show-executor-"
        : "show-nonexecutor-";
    status += statuses[program.StatusCode];
    $("#tableProgramExecutors" + modelId).DataTable().column(4).visible(status === 'show-executor-signed');
    statusesArr.forEach(s => {
        if (s != status)
            $("." + s).hide();
    });
    $("." + status).show();
    if (program.StatusCode == "OPProgramConfirmed") {
        $(".show-program-confirmed").show();
    }
}

function loadProgram() {
    $.ajax({
        url: 'OPProgram/LoadProgram',
        data: {
            declarationId: modelId
        },
        success: function (res) {
            if (res.isSuccess) {
                program = new Program(res.data);
                if (program.StatusCode == "OPProgramConfirmed") {
                    protocolsInit();
                    reportInit();
                }
                updateHtmlVisible();
            } else
                alert('Произошла ошибка');
        }
    });
}

function deleteAttach() {
    var item = program.AttachedFile.Items[0];
    $.ajax({
        url: '/Upload/FileDelete?' + item.AttachId + "&fileId=" + item.AttachName,
        success: function (res) {
            loadProgram();
        },
        error: function (err) {
            alert("Произошла ошибка, попробуйте еще раз..");
        }
    })
}
function downloadAttach() {
    var item = program.AttachedFile.Items[0];
    var link = document.createElement('a');
    link.setAttribute('href', '/Upload/FileDownload?' + item.AttachId + "&fileId=" + item.AttachName);
    link.setAttribute('download', 'download');
    onload = link.click();
}

function saveProgram() {
    var res = program.toVm();
    if (!res.DateFrom || !res.DateTo)
        return alert("Заполните поле Период проведения");
    if (program.AttachedFile.Items.length === 0)
        return alert("Прикрепите файл");
    $.ajax({
        url: '/OPProgram/SaveProgram',
        method: 'POST',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(res),
        success: function (res) {
            if (res.isSuccess) {
                loadProgram();
            }
            else {
                alert("Ошибка");
            }
        }
    });
}

function loadExecutors() {
    $.ajax({
        url: "/OPProgram/ListExecutors",
        data: {
            declarationId: modelId
        },
        success: function (res) {
            if (res.isSuccess) {
                executors = res.data;
                $("#tableProgramExecutors" + modelId).DataTable({
                    language: {
                        url: "/Content/json/Russian.json"
                    },
                    data: res.data,
                    destroy: true,
                    searching: false,
                    iDisplayLength: 5,
                    LengthMenu: false,
                    bLengthChange: false,
                    autoWidth: true,
                    columns: [
                        { data: "FullName" },
                        { data: "ExecuteResult" },
                        { data: "ExecuteComment" },
                        { data: "Date" },
                        {
                            data: "", targets: -1,
                            defaultContent: "<button type='button' class='k-button'>Удалить</button>"
                        }
                    ]
                });
            }
            if (res.isError) {
                return alert("Ошибка: " + res.data.Message);
            }
        }
    })
}
function addExecutor() {
    executor = new Executor({});
}
function removeExecutor(data) {
    $.ajax({
        url: "/OPProgram/RemoveStageExecutor",
        data: {
            executorId: data.ExecutorId,
            declarationId: modelId
        },
        success: function () {
            loadExecutors();
        }
    });
}

function saveExecutor(e) {
    if (!executor) return;
    var id = executor.EmployeeId;
    if (!id) return alert("Выберите сотрудника");
    $.ajax({
        url: "OPProgram/UpsertStageExecutor",
        method: "POST",
        data: {
            employeeId: id,
            declarationId: modelId
        },
        success: function (res) {
            if (res.isSuccess) {
                loadExecutors();
                executor.destroy();
            }
            if (res.isError) {
                alert("Ошибка: " + res.data.Message);
            }
        },
        error: function (err) {
            alert("Ошибка: " + err.message);
        }
    })
}
function cancelExecutor(e) {
    executor.destroy();
}

function sendToWork() {
    if (executors.length === 0)
        return alert("Выберите подтверждающих");
    $.ajax({
        url: "/OPProgram/SendToWork",
        method: "POST",
        data: {
            declarationId: modelId
        },
        success: function (res) {
            if (res.isSuccess) {
                loadProgram();
                loadExecutors();
            }
            if (res.isError) {
                return alert("Ошибка: " + res.data.Message);
            }
        },
        error: function (err) {
            console.error(err);
        }
    });
}

function initKendoUpload() {
    $("#files" + modelId).kendoUpload({
        localization: {
            select: 'Выбрать файл...',
            remove: 'удалить',
            cancel: 'отменить',
            uploadSelectedFiles: 'загрузить',
            headerStatusUploading: "Загрузка...",
            headerStatusUploaded: "Загружено!"
        },
        multiple: false,
        async: {
            saveUrl: "/Upload/filePost",
            autoUpload: false
        },
        upload: function (e) {
            e.data = {
                code: program.AttachedFile.Id,
                path: modelId,
                saveMetadata: true
            };
        },
        success: function (e) {
            loadProgram();
        },
        error: function (e) {
            alert("Ошибка: " + e.Message + e.message);
        }
    });
}
function initExecutorsTable() {
    $("#tableProgramExecutors" + modelId).DataTable({
        language: {
            url: "/Content/json/Russian.json"
        },
        data: null,
        searching: false,
        bLengthChange: false,
        columns: [
            { title: "Список согласующих" },
            { title: "Результат согласования" },
            { title: "Комментарий" },
            { title: "Дата согласования" },
            { title: "Действия" }
        ]
    });
    $('#tableProgramExecutors' + modelId + ' tbody').on('click', 'button', function () {
        var data = $("#tableProgramExecutors" + modelId).DataTable().row($(this).parents('tr')).data();
        return removeExecutor(data);
    });
}

function notMeetRequirements() {
    var comment = $("#executorComment" + modelId).val();
    if (!comment || comment == "") return alert("Введите комментарий");
    $.ajax({
        url: "/OPProgram/NotMeetRequirements",
        method: "POST",
        data: {
            declarationId: modelId,
            comment: comment
        },
        success: function (res) {
            loadProgram();
        },
        error: function (err) {
            console.error(err);
        }
    });
}
function meetRequirements() {
    var comment = $("#executorComment" + modelId).val();
    $.ajax({
        url: "/OPProgram/MeetRequirements",
        method: "POST",
        data: {
            declarationId: modelId,
            comment: comment
        },
        success: function (res) {
            loadProgram();
            loadExecutors();
        },
        error: function (err) {
            console.error(err);
        }
    });
}

function init() {
    initKendoUpload();
    initExecutorsTable();
    loadExecutors();
    loadProgram();
}

init();