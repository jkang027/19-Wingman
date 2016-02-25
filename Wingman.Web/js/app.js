angular.module('app', ['ngResource', 'ui.router', 'LocalStorageModule']);

angular.module('app').value('apiUrl', 'http://localhost:63608/api/');

angular.module('app').config(function ($stateProvider, $urlRouterProvider, $httpProvider) {
    $httpProvider.interceptors.push('AuthenticationInterceptor');
    $urlRouterProvider.otherwise('home');
    $stateProvider
        .state('home', { url: '/home', templateUrl: '/templates/home/home.html', controller: 'HomeController' })
        .state('app', { url: '/app', templateUrl: '/templates/app/app.html', controller: 'AppController' })
            .state('app.dashboard', { url: '/dashboard', templateUrl: '/templates/app/dashboard/dashboard.html', controller: 'DashboardController' })
            .state('app.submission', { url: '/submission', templateUrl: '/templates/app/submission/submission.html', controller: 'SubmissionController' })
            .state('app.response', { url: '/response', templateUrl: '/templates/app/response/response.html', controller: 'ResponseController' })
            .state('app.profile', { url: '/profile', templateUrl: '/templates/app/profile/profile.html', controller: 'ProfileController' })
    ;
});

angular.module('app').run(function (AuthenticationService) {
    AuthenticationService.initialize();
});