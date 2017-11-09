function expertiseProjectList($scope, $http, DTOptionsBuilder, DTColumnBuilder) {
    $scope.dtColumns = [
        DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
        DTColumnBuilder.newColumn("ApplicationDate", "Дата").withOption('name', 'ApplicationDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("NameRu", "Наименование").withOption('name', 'NameRu'),
        DTColumnBuilder.newColumn("ProjectStateName", "Текущий статус").withOption('name', 'ProjectStateName'),
        DTColumnBuilder.newColumn("Id", "Действия").withOption('name', 'Id').renderWith(actionsProject)
    ];
}

function projectAllCtrl($scope, $http, DTOptionsBuilder, DTColumnBuilder) {
    $scope.dtColumns = [
        DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number').renderWith(linkProject),
        DTColumnBuilder.newColumn("ApplicationDate", "Дата").withOption('name', 'ApplicationDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("NameRu", "Наименование").withOption('name', 'NameRu'),
        DTColumnBuilder.newColumn("ProjectStateName", "Текущий статус").withOption('name', 'ProjectStateName'),
        DTColumnBuilder.newColumn("Id", "Действия").withOption('name', 'Id')
    ];
}

function actionsProject(data, type, full, meta) {
    return '<a  class="btn btn-outline btn-success btn-xs" href="/Expertise/Project/DetailCard?id=' + data + '" >' +
   '   <i class="fa fa-search"></i>' +
   '</a>';
}

function linkProject(data, type, full, meta) {
    return '<a   href="/Expertise/Project/DetailTasks?id=' + full.Id + '" >' + full.Number + '</a>';
}

angular
    .module('app')
    .controller('expertiseProjectList',['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder',expertiseProjectList] )
    .controller('projectAllCtrl',['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder',projectAllCtrl] );
