angular.module('authInterceptorModule', [])
    .factory('authInterceptorSvc', ['$q', '$injector', '$location', 'authLocalStorageSvc', authInterceptorSvc]);

function authInterceptorSvc($q, $injector, $location, authLocalStorageSvc) {

    var authInterceptorServiceFactory = {};

    var _request = function (config) {
        config.headers = config.headers || {};

        var authService = $injector.get('customAuthSvc');
        if (authService.checkAuth()) {
            var authData = authLocalStorageSvc.getAuthData();
            if (authData) {
                config.headers.Authorization = 'Bearer ' + authData.token;
            }
        }

        return config;
    };

    var _responseError = function (rejection) {
        if (rejection.status === 401) {
            var authService = $injector.get('customAuthSvc');

            var authData = authLocalStorageSvc.getAuthData();

            if (authData) {
                if (authData.useRefreshTokens) {

                    if (rejection.config.url.indexOf('checkAuth') === -1) {
                        authService.logOut();
                        $location.path('/login');
                    }
                }
            } else {
                authService.logOut();
                $location.path('login');
            }
        }

        return $q.reject(rejection);
    };

    authInterceptorServiceFactory.request = _request;
    authInterceptorServiceFactory.responseError = _responseError;

    return authInterceptorServiceFactory;
}