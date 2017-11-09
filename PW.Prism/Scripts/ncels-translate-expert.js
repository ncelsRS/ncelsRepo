function sendTranslateOnAgreement(e, letterId, taskType, documentType) {
    var window = $("#TaskCommandWindow");
    window.kendoWindow({
        width: "650",
        height: "auto",
        modal: true,
        title: taskType === '1' ? "Отправка на согласование" : "Отправка на подписанние",
        actions: ["Close"]
    });
    $("#TaskCommandWindow").data("kendoWindow").dialogCallback = function (status) {
        ShowAlert('Сообщение', taskType === '1' ? "Письмо отправленно на согласование" : "Письмо отправленно на подписанние", 'Info', 5000);
        $('#' + letterId + '_status').text(status);
        $(e).hide();
    }
    window.data("kendoWindow").refresh('/Translate/SendTranslateOnAgreement?docId=' + letterId + '&documentType=' + documentType + '&taskType=' + taskType);
    window.data("kendoWindow").title(taskType === '1' ? "Отправка на согласование" : "Отправка на подписанние");
    window.data("kendoWindow").setOptions({
        width: "650",
        height: "auto"
    });
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
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