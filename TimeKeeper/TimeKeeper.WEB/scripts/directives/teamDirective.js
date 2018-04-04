(function(){

    var app = angular.module("timeKeeper");

    app.directive("team", [function() {
        return {
            restrict: 'E',
            scope: {
                data: '='
            },
            controller: 'teaController as $tea',
            templateUrl: 'views/team/team-widget.html'
        }
    }]);

}());