angular.module('app').factory('ResponseResource', function (apiUrl, $resource) {
	return $resource(apiUrl + 'responses/:responseId', { responseId: '@ResponseId' },
    {
    	'update': {
    		method: 'PUT'
    	}
    });
});