
function deciseAgreeement($scope, $http, notify) {
    $scope.deciseAgreeement = function () {
        if ($scope.executeAgreementForm.$valid) {
            $http({
                url: '/Expertise/Agreement/DecisionAgree',
                method: 'POST',
                data: JSON.stringify($scope.agreement)
            }).success(function (response) {
                if (!response.IsError) {
                    detailReport($scope, $http, response.Id);
                    notify({
                        message: 'Согласование исполненно!', classes: 'alert-info', templateUrl: '/Content/templates/notify.html'
                    });
                } else {
                    alert(response.Message);
                }
            });
        }
    }


    $scope.initAgreement = function (attach, taskId, projectId) {
        $scope.agreement = {
            Date: new Date(),
            Id: taskId,
            ProjectId: projectId
        };
        $scope.agreement.AttachPath = attach;

        $http({
            method: 'GET',
            url: '/Dictionaries/GetAgreementState',
            data: 'JSON'
        }).success(function (result) {
            $scope.AgreementType = result;
        });
    }
    $scope.detailAgreement = function () {
        $scope.htmlContentReportIn = '';
    }
}

function deciseSing($scope, $http,notify) {
    $scope.deciseSigning = function () {
        if ($scope.executeSigningForm.$valid) {
            $http({
                url: '/Expertise/Signing/DecisionSign',
                method: 'POST',
                data: JSON.stringify($scope.sign)
            }).success(function (response) {
                if (!response.IsError) {
                    detailReport($scope, $http, response.Id);
                    notify({
                        message: 'Подписание исполненно!', classes: 'alert-info', templateUrl: '/Content/templates/notify.html'
                    });
                } else {
                    alert(response.Message);
                }
            });
        }
    }


    $scope.initSign = function (attach, taskId, projectId) {
        $scope.sign = {
            Date: new Date(),
            Id: taskId,
            ProjectId: projectId
        };
        $scope.sign.AttachPath = attach;

        $http({
            method: 'GET',
            url: '/Dictionaries/GetSignState',
            data: 'JSON'
        }).success(function (result) {
            $scope.SignType = result;
        });
    }
    $scope.detailSign = function () {
        $scope.htmlContentReportIn = '';
    }
}

function detailReport($scope, $http,id) {
    $http({
        url: '/Expertise/Task/Report?taskId=' + id,
        method: 'GET'
    }).success(function (response) {
        if (!response.IsError) {
            $scope.htmlContentReportIn = response;
        } else {
            alert(response.Message);
        }
    });
}


angular
    .module('app')
    .controller('deciseAgreeement', ['$scope', '$http', 'notify', deciseAgreeement])
    .controller('deciseSing', ['$scope', '$http', 'notify', deciseSing]);
