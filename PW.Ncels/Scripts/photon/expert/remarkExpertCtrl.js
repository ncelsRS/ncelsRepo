 function remarkProjectCtrl($scope, $http, DTOptionsBuilder, DTColumnBuilder) {
     $scope.dtColumns = [
     DTColumnBuilder.newColumn("ExecutionDate", "Срок исполнения").withOption('name', 'ExecutionDate').renderWith(dateformatHtml),
     DTColumnBuilder.newColumn("Text", "Текст").withOption('name', 'Text'),
     DTColumnBuilder.newColumn("AuthorName", "Автор").withOption('name', 'AuthorName'),
     DTColumnBuilder.newColumn("ExecutorName", "Исполнитель").withOption('name', 'ExecutorName')
     ];

     $scope.selectFun = function (data) {
         $http({
             method: 'GET',
             url: '/Expertise/Remark/Details?id=' + data.Id,
             data: 'JSON'
         }).success(function (result) {
             $scope.htmlContent = result;
         });

         $http({
             method: 'GET',
             url: '/Expertise/Remark/ReadRemark?id=' + data.Id,
             data: 'JSON'
         }).success(function (result) {
             $scope.object = result;
         });
     }
 }

 function actionsCalculationLink(data, type, full, meta) {
     return '<a   href="/Expertise/Project/DetailCard?id=' + full.ProjectId + '" >' +
    full.ProjectNumber +
    '</a>';
 }

 function actionsCalculation(data, type, full, meta) {
     return '<a  class="btn btn-outline btn-warning btn-xs" href="/Expertise/Calculation/Edit?id=' + data + '">' +
    '   <i class="fa fa-edit"></i>' +
    '</a>&nbsp;' +
    '<a  class="btn btn-outline btn-success btn-xs" href="/Expertise/Calculation/Details?id=' + data + '">' +
    '   <i class="fa fa-search"></i>' +
    '</a>';
 }

angular
    .module('app')
    .controller('remarkProjectCtrl', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder',remarkProjectCtrl])