'use strict';
app.controller('addBaseSongController', ['$scope', '$sce', 'baseSongService', 'genreService', 'singerService',
    function ($scope, $sce, baseSongService, genreService, singerService) {

    $scope.baseSong = {
        name: "",
        genreName: "",
        singerName: "",
        videos: [],
        texts: [],
        musics: []
    };

    $scope.trustSrc = function (src) {
        return $sce.trustAsResourceUrl(src);
    }

    $scope.addSong = function (newSong) {
        alert(newSong.name);
        //TODO errors
        $scope.errors = baseSongService.addBaseSong(newSong);
    }

    $scope.genres = [];
    
    genreService.getAllGenres().then(function (results) {
        console.log(results);
        $scope.genres = results;
    }, function (error) {
        //alert(error.data.message);
    });

    $scope.singers = [];

    singerService.getAllSingers().then(function (results) {
        $scope.singers = results;
    }, function (error) {
        //alert(error.data.message);
    });

    $scope.isAddVideoHide = true;
    $scope.isAddMusicHide = true;
    $scope.isAddTextHide = true;

    $scope.addNewVideo = function (videoUrl) {
        $scope.baseSong.videos.push({ url: videoUrl });
        videoUrl = "";
        $scope.isAddVideoHide = true;
    }

    $scope.addNewText = function (textName, text) {
        console.log($(".jqte_editor").text());
        $scope.baseSong.texts.push({ name: textName, content: $(".jqte_editor").text() });
        $scope.isAddTextHide = true;
    }

    $scope.addNewMusic = function (musicName, url) {
        $scope.baseSong.musics.push({ name: musicName, url: url });
        $scope.isAddMusicHide = true;
    }

    $scope.editDocument = function(index)
    {
        var removedDocument = $scope.baseSong.texts.splice(index, 1)[0];

        $scope.isAddTextHide = false;
        $scope.text = { name: removedDocument.name, content: removedDocument.content}

    }
}]);