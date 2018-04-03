(function () {
    var app = angular.module("timeKeeper");

    app.factory("dataService", ['$rootScope', '$http', 'timeConfig', 'infoService', function ($rootScope, $http, timeConfig, infoService) {
        var source = timeConfig.apiUrl;
        function setLoader(flag){
            $rootScope.waitForLoad = flag;
        }

        return {
            list: function (dataSet, callback) {
                setLoader(true);
                $http.get(source + dataSet).then(
                    function (response) {
                        setLoader(false);
                        return callback(response.data, response.headers);
                    },
                    function (reason) {
                        setLoader(false);
                        window.alert(reason.data.message);
                    });
            },

            read: function (dataSet, id, callback) {
                setLoader(true);
                $http.get(source + dataSet + "/" + id)
                    .then(function success(response) {
                        setLoader(false);
                        return callback(response.data);
                    }, function error(error) {
                        setLoader(false);
                        window.alert(error.data.message);
                    });
            },

            insert: function (dataSet, data, callback) {
                setLoader(true);
                $http({ method: "post", url: source + dataSet, data: data })
                    .then(function success(response) {
                        setLoader(false);
                        infoService.success(dataSet.charAt(0).toUpperCase()+dataSet.slice(1,dataSet.length-1), "data successfully inserted" );
                        return callback(response.data);
                    }, function error(error) {
                        setLoader(false);
                        infoService.error(dataSet, error.data.message);
                    });
            },

            update: function (dataSet, id, data, callback) {
                setLoader(true);
                $http({ method: "put", url: source + dataSet + "/" + id, data: data })
                    .then(function success(response) {
                        setLoader(false);
                        infoService.success(dataSet.charAt(0).toUpperCase()+dataSet.slice(1,dataSet.length-1), "data successfully updated" );
                        return callback(response.data);
                    }, function error(error) {
                        setLoader(false);
                        infoService.error(dataSet, error.data.message);
                    });
            },

            delete: function (dataSet, id, callback) {
                setLoader(true);
                $http({ method: "delete", url: source + dataSet + "/" + id })
                    .then(function success(response) {
                        setLoader(false);
                        infoService.success(dataSet.charAt(0).toUpperCase()+dataSet.slice(1,dataSet.length-1), "data successfully deleted" );
                        return callback(response.data);
                    }, function error(error) {
                        setLoader(false);
                        infoService.error(dataSet, message );
                    });
            }
        };
    }]);
}());