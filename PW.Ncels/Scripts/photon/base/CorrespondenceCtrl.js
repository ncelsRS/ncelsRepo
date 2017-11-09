function actionsCorListHtmlAction(data, type, full, meta) {
        return '<a  class="pw-task-link" href="/Correspondence/Detail?id=' + full.Id + '" >' +
            data +
            '</a>';
}

function correspondenceList($scope, DTColumnBuilder, $http, $uibModal) {

    $scope.dtColumnsCor = [
        DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number').renderWith(actionsCorListHtmlAction), ,
        DTColumnBuilder.newColumn("DocumentDate", "Дата").withOption('name', 'DocumentDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("Summary", "Краткое содержание").withOption('name', 'Number'),
    ];

}

function correspondenceSend($scope, DTColumnBuilder, $http, $filter) {
    var id = $("#projectId").val();

    $http({
        method: 'GET',
        url: '/Correspondence/Load?id=' + id,
        data: 'JSON'
    }).success(function (response) {
        $scope.object = response;
        $scope.selectProjectType(response.ApplicantType);
    });



    $scope.selectProjectType = function (id) {
        $http({
            method: 'GET',
            url: '/Dictionaries/GetMyProjects?type=' + id,
            data: 'JSON'
        }).success(function (response) {
            angular.forEach(response, function (value, key) {
                value.CreatedDate = new Date(parseInt(value.CreatedDate.replace("/Date(", "").replace(")/", ""), 10));
                value.CreatedDate = $filter('date')(value.CreatedDate, "dd.MM.yyyy");
            });
            $scope.Projects = response;
        });
    }

    $scope.save = function () {
        $http({
            url: '/Correspondence/Create',
            method: 'POST',
            data: JSON.stringify($scope.object)
        }).success(function (response) {
            alert('Сохранено');
            window.location.href = '/Correspondence/Detail/'+response;
        });
    }

    $scope.delete = function (id) {
        $http({
            url: '/Correspondence/Delete?id='+id,
            method: 'POST'
        }).success(function (response) {
            alert('Удалено!');
            window.location.href = '/Correspondence/Index';
        });
    }

    $scope.send = function () {
        $http({
            url: '/Correspondence/Send',
            method: 'POST',
            data: JSON.stringify($scope.object)
        }).success(function (response) {
            alert('Отправлено');
            window.location.href = '/Correspondence/Detail/'+response;
        });
    }

    $scope.ProjectType = [
        {
            Id: 1,
            Name: "Ценообразование"
        },
        {
            Id: 2,
            Name: "Экспертиза"
        },
        {
            Id: 3,
            Name: "Договора"
        }
    ];
}



angular
    .module('app')
    .controller('correspondenceList', ['$scope', 'DTColumnBuilder', '$http', '$uibModal', correspondenceList])
    .controller('correspondenceSend', ['$scope', 'DTColumnBuilder', '$http', '$filter', correspondenceSend]);
