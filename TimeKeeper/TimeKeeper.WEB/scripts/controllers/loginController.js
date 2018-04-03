(function(){

    var app = angular.module("timeKeeper");

    app.controller("loginController", ["$scope", "$http", "timeConfig",
        function($scope, $http, timeConfig) {

            startApp("loginBtn");
            function startApp(actionButton) {
                gapi.load("auth2", function () {
                    auth2 = gapi.auth2.init({
                        client_id: "1051663319707-npc0vnjraqsee0tf5l1cgpuvakjii2ok.apps.googleusercontent.com"
                    });
                    attachSignin(document.getElementById(actionButton));
                });
            }

            function attachSignin(element) {
                auth2.attachClickHandler(element, {}, function (googleUser) {

                    var id_token = googleUser.getAuthResponse().id_token;
                    $http.defaults.headers.common.Authorization = "Bearer " + id_token;
                    $http({method: "post", url: timeConfig.apiUrl + 'login'})
                        .then(function (response) {
                            console.log(response.data);
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

                $http({
                    method: 'POST',
                    url: timeConfig.idsUrl,
                    headers: urlEncodedUrl,
                    data: userData,
                    transformRequest: function (obj) {
                        var str = [];
                        for (var p in obj)
                            str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                        return str.join("&");
                    }
                }).success(function (data, status, headers, config) {
                    $http.defaults.headers.common.Authorization = 'Bearer ' + data.access_token;
                    $http({
                        method: 'GET',
                        url: timeConfig.apiUrl + 'login'
                    }).success(function(data, status, headers, config){
                        console.log(data);
                    });
                }).error(function (data, status, headers, config) {
                    console.log('ERROR: ' + status);
                });
            };


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