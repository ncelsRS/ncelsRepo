angular.module('customAuthModule', [])
    .factory('customAuthSvc', customAuthSvc);

function customAuthSvc($rootScope, $http, $q, jwtHelper, dataSvc, HostIdentity, authLocalStorageSvc, $state) {

    var url = '/oauth/token';

    var _login = function (userLoginData) {
        _logOut();
        var defer = $q.defer();
        var data = 'grant_type=password';
        data += '&username=' + userLoginData.login;
        data += '&password=' + userLoginData.pass;
        data += '&client_id=' + 'web';
        data += '&client_secret=' + 'ECDF1DD2A9219DAA8F4283EAC5B8C';
        $http({
            method: 'POST',
            url: HostIdentity + url,
            data: data,
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }
        }).then(function successCallback(response) {
            if (response.status === 200) {
                if (response.data) {
                    var data = {
                        token: response.data.access_token,
                        refreshToken: response.data.refresh_token,
                        useRefreshTokens: true
                    };
                    $.ajaxSetup({
                        beforeSend: function (xhr) {
                            xhr.setRequestHeader('Authorization', 'Bearer ' + data.token);
                        }
                    });
                    authLocalStorageSvc.saveAuthData(data);
                    var jwtDecoded = jwtHelper.decodeToken(data.token);
                    $rootScope.vm.currentUser = jwtDecoded.user;
                }
            } else
                _logOut();
            defer.resolve(response);
        }, function errorCallback(response) {
            _logOut();
            defer.resolve(response);
        });

        return defer.promise;

    };

    var _logOut = function () {
        authLocalStorageSvc.clearAuthData();
        $rootScope.vm.currentUser = null;
        $state.go('appSimple.login');
    };

    var _refreshToken = function () {
        var defer = $q.defer();
        var authData = authLocalStorageSvc.getAuthData();
        if (authData) {
            if (authData.useRefreshTokens) {
                var data = 'grant_type=refresh_token';
                data += '&refresh_token=' + authData.refreshToken;
                data += '&client_id=' + 'web';
                data += '&client_secret=' + 'ECDF1DD2A9219DAA8F4283EAC5B8C';
                authLocalStorageSvc.clearAuthData();
                $http({
                    method: 'POST',
                    url: HostIdentity + url,
                    data: data,
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded'
                    }
                }).then(function successCallback(response) {
                    if (response.status === 200) {
                        if (response.data) {
                            var data = {
                                token: response.data.access_token,
                                refreshToken: response.data.refresh_token,
                                useRefreshTokens: true
                            };
                            $.ajaxSetup({
                                beforeSend: function (xhr) {
                                    xhr.setRequestHeader('Authorization', 'Bearer ' + data.token);
                                }
                            });
                            authLocalStorageSvc.saveAuthData(data);
                            var jwtDecoded = jwtHelper.decodeToken(data.token);
                            $rootScope.vm.currentUser = jwtDecoded.user;
                        }
                    } else
                        _logOut();
                    defer.reject(response);
                }, function errorCallback(response) {
                    _logOut();
                    defer.reject(response);
                });
                return defer.promise;
            }
        }
        return deferred.promise;
    };

    var _checkAuth = function () {
        var authData = authLocalStorageSvc.getAuthData();
        if (authData) {
            var jwtDecoded = jwtHelper.decodeToken(authData.token);
            var now = new Date();
            if (now < jwtDecoded.exp) {
                $rootScope.vm.currentUser = jwtDecoded.user;
                $.ajaxSetup({
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader('Authorization', 'Bearer ' + authData.token);
                    }
                });
            }
            else {
                jwtDecoded = jwtHelper.decodeToken(authData.refreshToken);
                if (now < jwtDecoded.exp) {
                    $rootScope.vm.currentUser = jwtDecoded.user;
                    _refreshToken();
                }
            }
        }
        return $rootScope.vm.currentUser;
    };

    return {
        login: _login,
        logOut: _logOut,
        refreshToken: _refreshToken,
        checkAuth: _checkAuth
    };
}