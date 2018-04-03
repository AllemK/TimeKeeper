(function(){
    var app = angular.module("timeKeeper");

    app.controller("calendarController", ["$scope", "$uibModal", "dataService", "timeConfig",
        function($scope, $uibModal, dataService, timeConfig){
            $scope.dayType = timeConfig.dayType;
            $scope.months = timeConfig.months;

            dataService.list("employees?all",function(data){
                $scope.people=data;
            });

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
                var url="calendar/"+$scope.employeeId;
                if($scope.year !== 'undefined') url += "/" + $scope.year;
                if($scope.month !== 'undefined') url += "/" + $scope.month;
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
                //if(day.typeOfDay!=='future') {
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
                //}
            }
        }]);

    app.controller('ModalCalendarCtrl', function ($uibModalInstance, $scope, dataService, timeConfig, day) {

        $scope.day = day;
        $scope.dayType = timeConfig.dayDesc;

        dataService.list("projects/?all", function(data){
            $scope.projects = data;
        });
        if(sumHours()<12) {
            initNewTask();
        }

        $scope.add = function(task){
            $scope.day.details.push(task);
            sumHours();
            initNewTask();
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
    });
}());