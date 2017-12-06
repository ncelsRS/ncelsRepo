i18nModule.directive('switchLang', [switchLang]);

function switchLang() {
    console.log('i18nModule.directive');
    return {
        restrict: 'EA',
        templateUrl: './common/i18n/switchLang.html'
    }
}
