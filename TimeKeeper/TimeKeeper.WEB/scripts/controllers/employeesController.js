(function () {
    var app = angular.module("timeKeeper");
    app.controller("employeesController", ['$scope', 'dataService', "$uibModal", function ($scope, dataService, $uibModal) {

        $scope.message = "Wait...";
        function listEmployees() {
            dataService.list("employees", function (data, headers) {
                $scope.page = angular.fromJson(headers('Pagination'));
                $scope.message = "";
                $scope.people = data;
            });
        }
        listEmployees();

        $scope.edit = function (person) {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: 'views/employee/empModal.html',
                controller: 'empModalCtrl',
                controllerAs: '$emp',
                resolve: {
                    employee: function () {
                        return person;
                    }
                }
            });
        };

        $scope.add = function(data){
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: 'views/employee/newEmployeeModal.html',
                controller: 'empModalCtrl',
                controllerAs: '$emp',
                resolve:{
                    employee:function(){
                        return data;
                    }
                }
            });
        };

        $scope.view = function(person){
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: 'views/employee/viewModal.html',
                controller: 'empModalCtrl',
                resolve:{
                    employee: function(){
                        return person;
                    }
                }
            })
        };

        $scope.$on("employeesUpdated", function(event){
            listEmployees();
        });

        $scope.delete = function(person){
            swal({
                    title: person.fullName,
                    text: "Are you sure you want to delete this employee?",
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
                        dataService.delete("employees",person.id,function(){
                            $scope.$emit("employeesUpdated");
                        });
                        console.log(person.fullName+": employee deleted");
                        swal.close();
                    }
                });
        }
    }]);
    app.controller("empModalCtrl", ["$scope", "$uibModalInstance", "dataService", "employee", function($scope, $uibModalInstance, dataService, employee) {
        var $emp = this;

        $scope.employee = employee;
        console.log(employee);
        dataService.list("roles", function(data){
            $scope.roles = data;
        });

        $scope.save = function(employee){
            console.log(employee);
            dataService.update("employees", employee.id, employee, function(data){
                window.alert("Data updated!");
            });

        };

        $scope.saveNew = function(employee){
            dataService.insert("employees", employee, function(data){
                window.alert("Data inserted!");
                $scope.$emit("employeesUpdated");
            })
        };

        $scope.cancel = function(){
            $uibModalInstance.dismiss();
        }
    }]);
}());