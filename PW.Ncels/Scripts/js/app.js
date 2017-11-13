/**
 * INSPINIA - Responsive Admin Theme
 *
 */
(function () {
    angular.module('app', [
        // Routing
        // ocLazyLoad
        'ui.bootstrap', // Ui Bootstrap
        'pascalprecht.translate', // Angular Translate
        // Idle timer
        'ngSanitize', // ngSanitize
        'datatables',
        'datatables.buttons',
        'ui.select',
        'datePicker',
        'summernote',
        'angularFileUpload',
        'ngJsTree',
        'oitozero.ngSweetAlert',
        'cgNotify',
        'angular-storage',
            'ui.mask',
            'ui.grid',
            'ui.grid.selection'
    ]);
})();


// Other libraries are loaded dynamically in the config.js file using the library ocLazyLoad