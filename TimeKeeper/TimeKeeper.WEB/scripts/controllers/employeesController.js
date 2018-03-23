(function () {
    var app = angular.module("timeKeeper");
    app.controller("employeesController", ['$scope', 'dataService', function ($scope, dataService) {

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

        $scope.edit = function (person) {
            $scope.person = person;
        };

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
    }]);
}());