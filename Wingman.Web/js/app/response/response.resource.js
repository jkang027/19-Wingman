angular.module('app').factory('ResponseResource', function (apiUrl, $resource) {
	return $resource(apiUrl + 'responses/:responseId', { responseId: '@ResponseId' },
    {
    	'update': {
    		method: 'PUT'
    	},
    	'responses': {
    	    url: apiUrl + 'submissions/:SubmissionId/responses',
    	    method: 'GET',
    	    isArray: true
    	}
    });
});