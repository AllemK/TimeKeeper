(function(){
    var app = angular.module("timeKeeper");

    app.controller("rolesController", ["$scope", "dataService", "timeConfig", "$log",
        function($scope, dataService, timeConfig, $log) {
            $scope.currentPage = 0;

            function listRoles() {
                dataService.list("roles", function (data, headers) {
                    $scope.page = angular.fromJson(headers('Pagination'));
                    $scope.totalItems = $scope.page.totalItems;
                    $scope.message = "";
                    $scope.projects = data;
                });
            }
            $scope.new = function(data){
                var modalInstance = $uibModal.open({
                    animation: true,
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'views/roles/roles.html',
                    controller: 'roleModalCtrl',
                    controllerAs: '$role',
                    resolve: {
                        project: function () {
                            return data;
                        }
                    }
                })
            }
        }]);
}());
