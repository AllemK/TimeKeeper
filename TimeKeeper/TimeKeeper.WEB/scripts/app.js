(function(){
    var app = angular.module("timeKeeper", ["ngRoute", "ui.bootstrap", "ngAnimate", "toaster"]);

    currentUser = {
        id: 0,
        name: '',
        role: '',
        teams: [],
        provider: '',
        token: ''
    }

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
                controller: 'teamsController' , logReq:false})
            .when('/employees', { templateUrl: 'views/Employee/employees.html',
                controller: 'employeesController' , logReq:false})
            .when('/customers', { templateUrl: 'views/Customer/customers.html',
                controller: 'customersController', logReq:false })
            .when('/projects',  { templateUrl: 'views/Project/projects.html',
                controller: 'projectsController' , logReq:false})
            .when('/calendar', {templateUrl: 'views/Calendar/calendar.html',
                controller: 'calendarController as $cal', logReq:false })
            .when('/login', {templateUrl: 'views/Login/login.html',
                controller: 'loginController' ,logReq:false })
            .when('/logout', {templateUrl: 'views/Login/logout.html',
                controller: 'logoutController' ,logReq:false })
            .otherwise({ redirectTo: '/calendar' });
    }]).run(['$rootScope', '$location', function ($rootScope, $location){
        $rootScope.$on('$routeChangeStart', function(event, next, current){
            if(currentUser.id==0 && next.$$route.logReq){
                $location.path("/login");
            }
        })
    }]);
}());