'use strict';
app.controller('baseSongController', ['$scope', 'baseSongService', function ($scope, baseSongService) {

    $scope.baseSong = [];

    baseSongService.getSongById(1).then(function (results) {
        console.log(results);
        $scope.baseSong = results;

    }, function (error) {
        //alert(error.data.message);
    });

    $scope.getRecentSongs = getRecentSongs;
    $scope.getPopularSongs = getPopularSongs;

    function getRecentSongs() {
        alert("fdsad");
    };

    function getPopularSongs() {
        alert("fdsafd");
    };
}]);