var i18nModule = angular.module('i18nModule', ['commonModule', 'pascalprecht.translate'])
    .config(['$translateProvider', '$translatePartialLoaderProvider', 'COMMON_API_HOST', i18nModuleConfig]);


function i18nModuleConfig($translateProvider, $translatePartialLoaderProvider, COMMON_API_HOST) {


    //$translatePartialLoaderProvider.addPart('main');
    $translatePartialLoaderProvider.addPart('siteMenu');
    $translatePartialLoaderProvider.addPart('footer');

    $translateProvider.useLoader('$translatePartialLoader', {
        urlTemplate: COMMON_API_HOST + '/api/translations?part={part}&lang={lang}'
    });

    var savedLang = window.localStorage.getItem('NG_TRANSLATE_LANG_KEY');

    $translateProvider.preferredLanguage(savedLang ? savedLang : 'ru');// is applied on first load
    $translateProvider.useLocalStorage();// saves selected language to localStorage
    $translateProvider.useSanitizeValueStrategy('escaped');

    // if (savedLang) {
    //     if (savedLang === 'kz')
    //         kendo.culture("kk-KZ");
    //
    //     if (savedLang === 'ru')
    //         kendo.culture("ru-RU");
    //
    //     if (savedLang === 'en')
    //         kendo.culture("en-US");
    // }
    // else {
    //     kendo.culture("ru-RU");
    // }
}

i18nModule.run(function ($rootScope) {

    var selectedLang = window.localStorage.getItem('NG_TRANSLATE_LANG_KEY');
    if (!$rootScope.vm) $rootScope.vm = {};
    $rootScope.vm.selectedLang = selectedLang ? selectedLang : 'ru';
});

