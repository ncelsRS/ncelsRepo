
var commonErrorMessage = "Повторите операцию позже или обратитесь к администратору";
function _enum_(name, elems) {
    var elem, value;
    window[name] = {};
    for (var i = elems.length; i--;) {
        elem = elems[i];
        value = elem.replace(/\s/g, '').split('=');
        window[name][value[0]] = {
            value: value[0],
            int: value[1] | i,
            toString: function () {
                return this.value;
            }
        };
    }
}
_enum_('AlertType', ['Warning', 'Error', 'Info', 'Success']);
var alertRebornNumber = 0;
function ShowAlert(title, message, type, time, needId) {
    if (time == undefined || time < 0) {
        time = 5000;
    }
    var alertClass = '';
    switch (type) {
        case window.AlertType.Warning:;
            alertClass = 'alert-warning';
            break;
        case window.AlertType.Error:
            alertClass = 'alert-danger';
            break;
        case window.AlertType.Info:
            alertClass = 'alert-info';
            break;
        case window.AlertType.Success:
            alertClass = 'alert-success';
            break;
        default:
            alertClass = 'alert-info';
            break;
    }
    if (!$('#alert-body').length) {
        $('body').append('<div id="alert-body" class="message-alert"></div>');
    }
    if (title == undefined) {
        title = 'Уведомление';
    }
    alertRebornNumber++;
    var id = 'alert-' + alertRebornNumber;
    if (needId != undefined && needId != "") {
        id = 'alert-' + needId;
    }
    var text = '<div id="' + id + '" class="alert alert-block ' + alertClass + ' ">' +
        '<a class="close" data-dismiss="alert" href="#">X</a>' +
            '<h4 class="alert-heading">' +
                '<span class="fa fa-warning"></span>' + title +
            '</h4>' +
            '<label>' + message + '</label>' +
        '</div>';
    $('#alert-body').append(text);
    setTimeout(function () {
        $('#' + id).remove();
    }, time);
}