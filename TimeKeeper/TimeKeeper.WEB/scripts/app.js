(function(){
    var app = angular.module("timeKeeper", ["ngRoute", "ui.bootstrap", "toaster", "ngAnimate"]);

    currentUser = {
        id: 0,
        name: '',
        role: '',
        teams:[],
        provider:'',
        token:''
    };

    app.constant("timeConfig", {
        apiUrl:"http://localhost:54283/api/",
        idsUrl:"http://localhost:59871/connect/token",
        dayType:['empty','workingday', 'publicholiday', 'otherabsence', 'religiousday', 'sickleave', 'vacation', 'businessabsence', 'weekend', 'future'],
        dayDesc:[' ', 'Working Day', 'Public Holiday', 'Other Absence', 'Religious Day', 'Sick Leave', 'Vacation', 'Business Absence'],
        months:['--select month--', 'January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
        projectStatus:['inProgress','onHold','finished','canceled'],
        projectStatusDesc:['','In Progress', 'On Hold', 'Finished', 'Canceled']
    });
    app.config(['$routeProvider', function($routeProvider) {
        $routeProvider
            .when('/teams',     { templateUrl: 'views/Team/teams.html',
                controller: 'teamsController', loginRequired:true })
            .when('/employees', { templateUrl: 'views/Employee/employees.html',
                controller: 'employeesController', loginRequired:true })
            .when('/customers', { templateUrl: 'views/Customer/customers.html',
                controller: 'customersController', loginRequired:true })
            .when('/projects',  { templateUrl: 'views/Project/projects.html',
                controller: 'projectsController', loginRequired:true })
            .when('/calendar', {templateUrl: 'views/Calendar/calendar.html',
                controller: 'calendarController as $cal', loginRequired:false })
            .when('/login', {templateUrl: 'views/login.html',
                controller: 'loginController', loginRequired:false })
            .otherwise({ redirectTo: '/login' });
    }])
        .run(['$rootScope', '$location', function($rootScope,$location){
        $rootScope.$on("$routeChangeStart", function(event, next, current){
            if(currentUser.id==0&&next.$$route.loginRequired){
                $location.path("/login");
            }
        })
    }]);
}());