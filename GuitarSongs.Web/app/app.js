var app = angular.module('AngularAuthApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar']);

app.config(function ($routeProvider) {

    //$routeProvider.when("/home", {
    //    controller: "homeController",
    //    templateUrl: "app/views/home.html"
    //});

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

    $routeProvider.when("/baseSong", {
        controller: "baseSongController",
        templateUrl: "/app/views/baseSongs.html"
    });

    $routeProvider.otherwise({ redirectTo: "/login" });

});

var serviceBase = 'http://localhost/Guitar/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

//app.config(function ($httpProvider) {
//    $httpProvider.interceptors.push('authInterceptorService');
//});

//app.run(['authService', function (authService) {
//    //authService.fillAuthData();
//}]);


