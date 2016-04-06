'use strict';
app.factory('genreService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var genresServiceFactory = {};

    var _getAllGenres = function (id) {

        return $http.get(serviceBase + '/genres').then(function (results) {
            console.log(results);
            return results.data;
        });
    };

    genresServiceFactory.getAllGenres = _getAllGenres;

    return genresServiceFactory;
}]);