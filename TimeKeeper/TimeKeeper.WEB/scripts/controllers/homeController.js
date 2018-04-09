(function(){
    app.module("timeKeeper");

    app.controller(["$scope", "$route", function(){
        $scope.login = function(){
            $route.path('/login');
        }
    }])
}());