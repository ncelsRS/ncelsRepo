var teme = angular.module('teme', [
    'ui.router',
    'oc.lazyLoad',
    'angular-jwt',
    'configModule',
    'authLocalStorageModule',
    'customAuthModule',
    'authInterceptorModule',
    'dataSvcModule',
    //'i18nModule',
    'kendo.directives'
]);

teme.config([function () {
}]);

teme.run(['$rootScope', '$state', '$stateParams', '$timeout',
    'customAuthSvc',
    function ($rootScope, $state, $timeout, $stateParams, customAuthSvc) {
        $rootScope.vm = {};
    }]);