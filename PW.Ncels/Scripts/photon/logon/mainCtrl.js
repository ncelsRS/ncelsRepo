function MainCtrl($scope, $http,store) {
    $scope.clearStore = function() {
        store.remove('task');
        store.remove('taskCount');
        store.remove('notification');
        store.remove('countNew');
    }
};

angular
    .module('app')
    .controller('MainCtrl', ['$scope', '$http','store',MainCtrl])