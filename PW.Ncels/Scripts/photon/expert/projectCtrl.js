
function cardProjectCtrl($scope, $http) {
   
    $http({
        method: 'GET',
        url: '/Expertise/Project/ReadHistoryTask',
        data: 'JSON',
        params: { id: $('#projectId').val() }
    }).success(function (result) {
        $scope.resultItems = result;
    });

}

angular
    .module('app')
    .controller('cardProjectCtrl', ['$scope', '$http',cardProjectCtrl]);

