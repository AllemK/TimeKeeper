(function(){
    var app = angular.module("timeKeeper");

    app.controller("adminDashController", ["$scope", "$uibModal", "dataService", "timeConfig",
        function($scope, $uibModal, dataService, timeConfig){
            $scope.labels = ["Download Sales", "In-Store Sales", "Mail-Order Sales"];
            $scope.data = [300, 500, 100];
        }]);
}());