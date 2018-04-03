(function(){
    var app = angular.module("timeKeeper");

    app.controller("teamsController", ["$scope", "dataService", "timeConfig", "$location", function($scope, dataService, timeConfig, $location) {

        $scope.currentPage = 0;
        $scope.message = "Wait...";
        dataService.list("teams", function(data, headers){
            $scope.page = angular.fromJson(headers('Pagination'));
            $scope.totalItems = $scope.page.totalItems;
            $scope.message = "";
            $scope.teams = data;
            console.log(data);
        });

        $scope.pageChanged = function() {
            dataService.list("teams?" +"page="+($scope.currentPage-1), function(data, headers){
                $scope.projects = data;
            });
            $log.log('Page changed to: ' + $scope.currentPage);
        };

    }]);
    app.controller("teaController", ["$scope", "$uibModal", "dataService" , function($scope, $uibModal, dataService) {
        var $tea = this;

        $scope.edit = function (data) {
            var modalInstance = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'views/Team/tmModal.html',
                controller: 'tmModalCtrl',
                controllerAs: '$tea',
                resolve: {
                    team: function () {
                        return data;
                    }
                }
            });
        };

        $scope.new = function(data){
            var modalInstance = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'views/Team/newTeam.html',
                controller: 'tmModalCtrl',
                controllerAs: '$tea',
                resolve: {
                    team: function () {
                        return data;
                    }
                }
            })
        };

        $scope.delete = function(team){
            dataService.delete("teams", team.id, function(data){
                window.location.reload();
                window.alert("Data deleted!");
            })
        };
    }]);

    app.controller('tmModalCtrl', ['$uibModalInstance', '$scope', 'dataService', 'team', function ($uibModalInstance, $scope, dataService, team) {
        var $tea = this;
        $scope.team = team;

        dataService.list("employees?all", function(data){
            $scope.employees = data;
        });

        dataService.list("projects?all", function(data){
            $scope.projects = data;
        });

        $scope.save = function (team) {
            dataService.update("teams", team.id, team, function(data){
                window.alert("Data updated!");
            });
        };
        $scope.saveNew = function (team) {
            dataService.insert("teams", team, function(data){
                window.alert("Data updated!");
            });
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss();
        };
    }]);
}());