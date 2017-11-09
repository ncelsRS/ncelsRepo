var expAgreementProc = {
    sendToAgreement: function (docParam) {
        debugger;
        docParam = docParam || {};
        docParam.documentId = docParam.documentId || '';
        docParam.documentTypeCode = docParam.documentTypeCode || '';
        docParam.activityTypeCode = docParam.activityTypeCode || '';
        if (docParam.loaderId)
            kendo.ui.progress($('#' + docParam.loaderId), true);
        $.ajax({
            type: 'GET',
            url: '/Agreement/SendToAgreement?documentId=' + docParam.documentId + '&documentTypeCode=' + docParam.documentTypeCode + '&activityTypeCode=' + docParam.activityTypeCode,            
            success: function (result) {                
                if (docParam.callBack)
                    docParam.callBack(result);
            },
            complete: function () {
                if (docParam.loaderId)
                    kendo.ui.progress($('#' + docParam.loaderId), false);
            }
        });
    }
};