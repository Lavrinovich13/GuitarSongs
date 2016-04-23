'use strict';
app.controller('learnController', ['$scope', '$routeParams', '$sce', '$location', 'userSongService',
    function ($scope, $routeParams, $sce, $location, userSongService) {
        var userSongId = $routeParams.userSongId;

        userSongService.getSongById(userSongId).then(function (result) {
            $scope.currentSong = result;
            $scope.currentVideo = result.video[0];
            $scope.currentText = result.text[0];
        });

        $scope.changeVideo = changeVideo;
        $scope.changeText = changeText;

        $scope.trustSrc = function (src) {
            return $sce.trustAsResourceUrl(src);
        };

        $scope.trustHtml = function (html) {
            return $sce.trustAsHtml(html);
        };

        function changeVideo(video) {
            $scope.currentVideo = video;
        };

        function changeText(text) {
            $scope.currentText = text;
        };
    }]);