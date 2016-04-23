'use strict';
app.controller('userSongsController', ['$scope', '$routeParams', '$sce', 'userSongService',
    function ($scope, $routeParams, $sce, userSongService) {

        userSongService.getUserSongs().then(function (result) {
            $scope.songs = result;
        });
    }]);