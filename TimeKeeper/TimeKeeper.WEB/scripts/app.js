(function(){
    var app = angular.module("timeKeeper", ["ngRoute", "ui.bootstrap", "toaster", "ngAnimate", "LocalStorageModule", "chart.js"]);

    app.constant("timeConfig", {
        apiUrl:"http://localhost:54283/api/",
        idsUrl:"http://localhost:59871/connect/token",
        dayType:['empty','workingday', 'publicholiday', 'otherabsence', 'religiousday', 'sickleave', 'vacation', 'businessabsence', 'weekend', 'future'],
        dayDesc:[' ', 'Working Day', 'Public Holiday', 'Other Absence', 'Religious Day', 'Sick Leave', 'Vacation', 'Business Absence'],
        months:['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
        projectStatus:['inProgress','onHold','finished','canceled'],
        projectStatusDesc:['--select status--','In Progress', 'On Hold', 'Finished', 'Canceled']
    });

    app.config(['$routeProvider', "localStorageServiceProvider", function($routeProvider, localStorageServiceProvider) {
        $routeProvider
			.when('/adminDash', {templateUrl: 'views/dashboard/adminDash.html',
                controller: 'adminDashController', loginRequired:true })
            .when('/teams',     { templateUrl: 'views/team/teams.html',
                controller: 'teamsController', loginRequired:true })
            .when('/employees', { templateUrl: 'views/employee/employees.html',
                controller: 'employeesController', loginRequired:true })
            .when('/customers', { templateUrl: 'views/customer/customers.html',
                controller: 'customersController', loginRequired:true })
            .when('/projects',  { templateUrl: 'views/project/projects.html',
                controller: 'projectsController', loginRequired:true })
            .when('/roles', {templateUrl: 'views/roles/roles.html',
                controller: 'rolesController', loginRequired:true })
            .when('/calendar', {templateUrl: 'views/calendar/calendar.html',
                controller: 'calendarController as $cal', loginRequired:true })

            .when('/monthlyReport', {templateUrl: 'views/Reports/monthlyReport.html',
                controller: 'monthlyReportController', loginRequired:false })
            .when('/annualReport', {templateUrl: 'views/Reports/annualReport.html',
                controller: 'annualReportController', loginRequired:false })

            .when('/login', {templateUrl: 'views/login.html',
                controller: 'loginController', loginRequired:false })
            // .when('/home',{templateUrl:'views/home.html',
            //     controller: 'homeController', loginRequired:false })
            // .when('/adminDash',{templateUrl:'views/home.html',
            //     controller: 'reportController', loginRequired:true })
            .when('/projectHistory',{templateUrl:'views/reports/projectHistory.html',
                controller: 'projectHistoryController', loginRequired:true })
            .otherwise({ redirectTo: '/login' });
        localStorageServiceProvider.setPrefix("timeKeeper").setStorageType("sessionStorage").setNotify(true,true)
    }])
        .run(['$rootScope', '$location', function($rootScope,$location){
        $rootScope.$on("$routeChangeStart", function(event, next, current){
            if($rootScope.currentUser===undefined){
                $location.path("/login");
            }
            else if( $rootScope.currentUser.id===0 && next.$$route.loginRequired){
                $location.path("/login");
            }
        })
    }]);
}());