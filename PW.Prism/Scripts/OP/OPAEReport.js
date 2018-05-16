class Protocol {
    constructor(protocol) {
        this._html = {
            Panel: $("#editProtocolPanel" + modelId),
            Number: $("#protocolNumber" + modelId),
            Name: $("#productName" + modelId),
            Executor: $("#executor" + modelId),
            ExecuteResult: $("#result" + modelId).data("kendoDropDownList")
        };

        this.Id = protocol.Id;
        this.Name = protocol.NameRu;
        this.Number = protocol.Number;
        this.Executor = protocol.Executor;
        this.ExecuteResult = protocol.ExecuteResult;

        this._html.Panel.show();
    }

    get Number() { return this._html.Number.val(); }
    set Number(value) { this._html.Number.val(value); }

    get Name() { return this._html.Name.val(); }
    set Name(value) { this._html.Name.val(value); }

    get Executor() { return this._html.Executor.val(); }
    set Executor(value) { this._html.Executor.val(value); }

    get ExecuteResult() { return this._html.ExecuteResult.value(); }
    set ExecuteResult(value) { this._html.ExecuteResult.value(value); }

    toDto() {
        return {
            Id: this.Id,
            Number: this.Number,
            Name: this.Name,
            Executor: this.Executor,
            ExecuteResult: this.ExecuteResult
        };
    }

    dispose() {
        this._html.Number.val("");
        this._html.Name.val("");
        this._html.Executor.val("");
        this._html.ExecuteResult.value("");
        this._html.Panel.hide();
    }
}

var protocols;
var protocol;

function loadProtocols() {
    $.ajax({
        url: "/OPAEReport/ListProtocols",
        data: {
            declarationId: modelId
        },
        success: function (res) {
            protocols = res;
            $("#tableAEReport" + modelId).DataTable({
                language: {
                    url: "/Content/json/Russian.json"
                },
                data: protocols,
                destroy: true,
                searching: false,
                iDisplayLength: 5,
                LengthMenu: false,
                bLengthChange: false,
                autoWidth: true,
                columns: [
                    { data: "Number" },
                    { data: "Date" },
                    { data: "NameRu" },
                    { data: "Executor" },
                    { data: "ExecuteResultText" }
                ]
            });
            if (res.isError) {
                return console.error(res.Message);
            }
        }
    })
}

function editProtocol(data) {
    protocol = new Protocol(data);
}
function cancelEditProtocol() {
    protocol.dispose();
}
function saveProtocol() {
    var protocolDto = protocol.toDto();
    if (protocolDto.ExecuteResult != 0 && protocolDto.ExecuteResult != 1)
        return alert("Заполните необходимые поля перед сохранением результата");
    $.ajax({
        url: "/OPAEReport/SaveProtocol",
        method: "POST",
        data: protocolDto,
        success: function (res) {
            protocol.dispose();
            loadProtocols();
        },
        error: function (err) {
            console.error(err);
        }
    })
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
function initAEReportTable() {
    $("#tableAEReport" + modelId).DataTable({
        language: {
            url: "/Content/json/Russian.json"
        },
        data: null,
        searching: false,
        bLengthChange: false,
        columns: [
            { title: "№ протокола" },
            { title: "Дата исполнения" },
            { title: "Наименование продукции" },
            { title: "Исполнитель" },
            { title: "Результат протокола" }
        ]
    });
    $('#tableAEReport' + modelId + ' tbody').on('click', 'td', function () {
        var data = $("#tableAEReport" + modelId).DataTable().row($(this).parents('tr')).data();
        return editProtocol(data);
    });
}

// Details

function protocolsInit() {
    initAEReportTable();
    loadProtocols();
}