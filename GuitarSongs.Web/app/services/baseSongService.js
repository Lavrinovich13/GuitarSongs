'use strict';
app.factory('baseSongService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var baseSongsServiceFactory = {};

    var _getSongById = function (id) {

        return $http.get(serviceBase + 'api/basesong' + '/' + id).then(function (results) {
            return results.data;
        });
    };

    var _addBaseSong = function (baseSong) {

        return $http({
            method: "POST",
            url: serviceBase + 'api/basesong',
            data: JSON.stringify(baseSong)
        }).then(function (result) {
            console.log(result);
            return result.error;
        });
    };

    baseSongsServiceFactory.getSongById = _getSongById;
    baseSongsServiceFactory.addBaseSong = _addBaseSong;

    return baseSongsServiceFactory;
}]);