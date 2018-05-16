
function conclusionIndexCtrl($scope, $http, DTOptionsBuilder, DTColumnBuilder) {

    $scope.dtColumns = [
      DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
      DTColumnBuilder.newColumn("Date", "Дата").withOption('name', 'Date').renderWith(dateformatHtml),
      DTColumnBuilder.newColumn("ProjectNumber", "Номер проекта").withOption('name', 'ProjectNumber').renderWith(actionsConclusion),

      DTColumnBuilder.newColumn("ConclusionTypeName", "Заключение").withOption('name', 'ConclusionTypeName'),
         DTColumnBuilder.newColumn("ConclusionStateName", "Статус").withOption('name', 'ConclusionStateName'),
      DTColumnBuilder.newColumn("SignEmployeeName", "Подписывающий").withOption('name', 'SignEmployeeName')
   
    ];
}
function actionsConclusion(data, type, full, meta) {
    return '<a   href="/Client/Project/DetailConclusion?id=' + full.ProjectId + '" >' +
   full.ProjectNumber +
   '</a>';
}
angular
    .module('app')
    .controller('conclusionIndexCtrl', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder',conclusionIndexCtrl])