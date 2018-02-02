class ReportOP {
    constructor(entity) {
        this._html = {
            StatusName: $("#opReportStatus" + modelId),
            Result: $("#result" + modelId),
            ExecuteResultCode: $("#resultReportOP" + modelId).data("kendoDropDownList"),
            ExecuteResultName: $("#resultReportOPLabel" + modelId),
            KendoUpload: $("#reportOPFiles" + modelId)
        };
        this.Id = entity.Id;
        this.DeclarationId = entity.DeclarationId;
        this.StatusCode = entity.StatusCode;
        this.StatusName = entity.StatusName;
        this.Date = entity.Date;
        this.ExecuteResultCode = entity.ExecuteResultCode;
        this.ExecuteResultName = entity.ExecuteResultName;
        this.Result = entity.Result;
        this.IsExecutor = entity.IsExecutor;
        this.AttachedFile = entity.Attach.Data[0];
    }

    set StatusName(value) { this._html.StatusName.html(value); }

    set ExecuteResultName(value) { this._html.ExecuteResultName.html(value); }

    set ExecuteResultCode(value) { this._html.ExecuteResultCode.value(value); }
    get ExecuteResultCode() { this._html.ExecuteResultCode.value(); }

    set Result(value) { this._html.Result.val(value); }
    get Result() { return this._html.Result.val(); }

    toDto() {
        return {
            Id: this.Id,
            DeclarationId: this.DeclarationId,
            StatusCode: this.StatusCode,
            ExecuteResultCode: this.ExecuteResultCode,
            Result: this.Result
        };
    }
}
class ReportExecutor {
    constructor(entity) {
        this._html = {
            OrganizationId: $("#ReportOrganizationId" + modelId).data('kendoDropDownList'),
            UnitId: $("#ReportUnitId" + modelId).data('kendoDropDownList'),
            EmployeeId: $("#ReportEmployeeId" + modelId).data('kendoDropDownList'),
            Panel: $("#creatyOrModifyReportExecutorPanel" + modelId)
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

var report;
var reportExecutor;
var reportExecutors;

function loadReport() {
    $.ajax({
        url: "/OPReport/LoadReportOP",
        data: {
            declarationId: modelId
        },
        success: function (res) {
            report = new ReportOP(res);
            updateReportHtmlVisible();
        },
        error: function (err) {
            console.error(err);
        }
    });
}
function loadReportExecutors() {
    $.ajax({
        url: "/OPReport/ListExecutors",
        data: {
            declarationId: modelId
        },
        success: function (res) {
            reportExecutors = res;
            $("#tableReportOPExecutors" + modelId).DataTable({
                language: {
                    url: "/Content/json/Russian.json"
                },
                data: res,
                destroy: true,
                searching: false,
                iDisplayLength: 5,
                LengthMenu: false,
                bLengthChange: false,
                autoWidth: true,
                columns: [
                    { data: "FullName" },
                    { data: "ExecuteResultName" },
                    { data: "Comment" },
                    { data: "Date" },
                    {
                        data: "", targets: -1,
                        defaultContent: "<button type='button' class='k-button'>Удалить</button>"
                    }
                ]
            });
        }
    })
}

var statusesR = {
    OPReportNew: "new",
    OPReportInConfirm: "inconfirm",
    OPReportConfirmed: "confirmed",
    OPReportOnReWork: "inrework"
};
var statusesArrR = [];
Object.keys(statusesR).forEach(key => {
    var value = statusesR[key];
    statusesArrR.push("show-e-" + value);
    statusesArrR.push("show-none-" + value);
});
function updateReportHtmlVisible() {
    var status = report.IsExecutor
        ? "show-e-"
        : "show-none-";
    status += statusesR[report.StatusCode];
    $("#tableReportOPExecutors" + modelId).DataTable().column(4).visible(status === 'show-e-new');
    statusesArrR.forEach(s => {
        if (s != status)
            $("." + s).hide();
    });
    $("." + status).show();
}

function sendReportOPToWork() {
    $.ajax({
        url: "/OPReport/SendToWork",
        data: {
            declarationId: modelId
        },
        success: function (res) {
            loadReport();
        },
        error: function (err) {
            console.error(err);
        }
    })
}

function addReportExecutor() {
    reportExecutor = new ReportExecutor({});
}
function removeReportExecutor(data) {
    $.ajax({
        url: "/OPReport/RemoveStageExecutor",
        data: {
            executorId: data.EmployeeId,
            declarationId: modelId
        },
        success: function () {
            loadReportExecutors();
        }
    });
}
function saveReportExecutor(e) {
    if (!reportExecutor) return;
    var id = reportExecutor.EmployeeId;
    if (!id) return alert("Выберите сотрудника");
    $.ajax({
        url: "OPReport/UpsertStageExecutor",
        method: "POST",
        data: {
            employeeId: id,
            declarationId: modelId
        },
        success: function (res) {
            if (res.isSuccess) {
                loadReportExecutors();
                reportExecutor.destroy();
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
function cancelReportExecutor(e) {
    reportExecutor.destroy();
}

function deleteReportAttach() {
    var item = report.AttachedFile.Items[0];
    $.ajax({
        url: '/Upload/FileDelete?' + item.AttachId + "&fileId=" + item.AttachName,
        success: function (res) {
            loadReport();
        },
        error: function (err) {
            alert("Произошла ошибка, попробуйте еще раз..");
        }
    })
}
function downloadAttach() {
    var item = report.AttachedFile.Items[0];
    var link = document.createElement('a');
    link.setAttribute('href', '/Upload/FileDownload?' + item.AttachId + "&fileId=" + item.AttachName);
    link.setAttribute('download', 'download');
    onload = link.click();
}

function initReportKendoUpload() {
    $("#reportOPFiles" + modelId).kendoUpload({
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
            removeUrl: '/Upload/FileDelete',
            autoUpload: false
        },
        upload: function (e) {
            debugger;
            e.data = {
                code: report.AttachedFile.Id,
                path: modelId,
                saveMetadata: true
            };
        },
        remove: function (e) {
            debugger;
            e.data = {
                fileUidToRemove: e.files[0].uid
            };
            //?' + item.AttachId + "&fileId=" + item.AttachName
            //fileUidToRemove = e.files[0].uid;
            //e.preventDefault();

            //kendo.confirm("Remove the file?").then(function () {
            //    $("#files").data("kendoUpload").removeFileByUid(fileUidToRemove);
            //});
        },
        success: function (e) {
            //loadReport();
        },
        error: function (e) {
            alert("Ошибка: " + e.Message + e.message);
        }
    });
}
function initExecutorsTable() {
    $("#tableReportOPExecutors" + modelId).DataTable({
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
    $('#tableReportOPExecutors' + modelId + ' tbody').on('click', 'button', function () {
        var data = $("#tableReportOPExecutors" + modelId).DataTable().row($(this).parents('tr')).data();
        return removeReportExecutor(data);
    });
}

function reportInit() {
    initReportKendoUpload();
    initExecutorsTable();
    loadReport();
    loadReportExecutors();
}