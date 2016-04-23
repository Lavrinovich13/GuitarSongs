'use strict';
app.factory('userSongService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var userSongsServiceFactory = {};

    userSongsServiceFactory.getUserSongs = _getUserSongs;
    userSongsServiceFactory.getSongById = _getSongById;


    function _getUserSongs() {
        return $http.get(serviceBase + 'usersong/mysongs').then(function (results) {
            return results.data;
        });
    };

    function _getSongById(userSongId) {
        return $http.get(serviceBase + 'usersong/songinfo/' + userSongId).then(function (results) {
            return results.data;
        });
    };

    return userSongsServiceFactory;
}]);