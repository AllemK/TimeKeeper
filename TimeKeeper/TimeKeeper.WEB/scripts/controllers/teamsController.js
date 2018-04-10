(function(){
    var app = angular.module("timeKeeper");

    app.controller("teamsController", ["$scope", "dataService", "$uibModal",
        function($scope, dataService, $uibModal) {

            $scope.currentPage = 0;
            $scope.currentPageMember=0;
            $scope.totalPages = 0;
            $scope.totalPagesMember=0;

            function listTeams() {
                dataService.list("teams?page="+$scope.currentPage, function (data, headers) {
                    $scope.page = angular.fromJson(headers('Pagination'));
                    $scope.teams = data;
                    $scope.totalPages = $scope.page.totalPages;
                });
            }
            listTeams();

            $scope.nextPage = function(){
                $scope.currentPage++;
                listTeams();
            };

            $scope.previousPage = function(){
                $scope.currentPage--;
                listTeams();
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
                $scope.totalPagesMember = Math.ceil($scope.team.engagements.length/3);
                $scope.currentPageMember = 0;
            };

            $scope.nextPageMember = function(){
                $scope.currentPageMember++;
            };

            $scope.previousPageMember = function(){
                $scope.currentPageMember--;
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
                    resolve: {
                        team: function () {
                            return data;
                        }
                    }
                })
            };

            $scope.delete = function(team){
                swal({
                        title: team.name,
                        text: "Are you sure you want to delete this team?",
                        type: "warning",
                        showCancelButton: true,
                        customClass: "sweetClass",
                        confirmButtonText: "Yes, sure",
                        cancelButtonText: "No, not ever!",
                        closeOnConfirm: false,
                        closeOnCancel: true
                    },

                    function (isConfirm) {
                        if (isConfirm) {
                            dataService.delete("teams",team.id,function(){
                                listTeams();
                            });
                            swal.close();
                        }
                    });
            };

            $scope.addMember = function(data){
                var modalInstance = $uibModal.open({
                    animation: true,
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'views/team/member/newMember.html',
                    controller: 'tmModalCtrl',
                    resolve:{
                        team: function(){
                            return data;
                        }
                    }
                });
            };

            $scope.editMember = function(data){
                var modalInstance = $uibModal.open({
                    animation: true,
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'views/team/member/editMember.html',
                    controller: 'tmModalCtrl',
                    resolve:{
                        team: function(){
                            return data;
                        }
                    }
                });
            };

            $scope.deleteMember = function(member){
                swal({
                        title: member.employee.name,
                        text: "Are you sure you want to delete this member?",
                        type: "warning",
                        showCancelButton: true,
                        customClass: "sweetClass",
                        confirmButtonText: "Yes, sure",
                        cancelButtonText: "No, not ever!",
                        closeOnConfirm: false,
                        closeOnCancel: true
                    },

                    function (isConfirm) {
                        if (isConfirm) {
                            dataService.delete("engagements",member.id,function(){
                                listTeams();
                            });
                            console.log(member.employee.name+": member deleted");
                            swal.close();
                        }
                    });
            }
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

            $scope.saveMember = function(member){
                if(member.id<1){
                    var m = {
                        id:0,
                        hours:member.hours,
                        team:{id:team.id},
                        employee:{id:member.employee.id,name:member.employee.name},
                        role:{id:member.role.id,name:member.role.name}
                    };
                    dataService.insert("members",m,function(data){
                        m.id=data.memberId;
                        team.engagements.push(m);
                    });
                }
                else{
                    dataService.update("members",member.id,member,function(data){
                    });
                }
                $uibModalInstance.close();
            };

            $scope.cancel = function () {
                $uibModalInstance.dismiss();
            };
        }]);
}());