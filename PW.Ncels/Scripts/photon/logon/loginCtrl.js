function iinCheck(iin) {
    //clientType: 1 - Физ. лицо (ИИН), 2 - Юр. лицо (БИН)
    //birthDate: дата рождения (в формате Javascript Date)
    //sex: true - м, false - ж
    //isResident: true - резидент, false: нерезидент (true: по умолчанию)

    if (!iin) return false;
    if (iin.length != 12) return false;
    if (!(/[0-9]{12}/.test(iin))) return false;

    //Проверяем контрольный разряд
    var b1 = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11];
    var b2 = [3, 4, 5, 6, 7, 8, 9, 10, 11, 1, 2];
    var a = [];
    var controll = 0;
    for (var i = 0; i < 12; i++) {
        a[i] = parseInt(iin.substring(i, i + 1));
        if (i < 11) controll += a[i] * b1[i];
    }
    controll = controll % 11;
    if (controll == 10) {
        controll = 0;
        for (var i = 0; i < 11; i++)
            controll += a[i] * b2[i];
        controll = controll % 11;
    }
    if (controll != a[11]) return false;
    return true;
}

function registerRequest($scope, $http, $window) {
    loadDictionary($scope, "Country", $http);

//    $scope.user = {
//        PassportDate: new Date()
//};
//    loadDictionary($scope, 'CorespondentTypeDict', $http);
//    loadDictionary($scope, 'BankNameDict', $http);
//    loadDictionary($scope, 'KatoDict', $http);
//    loadDictionary($scope, 'DocumentInfoDict', $http);
//    loadDictionary($scope, 'IncorporationFormDict', $http);
//    loadDictionary($scope, 'BuildingTypeDict', $http);

    //$scope.getActualCity = function() {
    //    loadDictionaryParams($scope, 'ActualCityKatoDict', $http, $scope.user.ActualRegionKatoDictId, 'KatoDict');
    //};

    //$scope.getLegalCity = function() {
    //    loadDictionaryParams($scope, 'LegalCityKatoDict', $http, $scope.user.LegalRegionKatoDictId, 'KatoDict');
    //};


    $scope.postRequest = function () {
        if ($scope.registerForm.$valid) {

            //if ($scope.user.hasIin && !iinCheck($scope.user.Iin)) {
            //    alert("Некорректное значение ИИН");
            //    return;
            //}
            //if ($scope.user.Bin != null && $scope.user.Bin.length > 0 && !iinCheck($scope.user.Bin)) {
            //    alert("Некорректное значение БИН");
            //    return;
            //}

            if ($scope.user.password != $scope.user.confirmPassword) {
                alert("Пароли не совпадают");
                return;
            }

            $scope.user.isEcp = false;
            if ($scope.user.userType == 'fl') {
                $scope.user.personType = $scope.user.hasIin ? "0" : "2";
            } else if ($scope.user.userType == 'ul') {
                $scope.user.personType = $scope.user.hasIin ? "1" : "3";
            }
            else {
                $scope.user.isEcp = true;
            }

            $http({
                url: '/Account/PostRequest',
                method: 'POST',
                data: JSON.stringify($scope.user)
            }).success(function(response) {
                if (!response.IsError) {
                    $window.location.href = '/Account/RegisterSuccess';
                } else {
                    alert(response.Message);
                }
            });
        }
    }
    $scope.onStepChanged = function(event, currentIndex, priorIndex) {

    };

    $scope.getBankBik = function() {
        var id = $scope.user.BankNameDictId;
        $http({
            method: 'GET',
            url: '/Dictionaries/GetReference',
            data: 'JSON',
            params: { type: 'BankBikDict', id: id }
        }).success(function (result) {
            $scope.user.BankBik = result[0].Name;
        });
    }
}


function setDefaultValue($scope,list, selected, code) {
    angular.forEach(list, function (value, key) {
        if (value.Code == code)
            $scope.user[selected] = value.Id;
    });
}

function loadDictionary($scope, name, $http) {
    $http({
        method: 'GET',
        url: '/Dictionaries/GetReference',
        data: 'JSON',
        params: { type: name }
    }).success(function (result) {
        $scope[name] = result;
        if (name == 'CorespondentTypeDict') {
            setDefaultValue($scope,$scope[name], "CorespondentTypeDictId", "customer");
        }
        if (name == 'DocumentInfoDict') {
            setDefaultValue($scope, $scope[name], "DocumentInfoDictId", "attorney");
        }
        if (name == 'IncorporationFormDict') {
            setDefaultValue($scope, $scope[name], "IncorporationFormDictId", "goverment");
        }
        if (name == 'Country') {
            $scope[name] = $scope[name].filter(function (item) {
                return !(item.Code == 'KZ');
            });
        }
    });
}
function loadDictionaryParams($scope, name, $http, id,dicName) {
    $http({
        method: 'GET',
        url: '/Dictionaries/GetReference',
        data: 'JSON',
        params: { type: dicName, id: id }
    }).success(function (result) {
        $scope[name] = result;
    });
}

function registerAttachBin($scope, $http, $window, FileUploader) {

    $scope.uploaders = {};
    $http({
        method: 'GET',
        url: '/Upload/GetPathAttach',
        data: 'JSON'
    }).success(function (path) {

        $scope.user.AttachPath = path;

        $http({
            method: 'GET',
            url: '/Dictionaries/GetReference',
            data: 'JSON',
            params: { type: 'sysAttachBin' }
        }).success(function (result) {
            result.forEach(function (item, i, arr) {
                $scope.uploaders[item.Id] = new FileUploader({
                    url: '/Upload/FilePost?code=' + item.Id + '&path=' + path
                });
                $scope.uploaders[item.Id].onSuccessItem = function (fileItem, response, status, headers) {
                    fileItem.Id = response.Id;               
                };
        
            });
            $scope.attachOrg = result;
        });

    });

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

function registerAttachIin($scope, $http, $window, FileUploader) {

    $scope.uploaders = {};

    $http({
        method: 'GET',
        url: '/Upload/GetPathAttach',
        data: 'JSON'
    }).success(function (path) {

        $scope.user.AttachPath = path;

        $http({
            method: 'GET',
            url: '/Dictionaries/GetReference',
            data: 'JSON',
            params: { type: 'sysAttachIin' }
        }).success(function (result) {
            result.forEach(function (item, i, arr) {
                $scope.uploaders[item.Id] = new FileUploader({
                    url: '/Upload/FilePost?code=' + item.Id + '&path=' + path
                });
                $scope.uploaders[item.Id].onSuccessItem = function (fileItem, response, status, headers) {
                    fileItem.Id = response.Id;
                    console.info('onSuccessItem', fileItem, response, status, headers);
                };
                $scope.uploaders[item.Id].onErrorItem = function (fileItem, response, status, headers) {
                    console.info('onErrorItem', fileItem, response, status, headers);
                };
                $scope.uploaders[item.Id].onCancelItem = function (fileItem, response, status, headers) {
                    console.info('onCancelItem', fileItem, response, status, headers);
                };
                $scope.uploaders[item.Id].onCompleteItem = function (fileItem, response, status, headers) {
                    console.info('onCompleteItem', fileItem, response, status, headers);
                };
                $scope.uploaders[item.Id].onCompleteAll = function () {
                    console.info('onCompleteAll');
                };
            });
            $scope.attachOrg = result;
        });

    });

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
angular
    .module('app')
    .controller('registerAttachIin', ['$scope', '$http', '$window', 'FileUploader',registerAttachIin])
    .controller('registerAttachBin', ['$scope', '$http', '$window', 'FileUploader',registerAttachBin])
    .controller('registerRequest',['$scope', '$http', '$window', registerRequest]);