'use strict';
app.controller('addBaseSongController', ['$scope', '$sce', 'baseSongService', 'genreService', 'singerService',
    function ($scope, $sce, baseSongService, genreService, singerService) {

        $scope.baseSong = {
            baseSongName: "",
            genre: {},
            singer: {},
            video: [],
            text: [],
            music: []
        };

        $scope.genres = [];
        $scope.singers = [];

        $scope.isAddVideoHide = true;
        $scope.isAddMusicHide = true;
        $scope.isAddTextHide = true;

        $scope.clearEditor = function () { setTextToTextEditor(""); }

        //initialization
        genreService.getAllGenres().then(function (results) {
            console.log(results);
            $scope.genres = results;
        }, function (error) {
            //alert(error.data.message);
        });

        singerService.getAllSingers().then(function (results) {
            $scope.singers = results;
        }, function (error) {
            //alert(error.data.message);
        });
        //

        $scope.trustSrc = function (src) {
            return $sce.trustAsResourceUrl(src);
        };

        $scope.addSong = addSong;
        $scope.addNewVideo = addNewVideo;
        $scope.addNewText = addNewText;
        $scope.addNewMusic = addNewMusic;
        $scope.editDocument = editDocument;
        $scope.getTextFromTextEditor = getTextFromTextEditor;
        
        function addSong(newSong) {
            return baseSongService.addBaseSong(newSong);
        };

        function addNewVideo(videos, video) {
            videos.push(video);
        };

        function addNewText(texts, text) {
            text.textContent = getTextFromTextEditor();
            texts.push(text);
        };

        function addNewMusic(musics, music) {
            musics.push(music);
        };

        function editDocument(texts, index) {
            //var document = texts.splice(index, 1)[0];
            //setTextToTextEditor(document.textContent);

            //$scope.text = { textName: document.name };
        };

        function getTextFromTextEditor()
        {
            return tinymce.get('texteditor').getContent();
        }

        function setTextToTextEditor(text) {
            $('#texteditor').html(text);
            initTinymce();
        };

        function initTinymce() {
            tinymce.init({
                selector: '#texteditor',
                toolbar: "styleselect | bold italic | accords",
                menubar: false,
                setup: function (editor) {
                    editor.addButton('accords', {
                        type: 'listbox',
                        text: 'Accords',
                        icon: false,
                        onselect: function (e) {
                            editor.insertContent(this.value());
                        },
                        values: [
                          { text: 'Am', value: '&nbsp;<strong>Am</strong>' },
                          { text: 'Dm', value: '&nbsp;<strong>Dm</strong>' },
                          { text: 'C', value: '&nbsp;<strong>C</strong>' }
                        ]
                    });
                }
            });
        };
}]);