(function(){

    var app = angular.module("timeKeeper");

    app.controller("customersController", ["$scope", "dataService", "$uibModal", function($scope, dataService, $uibModal) {
        var $cust = this;
        $scope.message = "Wait...";

        function listCustomers() {
                dataService.list("customers", function (data) {
                    $scope.message = "";
                    $scope.customers = data;
                });
        }

        listCustomers();

        $scope.$on("customersUpdated", function(event){
            listCustomers();
        });

        $scope.new = function (data){
            var modalInstance = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'views/Customer/newCustModal.html',
                controller: 'ModalCtrl',
                controllerAs: '$cust',
                resolve: {
                    customer: function () {
                        return data;
                    }
                }
            })
        }
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
        };

        $scope.delete = function (data){
            var modalInstance = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl:'views/Customer/confirmCustDeleteModal.html',
                controller:'ModalCtrl',
                controllerAs:'$cust',
                resolve:{
                    customer:function(){
                        return data;
                    }
                }
            });
        };

        $scope.$on("customersUpdated", function(event){
            $scope.$emit("customersUpdated");
        })
    }]);

    app.controller('ModalCtrl', ["$uibModalInstance", "$scope", "dataService", "customer", function ($uibModalInstance, $scope, dataService, customer) {
        var $cust = this;
        $scope.customer = customer;

        $scope.save = function(customer){
            dataService.update("customers", customer.id, customer, function(data){
                window.alert("Data updated!");
            });
            $scope.$emit("customersUpdated");
            $uibModalInstance.close();
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss();
        };

        $scope.saveNew = function(customer){
            dataService.insert("customers",customer,function(data){
                window.alert("Data updated!");
            });
            $scope.$emit("customersUpdated");
            $uibModalInstance.close();
        };

        $scope.delete = function(customer){
            console.log(customer);
            dataService.delete("customers",customer.id,function(data){
                window.location.reload();
                window.alert("Data deleted!");
            });
            $scope.$emit("customersUpdated");
            $uibModalInstance.close();
        };
    }]);
}());