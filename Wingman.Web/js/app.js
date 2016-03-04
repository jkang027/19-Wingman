angular.module('app', ['ngResource', 'ui.router', 'LocalStorageModule', 'stripe.checkout']);

angular.module('app').value('apiUrl', 'http://localhost:63608/api/');

angular.module('app').config(function ($stateProvider, $urlRouterProvider, $httpProvider) {
    $httpProvider.interceptors.push('AuthenticationInterceptor');
    $urlRouterProvider.otherwise('home');
    $stateProvider
        .state('home', { url: '/home', templateUrl: '/templates/home/home.html', controller: 'HomeController' })
        .state('register', { url: '/register', templateUrl: '/templates/register/register.html', controller: 'RegisterController' })
        .state('app', { url: '/app', templateUrl: '/templates/app/app.html', controller: 'AppController' })
            .state('app.dashboard', { url: '/dashboard', templateUrl: '/templates/app/dashboard/dashboard.html', controller: 'DashboardController' })
            .state('app.submission', { url: '/submission', templateUrl: '/templates/app/submission/submission.html', controller: 'SubmissionController' })
            .state('app.wingman', { url: '/response', templateUrl: '/templates/app/wingman/wingman.html', controller: 'WingmanController' })
            .state('app.profile', { url: '/profile', templateUrl: '/templates/app/profile/profile.html', controller: 'ProfileController' })
            .state('app.activity', { url: '/activity', templateUrl: '/templates/app/activity/activity.html', controller: 'ActivityController' })
            .state('app.keys', { url: '/keys', templateUrl: '/templates/app/keys/keys.html', controller: 'KeysController' });
});

angular.module('app').run(function (AuthenticationService) {
    AuthenticationService.initialize();
});