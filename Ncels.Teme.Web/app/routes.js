angular.module('teme').config(['$stateProvider', '$urlRouterProvider', '$ocLazyLoadProvider', '$httpProvider',
    function ($stateProvider, $urlRouterProvider, $ocLazyLoadProvider, $httpProvider) {

        $httpProvider.defaults.useXDomain = true;
        delete $httpProvider.defaults.headers.common['X-Requested-With'];

        $httpProvider.interceptors.push('authInterceptorSvc');

        $urlRouterProvider.otherwise('/login');

        $ocLazyLoadProvider.config({
            debug: false
        });

        $stateProvider
            .state('login', {
                url: '/login',
                templateUrl: 'app/account/login.html',
                resolve: {
                    loadCSS: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([{
                            serie: true,
                            name: 'loginStyles',
                            files: ['app/account/login.css']
                        }]);
                    }],
                    loadPlugin: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([{
                            serie: true,
                            name: 'loginCtrl',
                            files: ['app/account/loginCtrl.js']
                        }])
                    }]
                }
            })
            .state('home', {
                url: '/home',
                abstract: true,
                templateUrl: 'app/home/layout.html',
                resolve: {
                    loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                        // you can lazy load controllers
                        return $ocLazyLoad.load({
                            files: ['app/home/layoutCtrl.js']
                        });
                    }]
                }
            })
            .state('home.index', {
                url: '/index',
                templateUrl: 'app/home/index/index.html',
                resolve: {
                    loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                        // you can lazy load controllers
                        return $ocLazyLoad.load({
                            files: ['app/home/index/indexCtrl.js']
                        });
                    }]
                }
            })
    }]);