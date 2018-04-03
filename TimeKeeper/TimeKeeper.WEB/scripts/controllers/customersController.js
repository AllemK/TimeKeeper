(function(){

    var app = angular.module("timeKeeper");

    app.controller("customersController", ["$scope", "$route", "dataService", "$uibModal",
        function( $scope, $route, dataService, $uibModal) {
            $scope.currentPage = 0;
            $scope.totalPages = 0;
            function listCustomers() {
                dataService.list("customers?page="+($scope.currentPage), function (data, headers) {
                    $scope.page=angular.fromJson(headers("Pagination"));
                    $scope.customers = data;
                    $scope.totalPages = $scope.page.totalPages;
                });
            }

            listCustomers();

            $scope.nextPage = function() {
                $scope.currentPage++;
                listCustomers();
            };

            $scope.prevPage = function() {
                $scope.currentPage--;
                listCustomers();
            };

            $scope.$on("customersUpdated", function(event){
                listCustomers();
            });

            $scope.new = function (data){
                var modalInstance = $uibModal.open({
                    animation: true,
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'views/customer/newCustModal.html',
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

    app.controller("custController", ["$scope", "$uibModal", "dataService",
        function($scope, $uibModal, dataService) {

        $scope.edit = function (data) {
            data.status=data.status.toString();
            var modalInstance = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'views/customer/custModal.html',
                controller: 'ModalCtrl',
                controllerAs: '$cust',
                resolve: {
                    customer: function () {
                        return data;
                    }
                }
            });
        };

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

    app.controller('ModalCtrl', ["$uibModalInstance", "$scope", "dataService", "customer",
        function ($uibModalInstance, $scope, dataService, customer) {
        $scope.customer = customer;

        $scope.save = function(customer){
            customer.status=Number(customer.status);
            dataService.update("customers", customer.id, customer, function(data){
                $scope.$emit("customersUpdated");
            });
            $uibModalInstance.close();
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss();
        };

        $scope.saveNew = function(customer){
            dataService.insert("customers",customer,function(data){
                $scope.$emit("customersUpdated");
            });
            $uibModalInstance.close();
        };
    }]);
}());