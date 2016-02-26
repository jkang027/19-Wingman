angular.module('app').factory('TopicResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/topics/:topicId', { topicId: '@TopicId' },
    {
        'update': {
            method: 'PUT'
        }
    });
});