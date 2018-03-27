(function(){
    var app = angular.module("timeKeeper", ["ngRoute", "ui.bootstrap", "ngAnimate", "toaster"]);
    app.constant("timeConfig", {
        apiUrl:"http://localhost:54283/api/",
        idsUrl:"http://localhost:54283/connect/token",
        dayType:['empty','workingday', 'publicholiday', 'otherabsence', 'religiousday', 'sickleave', 'vacation', 'businessabsence', 'weekend', 'future'],
        dayDesc:[' ', 'Working Day', 'Public Holiday', 'Other Absence', 'Religious Day', 'Sick Leave', 'Vacation', 'Business Absence'],
        months:['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
        projectStatus:['inProgress','onHold','finished','canceled'],
        projectStatusDesc:['','In Progress', 'On Hold', 'Finished', 'Canceled']
    });
    app.config(['$routeProvider', function($routeProvider) {
        $routeProvider
            .when('/teams',     { templateUrl: 'views/Team/teams.html',
                controller: 'teamsController' })
            .when('/employees', { templateUrl: 'views/Employee/employees.html',
                controller: 'employeesController' })
            .when('/customers', { templateUrl: 'views/Customer/customers.html',
                controller: 'customersController' })
            .when('/projects',  { templateUrl: 'views/Project/projects.html',
                controller: 'projectsController' })
            .when('/calendar', {templateUrl: 'views/Calendar/calendar.html',
                controller: 'calendarController as $cal' })
            .otherwise({ redirectTo: '/calendar' });
    }]);
}());