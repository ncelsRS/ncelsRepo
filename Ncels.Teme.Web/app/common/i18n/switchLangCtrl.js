i18nModule.controller('switchLangCtrl', ['$scope', 'i18nService', '$rootScope', switchLangCtrl]);

function switchLangCtrl($scope, i18nService, $rootScope) {


    $scope.vm.selectedLang = $rootScope.vm.selectedLang;

    $scope.vm.languages = [
        {key: 'ru', value: 'Рус'},
        {key: 'kk', value: 'Қаз'},
        //{key: 'en', value: 'ENG'}
    ];

    $scope.vm.switchLang = function (lang) {

        $scope.vm.selectedLang = lang;
        $rootScope.vm.selectedLang = lang;

        $scope.langChangedEvent(lang);

        i18nService.changeLanguage(lang);

        if (lang === 'kk')
            kendo.culture("kk-KZ");

        if (lang === 'ru')
            kendo.culture("ru-RU");

        if (lang === 'en')
            kendo.culture("en-US");

    };


    $scope.langChangedEvent = function (lang) {
        $rootScope.$broadcast('lang-changed', lang);
    };

}