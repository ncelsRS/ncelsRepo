function dateformatHtml1(data, type, full, meta) {
    debugger;
    if (data == null)
        return '';
    var date = new Date(parseInt(data.slice(6, -2)));
    var month = date.getMonth() + 1;
    return date.getDate() + "." + (month > 9 ? month : "0" + month) + "." + date.getFullYear();

}
$(document).ready(function () {
    var number;
    var inputs = document.getElementsByTagName('input');

    for (var i = 0; i < inputs.length; i++) {
        number = inputs[i];
        if (inputs[i].type.toLowerCase() == 'number') {
            number.onkeydown = function (e) {
                if (!((e.keyCode > 95 && e.keyCode < 106)
                    || (e.keyCode > 47 && e.keyCode < 58)
                    || e.keyCode == 8)) {
                    return false;
                }
            }
        }
    }
});
function actionsSafetyDeclarationHtmlAction(data, type, full, meta) {
    var editBtn = "";
    var deleteBtn = "";
    if (full.StatusId === 1 || full.StatusId === 4 || full.StatusId === 6 || full.StatusId === 8) {
        editBtn = '<li class="btn-warning"><a href="/SafetyAssessment/Edit?id=' +
            full.Id +
            '" class="link-object" ><span class="glyphicon glyphicon-edit" aria-hidden="true"></span> Редактировать</a></li>';

    }
    if (full.StatusId === 1) {
        deleteBtn = '<li class="btn-danger"><a href="/SafetyAssessment/Delete?id=' +
            full.Id +
            '" class="link-object" ><span class="glyphicon glyphicon-remove" aria-hidden="true"></span> Удалить</a></li>';

    }
    return '<div class="btn-group" style="float: right;margin-right: 40px">' +
            '<button type="button" class="btn btn-success dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Операции <span class="caret"></span></button>' +
            '<ul class="dropdown-menu btnmenu"> ' +
            '<li class="btn-info" ><a href="/SafetyAssessment/ShowDetails?id=' +
            full.Id +
            '" class="link-object"><span class="glyphicon glyphicon-search" aria-hidden="true"></span> Просмотр</a></li>' +
            editBtn +
            deleteBtn +
            '<li class="btn-default"><a href="/SafetyAssessment/DublicateDrug?id=' +
            full.Id +
            '" class="link-object" ><span class="glyphicon glyphicon-copy" aria-hidden="true"></span> Создать подобное</a></li></ul></div>'
        ;
}
function safetyDeclataionGrid($scope, DTColumnBuilder) {
    $scope.dtColumns = [

        DTColumnBuilder.newColumn("TypeName", "Тип заявление").withOption('name', 'TypeName'),
        DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("StausName", "Текущий статус").withOption('name', 'StausName'),
        DTColumnBuilder.newColumn("CreatedDate", "Дата начала").withOption('name', 'CreatedDate').renderWith(dateformatHtml1),
        DTColumnBuilder.newColumn("SendDate", "Дата отправки").withOption('name', 'SendDate').renderWith(dateformatHtml1),
        DTColumnBuilder.newColumn("SortDate", "Дата решение").withOption('name', 'SortDate').renderWith(dateformatHtml1).notVisible(),
        DTColumnBuilder.newColumn("Id", "").withOption('name', 'Id').notSortable().renderWith(actionsSafetyDeclarationHtmlAction)
    ];

}

angular
    .module('app')
    .controller('safetyDeclataionGrid', ['$scope', 'DTColumnBuilder', safetyDeclataionGrid]);
