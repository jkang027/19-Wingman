angular.module('app').factory('DashboardResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/dashboard/feed',
    {
        'update': {
            method: 'PUT'
        }
    });
});