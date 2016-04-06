'use strict';
app.factory('singerService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var singersServiceFactory = {};

    var _getAllSingers = function (id) {

        return $http.get(serviceBase + '/singers').then(function (results) {
            return results.data;
        });
    };

    singersServiceFactory.getAllSingers = _getAllSingers;

    return singersServiceFactory;
}]);