(function(){
    var app = angular.module("timeKeeper");

    app.controller("projectsController", ["$scope", "dataService", "timeConfig", function($scope, dataService, timeConfig) {
        $scope.currentPage = 0;
        $scope.message = "Wait...";
        dataService.list("projects", function(data, headers){
            $scope.page = angular.fromJson(headers('Pagination'));
            console.log($scope);
            $scope.totalItems = $scope.page.totalItems;
            $scope.message = "";
            $scope.projects = data;
            console.log(data);
        });

        $scope.pageChanged = function() {
            dataService.list("projects?" +"page="+($scope.currentPage-1), function(data, headers){
                $scope.projects = data;
            })
            $log.log('Page changed to: ' + $scope.currentPage);
        };
        $scope.projectStatusDesc = timeConfig.projectStatusDesc;

    }]);
    app.controller("projController", ["$scope", "$uibModal", "dataService" , function($scope, $uibModal, dataService) {
        var $proj = this;

        $scope.edit = function (data) {
            var modalInstance = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'views/Project/projModal.html',
                controller: 'projModalCtrl',
                controllerAs: '$proj',
                resolve: {
                    project: function () {
                        return data;
                    }
                }
            });
        }

        $scope.new = function(data){
            var modalInstance = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'views/Project/newProject.html',
                controller: 'projModalCtrl',
                controllerAs: '$proj',
                resolve: {
                    project: function () {
                        return data;
                    }
                }
            })
        }

        $scope.clickwar = function(data) {
            swal({
                    title: data.name,
                    text: "Are you sure you want to delete " + data.name + " project?",
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
                        dataService.delete("projects",data.id,function () {
                        });
                        console.log(data.name + ": project deleted.");
                        swal.close();
                        //$scope.$emit("Projectsupdated");  - live update
                    }
                });
        }
    }]);

    app.controller('projModalCtrl', ['$uibModalInstance', '$scope', 'dataService', 'project', function ($uibModalInstance, $scope, dataService, project) {
        var $proj = this;
        console.log(project);
        $scope.project = project;

        dataService.list("teams?all", function(data){
            $scope.teams = data;
        });
        dataService.list("customers?all", function(data){
            $scope.customers = data;
        });

        $scope.save = function (project) {
            dataService.update("projects", project.id, project, function(data){
                window.alert("Data updated!");
            });
        };
        $scope.saveNew = function (project) {
            dataService.insert("projects", project, function(data){
                window.alert("Data updated!");
            });
        };

        $scope.delete = function(project){
            dataService.delete("projects", project.id, function(data){
                window.alert("Data deleted!");
            })
        }
        $scope.cancel = function () {
            $uibModalInstance.dismiss();
        };
    }]);
}());