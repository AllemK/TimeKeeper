(function(){
    var app = angular.module("timeKeeper");

    app.controller("projectsController", ["$scope", "dataService", function($scope, dataService) {

        $scope.message = "Wait...";
        dataService.list("projects", function(data){
            $scope.message = "";
            $scope.projects = data;
        });

    }]);
    app.controller("projController", ["$scope", "$uibModal", "dataService" , function($scope, $uibModal, dataService) {
        var $proj = this;

        $scope.edit = function (data) {
            var modalInstance = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'views/Project/projectModal.html',
                controller: 'projModalCtrl',
                controllerAs: '$proj',
                resolve: {
                    project: function () {
                        return data;
                    }
                }
            });
        }
    }]);

    app.controller('projModalCtrl', ['$uibModalInstance', '$scope', 'dataService', 'project', function ($uibModalInstance, $scope, dataService, project) {
        var $proj = this;
        console.log(project);
        $scope.project = project;

        $scope.save = function (project) {
            dataService.update("projects", project.id, project, function(data){
                window.alert("Data updated!");
            });
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss();
        };
    }]);
}());