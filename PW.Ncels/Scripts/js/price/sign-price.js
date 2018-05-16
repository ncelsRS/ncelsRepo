function doSign() {
    debugger;
        $.blockUI({
            message: '<h1><img src="../../Content/css/plugins/slick/ajax-loader.gif"/> Идет подпись отчета...</h1>',
            css: { opacity: 1 }
        });
        signXmlCall(function () {
            var model = { preambleId: $("#projectId").val(), xmlAuditForm: $("#Certificate").val() };
            $.ajax({
                url: '/Price/SignForm',
                type: "POST",
                dataType: 'json',
                contentType: "application/json",
                async: false,
                data: JSON.stringify(model),
                success: function (data) {

                    if (data.success) {
                        var url = window.location.href;
                        if (url.indexOf('PriceLs') > 0) {
                            window.location.href = "/Project/PriceLsDetails/" + $("#projectId").val();
                        } else if (url.indexOf('RePriceLs') > 0) {
                            window.location.href = "/Project/RePriceLsDetails/" + $("#projectId").val();
                        } else if (url.indexOf('PriceImn') > 0) {
                            window.location.href = "/Project/PriceImnDetails/" + $("#projectId").val();
                        } else if (url.indexOf('RePriceImn') > 0) {
                            window.location.href = "/Project/RePriceImnDetails/" + $("#projectId").val();
                        }


                        /*   $("#signBtn").attr('disabled', 'disabled');
                           $("#notSignBtn").attr('disabled', 'disabled');
                           $("#checkBtn").attr('disabled', 'disabled');
                           setReadOnlyControl();*/
                    }
                        //                    window.location = data.url;
                    else {
                        $("#formCertValidation").show();
                    }
                    $.unblockUI();
                    //                window.location.reload();
                },
                error: function(data) {
                    $.unblockUI();
                }
            });
        },
            $("#hfXmlToSign").val());
    }

