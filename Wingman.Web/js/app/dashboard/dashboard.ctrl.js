angular.module('app').controller('DashboardController', function ($scope, DashboardResource) {
    
    function activate() {
        DashboardResource.getDashboardFeed().then(function (response) {
            $scope.feed = response;
        });
        DashboardResource.getTopWingmen().then(function (response) {
            $scope.topwingmen = response;
        });
    }

    activate();
});