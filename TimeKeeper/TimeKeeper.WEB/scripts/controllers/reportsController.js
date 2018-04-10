(function(){
    var app = angular.module("timeKeeper");

    app.controller("projectHistoryController",["$scope","dataService","timeConfig",
        function($scope,dataService,timeConfig){
            dataService.list("projects?all",function(data){
               $scope.projects = data;
            });
            function listProjectHistory(projectId){
                dataService.list("projectHistory?projectId="+projectId,function(data){
                    console.log(data);
                    $scope.projectHistory = data;
                })
            }

            $scope.getReport = function() {
                listProjectHistory($scope.projectId);
            }
        }])
}());