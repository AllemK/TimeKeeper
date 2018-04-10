(function(){
    var app = angular.module("timeKeeper");

    app.controller("calendarController", ["$rootScope","$scope", "$uibModal", "dataService", "timeConfig",
        function($rootScope, $scope, $uibModal, dataService, timeConfig){
            $scope.dayType = timeConfig.dayType;
            $scope.months = timeConfig.months;

            listCalendar($rootScope.currentUser.id,0,0);

            if($rootScope.currentUser.role.search("Lead")>=0 || $rootScope.currentUser.role.search("User")>=0){
                dataService.list("employees?role="+$rootScope.currentUser.role+"&teamId="+$rootScope.currentUser.teams[0],function(data) {
                    $scope.people = data;
                });

                for(i = 1; i<$rootScope.currentUser.teams.length; i++) {
                    dataService.list("employees?role=" + $rootScope.currentUser.role + "&teamId=" + $rootScope.currentUser.teams[i], function (data) {
                        $scope.people += data;
                    });
                }
            }
            else{
                dataService.list("employees?role="+$rootScope.currentUser.role,function(data){
                    $scope.people=data;
                });
            }

            $scope.buildCalendar = function(){
                if($scope.employeeId === undefined)
                    window.alert('You have to choose an employee');
                else
                    listCalendar($scope.employeeId, $scope.year, $scope.month + 1);
            };

            $scope.$on('calendarUpdated', function(event){
                listCalendar($scope.employeeId, $scope.year, $scope.month + 1);
            });

            function listCalendar(empId,year,month){
                //validate employee, year and month
                var url="calendar/"+empId;
                if(year !== 'undefined') url += "/" + year;
                if(month !== 'undefined') url += "/" + month;
                dataService.list(url,function(data){
                    $scope.calendar=data;
                    $scope.employeeId = data.employee.id;
                    $scope.year = data.year;
                    $scope.month = data.month - 1;
                    $scope.num = function(){
                        var size = new Date(data.days[0].date).getDay()-1;
                        if(size<0) size=6;
                        return new Array(size);
                    }
                });
            }

            $scope.edit = function(day){
                if(day.typeOfDay!=='future' || $rootScope.currentUser.role.search("Admin")>-1 ) {
                    var modalInstance = $uibModal.open({
                        animation: true,
                        templateUrl: 'views/calendar/calendarModal.html',
                        controller: 'ModalCalendarCtrl',
                        size: 'lg',
                        resolve: {
                            day: function () {
                                return day;
                            }
                        }
                    });
                }
            }
        }]);

    app.controller('ModalCalendarCtrl', ["$uibModalInstance", "$rootScope", "$scope", "dataService", "timeConfig", "day",
        function ($uibModalInstance, $rootScope, $scope, dataService, timeConfig, day) {

        $scope.day = day;
        $scope.dayType = timeConfig.dayDesc;

        if($rootScope.currentUser.role.search("Lead")>=0 || $rootScope.currentUser.role.search("User")>=0){
            dataService.list("projects?role="+$rootScope.currentUser.role+"&teamId="+$rootScope.currentUser.teams[0],function(data) {
                $scope.projects = data;
            });

            for(i = 1; i<$rootScope.currentUser.teams.length; i++) {
                dataService.list("projects?role=" + $rootScope.currentUser.role + "&teamId=" + $rootScope.currentUser.teams[i], function (data) {
                    $scope.projects += data;
                });
            }
        }
        else{
            dataService.list("projects?role="+$rootScope.currentUser.role,function(data){
                $scope.projects=data;
            });
        }
        if(Number($scope.day.hours)<12) {
            initNewTask();
        }

        $scope.add = function(task){
            if((Number($scope.day.hours)+Number(task.hours))<=12) {
                $scope.day.details.push(task);
                sumHours();
            }
            if(Number($scope.day.hours)<12){
                initNewTask();
            }
        };

        $scope.upd = function(task, index) {
            sumHours();
        };

        $scope.del = function(index) {
            $scope.day.details[index].deleted = true;
            sumHours();
        };

        function sumHours() {
            $scope.day.hours = 0;
            for(var i=0; i<$scope.day.details.length; i++) {
                if(!$scope.day.details[i].deleted) $scope.day.hours += Number($scope.day.details[i].hours);
            }
        }

        function initNewTask() {
            $scope.newTask = {id: 0, description: '', hours: 0, project: {id: 0, name: ''}, deleted: false};
        }

        $scope.ok = function () {
            dataService.insert("calendar",$scope.day,function(data){
                $scope.$emit("calendarUpdated");
            });
            $uibModalInstance.close();
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss();
        };

        $scope.typeChanged = function(){
            if($scope.day.type !== 1) {
                $scope.day.hours = 8;
            }
        };
    }]);
}());