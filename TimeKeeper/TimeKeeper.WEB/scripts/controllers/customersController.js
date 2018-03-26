(function(){

    var app = angular.module("timeKeeper");

    app.controller("customersController", ["$scope", "dataService",  function($scope, dataService) {

        $scope.message = "Wait...";
        dataService.list("customers", function(data){
            $scope.message = "";
            $scope.customers = data;
        });
    }]);

    app.controller("custController", ["$scope", "$uibModal", function($scope, $uibModal) {

        var $cust = this;
        $scope.edit = function (data) {
            var modalInstance = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'views/Customer/custModal.html',
                controller: 'ModalCtrl',
                controllerAs: '$cust',
                resolve: {
                    customer: function () {
                        return data;
                    }
                }
            });
        }
    }]);

    app.controller('ModalCtrl', function ($uibModalInstance, $scope, customer) {
        var $cust = this;
        console.log(customer);
        $scope.customer = customer;

        $scope.ok = function () {
            $uibModalInstance.close();
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss();
        };
    });
}());