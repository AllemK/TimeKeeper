(function(){
    var app = angular.module("timeKeeper");

    app.controller("monthlyReportController", ["$scope", "$route", "dataService", "$uibModal",
        function( $scope, $route, dataService, $uibModal) {
            $scope.currentPage = 0;
            $scope.totalPages = 0;
            dataService.list("monthlyReport?page=" + ($scope.currentPage), function (data, headers) {
                $scope.page = angular.fromJson(headers("Pagination"));
                $scope.customers = data;
                $scope.totalPages = $scope.page.totalPages;
            });
        }])
}());