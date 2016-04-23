'use strict';
app.controller('addBaseSongController', ['$scope', '$sce', 'baseSongService', 'genreService', 'singerService',
    function ($scope, $sce, baseSongService, genreService, singerService) {

        var uniqId = 1;
        initTinymce();

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
        $scope.editText = editText;
        $scope.getTextFromTextEditor = getTextFromTextEditor;
        $scope.showDialog = showDialog;
        $scope.closeDialog = closeDialog;
        
        function showDialog(selector) {
            $(selector).show();
        };

        function closeDialog(selector) {
            $(selector).hide();
        };

        function addSong(newSong) {
            baseSongService.addBaseSong(newSong).then(function () {
                $location.path("/mySongs");
            });
        };

        function addNewVideo(videos, video) {
            videos.push(video);
        };

        function addNewText(texts, text) {
            text.textContent = getTextFromTextEditor();
            if (text._id == undefined) {
                text._id = uniqId++;
                text.isExists == true;
                texts.push(text);
            }
            else {
                for (var i = 0; i < texts.length; i++) {
                    if (texts[i]._id == text._id) {
                        texts[i] = text;
                    }
                }
            }
        };

        function editText(texts, index) {
            var text = texts[index];
            $scope.text = {
                _id: text._id,
                textName: text.textName,
                textContent: text.textContent
            };
            setTextToTextEditor(text.textContent);
        }

        function addNewMusic(musics, music) {
            musics.push(music);
        };

        function getTextFromTextEditor()
        {
            return tinymce.get('texteditor').getContent();
        }

        function setTextToTextEditor(text) {
            tinymce.activeEditor.setContent(text);
        };

        function initTinymce() {
            tinymce.init({
                selector: '#texteditor'
                //toolbar: "styleselect | bold italic | accords",
                //menubar: false,
                //setup: function (editor) {
                //    editor.addButton('accords', {
                //        type: 'listbox',
                //        text: 'Accords',
                //        icon: false,
                //        onselect: function (e) {
                //            editor.insertContent(this.value());
                //        },
                //        values: [
                //          { text: 'Am', value: '&nbsp;<strong>Am</strong>' },
                //          { text: 'Dm', value: '&nbsp;<strong>Dm</strong>' },
                //          { text: 'C', value: '&nbsp;<strong>C</strong>' }
                //        ]
                //    });
                //}
            });
        };
}]);