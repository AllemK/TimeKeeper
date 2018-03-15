(function(){

    var app = angular.module("timeKeeper");

    app.controller("customersController", ["$scope", "dataService", function($scope, dataService) {

        $scope.message = "Wait...";

        dataService.list("customers", function(data){
            $scope.message = "";
            $scope.customers = data;
        });
    }]);
}());