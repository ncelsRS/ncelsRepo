showConfirmation = function (title, message, success, cancel) {
    title = title ? title : 'Вы уверены?';
    var modal = $("#main_confirmation");
    modal.find(".modal-title").html(title).end()
        .find(".modal-body").html(message).end()
        .modal({ backdrop: 'static', keyboard: false })
        .on('hidden.bs.modal', function () {
            modal.unbind();
        });
    $("#confirmCancel").one('click', cancel);
    if (success) {
        modal.one('click', '.modal-footer .btn-primary', success);
        return true;
    }
    if (cancel) {
        modal.one('click', '.modal-header .close, .modal-footer .btn-primary', cancel);
        return false;
    }
    return true;
};
function showWarning(message) {
    var modal = $("#main_warning");
    modal.find(".modal-body").html(message).end()
        .modal({ backdrop: 'static', keyboard: false })
        .on('hidden.bs.modal', function () {
            modal.unbind();
        });
  
};
function showAlert(message, title, type, size) {
    if (title == null)
        title = "Сообщение";

    var typeClass = "alert-info";
    if (type === "warning") {
        typeClass = "alert-warning";
    }
    var sizeClass = "modal-size-lg";
    if (size === "lg") {
        sizeClass = "modal-size-lg";
    }
    if (size === "md") {
        sizeClass = "modal-size-md";
    }
    if (size === "sm") {
        sizeClass = "modal-size-sm";
    }
    var modal = $("#main_alert");
    modal.find(".modal-content").removeClass("alert-warning");
    modal.find(".modal-content").removeClass("alert-info");
    modal.find(".modal-content").addClass(typeClass);
    modal.find(".modal-dialog").addClass(sizeClass);
    modal.find("#title-text").text(title);
    //content.addClass("alert-info");
    modal.find(".modal-body").html(message).end()
        .modal({ backdrop: 'static', keyboard: false })
        .on('hidden.bs.modal', function () {
            modal.unbind();
        });

};

function showReport (content) {
    var modal = $("#main_view");
    modal.find(".modal-body").html(content).end()
         .modal({ backdrop: 'static', keyboard: false })
         .on('hidden.bs.modal', function () {
             modal.unbind();
         });
}

function bindTodeleteBtn (title, text) {
    $(".deleteBtn").click(function () {
        var href = $(this).attr('href');
        var success = function () {
            window.location = href;
        };
        var cancel = function () {

        };
        showConfirmation(title, text, success, cancel);
        return false;
    });
};
