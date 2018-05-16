function checkLevelCtrl($scope, $http) {
    $scope.LevelResponsibilityDict = [];
    $scope.DangerClassDict = [];
    $http({
        method: 'GET',
        url: '/Dictionaries/GetReference',
        data: 'JSON',
        params: { type: 'LevelResponsibilityDict' }
    }).success(function (result) {
        $scope.LevelResponsibilityDict = result;
    });


    $http({
        method: 'GET',
        url: '/Dictionaries/GetReference',
        data: 'JSON',
        params: { type: 'DangerClassDict' }
    }).success(function (result) {
        $scope.DangerClassDict = result;
    });

    $scope.check = function () {
        if ($scope.DangerClassDict.selected.Id == 72) {
            $scope.isFormFirst = true;
            $scope.isFormSecond = false;
        } else {
            $scope.isFormFirst = false;
            $scope.isFormSecond = true;
        }
    }
}
angular
    .module('app')
    .controller('checkLevelCtrl', ['$scope', '$http',checkLevelCtrl]);