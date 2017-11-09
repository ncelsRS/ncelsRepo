function clientProfile($scope, $http) {
    $http({
        method: 'GET',
        url: '/Client/Home/GetProfile',
        data: 'JSON',
    }).success(function (result) {
        $scope.client = result;
        if (result.PassportDate != null) {
            var value = result.PassportDate;
            $scope.client.PassportDate = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
        }
    });
}



angular
    .module('app')
    .controller('clientProfile',['$scope', '$http',clientProfile]);