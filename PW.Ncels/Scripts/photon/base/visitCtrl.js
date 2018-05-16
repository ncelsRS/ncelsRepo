function visitList($scope, DTColumnBuilder, $http, $uibModal) {
    $scope.dtColumnsCor = [
        DTColumnBuilder.newColumn("VisitId", "Номер").withOption('name', 'VisitId'),
        DTColumnBuilder.newColumn("VisitStatusName", "Статус").withOption('name', 'VisitStatusName'),
        DTColumnBuilder.newColumn("VisitDate", "Дата").withOption('name', 'VisitDate').renderWith(dateformatHtml),
        DTColumnBuilder.newColumn(null).withTitle('Начало/Длина').renderWith(function (data, type, full) {
            var begin = moment().startOf('day')
                .minutes(data.VisitTimeBegin)
                .format('HH:mm');
            var result = begin + " (" + data.VisitDuration + "мин.)";
            return result;
        }).withOption('sortable', false),
        DTColumnBuilder.newColumn(null).withTitle('Вид приёма').renderWith(function (data, type, full) {
            var visitType = data.VisitTypeName + '(' + data.VisitTypeGroup + ')';
            return visitType;
        }).withOption('sortable', false),
        DTColumnBuilder.newColumn(null).withTitle('Принимающий').renderWith(function (data, type, full) {
            var empl = data.EmployeeLastName + ' ' + data.EmployeeFirstName + ' ' + data.EmployeeMiddleName;
            return empl;
        }).withOption('sortable', false),
        DTColumnBuilder.newColumn("VisitComment", "Комментарий").withOption('sortable', false),
        DTColumnBuilder.newColumn(null).withTitle('Отменить').renderWith(function (data, type, full) {
            var result = "";
            if (data.VisitStatusId == 1 || data.VisitStatusId == 2)//ожидает подтверждения/запланировано
            {
                result = "<button class='btn btn-default btn-sm' onclick='cancelVisit(" + data.VisitId + ");'>отменить</button>";
            }
            return result;
        }).withOption('defaultContent', ' ').notSortable(),
        DTColumnBuilder.newColumn(null).withTitle('Оценка').renderWith(function (data, type, full) {
            var result = "";
            if (data.VisitStatusId == 4)//ожидает подтверждения/запланировано
            {
                if (data.VisitRatingValue == null || 1 == 0) {
                    result = "<button class='btn btn-default btn-sm' onclick='ratingVisit(" + data.VisitId + ");'>оценить</button>";
                }else {
                    result = "<b>"+data.VisitRatingValue +" из 5</b> ";
                    if(data.VisitRatingComment != null) {
                        result = result + ' <div>' + data.VisitRatingComment+' </div>';
                    }
                }
            }
            return result;
        }).withOption('defaultContent', ' ').notSortable()
    ];

    cancelVisit = function (id) {
        $http({
            url: '/Visit/Delete?id=' + id,
            method: 'POST'
        }).success(function (result) {
            if (result.success == false) {
                window.ShowAlert('Внимание!', result.message, window.AlertType.Warning, 3000);
            } else {
                window.ShowAlert('Успех!', "Запись успешно отменена", window.AlertType.Success, 2000);
                setTimeout(function () { //строку с таблички сходу не смог удалить, и ngclick не работает там почемуто :(
                    window.location.href = '/Visit/Index';
                }, 1500);
            }
        }).error(function () {
            window.ShowAlert('Ошибка!', window.commonErrorMessage, window.AlertType.Error, 3000);
        });
    }

    ratingVisit = function (id) { //скопировал откудато)
        var url = "/Visit/VisitRating?Id=" + id;
            $("<div style=" +
                    '"' +
                    "text-align: center;" +
                    '"' +
                    "><img src=" +
                    '"' +
                    "../../content/img/spinner.gif" +
                    '"' +
                    " style=" +
                    '"' +
                    "display: block; margin: 0 auto;" +
                    '"' +
                    " /></br>" +
                    "....</div>")
                .addClass("dialog")
                .attr("id",
                    $(this)
                    .attr("data-dialog-id"))
                .appendTo("body")
                .dialog({
                    title: "Оценка приёма",
                    closeText: "x",
                    open: function (event, ui) {
                        $(event.target).parent().css('position', 'fixed');
                        $(event.target).parent().css('top', '150px');
                        $(event.target).parent().css('center');
                        $(this).closest(".ui-dialog")
                            .find(".ui-dialog-titlebar-close")
                            //      .removeClass("ui-dialog-titlebar-close")
                            .html("<span class='ui-button-icon-primary ui-icon ui-icon-closethick'>X</span>");


                    },
                    close: function () { $(this).remove(); },
                    width: 800,
                    height: 400,
                    modal: true,
                    //   open: function(event, ui) { $(".ui-dialog-titlebar-close").text(''); },
                    buttons: {

                    }
                }).load(url);
    }
}

function visitCreate($scope, DTColumnBuilder, $http, $filter) {
    var id = $("#visitId").val();

    $http({
        method: 'GET',
        url: '/Visit/Load?id=' + id,
        data: 'JSON'
    }).success(function (response) {
        $scope.object = response.Object;
        $scope.VisitTypes = response.Types;
    });

    $scope.selectVisitType = function (id) {
        $http({
            method: 'GET',
            url: '/Visit/GetDatesForCreateVisits?type=' + id,
            data: 'JSON'
        }).success(function (response) {
            window.visitType = id;
            window.visitDates = [];
            window.ChangeDate($('#visitDate').val());
            angular.forEach(response, function (value, key) {
               var date = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
               window.visitDates.push(jQuery.datepicker.formatDate('yy-mm-dd', date));
               });
            $scope.Projects = response;
        });
    }

    $scope.save = function () {
        var visit = {
            Comment: $scope.object.VisitComment,
            VisitTypeId: $scope.object.VisitTypeId,
            Date: $('#visitDate').val(),
            TimeBegin: $('#visitTime').val()
        };
        $http({
            url: '/Visit/Create',
            method: 'POST',
            data: JSON.stringify(visit)
        }).success(function (result) {
            console.log(result);
            if (result.success == false) {
                window.ShowAlert('Внимание!', result.message, window.AlertType.Info, 3000);
            } else {
                window.ShowAlert('Успех!', "Запись успешно создана, ожидайте подтверждения", window.AlertType.Success, 2000);
                setTimeout(function () {
                    window.location.href = '/Visit/Index';
                }, 1500);
            }
        }).error(function() {
            window.ShowAlert('Ошибка!', window.commonErrorMessage, window.AlertType.Error, 3000);
        });
    }
}



angular
    .module('app')
    .controller('visitList', ['$scope', 'DTColumnBuilder', '$http', '$uibModal', visitList])
    .controller('visitCreate', ['$scope', 'DTColumnBuilder', '$http', '$filter', visitCreate]);
