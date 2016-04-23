'use strict';
app.controller('songInfoViewController', ['$scope', '$routeParams', '$sce', '$location', 'baseSongService',
    function ($scope, $routeParams, $sce, $location, baseSongService) {
        var baseSongId = $routeParams.baseSongId;

        baseSongService.getSongById(baseSongId).then(function (result) {
            $scope.currentSong = result;
        });

        $scope.trustSrc = function (src) {
            return $sce.trustAsResourceUrl(src);
        };

        $scope.trustHtml = function (html) {
            return $sce.trustAsHtml(html);
        };

        $scope.addToFavorite = function (baseSongId) {
            baseSongService.addToFavorite(baseSongId).then(function (result) {
                console.log(result);
                $location.path("/mySongs");
            }, function (result) { console.log(result);});
        };
}]);