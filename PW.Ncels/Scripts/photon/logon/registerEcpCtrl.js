function registerEcpRequest($scope, $http, $window) {

    $scope.postRequest = function () {
        if ($scope.registerForm.$valid) {
            $http({
                url: '/Account/PostRequest',
                method: 'POST',
                data: JSON.stringify($scope.user)
            }).success(function (response) {
                if (!response.IsError) {
                    $window.location.href = '/Account/RegisterSuccess';
                } else {
                    alert(response.Message);
                }
            });
        }
    }
    $scope.onStepChanged = function (event, currentIndex, priorIndex) {

    };

 }


function setDefaultValue($scope, list, selected, code) {
    angular.forEach(list, function (value, key) {
        if (value.Code == code)
            $scope.user[selected] = value.Id;
    });
}

angular
    .module('app')
    .controller('registerEcpRequest', ['$scope', '$http', '$window', registerEcpRequest]);