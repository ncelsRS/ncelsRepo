function actionsEmpContractListHtmlAction(data, type, full, meta, $scope) {
    //if (full.ParentId) {
    //    return '<a  class="pw-task-link" href="/EMPContract/ContractAddition?id=' + full.Id + '" >' + data + '</a>';
    //}
    //else {
        return '<a  class="pw-task-link" href="/EMPContract/Contract?id=' + full.Id + '" >' + data + '</a>';
    //}
}

function dateformatHtml(data, type, full, meta) {
    if (data == null)
        return '';
    var d = new Date(parseInt(data.slice(6, -2)));

    var yyyy = d.getFullYear();
    var mm = d.getMonth() < 9 ? "0" + (d.getMonth() + 1) : (d.getMonth() + 1);
    var dd = d.getDate() < 10 ? "0" + d.getDate() : d.getDate();

    return dd + "." + mm + "." + yyyy;
}

function empContractGrid($scope, $http, DTColumnBuilder) {

    function renderEmpNumFunc(data, type, full, meta) {
        return actionsEmpContractListHtmlAction(data, type, full, meta, $scope);
    };

    var scopeCode = $("#scopeCode").val();
    var mdName = scopeCode === "national" ? mdName = 'Наименование ИМН/МТ' : mdName = 'Наименование МИ';

    $scope.dtColumns = [
        DTColumnBuilder.newColumn("Number", "№ договора/доп. согл-я").withOption('name', 'Number').renderWith(renderEmpNumFunc),
        DTColumnBuilder.newColumn("CreatedDate", "Дата создания").withOption('name', 'CreatedDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("Status", "Статус").withOption('name', 'Status'),
        DTColumnBuilder.newColumn("ManufacturName", "Производитель").withOption('name', 'ManufacturName'),
        DTColumnBuilder.newColumn("MedicalDeviceName", mdName).withOption('name', 'MedicalDeviceName'),
        DTColumnBuilder.newColumn("StartDate", "Дата заключения/Дата начала действия договора").withOption('name', 'StartDate').renderWith(dateformatHtml)
    ];
}

angular
    .module('app')
    .controller('empContractGrid', ['$scope', '$http', 'DTColumnBuilder', empContractGrid]);
