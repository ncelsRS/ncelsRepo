function createExpetiseObject($scope, $http, FileUploader,notify) {
    $scope.object = {
        OutgoingDate: new Date(),
        ApplicationDate: new Date()
    };

  loadDictionary($scope, 'BuildingTypeDict', $http);
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
  loadDictionary($scope, 'ObjectTypeDict', $http);

     function loadDictionary($scope, name, $http) {
         $http({
             method: 'GET',
             url: '/Dictionaries/GetReference',
             data: 'JSON',
             params: { type: name }
         }).success(function (result) {
             $scope[name] = result;
             if (name === 'PrimaryReviewDict') {
                 setDefaultValue($scope, $scope[name], "PrimaryReviewDictId", "first");
             }
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

    $scope.getObjectTypeSecond = function () {
        loadDictionaryParams($scope, 'ObjectTypeSecondDict', $http, $scope.object.ObjectTypeFirstDictId, 'ObjectTypeDict');
    };

    $scope.getObjectTypeThird = function () {
        loadDictionaryParams($scope, 'ObjectTypeThirdDict', $http, $scope.object.ObjectTypeSecondDictId, 'ObjectTypeDict');
    };

    $scope.getKatoCity = function () {
        loadDictionaryParams($scope, 'TerritoryKatoDict', $http, $scope.object.KatoDictId, 'KatoDict');
    };

    getExpertUnit($scope, $http, "ExecutorExpertUnit", "expert");
    getExpertUnit($scope, $http, "ExecutorSesUnit", "ses");
    getExpertUnit($scope, $http, "ExecutorEcoUnit", "eco");

    $scope.postRequest = function () {
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


 
   
    $scope.onStepChanged = function(event, currentIndex, priorIndex) {
        if (currentIndex === 3) {
            $http({
                url: '/Client/Project/PostNewGetHtml',
                method: 'POST',
                data: JSON.stringify($scope.object)
            }).success(function (response) {
      
                if (!response.IsError) {
                    $scope.object.Id = response.Data.Id;

                    $('#projectView').html(response.Html);
                    notify({ message: 'Проект сохранен в черновиках', classes: 'alert-info', templateUrl: '/Content/templates/notify.html' });
                } else {
                    alert(response.Message);
                }
            });
        }
    }


}
 function getExpertUnit($scope, $http, name, code) {
     $http({
         method: 'GET',
         url: '/Dictionaries/GetExpertUnit',
         data: 'JSON',
         params: { code: code }
     }).success(function (result) {
         $scope[name] = result;
     });
 }




function loadDictionaryParams($scope, name, $http, id, dicName) {
    $http({
        method: 'GET',
        url: '/Dictionaries/GetReference',
        data: 'JSON',
        params: { type: dicName, id: id }
    }).success(function (result) {
        $scope[name] = result;
    });
}

angular
    .module('app')
    .controller('createExpetiseObject', ['$scope', '$http', 'FileUploader','notify',createExpetiseObject])