angular.module('app').factory('ActivityResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/submissions/user',
    {
        'update': {
            method: 'PUT'
        }
    });
});