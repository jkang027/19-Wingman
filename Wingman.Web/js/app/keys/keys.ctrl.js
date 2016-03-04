angular.module('app').controller('KeysController', function ($scope, $http, apiUrl) {
    $scope.tokenReceived = function (token) {
        alert("Got Stripe token: " + token.id);

        $http.post(apiUrl + 'accounts/payforkeys', {
            token: token.id,
            numberOfKeys: $scope.numberOfKeys
        }).then(function(response) {
            alert('Hurray! You now have ' + $scope.numberOfKeys + ' more keys at your disposal.');
        }, function(err) {
            alert('Uh oh! That problem will have to wait while we figure out stuff. Please contact Jun at 555-JUN-WOOP');
        }); 
    }
}); 