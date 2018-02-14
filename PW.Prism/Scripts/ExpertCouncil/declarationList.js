function getDeclarationDetailsForEC(declarationId, stageId, number) {
    if (!declarationId) return;
    if (docArray.indexOf(declarationId) !== -1)
        docArray.splice(docArray.indexOf(parameters.toLowerCase()), 1);
    var element = document.getElementById("ec" + declarationId);
    if (element === null) {
        var tabStrip = $("#tabstrip").data("kendoTabStrip");
        var content = '<div id="ec' + declarationId + '"> </div>';
        var idContent = '#ec' + declarationId;
        tabStrip.append({
            text: 'Заявление: № ' + number,
            content: content
        });

        tabStrip.select(tabStrip.items().length - 1);

        var gridElement = $(idContent);

        gridElement.height($(window).height() - 100);

        $.ajax({
            url: "/SafetyAssessment/Edit?id=" + stageId,
            success: function (result) {
                $(idContent).html(result);
            }
        });
    } else {

        var itesm = $('#ec' + declarationId)[0].parentElement.getAttribute('id').split('-')[1];
        if (itesm) {
            $("#tabstrip").data("kendoTabStrip").select(itesm - 1);
        }
    }
}