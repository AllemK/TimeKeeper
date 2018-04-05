(function(){
    var app = angular.module("timeKeeper");

    app.controller("projectsController", ["$scope", "dataService", "timeConfig", "$log",
        function($scope, dataService, timeConfig, $log) {
        $scope.currentPage = 0;

        function listProjects() {
            dataService.list("projects", function (data, headers) {
                $scope.page = angular.fromJson(headers('Pagination'));
                $scope.totalItems = $scope.page.totalItems;
                $scope.message = "";
                $scope.projects = data;
            });
        }

        listProjects();

        $scope.$on("projectsUpdated", function(event){
            listProjects();
        });

        $scope.pageChanged = function() {
            dataService.list("projects?" +"page="+($scope.currentPage-1), function(data, headers){
                $scope.projects = data;
            });
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
                templateUrl: 'views/project/projModal.html',
                controller: 'projModalCtrl',
                controllerAs: '$proj',
                resolve: {
                    project: function () {
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
                templateUrl: 'views/project/newProject.html',
                controller: 'projModalCtrl',
                controllerAs: '$proj',
                resolve: {
                    project: function () {
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
                templateUrl: 'views/Project/projectProfile.html',
                controller: 'projModalCtrl',
                controllerAs: '$proj',
                resolve: {
                    project: function () {
                        return data;
                    }
                }
            })
        };

        $scope.clickwar = function(project){
            swal({
                    title: project.name,
                    text: "Are you sure you want to delete this project?",
                    type: "warning",
                    //imageUrl: 'images/hhasic.jpg',
                    //imageSize: '240x100',
                    showCancelButton: true,
                    customClass: "sweetClass",
                    confirmButtonColor: "red",
                    confirmButtonText: "Yes, sure",
                    cancelButtonColor: "darkred",
                    cancelButtonText: "No, not ever!",
                    closeOnConfirm: false,
                    closeOnCancel: true
                },
                function(isConfirm){
                    if(isConfirm){
                        console.log("project deleted");
                        swal.close();
                    }
                });
        };

    }]);

    app.controller('projModalCtrl', ['$uibModalInstance', '$scope', 'dataService', 'project',
        function ($uibModalInstance, $scope, dataService, project) {

        $scope.project = project;

        dataService.list("teams?all", function(data){
            $scope.teams = data;
        });
        dataService.list("customers?all", function(data){
            $scope.customers = data;
        });

        $scope.projectStatus = [
            {value: 0, text: "--select status--"},
            {value: 1, text: "In Progress"},
            {value: 2, text: "On Hold"},
            {value: 3, text: "Finished"},
            {value: 4, text: "Canceled"}
        ];

        $scope.projectPricing=[
            {value: 0, text: "--select pricing--"},
            {value: 1, text: "Hourly Rate"},
            {value: 2, text: "Per Capita Rate"},
            {value: 3, text: "Fixed Price"},
            {value: 4, text: "Not Billable"}
        ];

        $scope.save = function (project) {
            dataService.update("projects", project.id, project, function(data){
                $scope.$emit("projectsUpdated")
            });
            $uibModalInstance.close();
            $scope.$emit("projectsUpdated");
        };
        $scope.saveNew = function (project) {
            dataService.insert("projects", project, function(data){
                $scope.$emit("projectsUpdated");
            });
            $uibModalInstance.close();
        };

        $scope.delete = function(project){
            dataService.delete("projects", project.id, function(data){
                $scope.$emit("projectsUpdated");
            });
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss();
        };
    }]);
}());