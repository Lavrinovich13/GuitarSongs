'use strict';
app.factory('baseSongService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var baseSongsServiceFactory = {};

    baseSongsServiceFactory.addBaseSong = _addBaseSong;
    baseSongsServiceFactory.getRecentSongs = _getRecentSongs;
    baseSongsServiceFactory.getSongById = _getSongById;
    baseSongsServiceFactory.searchFor = _searchFor;

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
        return $http.get(serviceBase + 'basesong/songfullinfo/' + id).then(function (results) {
            return results.data;
        });
    };

    function _searchFor(text) {
        return $http.get(serviceBase + 'basesong/search/' + text).then(function (results) {
            return results.data;
        });
    };

    return baseSongsServiceFactory;
}]);