function setReadNotification(control) {
    if (!$(control).find("span").hasClass("not-read")) {
        return;
    }
    var id = $(control).attr("notificationId");
    $('.is-read-'+id).each(function () {
        if ($(this).hasClass("not-read")) {
            $(this).removeClass("not-read");
        }
    });
    $('#readId_' + id).text("Прочитано");
    $('#readId_' + id).removeClass("label-primary");
    $('#readId_' + id).addClass("label-warning");
    var rowindex = parseInt($("#topNavCountNew").text(), 10) || 0;
    rowindex = rowindex - 1;
    $("#topNavCountNew").text(rowindex);
    var params = JSON.stringify({ 'id': id });
    $.ajax({
        type: "POST",
        url: '/Notification/SetReadNotification',
        data: params,
        dataType: 'json',
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {

        },
        error: function () {
            alert("Connection Failed. Please Try Again");
        }
    });
}

function navbarNotification($scope, $http, store, $interval) {

 //refresh each 5 minutes
    //$interval(readTask, 300000);
    //$interval(readNotification, 300000);
 //   $interval(readNotification, 3000);

    //if (store.get('task') == null) {
    //   readTask();
    //} else {
    //    $scope.task = store.get('task');
    //    $scope.taskCount = store.get('taskCount');
    //}
    if (store.get('notification') == null) {
        
        readNotification();
    } else {
        $scope.notification = store.get('notification');
        $scope.countNew = store.get('countNew');
    }
     
    //function readTask() {
    //    $http({
    //        method: 'GET',
    //        url: '/Notification/GetNewTask',
    //        data: 'JSON',
    //    }).success(function (result) {
    //        $scope.task = 0;
    //        $scope.taskCount = 0;
    //        store.set('task', result.Data);
    //        store.set('taskCount', result.Count);
    //    });
    //}

     function readNotification(){
        $http({
            method: 'GET',
            url: '/Notification/GetNewNotification',
            data: 'JSON'
        }).success(function (result) {
            $scope.notification = result.Data;
         //  $scope.notification.push(result.Data);
      //      store.set('notification', result.Data);
          //  $scope.notification = 0; //store.get('notification');
            store.set('countNew', result.Count);
            $scope.countNew = result.Count; //store.get('countNew');
        });
    }

}

function notificationAll($scope, $http) {
    $scope.initNotification = function () {
        $http({
            method: 'GET',
            url: '/Notification/GetListAllNotification',
            data: 'JSON',
        }).success(function (result) {
            $scope.notification = result;
            angular.forEach($scope.notification, function (value, key) {
                $scope.isCheked.push(value);
                $scope.notification[key].CreatedDate = new Date(parseInt(value.CreatedDate.replace("/Date(", "").replace(")/", ""), 10));
            });
        });
    }

    $scope.chekedList = [];
    $scope.isCheked = [];
    $scope.checked = function (isCheked, id) {
        if (isCheked) {
            $scope.chekedList.push(id);
        } else {
            var index = $scope.chekedList.indexOf(id);
            if (index > -1) {
                $scope.chekedList.splice(index, 1);
            }
        }
    }

    $scope.setViewed = function () {
        if ($scope.chekedList.length > 0) {
            $http({
                url: '/Notification/SetViewed',
                method: 'POST',
                data: JSON.stringify($scope.chekedList)
            }).success(function (response) {
                if (!response.IsError) {
                    $scope.initNotification();
                    //var oldCount = $("#topNavCountNew").text();
                    //$("#topNavCountNew").text(oldCount - $scope.chekedList.length);
                    //$("#navCountNew").text(oldCount - $scope.chekedList.length);
                } else {
                    alert(response.Message);
                }
            });
        } else {
            alert("Выберите элементы");
        }

    }
}

function notificationNew($scope, $http) {
    $scope.initNotification = function () {
        $http({
            method: 'GET',
            url: '/Notification/GetListNewNotification',
            data: 'JSON',
        }).success(function (result) {
            $scope.notification = result;
            angular.forEach($scope.notification, function (value, key) {
                $scope.isCheked.push(value);
                $scope.notification[key].Date = new Date(parseInt(value.Date.replace("/Date(", "").replace(")/", ""), 10));
            });
        });
    }

    $scope.chekedList = [];
    $scope.isCheked = [];
    $scope.checked = function (isCheked, id) {
        if (isCheked) {
            $scope.chekedList.push(id);
        } else {
            var index = $scope.chekedList.indexOf(id);
            if (index > -1) {
                $scope.chekedList.splice(index, 1);
            }
        }
    }

    $scope.setViewed = function () {
        if ($scope.chekedList.length > 0) {
            $http({
                url: '/Notification/SetViewed',
                method: 'POST',
                data: JSON.stringify($scope.chekedList)
            }).success(function (response) {
                if (!response.IsError) {
                    $scope.initNotification();
                    //var oldCount = $("#topNavCountNew").text();
                    //$("#topNavCountNew").text(oldCount - $scope.chekedList.length);
                    //$("#navCountNew").text(oldCount - $scope.chekedList.length);
                } else {
                    alert(response.Message);
                }
            });
        } else {
            alert("Выберите элементы");
        }

    }

}


function notificationList($scope, DTColumnBuilder, $http, $uibModal) {
  
    $scope.dtColumns = [
    DTColumnBuilder.newColumn("Id", "Номер").withOption('name', 'CreatedDate'),
    DTColumnBuilder.newColumn("CreatedDate", "Дата").withOption('name', 'CreatedDate').renderWith(dateformatHtml),
    DTColumnBuilder.newColumn("Note", "Краткое содержание").withOption('name', 'CreatedDate'),
    ];
}

angular
    .module('app')
    .controller('navbarNotification', ['$scope', '$http', 'store', '$interval',navbarNotification])
    .controller('notificationList', ['$scope', 'DTColumnBuilder', '$http', '$uibModal', notificationList])
    .controller('notificationAll', ['$scope', '$http',notificationAll])
    .controller('notificationNew', ['$scope', '$http',notificationNew]);