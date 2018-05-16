
function cardProjectCtrl($scope, $http,notify) {
    var id = $("#projectId").val();
    $scope.detail = '';
    $http({
        method: 'GET',
        url: '/Client/Project/ReadHistoryTask',
        data: 'JSON',
        params: { id: $('#projectId').val() }
    }).success(function (result) {
        $scope.resultItems = result;
    });


    $http({
        method: 'GET',
        url: '/Client/Project/Details?id=' + id,
        data: 'JSON'
    }).success(function (result) {

        $scope.htmlContent = result;
        $scope.detail = result;
    });

   
    $http({
        method: 'GET',
        url: '/Client/Project/GetDataEdit?id=' + id,
        data: 'JSON'
    }).success(function (result) {
        $scope.object = result;
        $scope.object.OutgoingDate = new Date(parseInt(result.OutgoingDate.slice(6, -2)));
        $scope.object.ApplicationDate = new Date(parseInt(result.ApplicationDate.slice(6, -2)));
        $scope.object.sysCreatedDate = new Date(parseInt(result.sysCreatedDate.slice(6, -2)));
        $scope.object.KatoDictId = result.KatoParentId;
        $scope.object.ObjectTypeFirstDictId = result.ObjectTypeFirstDictId;
        $scope.object.ObjectTypeSecondDictId = result.ObjectTypeSecondDictId;
    
    });

    $scope.send = function () {
        $http({
            url: '/Client/Project/PostNew',
            method: 'POST',
            data: JSON.stringify($scope.object)
        }).success(function (response) {
            if (!response.IsError) {
                notify({ message: 'Объект отправлен на экспертизу успешно!', classes: 'alert-info', templateUrl: '/Content/templates/notify.html' });
                window.location.href = '/Client/Project/DetailCard?id=' + response.Data.Id;
            } else {
                alert(response.Message);
            }
        });
    }

    $scope.editProject = function() {
        $http({
            method: 'GET',
            url: '/Client/Project/Edit' ,
            data: 'JSON'
        }).success(function (result) {

            $scope.htmlContent = result;
        });

       
        loadDictionaryParams($scope, 'TerritoryKatoDict', $http, $scope.object.KatoParentId, 'KatoDict');
        loadDictionaryParams($scope, 'ObjectTypeSecondDict', $http, $scope.object.ObjectTypeFirstDictId, 'ObjectTypeDict');
        loadDictionaryParams($scope, 'ObjectTypeThirdDict', $http, $scope.object.ObjectTypeSecondDictId, 'ObjectTypeDict');
        loadDictionary($scope, 'BuildingTypeDict', $http);
        loadDictionary($scope, 'ObjectTypeDict', $http);
        loadDictionary($scope, 'KatoDict', $http);
        loadDictionary($scope, 'BuildingBranchDict', $http);
        loadDictionary($scope, 'DesignStageDict', $http);
        loadDictionary($scope, 'DangerClassDict', $http);
        loadDictionary($scope, 'CategoryEccologyDict', $http);
        loadDictionary($scope, 'DeclaredCostDict', $http);
        loadDictionary($scope, 'PrimaryReviewDict', $http);
        loadDictionary($scope, 'FloorCountDict', $http);
        loadDictionary($scope, 'LevelResponsibilityDict', $http);
        loadDictionary($scope, 'CustomerConstructionDict', $http);
        loadDictionary($scope, 'DeveloperDict', $http);
        loadDictionary($scope, 'SourceFinancingDict', $http);

        $scope.getObjectTypeSecond = function () {
            loadDictionaryParams($scope, 'ObjectTypeSecondDict', $http, $scope.object.ObjectTypeFirstDictId, 'ObjectTypeDict');
        };

        $scope.getObjectTypeThird = function () {
            loadDictionaryParams($scope, 'ObjectTypeThirdDict', $http, $scope.object.ObjectTypeSecondDictId, 'ObjectTypeDict');
        };

        $scope.getKatoCity = function () {
            loadDictionaryParams($scope, 'TerritoryKatoDict', $http, $scope.object.KatoDictId, 'KatoDict');
        };


        function loadDictionary($scope, name, $http) {
            $http({
                method: 'GET',
                url: '/Dictionaries/GetReference',
                data: 'JSON',
                params: { type: name }
            }).success(function (result) {
                $scope[name] = result;
                if (name === 'SourceFinancingDict') {
                    setDefaultValue($scope, $scope[name], "govermentId", "goverment");
                }
            });
        }
        function setDefaultValue($scope, list, selected, code) {
            angular.forEach(list, function (value, key) {
                if (value.Code == code)
                    $scope.object[selected] = value.Id;
            });
        }

        getExpertUnit($scope, $http, "ExecutorExpertUnit", "expert");
        getExpertUnit($scope, $http, "ExecutorSesUnit", "ses");
        getExpertUnit($scope, $http, "ExecutorEcoUnit", "eco");

    }
    $scope.detailProject = function() {
        $scope.htmlContent = $scope.detail;
    }
    $scope.postRequest = function () {

        $http({
            url: '/Client/Project/PostUpdate',
            method: 'POST',
            data: JSON.stringify($scope.object)
        }).success(function (result) {
            if (!result.IsError) {
                $scope.htmlContent = $scope.detail;
                $scope.object = result.Obj;
                $scope.object.OutgoingDate = new Date(parseInt(result.Obj.OutgoingDate.slice(6, -2)));
                $scope.object.ApplicationDate = new Date(parseInt(result.Obj.ApplicationDate.slice(6, -2)));
                $scope.object.sysCreatedDate = new Date(parseInt(result.Obj.sysCreatedDate.slice(6, -2)));
                $scope.object.KatoDictId = result.Obj.KatoParentId;
                $scope.object.ObjectTypeFirstDictId = result.Obj.ObjectTypeFirstDictId;
                $scope.object.ObjectTypeSecondDictId = result.Obj.ObjectTypeSecondDictId;
            } else {
                alert(result.Message);
            }
        });
    }
}

angular
    .module('app')
    .controller('cardProjectCtrl', ['$scope', '$http', 'notify', cardProjectCtrl]);

