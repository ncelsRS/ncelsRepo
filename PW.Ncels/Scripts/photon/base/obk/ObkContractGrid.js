function actionsObkContractListHtmlAction(data, type, full, meta, $scope) {
    if (full.ParentId) {
        return '<a  class="pw-task-link" href="/OBKContract/ContractAddition?id=' + full.Id + '" >' + data + '</a>';
    }
    else {
        return '<a  class="pw-task-link" href="/OBKContract/Contract?id=' + full.Id + '" >' + data + '</a>';
    }
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

function obkContractGrid($scope, $http, DTColumnBuilder) {

    function renderNumFunc(data, type, full, meta) {
        return actionsObkContractListHtmlAction(data, type, full, meta, $scope);
    };

    $scope.dtColumns = [
        DTColumnBuilder.newColumn("Number", "№ договора/доп. согл-я").withOption('name', 'Number').renderWith(renderNumFunc),
        DTColumnBuilder.newColumn("CreatedDate", "Дата создания").withOption('name', 'CreatedDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("Status", "Статус").withOption('name', 'Status'),
        DTColumnBuilder.newColumn("DeclarantName", "Заявитель").withOption('name', 'DeclarantName'),
        DTColumnBuilder.newColumn("Type", "Тип договора").withOption('name', 'Type'),
        DTColumnBuilder.newColumn("StartDate", "Дата заключения/Дата начала действия договора").withOption('name', 'StartDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("EndDate", "Дата истечения договора").withOption('name', 'EndDate').renderWith(dateformatHtml)
    ];

    $scope.viewContract = function () {
        if ($scope.row === undefined) {
            alert("Выберите договор!");
            return;
        }
        var rowId = $scope.row.Id;
        $http({
            method: 'GET',
            url: '/SafetyAssessment/EditContract?id=' + rowId
        }).success(function (result) {
            window.location.href = "/SafetyAssessment/Edit/" + result;
        });;
    };
}

angular
    .module('app')
    .controller('obkContractGrid', ['$scope', '$http', 'DTColumnBuilder', obkContractGrid]);
