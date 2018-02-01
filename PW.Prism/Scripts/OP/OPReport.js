class ReportOP {
    constructor(entity) {
        this._html = {
            StatusName: $("#opReportStatus" + modelId),
            Result: $("#result" + modelId),
            ExecuteResultCode: $("#resultReportOP" + modelId).data("kendoDropDownList"),
            ExecuteResultName: $("#resultReportOPLabel" + modelId)
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
    }

    set StatusName(value) { this._html.StatusName.val(value); }

    set ExecuteResultName(value) { this._html.ExecuteResultName.val(value); }

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

var report;

function loadReport() {
    $.ajax({
        url: "/OPReport/LoadReportOP",
        data: {
            declarationId: modelId
        },
        success: function (res) {
            report = new ReportOP(res);
        },
        error: function (err) {
            console.error(err);
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
                $("#tableReportOPExecutors" + modelId).DataTable({
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

function initKendoUpload() {
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
    $('#tableProgramExecutors' + modelId + ' tbody').on('click', 'button', function () {
        var data = $("#tableProgramExecutors" + modelId).DataTable().row($(this).parents('tr')).data();
        return removeExecutor(data);
    });
}

function init() {
    initKendoUpload();
    loadReport();
}

init();