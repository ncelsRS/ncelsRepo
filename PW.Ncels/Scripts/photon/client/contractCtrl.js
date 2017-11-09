function contractIndexCtrl($scope, $http, DTOptionsBuilder, DTColumnBuilder) {
  
    $scope.dtColumns = [
      DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
      DTColumnBuilder.newColumn("Date", "Дата договора").withOption('name', 'Date').renderWith(dateformatHtml),
      DTColumnBuilder.newColumn("ProjectNumber", "Номер проекта").withOption('name', 'ProjectNumber').renderWith(actionsContract),
      DTColumnBuilder.newColumn("ContractTypeName", "Тип").withOption('name', 'ContractTypeName'),
      DTColumnBuilder.newColumn("SignEmployeeName", "Подписывающий").withOption('name', 'SignEmployeeName'),
      DTColumnBuilder.newColumn("ExecutorName", "Экпертная организация").withOption('name', 'ExecutorName')
    ];
}

function contractOnAgreeingCtrl($scope, $http, DTOptionsBuilder, DTColumnBuilder) {
 
    $scope.dtColumns = [
      DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
      DTColumnBuilder.newColumn("Date", "Дата договора").withOption('name', 'Date').renderWith(dateformatHtml),
      DTColumnBuilder.newColumn("ProjectNumber", "Номер проекта").withOption('name', 'ProjectNumber').renderWith(actionsContract),

      DTColumnBuilder.newColumn("ContractTypeName", "Тип").withOption('name', 'ContractTypeName'),
      DTColumnBuilder.newColumn("SignEmployeeName", "Подписывающий").withOption('name', 'SignEmployeeName'),
      DTColumnBuilder.newColumn("ExecutorName", "Экпертная организация").withOption('name', 'ExecutorName')
    ];
}

function contractOnSigningCtrl($scope, $http, DTOptionsBuilder, DTColumnBuilder) {
   
    $scope.dtColumns = [
      DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
      DTColumnBuilder.newColumn("Date", "Дата договора").withOption('name', 'Date').renderWith(dateformatHtml),
      DTColumnBuilder.newColumn("ProjectNumber", "Номер проекта").withOption('name', 'ProjectNumber').renderWith(actionsContract),

      DTColumnBuilder.newColumn("ContractTypeName", "Тип").withOption('name', 'ContractTypeName'),
      DTColumnBuilder.newColumn("SignEmployeeName", "Подписывающий").withOption('name', 'SignEmployeeName'),
      DTColumnBuilder.newColumn("ExecutorName", "Экпертная организация").withOption('name', 'ExecutorName')
    ];
}

function contractSignedCtrl($scope, $http, DTOptionsBuilder, DTColumnBuilder) {
 
    $scope.dtColumns = [
      DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number'),
      DTColumnBuilder.newColumn("Date", "Дата договора").withOption('name', 'Date').renderWith(dateformatHtml),
      DTColumnBuilder.newColumn("ProjectNumber", "Номер проекта").withOption('name', 'ProjectNumber').renderWith(actionsContract),

      DTColumnBuilder.newColumn("ContractTypeName", "Тип").withOption('name', 'ContractTypeName'),
      DTColumnBuilder.newColumn("SignEmployeeName", "Подписывающий").withOption('name', 'SignEmployeeName'),
      DTColumnBuilder.newColumn("ExecutorName", "Экпертная организация").withOption('name', 'ExecutorName')
    ];
}

function actionsContract(data, type, full, meta) {
    return '<a   href="/Client/Project/DetailContract?id=' + full.ProjectId + '" >' +
   full.ProjectNumber +
   '</a>';
}




angular
    .module('app')
    .controller('contractIndexCtrl', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder',contractIndexCtrl])
    .controller('contractOnAgreeingCtrl', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder',contractOnAgreeingCtrl])
    .controller('contractOnSigningCtrl', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder',contractOnSigningCtrl])
    .controller('contractSignedCtrl', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder',contractSignedCtrl])