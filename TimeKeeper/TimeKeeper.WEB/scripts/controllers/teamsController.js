(function(){
    var app = angular.module("timeKeeper");

    app.controller("teamsController", ["$scope", "dataService", "timeConfig", "$uibModal","$location", function($scope, dataService, timeConfig, $uibModal, $location) {

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

        function listTeams() {
            dataService.list("teamss", function (data) {
                $scope.message = "";
                $scope.teams = data;
            });
        }

        listTeams();

        $scope.$on("teamsUpdated", function(event){
            listTeams();
        });

    }]);
    app.controller("teaController", ["$scope", "$uibModal", "dataService", "toaster" , function($scope, $uibModal, dataService, toaster) {
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

        $scope.view = function(data){
            var modalInstance = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'views/Team/teamProfile.html',
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
                window.alert("Data deleted!");
            })
        };



        $scope.clickwar = function(team){
            swal({
                title: team.name,
                text: "Are you sure you want to delete this team?",
                type: "warning",
                showCancelButton: true,
                customClass: "sweetClass",
                confirmButtonColor: "red",
                confirmButtonText: "Yes, sure",
                cancelButtonColor: "",
                cancelButtonText: "No, not ever!",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function(isConfirm){
                    if(isConfirm){
                        dataService.delete("teams", team.id, function(){
                            $scope.$emit("teamsUpdated");
                        });
                        console.log("Team deleted");
                        swal.close();
                    }
                });
        };
        $scope.$on("teamsUpdated", function(event){
            $scope.$emit("teamsUpdated");
        })


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
        $scope.saveNew = function(team){
            dataService.insert("teams",team,function(data){
                window.alert("Data updated!");
            });
            $scope.$emit("teamsUpdated");
            $uibModalInstance.close();
        };

        /* delete employee in team */

        /*$scope.delEmp = function(person){
            dataService.delEmp("teams", person.id, function(data){
                window.alert("Data deleted!");
            })
        };*/

        /*$scope.delEmp = function(team) {
            $scope.people.teams[team].deleted = true;
        };*/


        $scope.cancel = function () {
            $uibModalInstance.dismiss();
        };
    }]);
}());