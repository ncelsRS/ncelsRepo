function sendTranslateOnAgreement(e, letterId, taskType, documentType) {
    kendo.ui.progress($("#mainWindowLoader"), true);
    var data = JSON.stringify({
        docId: letterId,
        documentType: documentType,
        taskType: taskType
    });
    $.ajax({
        type: 'POST',
        url: '/Translate/SendTranslateOnAgreement',
        data: data,
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            ShowAlert('Сообщение', taskType === '1' ? "Письмо отправленно на согласование" : "Письмо отправленно на подписанние", 'Info', 5000);
            $('#' + letterId + '_status').text(result);
        },
        complete: function () {
            kendo.ui.progress($("#mainWindowLoader"), false);
        }
    });    
}
function sendToApplicant(id)
{
 var success = function () {
                $.ajax({
                    type: "POST",
                    url: "/Translate/SendToApplicant",
                    data: { 'id': id },
                    dataType: 'json',
                    cache: false,
                    success: function (data) {
                        var status = "#"+id + "_status";
                        var sendBtn = "#" + id + "_sendBtn";
                        $(status).text(data.statusName);
                        $(sendBtn).hide();
                    },
                    error: function (data) {
                        alert("1Error" + data);
                    }
                });
            }
            var cancel = function () {
            };
            showConfirmation("Подтверждение", "Вы уверены что хотите отправить письмо?", success, cancel);
}