(function(){

    var app = angular.module("timeKeeper");

    app.controller("loginController", ["$scope", "$rootScope", "$http", "$location", "timeConfig", "localStorageService",
        function($scope, $rootScope, $http, $location, timeConfig, localStorageService) {
            $rootScope.currentUser = localStorageService.get("currentUser");
            $rootScope.token = localStorageService.get("access_token");
            if($rootScope.token!==null){
                $http.defaults.headers.common.Authorization = "Bearer " + $rootScope.token;
                $http.defaults.headers.common.Provider = "iserver";
            }
            if(localStorageService.get("currentUser")===null){
                $rootScope.currentUser = {
                    id: 0,
                    name: '',
                    role: '',
                    teams:[],
                    provider:'',
                    token:''
                }
            }
            function setLoader(flag){
                $rootScope.waitForLoad = flag;
            }

            startApp("loginBtn");
            function startApp(actionButton) {
                gapi.load("auth2", function () {
                    auth2 = gapi.auth2.init({
                        client_id: "53671047349-a7u8e0u88vdhqp10tb0833durqo9jdnt.apps.googleusercontent.com"
                    });
                    attachSignin(document.getElementById(actionButton));
                });
            }

            function attachSignin(element) {
                auth2.attachClickHandler(element, {}, function (googleUser) {

                    var authToken = googleUser.getAuthResponse().id_token;
                    $http.defaults.headers.common.Authorization = "Bearer " + authToken;
                    $http.defaults.headers.common.Provider = "google";
                    $http({method: "post", url: timeConfig.apiUrl + 'login'})
                        .then(function (response) {
                            $rootScope.currentUser = response.data;
                        }, function (error) {
                            window.alert(error.message);
                        });
                })
            }

            $scope.login = function(){
                var userData = {
                    grant_type: 'password',
                    username: $scope.user.name,
                    password: $scope.user.pass,
                    scope: 'openid'
                };
                var urlEncodedUrl = {
                    'Content-Type': 'application/x-www-form-urlencoded',
                    'Authorization': 'Basic dGltZWtlZXBlcjokY2gwMGw='
                };
                setLoader(true);
                $http({
                    method: 'POST',
                    url: timeConfig.idsUrl,
                    headers: urlEncodedUrl,
                    data: userData,
                    transformRequest: function (obj) {
                        var str = [];
                        for (var p in obj) {
                            str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                        }
                        return str.join("&");
                    }
                }).then(function (data, status, headers, config) {
                    var authToken = data.data.access_token;
                    localStorageService.set("access_token",authToken);
                    $http.defaults.headers.common.Authorization = 'Bearer ' + authToken;
                    $http.defaults.headers.common.Provider = "iserver";
                    $http({
                        method: 'GET',
                        url: timeConfig.apiUrl + 'login'
                    }).then(function(data, status, headers, config){
                        $rootScope.currentUser = data.data;
                        console.log(status);
                        localStorageService.set("currentUser", data.data);
                        setLoader(false);
                        $location.path("/adminDash");
                    });
                })/*.otherwise(function (data, status, headers, config) {
                    console.log('ERROR: ' + status);
                });*/
            };
        }]);

    app.controller("logoutController", ["$rootScope", "$scope", "$location", "localStorageService",
        function($rootScope, $scope, $location, localStorageService) {
        $scope.logout = function() {
            $rootScope.currentUser = {id: 0};
            $rootScope.token = null;
            localStorageService.clearAll();
            $location.path("/login");
        }
    }]);
}());

/*
var profile = googleUser.getBasicProfile();
console.log('ID: ' + profile.getId()); // Do not send to your backend! Use an ID token instead.
console.log('Name: ' + profile.getName());
console.log('Image URL: ' + profile.getImageUrl());
console.log('Email: ' + profile.getEmail()); // This is null if the 'email' scope is not present.

console.log(id_token);
*/