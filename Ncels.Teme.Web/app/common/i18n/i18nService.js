i18nModule.service('i18nService', ['$translate', i18nService]);

function i18nService($translate) {

    this.changeLanguage = function (lang) {
        $translate.use(lang);
    }

}