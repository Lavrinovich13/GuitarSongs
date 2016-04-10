'use strict';
app.controller('baseSongController', ['$scope','$location', '$sce', 'baseSongService', function ($scope, $location, $sce,baseSongService) {

    $scope.searchtext = "";
    $scope.songs = [];
    $scope.currentSong = {};
    $scope.isHideFullInfo = true;

    $scope.$location = $location;
    $scope.getRecentSongs = getRecentSongs;
    $scope.getSongInfo = getSongInfo;
    $scope.searchFor = searchForEnter;
    $scope.trustSrc = function (src) {
        return $sce.trustAsResourceUrl(src);
    };

    function getRecentSongs() {
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

    function searchForEnter(keyEvent, text) {
        if (keyEvent.which === 13) {
            if (text != "") {
                baseSongService.searchFor(text).then(function (result) {
                    $scope.songs = result;
                });
            }
        }
    };
}]);