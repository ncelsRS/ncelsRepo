angular.module('authLocalStorageModule', [])
    .service('authLocalStorageSvc', [authLocalStorageSvc]);

function authLocalStorageSvc() {

    var _authData = null;

    this.clearAuthData = function () {
        _authData = null;
        if (window.localStorage.getItem('AUTH_DATA'))
            window.localStorage.removeItem('AUTH_DATA');
    };

    this.saveAuthData = function (authData) {
        this.clearAuthData();
        _authData = authData;
        window.localStorage.setItem('AUTH_DATA', JSON.stringify(authData));
    };

    this.getAuthData = function () {
        if (_authData) return _authData;
        if (window.localStorage.getItem('AUTH_DATA')) {
            _authData = JSON.parse(window.localStorage.getItem('AUTH_DATA'));
            return _authData;
        }
    };
}
