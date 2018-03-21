(function(){

    var app = angular.module("timeKeeper");

    app.controller("customersController", ["$scope", "dataService", function($scope, dataService) {

        $scope.message = "Wait...";

        dataService.list("customers", function(data){
            $scope.message = "";
            $scope.customers =  data;
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

        $scope.delete = function (data){

        }
    }]);

    app.controller('ModalCtrl', ["$uibModalInstance", "$scope", "dataService", "customer", function ($uibModalInstance, $scope, dataService, customer) {
        var $cust = this;
        console.log(customer);
        $scope.customer = customer;

        $scope.save = function(customer){
            dataService.update("customers", customer.id, customer, function(data){
                window.alert("Data updated!");
            });
            $uibModalInstance.close();
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss();
        };
    }]);
}());