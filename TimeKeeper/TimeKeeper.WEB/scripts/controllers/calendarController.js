(function(){
    var app = angular.module("timeKeeper");

    app.controller("calendarController", ["$scope", "$uibModal", "dataService",
        function($scope, $uibModal, dataService){
            var $cal = this;
            dataService.list("employees?all",function(data){
                $scope.people=data;
            });

            $scope.listCalendar=function(){
                //validate employee, year and month
                var url="calendar/"+$scope.employeeId;
                if($scope.year !== 'undefined') url += "/" + $scope.year;
                if($scope.month !== 'undefined') url += "/" + $scope.month;
                dataService.list(url,function(data){
                    $scope.calendar=data;
                    $scope.num = function(){
                        var size = new Date(data.days[0].date).getDay()-1;
                        if(size<0) size=6;
                        return new Array(size);
                    }
                });
            };

            $scope.edit = function(day){
                if(day.typeOfDay!=='weekend' && day.typeOfDay!=='future') {
                    var modalInstance = $uibModal.open({
                        animation: true,
                        templateUrl: 'views/Calendar/calendarModal.html',
                        controller: 'ModalCalendarCtrl',
                        controllerAs: '$cal',
                        size: 'lg',
                        resolve: {
                            data: function () {
                                return {
                                    day: day,
                                    empId: $scope.employeeId,
                                    empName: $scope.calendar.days[0].employee
                                };
                            }
                        }
                    });
                }
            }
        }]);
    app.controller('ModalCalendarCtrl', function ($uibModalInstance, $scope, dataService, data) {

        var $cal = this;
        $scope.day = data.day;
        $scope.empId = data.empId;
        $scope.empName = data.empName;

        dataService.list("projects/?all", function(data){
            $scope.projects = data;
        });

        $scope.ok = function () {
            var data = {
                id : $scope.day.id,
                date : $scope.day.date,
                type : $scope.day.type,
                hours : $scope.day.hours,
                employee : {id : $scope.empId}
            };
            if(data.id == 0){
                dataService.insert("calendar",data,function(data){
                    //success
                    //$scope.listCalendar();
                    $scope.$emit('calendarUpdated');
                })
            }else{
                dataService.update("calendar",data.id, data, function(data){
                    //success
                    // $scope.listCalendar();
                    $scope.$emit('calendarUpdated');
                })
            }
            $uibModalInstance.close();
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss();
        };

        $scope.typeChanged = function(){
            console.log($scope.day);
        };

        $scope.save = function(task){
            //console.log($scope.task);
            var detail = {
                id : task.id,
                description : task.description,
                hours : task.hours,
                day : {
                    id : task.dayId
                },
                project : {
                    id : task.projectId
                }
            }
            if(detail.id == 0){
                dataService.insert("details", detail, function(detail){});
            }else{
                dataService.update("details", detail.id, detail, function(detail){});
            }
        }
    });
}());