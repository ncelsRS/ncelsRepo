function detailPsdCtrl($scope, $http, FileUploader) {
    var id = $("#projectId").val();

    $scope.readPsd =  function () {
        $http({
            method: 'GET',
            url: '/Client/Project/DetailPsdRead?id=' + id,
            data: 'JSON'
        }).success(function (result) {
            $scope.htmlContent = result;
        });
    }
    

    $scope.editPsd = function() {
        $http({
            method: 'GET',
            url: '/Client/Project/DetailPsdEdit?id=' + id,
            data: 'JSON'
        }).success(function (result) {
            $scope.htmlContent = result;
        });
    }

    $scope.readPsd();
}

angular
    .module('app')
    .controller('detailPsdCtrl', ['$scope', '$http', 'FileUploader',detailPsdCtrl]);