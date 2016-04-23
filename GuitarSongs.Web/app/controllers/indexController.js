'use strict';
app.controller('indexController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {

    $scope.logOut = function () {
        authService.logOut();
        $location.path('/recentSongs');
    }

    $scope.authentication = authService.authentication;
    $scope.search = function (searchText) {
        $location.path('/search/' + searchText);
    };
}]);