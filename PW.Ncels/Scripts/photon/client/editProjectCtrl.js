function editExpetiseObject($scope, $http, FileUploader) {
    $scope.object = {};

  
    var id = $("#projectId").val();
    $http({
        method: 'GET',
        url: '/Client/Project/GetDataEdit?id=' + id,
        data: 'JSON'
    }).success(function(result) {
        $scope.object = result;
        $scope.object.OutgoingDate = new Date(parseInt(result.OutgoingDate.slice(6, -2)));
        $scope.object.ApplicationDate = new Date(parseInt(result.ApplicationDate.slice(6, -2)));
        $scope.object.sysCreatedDate = new Date(parseInt(result.sysCreatedDate.slice(6, -2)));
        $scope.object.KatoDictId = result.KatoParentId;
        $scope.object.ObjectTypeFirstDictId = result.ObjectTypeFirstDictId;
        $scope.object.ObjectTypeSecondDictId = result.ObjectTypeSecondDictId;
        loadDictionaryParams($scope, 'TerritoryKatoDict', $http, result.KatoParentId, 'KatoDict');
        loadDictionaryParams($scope, 'ObjectTypeSecondDict', $http, result.ObjectTypeFirstDictId, 'ObjectTypeDict');
        loadDictionaryParams($scope, 'ObjectTypeThirdDict', $http, result.ObjectTypeSecondDictId, 'ObjectTypeDict');
    });
 
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

    $scope.postRequest = function () {

        $http({
            url: '/Client/Project/PostUpdate',
            method: 'POST',
            data: JSON.stringify($scope.object)
        }).success(function (response) {
            if (!response.IsError) {
                window.location = '/Home/Success';
            } else {
                alert(response.Message);
            }
        });
    }


    $scope.uploaders = {};



    var path = $('#pathAttach').val();

    loadAttach(FileUploader, $scope, $http, id, 'sysAttachMaterialsDict', path);
    loadAttach(FileUploader, $scope, $http, id, 'sysAttachExpertDic', path);
    loadAttach(FileUploader, $scope, $http, id, 'sysAttachProjectDocumentationDict', path);


    $scope.AttachRemove = function (item) {
        $http({
            url: '/Upload/FileDelete',
            method: 'POST',
            data: JSON.stringify({ Id: item.Id })
        }).success(function (response) {
            if (!response.IsError) {
                item.remove();
            } else {

            }
        });
    }
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

function loadAttach(FileUploader, $scope, $http,id, name, path) {
    $http({
        method: 'GET',
        url: '/Upload/GetAttachListAll',
        data: 'JSON',
        params: { id: id, type: name }
    }).success(function (result) {
        result.forEach(function (item, i, arr) {
            $scope.uploaders[item.Id] = new FileUploader({
                url: '/Upload/FilePost?code=' + item.Id + '&path=' + path
            });
            $scope.uploaders[item.Id].onSuccessItem = function (fileItem, response, status, headers) {
                fileItem.Id = response.Id;

            };
            item.Items.forEach(function (file, i, arr) {
                var dummy = new FileUploader.FileItem($scope.uploaders[item.Id], {
                    lastModifiedDate: file.sysCreatedDate,
                    size: file.AttachSize,
                    name: file.AttachName


                });
                dummy.Id = file.AttachId;
                dummy.progress = 100;
                dummy.isUploaded = true;
                dummy.isSuccess = true;

                $scope.uploaders[item.Id].queue.push(dummy);
            });
        });

        $scope[name] = result;
    });
}


angular
    .module('app')
    .controller('editExpetiseObject', ['$scope', '$http', 'FileUploader',editExpetiseObject])