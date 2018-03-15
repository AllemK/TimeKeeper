(function(){

    var app = angular.module("timeKeeper");

    app.controller("projectsController", ["$scope", "dataService", function($scope, dataService) {

        $scope.message = "Wait...";

        dataService.list("projects", function(data){
            $scope.message = "";
            $scope.projects = data;
        });

        $scope.edit = function(person) {
            $scope.employee = person;
        };
    }]);
}());