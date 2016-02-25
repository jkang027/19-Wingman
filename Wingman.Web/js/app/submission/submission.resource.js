angular.module('app').factory('SubmissionResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/submissions/:submissionId', { submissionId: '@SubmissionId' },
    {
        'update': {
            method: 'PUT'
        }
    });
});