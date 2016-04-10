'use strict';
app.controller('recentSongsController', ['$scope', '$location', 'baseSongService',
    function ($scope, $location, baseSongService) {
        baseSongService.getRecentSongs().then(function (result) {
            $scope.songs = result;
        });
}]);