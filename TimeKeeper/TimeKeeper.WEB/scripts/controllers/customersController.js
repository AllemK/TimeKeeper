(function(){

    var app = angular.module("timeKeeper");

    app.controller("customersController", ["$scope", "dataService", "$uibModal", function($scope, dataService, $uibModal) {
        var $cust = this;
        $scope.message = "Wait...";

        function listCustomers() {
            dataService.list("customers", function (data, headers) {
                $scope.page=angular.fromJson(headers("Pagination"));
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
        };
    }]);
    app.controller("custController", ["$scope", "$uibModal", "dataService", function($scope, $uibModal, dataService) {

        var $cust = this;
        $scope.edit = function (data) {
            console.log(data);
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

        $scope.$on("customerModalUpdated", function(event){
            $scope.$emit("customersUpdated");
        });

        $scope.clickwar = function(data) {
            swal({
                    title: data.name,
                    text: "Are you sure you want to delete this customer?",
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
                        dataService.delete("customers",data.id,function(){
                            $scope.$emit("customersUpdated");
                        });
                        console.log(data.name+": customer deleted");
                        swal.close();
                    }
                });
        }
    }]);

    app.controller('ModalCtrl', ["$uibModalInstance", "$scope", "dataService", "customer", function ($uibModalInstance, $scope, dataService, customer) {
        var $cust = this;
        $scope.customer = customer;

        $scope.save = function(customer){
            dataService.update("customers", customer.id, customer, function(data){
                $scope.$emit("customerModalUpdated");
            });
            $uibModalInstance.close();
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss();
        };

        $scope.saveNew = function(customer){
            dataService.insert("customers",customer,function(data){
                $scope.$emit("customerModalUpdated");
            });
            $uibModalInstance.close();
        };

    }]);
}());