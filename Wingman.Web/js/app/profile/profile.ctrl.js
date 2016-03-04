angular.module('app').controller('ProfileController', function ($scope, $http, ProfileResource) {
    // make a get request
    function getCurrentUser() {
        return $http.get(apiUrl + 'accounts/currentuser')
                    .then(function (response) {
                        return response.data;
                    });
        // bind the response.data to $scope.user
        // bind $scope.user to the view
    }});