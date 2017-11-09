/*оплата DirectionPayment*/
function panelDirectionPaymentSelect(e) {
    debugger;
    var selectType = $(e.item).find("> .k-link").attr('ItemType');
    if (selectType !== null) {
        var selectValue = $(e.item).find("> .k-link").attr('ItemId');
        var gridId = $(e.item).find("> .k-link").attr('ModelId');
        var grid = $("#gridOBKPayment" + gridId).data("kendoGrid");
        var filter = new Array();

        if (selectType === "StatusCode") {

            filter.push({ field: "StatusCode", operator: "eq", value: selectValue });
        }

        if (selectValue === 'reqSign') {
            $("#generateDoc" + gridId).show();
            $("#sendToDeclarant" + gridId).show();
            $("#signObkPayDocument" + gridId).show();
            $("#toSignChiefAccountant" + gridId).show();
        }
        else {
            $("#generateDoc" + gridId).hide();
            $("#sendToDeclarant" + gridId).hide();
            $("#signObkPayDocument" + gridId).hide();
            $("#toSignChiefAccountant" + gridId).hide();
        }
        if (selectValue === 'sendToPayment') {
            $("#generateDoc" + gridId).show();
        }
        

        if (selectValue === '') {
            grid.dataSource.filter([]);
        } else {
            grid.dataSource.filter({
                logic: "and",
                filters: filter
            });
        }
    }
}

function gridSelectRow(e) {
    debugger;
    var data = this.dataItem(this.select());
    var modelId = $("#modelId").val();
    var employeeId = $("#employeeId").val();

    //if (data.ExecutorSign === "False" && data.ChiefAccountantId == null && data.ChiefAccountantSign === "False") {
    //    $("#signObkPayDocument" + modelId).prop('disabled', false);
    //    $("#toSignChiefAccountant" + modelId).attr('disabled', 'disabled');
    //} 
    //if (data.ExecutorSign === "True" && data.ChiefAccountantId == null && data.ChiefAccountantSign === "False") {
    //    $("#signObkPayDocument" + modelId).attr('disabled', 'disabled');
    //    $("#toSignChiefAccountant" + modelId).prop('disabled', false);
    //}
    //if (data.ExecutorSign === "True" && employeeId === data.ChiefAccountantId && data.ChiefAccountantSign === "False") {
    //    $("#signObkPayDocument" + modelId).prop('disabled', false);
    //    $("#toSignChiefAccountant" + modelId).attr('disabled', 'disabled');
    //}
}


function toSignChiefAccountant(id) {
    var window = $("#TaskCommandWindow");
    window.kendoWindow({
        width: "550px",
        height: "auto",
        modal: true, resizable: false,
        close: onCloseCommandWindow,
        actions: ["Close"]
    });
    window.data("kendoWindow").dialogCallback = function () {
        grid.dataSource.read();
    };
    window.data("kendoWindow").gridSelectedIds = id;
    window.data("kendoWindow").title('Отправить на подпись руководителю');
    window.data("kendoWindow").setOptions({
        width: 550,
        height: 'auto'
    });
    window.data("kendoWindow").refresh('/OBKPayment/SetChiefAccountant');
    window.data("kendoWindow").center();
    window.data("kendoWindow").open();
}


function signPayment(id) {
    debugger;
    kendo.ui.progress($("#mainWindowLoader"), true);
    $.ajax({
        type: 'GET',
        url: '/OBKPayment/GetSignDirectionToPayment?id=' + id,
        success: function(result) {
            debugger;
            startSign(result.data, id, saveSignedPayment);
        },
        complete: function() {
            kendo.ui.progress($("#mainWindowLoader"), false);
        }
    });

    function saveSignedPayment(signedData, paymentId) {
        debugger;
        var data = {
            paymentId: paymentId,
            signedData: signedData
        };
        var json = JSON.stringify(data);
        $.ajax({
            type: 'POST',
            url: '/OBKPayment/SaveSignedPayment',
            contentType: 'application/json; charset=utf-8',
            data: json,
            success: function (result) {
                debugger;
                alert(result.message);
            },
            complete: function(e) {
                debugger;
                var modelId = $("#modelId").val();
                var grid = $("#gridOBKPayment" + modelId).data("kendoGrid");
                grid.dataSource.read();
            }
        });
    }
}