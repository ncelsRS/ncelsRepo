angular.module('teme').controller('loginCtrl', loginCtrl);

function loginCtrl($rootScope, $scope, customAuthSvc, $state) {
    var vm = this;
    $scope.loginCtrl = vm;

    vm.errors = {};

    vm.submit = function (e) {
        if (!vm.login) vm.errors.login = true;
        if (!vm.password) vm.errors.password = true;
        vm.isValidate = true;
        var url = $state.href('home.index');
        window.open(url, '_blank');
    }
}