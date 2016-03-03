angular.module('app').factory('DashboardResource', function (apiUrl, $http) {
    function getDashboardFeed() {
        return $http.get(apiUrl + 'dashboard/feed')
                    .then(function (response) {
                        return response.data;
                    });
    }

    function getTopWingmen() {
        return $http.get(apiUrl + 'dashboard/topwingmen')
                    .then(function (response) {
                        return response.data;
                    });
    }

    return {
        getDashboardFeed: getDashboardFeed,
        getTopWingmen: getTopWingmen
    };
});