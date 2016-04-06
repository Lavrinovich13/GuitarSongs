'use strict';
app.factory('baseSongService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var baseSongsServiceFactory = {};

    baseSongsServiceFactory.addBaseSong = _addBaseSong;
    baseSongsServiceFactory.getRecentSongs = _getRecentSongs;
    baseSongsServiceFactory.getSongById = _getSongById;

    function _addBaseSong(baseSong) {
        return $http({
            method: "POST",
            url: serviceBase + 'basesong/addnewsong',
            data: JSON.stringify(baseSong)
        }).then(function (result) {
            console.log(result);
            return result.error;
        });
    };

    function _getRecentSongs() {
        return $http.get(serviceBase + 'basesong/recentsongs').then(function (results) {
            return results.data;
        });
    };

    function _getSongById(id) {
        return $http.get(serviceBase + 'basesong/getfullinfo/' + id).then(function (results) {
            return results.data;
        });
    }

    return baseSongsServiceFactory;
}]);