var app = angular.module('AngularAuthApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar']);

app.config(function ($routeProvider) {

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "/app/views/signup.html"
    });

    $routeProvider.when("/addSong", {
        controller: "addBaseSongController",
        templateUrl: "/app/views/addSong.html"
    });

    $routeProvider.when("/search/:searchText", {
        controller: "searchBaseSongController",
        templateUrl: "/app/views/baseSongs.html"
    });

    $routeProvider.when("/recentSongs", {
        controller: "recentSongsController",
        templateUrl: "/app/views/baseSongs.html"
    });

    $routeProvider.when("/mySongs", {
        controller: "userSongsController",
        templateUrl: "/app/views/userSongs.html"
    });

    $routeProvider.when("/songInfo/:baseSongId", {
        controller: "songInfoViewController",
        templateUrl: "/app/views/fullInfoSong.html"
    });

    $routeProvider.when("/learn/:userSongId", {
        controller: "learnController",
        templateUrl: "/app/views/learnSong.html"
    });

    $routeProvider.otherwise({ redirectTo: "/recentSongs" });

});

var serviceBase = 'http://localhost/Guitar/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);


