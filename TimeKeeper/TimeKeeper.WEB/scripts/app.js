(function(){
    var app = angular.module("timeKeeper", ["ngRoute", "ui.bootstrap"]);
    app.constant("timeConst", {
        apiUrl:"http://localhost:54283/api/"
    });
    app.config(['$routeProvider', function($routeProvider) {
        $routeProvider
            .when('/teams',     { templateUrl: 'views/teams.html',
                controller: 'teamsController' })
            .when('/employees', { templateUrl: 'views/employees.html',
                controller: 'employeesController' })
            .when('/customers', { templateUrl: 'views/Customer/customers.html',
                controller: 'customersController' })
            .when('/projects',  { templateUrl: 'views/projects.html',
                controller: 'projectsController' })
            .when('/calendar', {templateUrl: 'views/Calendar/calendar.html',
                controller: 'calendarController' })
            .otherwise({ redirectTo: '/teams' });
    }]);
}());