angular.module('app').controller('WingmanController', function ($scope, SubmissionResource, ResponseResource) {

    $scope.newResponse = {};

    function activate() {
        $scope.submissions = SubmissionResource.query();
        $scope.responses = ResponseResource.query();
    }


    $scope.addResponse = function (submission) {
        submission.newResponse.SubmissionId = submission.SubmissionId;
        ResponseResource.save(submission.newResponse, function () {
            alert('Answer saved');
            activate();
        });
    };

    activate();
});