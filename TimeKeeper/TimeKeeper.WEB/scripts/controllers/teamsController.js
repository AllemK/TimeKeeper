(function () {

    var app = angular.module("timeKeeper");

    app.controller("teamsController", ["$scope", "dataService", function ($scope, dataService) {

        $scope.message = "Wait...";

        dataService.list("teams", function (data) {
            $scope.message = "";
            $scope.teams = data;
        });
    }]);
}());