(function () {
    var app = angular.module("timeKeeper");

    app.controller("employeesController", ['$scope', 'dataService', "$uibModal", "timeConfig", function ($scope, dataService, $uibModal, timeConfig) {

        function listEmployees() {
            dataService.list("employees", function (data, headers) {
                $scope.page = angular.fromJson(headers('Pagination'));
                $scope.totalItems = $scope.page.totalItems;
                $scope.people = data;

            });
        }
        listEmployees();

        $scope.pageChanged = function() {
            dataService.list("employees?" +"page="+($scope.currentPage-1), function(data, headers){
                $scope.people = data;
            });
            //$log.log('Page changed to: ' + $scope.currentPage);
        };

        $scope.edit = function (person) {
            console.log(person);
            person.status=person.status.toString();
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
        $scope.search = function (filter) {
            if (filter !== "") {
                dataService.list("employees?filter=" + filter, function (data, headers) {
                    $scope.people = data;
                })
            }
            else if (filter === "") {
                dataService.list("employees", function (data, headers) {
                    $scope.people = data;
                })
            }
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
        $scope.delete = function (data){
            var modalInstance = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl:'views/Employee/confirmEmpDeleteModal.html',
                controller:'ModalCtrl',
                controllerAs:'$emp',
                resolve:{
                    customer:function(){
                        return data;
                    }
                }
            });
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
        };

        $scope.search = function(filter){
            if(filter===undefined)
                listEmployees();
            dataService.list("employees?filter="+filter,function(data){
                $scope.people=data;
            })
        }
    }]);
<<<<<<< HEAD

    app.controller("empModalCtrl", ["$scope", "$uibModalInstance", "dataService", "employee", function($scope, $uibModalInstance, dataService, employee) {
        var $emp = this;
=======
    app.controller("empModalCtrl", ["$scope", "$uibModalInstance", "dataService", "employee",
        function($scope, $uibModalInstance, dataService, employee) {
>>>>>>> 961a4c83444f803cb0888d478145b9f4ea4c7218

        $scope.employee = employee;

        dataService.list("roles", function(data){
            $scope.roles = data;
        });
        $scope.message = "Wait...";
        dataService.list("employees", function (data, headers) {
            $scope.page = angular.fromJson(headers('Pagination'));
            console.log($scope.page);
            $scope.message = "";
            $scope.people = data;
        });

        $scope.save = function(employee){
            console.log(employee);
            employee.status=Number(employee.status);
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

<<<<<<< HEAD
        $scope.cancel = function(){
            $uibModalInstance.dismiss();
        };

        $scope.delete = function(employees){
            console.log(employees);
            dataService.delete("employees",employees.id,function(data){
                window.location.reload();
                window.alert("Data deleted!");
            });

        };
        $scope.save = function(emp){
            emp.image = ($scope.imagefile)?$scope.imagefile.base64:"";
            dataService.insert("employees", emp, function(data, status){
                if(status === 400){

                    $scope.insertEmployeeErrors = data.modelState['employee errors'];
                    return;
                }
                $scope.insertEmployeeErrors = null;
                $scope.people.unshift(data);
            });
        };

        $scope.$emit("employeesUpdated");
        $uibModalInstance.close();
=======
        $scope.save = function(person){
            console.log(person);
            if(person.id === undefined){
                dataService.insert("employees", person, function(data){
                    window.alert("Data inserted!");
                });
            }
            else{
                dataService.update("employees", person.id, person, function(data){
                    window.alert("Data updated!");
                });
            }
        }
>>>>>>> 961a4c83444f803cb0888d478145b9f4ea4c7218
    }]);
}());