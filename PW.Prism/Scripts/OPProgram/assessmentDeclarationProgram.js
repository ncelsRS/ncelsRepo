var attachFile;

function updateElementsState() {

}

function updateProgramIsUploaded() {
    $.ajax({
        url: 'Upload/GetAttachList?',
        data: {
            id: modelId,
            type: 'assessmentDeclarationOPProgram'
        },
        success: function (res) {
            attachFile = res.Data[0];
            attachFile.Type = 'assessmentDeclarationOPProgram';
            if (attachFile.Items.length === 0) {
                $("#assessmentDeclarationOPProgramKendoUpload" + modelId).show();
                $("#assessmentDeclarationOPProgramLink" + modelId).hide();
            } else {
                $("#assessmentDeclarationOPProgramKendoUpload" + modelId).hide();
                $("#assessmentDeclarationOPProgramLink" + modelId).show();
            }
        }
    });
}

updateProgramIsUploaded();

function deleteAttach() {
    var item = attachFile.Items[0];
    $.ajax({
        url: '/Upload/FileDelete?' + item.AttachId + "&fileId=" + item.AttachName,
        success: function (res) {
            updateProgramIsUploaded();
        },
        error: function (err) {
            alert("Произошла ошибка, попробуйте еще раз..");
        }
    })
}

function downloadAttach() {
    var item = attachFile.Items[0];
    var link = document.createElement('a');
    link.setAttribute('href', '/Upload/FileDownload?' + item.AttachId + "&fileId=" + item.AttachName);
    link.setAttribute('download', 'download');
    onload = link.click();
}

function saveProgram() {
    var program = {
        DeclarationId: modelId,
        Name: $("#organizationName" + modelId).val(),
        DateFrom: $("#declarationProgramDateFrom" + modelId).val(),
        DateTo: $("#declarationProgramDateTo" + modelId).val()
    };
    if (!program.DateFrom || !program.DateTo) return alert("Заполните поле Период проведения");
    $.ajax({
        url: '/OPProgram/SaveProgram',
        method: 'POST',
        data: program,
        success: function (res) {
            if (res.isSuccess) {
                alert("Сохранено");
            }
            else {
                alert("Ошибка");
            }
        }
    })
}

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
            code: attachFile.Id,
            path: modelId,
            saveMetadata: true
        };
    },
    success: function (e) {
        updateProgramIsUploaded();
    },
    error: function (e) {
        alert("Ошибка: " + e.Message + e.message);
    }
});