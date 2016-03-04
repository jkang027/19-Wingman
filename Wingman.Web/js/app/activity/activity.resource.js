angular.module('app').factory('ActivityResource', function (apiUrl, $resource, $http) {

    function getUserSubmissions() {
        return $http.get(apiUrl + 'submissions/user')
                    .then(function (response) {
                        return response.data;
                    });
    }

    function getUserResponses() {
        return $http.get(apiUrl + 'responses/user')
                    .then(function (response) {
                        return response.data;
                    });
    }

    return {
        getUserSubmissions: getUserSubmissions,
        getUserResponses: getUserResponses
    };
});