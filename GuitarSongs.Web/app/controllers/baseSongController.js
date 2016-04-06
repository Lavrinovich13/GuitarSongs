'use strict';
app.controller('baseSongController', ['$scope','$location', '$sce', 'baseSongService', function ($scope, $location, $sce,baseSongService) {

    $scope.songs = [];
    $scope.currentSong = {};
    $scope.isHideFullInfo = true;

    $location.path("/baseSong");
    getRecentSongs();

    $scope.$location = $location;
    $scope.getRecentSongs = getRecentSongs;
    $scope.getSongInfo = getSongInfo;
    $scope.trustSrc = function (src) {
        return $sce.trustAsResourceUrl(src);
    };

    function getRecentSongs() {
        $location.path("/baseSong");
        baseSongService.getRecentSongs().then(function (result) {
            $scope.songs = result;
        });
    };

    function getSongInfo(id) {
        $scope.isHideFullInfo = false;
        baseSongService.getSongById(id).then(function (result) {
            $scope.currentSong = result;
        });
    };
}]);