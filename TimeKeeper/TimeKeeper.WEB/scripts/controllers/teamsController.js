(function(){
    var app = angular.module("timeKeeper");

    app.controller("teamsController", ["$scope", "dataService", "timeConfig", "$location", function($scope, dataService, timeConfig, $location) {

        $scope.currentPage = 0;

        function listTeams() {
            dataService.list("teams", function (data, headers) {
                $scope.page = angular.fromJson(headers('Pagination'));
                $scope.totalItems = $scope.page.totalItems;
                $scope.teams = data;
            });
        }
        listTeams();
        $scope.pageChanged = function() {
            dataService.list("teams?" +"page="+($scope.currentPage-1), function(data, headers){
                $scope.projects = data;
            });
            $log.log('Page changed to: ' + $scope.currentPage);
        };

        dataService.list("employees?all", function(data){
            $scope.employees=data;
        });

        dataService.list("roles", function(data){
            $scope.roles=data;
        });

        $scope.$on("teamsUpdated",function(event){
            listTeams();
        });

        $scope.clicked = false;
        $scope.select = function(data) {
            $scope.team = data;
            $scope.clicked = true;
        };

    }]);
    app.controller("teaController", ["$scope", "$uibModal", "dataService", function($scope, $uibModal, dataService) {
        $scope.addMember = function(data){
            var modalInstance = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'views/team/newMember.html',
                controller: 'tmModalCtrl',
                resolve:{
                    team: function(){
                        return data;
                    }
                }
            });
        };

        $scope.edit = function (data) {
            var modalInstance = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'views/team/tmModal.html',
                controller: 'tmModalCtrl',
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
                templateUrl: 'views/team/newTeam.html',
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
            })
        };
    }]);

    app.controller('tmModalCtrl', ['$uibModalInstance', '$scope', 'dataService', 'team',
        function ($uibModalInstance, $scope, dataService, team) {
        $scope.team = team;

        dataService.list("roles",function(data){
           $scope.roles = data;
        });

        dataService.list("employees?all", function(data){
            $scope.employees = data;
        });

        dataService.list("projects?all", function(data){
            $scope.projects = data;
        });

        $scope.save = function (team) {
            dataService.update("teams", team.id, team, function(data){
                $scope.$emit("teamsUpdated");
            });
        };
        $scope.saveNew = function (team) {
            dataService.insert("teams", team, function(data){
                $scope.$emit("teamsUpdated");
            });
        };

        $scope.saveMember = function(team){
            console.log()
            var m = {
                id:0,
                hours:team.engagements.member.hours,
                team:{id:team.id},
                employee:member.employee,
                role:member.role
            };
            //dataService.insert("members",member,function(data){
                console.log($scope.team.engagements);
            //});

            $uibModalInstance.close();
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss();
        };
    }]);
}());