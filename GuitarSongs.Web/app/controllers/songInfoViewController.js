'use strict';
app.controller('songInfoViewController', ['$scope', '$routeParams', '$sce', 'baseSongService',
    function ($scope, $routeParams, $sce, baseSongService) {
        var baseSongId = $routeParams.baseSongId;

        baseSongService.getSongById(baseSongId).then(function (result) {
            $scope.currentSong = result;
        });

        $scope.trustSrc = function (src) {
            return $sce.trustAsResourceUrl(src);
        };
}]);