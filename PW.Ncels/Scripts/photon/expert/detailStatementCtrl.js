function detailStatementCtrl($scope, $http) {

    $http({
        url: '/Report/Details?id=' + $("#projectId").val() + '&name=ProjectView.mrt',
        method: 'POST'
    }).success(function (response) {

        $scope.htmlContent = response;
       
    });
}

angular
    .module('app')
    .controller('detailStatementCtrl', ['$scope', '$http',detailStatementCtrl]);