angular.module('dataSvcModule', [])
    .service('dataSvc', ['$http', '$q', 'HostAdmin', dataService]);

function dataService($http, $q, HostAdmin) {

    //response with status
    this.get = function (apiHost, url, params) {
        var defer = $q.defer();

        $http({
            method: 'GET',
            url: apiHost + url,
            params: params
        }).then(function successCallback(response) {
            defer.resolve(response);
        }, function errorCallback(response) {
            defer.resolve(response);
        });

        return defer.promise;
    };

    this.post = function (apiHost, url, data) {
        var defer = $q.defer();

        $http({
            method: 'POST',
            url: apiHost + url,
            data: JSON.stringify(data),
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(function successCallback(response) {
            defer.resolve(response);
        }, function errorCallback(response) {
            defer.resolve(response);
        });

        return defer.promise;
    };

    this.put = function (apiHost, url, data) {

        var defer = $q.defer();

        $http({
            method: 'PUT',
            url: apiHost + url,
            data: data
        }).then(function successCallback(response) {
            defer.resolve(response);
        }, function errorCallback(response) {
            defer.resolve(response);
        });

        return defer.promise;
    };

    this.delete = function (host, url, id) {

        var defer = $q.defer();

        $http({
            method: 'DELETE',
            url: host + url,
            params: id
        }).then(function successCallback(response) {
            defer.resolve(response);
        }, function errorCallback(response) {
            defer.resolve(response);
        });

        return defer.promise;
    };

    this.upload = function (host, url, data) {

        var defer = $q.defer();
        var formData = new FormData();
        for (var key in data) {
            if (data.hasOwnProperty(key)) {
                var value = data[key];
                formData.append(key, value);
            }
        }

        $http({
            method: 'POST',
            url: host + url,
            data: formData,
            //IMPORTANT!!! You might think this should be set to 'multipart/form-data'
            // but this is not true because when we are sending up files the request
            // needs to include a 'boundary' parameter which identifies the boundary
            // name between parts in this multi-part request and setting the Content-type
            // manually will not set this boundary parameter. For whatever reason,
            // setting the Content-type to 'undefined' will force the request to automatically
            // populate the headers properly including the boundary parameter.
            headers: {
                'Content-Type': undefined
            },
            /*transformRequest: function (data) {
             console.log("transformRequest " + data)
             }*/
        }).then(function successCallback(response) {
            defer.resolve(response);
        }, function errorCallback(response) {
            defer.resolve(response);
        });

        return defer.promise;
    };

}