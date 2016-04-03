'use strict';
app.factory('baseSongService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var baseSongsServiceFactory = {};

    baseSongsServiceFactory.getSongById = _getSongById;
    baseSongsServiceFactory.addBaseSong = _addBaseSong;

    function _getSongById(id) {

        return $http.get(serviceBase + 'api/basesong' + '/' + id).then(function (results) {
            return results.data;
        });
    };

    function _addBaseSong(baseSong) {
        return $http({
            method: "POST",
            url: serviceBase + 'api/basesong',
            data: JSON.stringify(baseSong)
        }).then(function (result) {
            console.log(result);
            return result.error;
        });
    };

    return baseSongsServiceFactory;
}]);