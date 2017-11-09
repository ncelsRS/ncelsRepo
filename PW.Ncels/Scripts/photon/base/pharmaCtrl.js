function pharmaProfile($scope, DTColumnBuilder, $http, $uibModal) {
    loadDictionary($scope, 'Kato', $http);

    $scope.PharmaType = [{
        Id: true,
        Name: "оптовая"
    }, {
        Id: false,
        Name: "розничная"
    }];

}

function loadDictionary($scope, name, $http, orderByName) {
    $http({
        method: 'GET',
        url: '/Dictionaries/GetReference',
        data: 'JSON',
        params: {
            type: name,
            orderByName: !orderByName ? false : orderByName
        }
    }).success(function (result) {
        $scope[name] = result;
    });
}

angular
    .module('app')
    .controller('pharmaProfile', ['$scope', 'DTColumnBuilder', '$http', '$uibModal', pharmaProfile]);
