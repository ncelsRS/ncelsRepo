function pharmaProfile($scope, DTColumnBuilder, $http, $uibModal) {
    $scope.object = {};
    $scope.object.PharmaType = 0;
    loadDictionary($scope, 'Kato', $http);

    

    $scope.PharmaTypes = [{
        Id: 0,
        Name: "оптовая"
    }, {
        Id: 1,
        Name: "розничная"
    }];

    $scope.dtColumns1 = [
        DTColumnBuilder.newColumn("Type", "Тип"),
        DTColumnBuilder.newColumn("Country", "Страна"),
        DTColumnBuilder.newColumn("Manufacturer", "Производитель")
    ];

    $scope.selectPharmaGrid = function (data) {
        debugger;
        console.log(data);
        $scope.curentReg = data;
    };

    $scope.PharmaTypeChange = function (val) {
        var pType = $scope.object.PharmaType;
        //debugger;
        //$scope.reloadPharmaGrid("555");
    }

}

function loadDictionary($scope, name, $http, orderByName, cb) {
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
        if (cb && typeof cb === 'function') cb();
    });
}

angular
    .module('app')
    .controller('pharmaProfile', ['$scope', 'DTColumnBuilder', '$http', '$uibModal', pharmaProfile]);
