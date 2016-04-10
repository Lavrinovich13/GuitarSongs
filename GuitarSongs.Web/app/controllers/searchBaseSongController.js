'use strict';
app.controller('searchBaseSongController', ['$scope', '$routeParams', 'baseSongService',
    function ($scope, $routeParams, baseSongService) {
        var searchText = $routeParams.searchText;

        if (searchText != "") {

            baseSongService.searchFor(searchText).then(function (result) {
                $scope.songs = result;
            });
        }
    }
]);