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
