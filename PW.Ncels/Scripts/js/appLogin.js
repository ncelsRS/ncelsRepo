/**
 * INSPINIA - Responsive Admin Theme
 *
 */
(function () {
    angular.module('app', [
        // ocLazyLoad
        'ui.mask',
        'ui.bootstrap', // Ui Bootstrap
        'pascalprecht.translate', // Angular Translate
        'ngSanitize',
        'ui.select',
        'datePicker',
        'angularFileUpload',
        'cgNotify',
        'angular-storage'
    ]);
})();

// Other libraries are loaded dynamically in the config.js file using the library ocLazyLoad