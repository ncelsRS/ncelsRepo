var host = 'http://localhost:';
// var host = 'http://mpl.kaf.kz:';
// var host = 'https://citysoft.ddns.net:';
angular.module('configModule', [])
    .constant('HostIdentity', host + '5433')
    .constant('HostAdmin', host + '5003')
;